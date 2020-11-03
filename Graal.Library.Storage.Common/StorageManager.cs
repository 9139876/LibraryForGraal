using System;
using System.Collections.Generic;
using System.Linq;
using Graal.Library.Common;
using System.Data;
using Graal.Library.Common.Quotes;
using Graal.Library.Common.Enums;
using Npgsql;
using System.IO;
using NLog;
using NLog.Fluent;
using Graal.Library.Common.Interfaces;

namespace Graal.Library.Storage.Common
{
    public class StorageManager : IStorageManager
    {
        const string ConnectionStringFile = "ConnectionString.txt";

        readonly IStorageSqlDriver sqlDriver;

        public event Action StorageStatusChanged;

        public StorageManager()
        {
            sqlDriver = new CommonPostgresSqlDriver();

            sqlDriver.ConnectionStatusChange += StorageStatusChanged;

            if (TryGetConnection(out IDbConnection connection, out string err))
                sqlDriver.Connection = connection;
            else
                AppGlobal.Logger.Error(err);
        }

        public bool StorageStatus => sqlDriver.ConnectionStatus;

        public string StorageStatusS => StorageStatus ? $"Установлено соединение с хранилищем '{sqlDriver.DBName}'" : "Соединение с хранилищем не установлено";

        /// <summary>
        /// Проверка существования схемы Graal
        /// </summary>
        /// <returns></returns>
        public bool GraalSchemaExist() => sqlDriver.SchemaExist();

        /// <summary>
        /// Проверка существования схемы Graal и наличия в ней необходимых таблиц
        /// </summary>
        /// <returns></returns>
        public bool GraalSchemaExistAndCorrect() => sqlDriver.SchemaExistAndCorrect();

        /// <summary>
        /// Создание схемы, таблиц, процедур
        /// </summary>
        public void GraalSchemaCreate() => sqlDriver.CreateGraalSchema();

        /// <summary>
        /// Пытается получить соединение с БД, используя вводимые пользователем параметры
        /// </summary>
        public void ChangeConnectionParameters()
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
                    if (!AppGlobal.TryGetFullPath(GraalTypeFolder.Storage, ConnectionStringFile, out string path))
                        return;

                    using (var fs = new FileStream(path, FileMode.Create))
                    using (var sW = new StreamWriter(fs))
                        sW.Write(new Crypt(Environment.UserName, AppGlobal.Logger.Info).Encrypt(connectionString));

                    sqlDriver.Connection = connection;
                }
            }
        }

        #region Parsers

        public Dictionary<string, string> GetAllParsers() => sqlDriver.GetAllParsers();

        public void RenameParser(string from, string to) => sqlDriver.RenameParser(from, to);

        public void AddParser(string name, string serialize) => sqlDriver.AddParser(name, serialize);

        #endregion

        #region static

        public static string GetGraalSchemaName() => Environment.GetEnvironmentVariable("GraalSchemaName", EnvironmentVariableTarget.User);

        public static void SetGraalSchemaName(string name) => Environment.GetEnvironmentVariable("GraalSchemaName", EnvironmentVariableTarget.User);

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

        /// <summary>
        /// Возвращает расшифрованную сохраненную строку соединения
        /// </summary>
        public static bool TryGetConnectionString(out string connStr, out string err)
        {
            connStr = string.Empty;
            err = string.Empty;

            if (!AppGlobal.TryGetFullPath(GraalTypeFolder.Storage, ConnectionStringFile, out string path))
            {
                err = "Не удалось получить значение имени папки данных Graal из переменной среды.";
                return false;
            }

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

            connStr = new Crypt(Environment.UserName, AppGlobal.Logger.Debug).Decrypt(encriptedConnectionString);
            return true;
        }

        public static bool TryGetConnection(out IDbConnection connection, out string error)
        {
            if (TryGetConnectionString(out string connStr, out error))
            {
                return TryGetConnection(connStr, out connection, out error);
            }
            else
            {
                connection = null;
                return false;
            }

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
