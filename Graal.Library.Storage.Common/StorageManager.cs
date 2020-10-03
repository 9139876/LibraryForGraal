using System;
using System.Collections.Generic;
using System.Linq;
using Graal.Library.Common;
using System.Data;
using Graal.Library.Common.Quotes;
using Graal.Library.Common.Enums;
using Npgsql;
using Graal.Library.Storage.Common.Properties;

namespace Graal.Library.Storage.Common
{
    public class StorageManager : IStorageManager
    {
        readonly protected IStorageSqlDriver sqlDriver;

        protected Action<string> Message, Debug;

        public event Action StorageStatusChanged;

        public StorageManager(Action<string> message, Action<string> debug)
        {
            Message = message;
            Debug = debug;

            sqlDriver = new CommonPostgresSqlDriver();

            sqlDriver.ConnectionStatusChange += StorageStatusChanged;

            if (GetConnection(out NpgsqlConnection connection))
                sqlDriver.Connection = connection;
        }

        /// <summary>
        /// Возвращает расшифрованную сохраненную строку соединения
        /// </summary>
        protected bool GetConnectionString(out string connStr)
        {
            if (Settings.Default.dbConnectionString?.Length == 0)
            {
                connStr = string.Empty;
                return false;
            }

            connStr = new Crypt(Environment.UserName, Debug).Decrypt(Properties.Settings.Default.dbConnectionString);
            return true;
        }

        protected bool GetConnection(out NpgsqlConnection connection)
        {
            connection = null;

            try
            {
                if (GetConnectionString(out string connStr))
                {
                    connection = new NpgsqlConnection(connStr);
                    connection.Open();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                PrintException(ex, "Не удалось подключиться к БД - ошибка");
                connection?.Dispose();
                return false;
            }
        }

        /// <summary>
        /// Пытается получить соединение с БД, используя вводимые пользователем параметры
        /// </summary>
        protected void InputConnectionParameters()
        {
            string server = string.Empty, port = string.Empty, user = string.Empty, password = string.Empty, database = string.Empty;

            if (GetConnectionString(out string connectionString))
            {
                try
                {
                    var sb = new NpgsqlConnectionStringBuilder(connectionString);

                    server = sb.Host;
                    port = sb.Port.ToString();
                    user = sb.Username;
                    password = "***";
                    database = sb.Database;
                }
                catch { }
            }

            IDbConnection connection = null;

            using (var paramsQueryWindow = new SqlConnectionParametersWindow(server, port, user, password, database, (c) => connection = c, (cs) => connectionString = cs))
            {
                paramsQueryWindow.ShowDialog();

                if (paramsQueryWindow.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    Properties.Settings.Default.dbConnectionString = new Crypt(Environment.UserName, Debug).Encrypt(connectionString);
                    Properties.Settings.Default.Save();

                    sqlDriver.Connection = connection;
                }
            }
        }

        protected void PrintException(Exception ex, string prefix = "")
        {
            Message?.Invoke($"{prefix}: {ex.Message}");
            Debug?.Invoke($"[{DateTime.Now}] {ex.Source} - {ex.GetType()} - {ex.Message} - {ex.StackTrace}");
        }

        public bool StorageStatus => sqlDriver.ConnectionStatus;

        public string StorageStatusS => StorageStatus ? $"Установлено соединение с хранилищем '{sqlDriver.DBName}'" : "Соединение с хранилищем не установлено";

        public bool GraalSchemaExist() => sqlDriver.SchemaExistAndCorrect();

        public void GraalSchemaCreate() => sqlDriver.CreateNeededTables();

        public void ChangeStorageSettings() => InputConnectionParameters();


        #region static

        public static string CreateConnectionString(string host, string port, string user, string password, string dataBase)
        {
            return new NpgsqlConnectionStringBuilder()
            {
                Host = host,
                Port = int.Parse(port),
                Username = user,
                Password = password,
                Database = dataBase
            }
            .ConnectionString;
        }

        public static bool TryGetConnection(string connectionString, out IDbConnection connection, out string error)
        {
            try
            {
                connection = new NpgsqlConnection(connectionString);
                connection.Open();
                error = string.Empty;
                return true;
            }
            catch (Exception e)
            {
                error = e.Message;
                connection = null;
                return false;
            }
        }
        #endregion
    }
}
