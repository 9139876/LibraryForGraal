using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using Graal.Library.Common.Quotes;
using Graal.Library.Common.Enums;
using Graal.Library.Common.Storage;
using Graal.Library.Storage.Common.Properties;
using Graal.Library.Common;

namespace Graal.Library.Storage.Common
{
    public class CommonPostgresSqlDriver : IStorageSqlDriver
    {
        protected readonly Dictionary<string, string> Names;

        protected IDbConnection connection;

        public IDbConnection Connection
        {
            get => connection;
            set
            {
                connection = value;

                if (connection != null && connection.State != ConnectionState.Open)
                    connection.Open();

                ConnectionStatusChange?.Invoke();
            }
        }

        public event Action ConnectionStatusChange;

        public CommonPostgresSqlDriver()
        {
            var consts = new SQLNames();

            Names = new Dictionary<string, string>();

            typeof(SQLNames)
                .GetFields()
                .ToList()
                .ForEach(f => Names.Add(f.Name, f.GetValue(consts).ToString()));
        }

        ~CommonPostgresSqlDriver()
        {
            if (Connection != null && Connection.State != ConnectionState.Closed)
                Connection.Close();
        }

        #region AUX

        /// <summary>
        /// Возвращает имена всех существующих схем в БД, кроме служебных
        /// </summary>
        /// <param name="connection">Соединение с БД</param>
        /// <param name="error">Ошибка выполнения</param>
        /// <returns></returns>
        public List<string> GetAllSchemasNames()
        {
            List<string> schemasNames = new List<string>();

            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = "select schema_name from information_schema.schemata WHERE schema_name!~'^pg_' AND schema_name<> 'information_schema'";

                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        schemasNames.Add(reader[0].ToString());
            }

            return schemasNames;
        }

        protected void ToStorage(string cmdText)
        {
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = cmdText;
                cmd.ExecuteNonQuery();
            }
        }

        public bool SchemaExist()
        {
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = $"select count(distinct(table_schema)) from information_schema.tables where table_schema='{SchemaName}'";

                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public bool SchemaExistAndCorrect()
        {
            if (!SchemaExist())
                return false;
            //Проверка наличия таблиц
            var existingTables = new List<string>();

            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = $"select table_name from information_schema.tables where table_schema='{SchemaName}'";

                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        existingTables.Add(reader[0].ToString());
            }

            foreach (var needTable in Enum.GetNames(typeof(TablesNames)))
            {
                if (!existingTables.Contains(needTable))
                {
                    ApplicationGlobal.Logger.Debug($"Таблица {needTable} не найдена в БД");
                    return false;
                }
            }

            //Проверка наличия хранимых функций
            var existingroutines = new List<string>();

            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = $"select routine_name from information_schema.routines where specific_schema='{SchemaName}'";

                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        existingroutines.Add(reader[0].ToString());
            }
            
            foreach (var needRoutine in Enum.GetNames(typeof(RoutinesNames)))
            {
                if (!existingroutines.Contains(needRoutine))
                {
                    ApplicationGlobal.Logger.Debug($"Хранимая процедура {needRoutine} не найдена в БД");
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Initial

        public virtual void CreateGraalSchema()
        {
            if (string.IsNullOrEmpty(SchemaName))
                throw new InvalidOperationException("Невозможно создать схему - имя схемы не может быть пустым!");

            if (!new Regex(@"^[a-z][a-z0-9_]*$").IsMatch(SchemaName))
                throw new InvalidOperationException($"Невозможно создать схему - имя схемы {SchemaName} содержит недопустимые символы!");

            using (var cmd = Connection.CreateCommand())
            {
                ApplicationGlobal.Logger.Trace("Создание схемы Graal");

                foreach (var cmdText in Resources.sql_create_commands
                                                    .Replace("%schema_name%", SchemaName)
                                                    .Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                                                    .Select(s => Regex.Replace(s, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline)))
                {
                    try
                    {
                        ApplicationGlobal.Logger.Trace(cmdText);
                        cmd.CommandText = cmdText;
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        var error = $"Ошибка создания хранилища - '{ex.Message}'{Environment.NewLine}Command text{Environment.NewLine}'{cmdText}'";
                        ApplicationGlobal.Logger.Error(error);
                        throw new InvalidOperationException(error);
                    }
                }

                ApplicationGlobal.Logger.Trace("Создание схемы Graal завершено");
            }
        }

        #endregion

        #region Connection

        /// <summary>
        /// Состояние соединения с БД
        /// </summary>
        public bool ConnectionStatus => Connection?.State == ConnectionState.Open;

        /// <summary>
        /// Имя базы данных, к которой произведено подключение
        /// </summary>
        public string DBName => Connection?.Database ?? string.Empty;

        #endregion

        public static string SchemaName => Environment.GetEnvironmentVariable(ApplicationGlobal.EnvironmentVariableGraalSchemaName, EnvironmentVariableTarget.User);
    }
}
