using System;
using System.Collections.Generic;
using System.Linq;
using Graal.Library.Common;
using System.Data;
using Graal.Library.Common.Quotes;
using Graal.Library.Common.Enums;
using Npgsql;
using Graal.Library.Storage.Common.Properties;
using System.IO;

namespace Graal.Library.Storage.Common
{
    public class StorageManager : IStorageManager
    {
        protected const string ConnectionStringFile = @"\Storage\ConnectionString.txt";

        readonly protected IStorageSqlDriver sqlDriver;

        protected Action<string> Message, Debug;

        public event Action StorageStatusChanged;

        public StorageManager(Action<string> message, Action<string> debug)
        {
            Message = message;
            Debug = debug;

            sqlDriver = new CommonPostgresSqlDriver();

            sqlDriver.ConnectionStatusChange += StorageStatusChanged;

            if (TryGetConnection(out NpgsqlConnection connection, out string err))
                sqlDriver.Connection = connection;
            else
                Message?.Invoke(err);

        }

        /// <summary>
        /// Возвращает расшифрованную сохраненную строку соединения
        /// </summary>
        protected bool TryGetConnectionString(out string connStr, out string err)
        {
            connStr = string.Empty;
            err = string.Empty;

            if (string.IsNullOrEmpty(GlobalVariables.GraalDataPath))
            {
                err = "Не удалось получить значение имени папки данных Graal из переменной среды.";
                return false;
            }

            string path = GlobalVariables.GraalDataPath + ConnectionStringFile;

            if (!File.Exists(path))
            {
                err = "Файл с параметрами подключения к БД не найден.";
                return false;
            }

            string encriptedConnectionString;

            using (var fs = new FileStream(path, FileMode.Open))
            using (var sr = new StreamReader(fs))
                encriptedConnectionString = sr.ReadToEnd();

            if (string.IsNullOrEmpty(encriptedConnectionString))
            {
                err = "Файл с параметрами подключения к БД пуст.";
                return false;
            }

            connStr = new Crypt(Environment.UserName, Debug).Decrypt(encriptedConnectionString);
            return true;
        }

        protected bool TryGetConnection(out NpgsqlConnection connection, out string err)
        {
            connection = null;

            try
            {
                if (TryGetConnectionString(out string connStr, out err))
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
                err = ex.Message;
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

            if (TryGetConnectionString(out string connectionString, out _))
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
                    if (string.IsNullOrEmpty(GlobalVariables.GraalDataPath))
                    {
                        Message?.Invoke("Не удалось получить значение имени папки данных Graal из переменной среды.");
                        return;
                    }

                    string path = GlobalVariables.GraalDataPath + ConnectionStringFile;

                    using (var fs = new FileStream(path, FileMode.Create))
                    using (var sW = new StreamWriter(fs))
                        sW.Write(new Crypt(Environment.UserName, Debug).Encrypt(connectionString));

                    sqlDriver.Connection = connection;
                }
            }
        }

        //protected void PrintException(Exception ex, string prefix = "")
        //{
        //    Message?.Invoke($"{prefix}: {ex.Message}");
        //    Debug?.Invoke($"[{DateTime.Now}] {ex.Source} - {ex.GetType()} - {ex.Message} - {ex.StackTrace}");
        //}

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
