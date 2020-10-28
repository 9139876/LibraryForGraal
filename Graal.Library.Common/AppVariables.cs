using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using NLog.Fluent;
using System.IO;
using Graal.Library.Common.Enums;

namespace Graal.Library.Common
{
    public static class AppVariables
    {
        #region noMutable

        public static DateTime FIRST_DATE => new DateTime(1995, 1, 1);

        public static NumberFormatInfo NFI { get; } = new NumberFormatInfo() { CurrencyGroupSeparator = "." };

        public static string EnvironmentVariableGraalDataPathName => "GraalDataPath";

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

        #endregion

        #region mutable

        public static Action<string> Message { get; set; }

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
