using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using NLog.Fluent;
using System.IO;
using Graal.Library.Common.Enums;
using System.Windows.Forms;

namespace Graal.Library.Common
{
    public static class ApplicationGlobal
    {
        #region noMutable

        public static DateTime FIRST_DATE => new DateTime(1995, 1, 1);

        public static NumberFormatInfo NFI { get; } = new NumberFormatInfo() { CurrencyGroupSeparator = "." };

        public static string EnvironmentVariableGraalDataPathName => "GraalDataPath";

        public static string EnvironmentVariableGraalSchemaName => "GraalSchemaName";

        public static bool TryGetFullPath(GraalTypeFolder typeFolder, string fileName, out string fullPath)
        {
            if (TryGetGraalDataPath(out string path))
            {
                fullPath = Path.Combine(path, typeFolder.ToString(), fileName);
                return true;
            }
            else
            {
                fullPath = null;
                return false;
            }
        }

        public static bool EnvironmentIsReady() => TryGetGraalDataPath(out string path) && Directory.Exists(path);

        #endregion

        #region mutable

        public static Action<string> ErrorMessage { get; set; } = (s) => MessageBox.Show(s, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static Action<string> WarningMessage { get; set; } = (s) => MessageBox.Show(s, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        public static Action<string> InfoMessage { get; set; } = (s) => MessageBox.Show(s, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);

        static NLog.Logger logger;
        public static NLog.Logger Logger
        {
            get => logger ?? throw new Exception("Логгер для приложения был запрошен, но не задан");
            set => logger = value;
        }

        static bool TryGetGraalDataPath(out string path)
        {
            path = Environment.GetEnvironmentVariable(EnvironmentVariableGraalDataPathName, EnvironmentVariableTarget.User);
            return !string.IsNullOrEmpty(path);
        }

        #endregion
    }
}
