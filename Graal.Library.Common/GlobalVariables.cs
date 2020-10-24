using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Graal.Library.Common
{
    public static class GlobalVariables
    {
        public static DateTime FIRST_DATE => new DateTime(1995, 1, 1);

        readonly static NumberFormatInfo nfi = new NumberFormatInfo() { CurrencyGroupSeparator = "." };

        public static NumberFormatInfo NFI => nfi;

        public static string EnvironmentVariableGraalDataPathName => "GraalDataPath";

        static string graalDataPath;

        public static string GraalDataPath
        {
            get
            {
                if (graalDataPath == null)
                    Environment.GetEnvironmentVariable(EnvironmentVariableGraalDataPathName, EnvironmentVariableTarget.User);

                return graalDataPath;
            }
        }
    }
}
