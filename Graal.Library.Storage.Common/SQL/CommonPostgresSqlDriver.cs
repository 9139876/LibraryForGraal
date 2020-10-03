using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using Graal.Library.Common.Quotes;
using Graal.Library.Common.Enums;
using Graal.Library.Common.Storage;

namespace Graal.Library.Storage.Common
{
    public class CommonPostgresSqlDriver : IStorageSqlDriver
    {
        protected readonly Dictionary<string, string> Names;

        public string SchemaName => Environment.GetEnvironmentVariable("GraalSchemaName") ?? "graal";

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

        public virtual bool SchemaExistAndCorrect()
        {
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = $"select distinct(table_schema) from information_schema.tables where table_schema='{SchemaName}'";

                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        #endregion

        #region Initial

        public virtual void CreateNeededTables()
        {
            if (!this.SchemaExistAndCorrect())
            {
                if (SchemaName.Length == 0)
                    throw new InvalidOperationException("Невозможно создать схему - имя схемы не может быть пустым!");

                if (!new Regex(@"^[a-z][a-z0-9_]*$").IsMatch(SchemaName))
                    throw new InvalidOperationException($"Невозможно создать схему - имя схемы {SchemaName} содержит недопустимые символы!");

                using (var cmd = Connection.CreateCommand())
                {
                    cmd.CommandText = $"create schema {SchemaName}";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region Connection

        /// <summary>
        /// Состояние соединения с БД
        /// </summary>
        public bool ConnectionStatus => Connection != null && Connection.State == ConnectionState.Open;

        /// <summary>
        /// Имя базы данных, к которой произведено подключение
        /// </summary>
        public string DBName => ConnectionStatus ? Connection.Database : string.Empty;

        #endregion
    }
}
