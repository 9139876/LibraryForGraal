using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Graal.Library.Common
{
    public static class Constatnts
    {
        public static DateTime FIRST_DATE => new DateTime(1995, 1, 1);

        readonly static NumberFormatInfo nfi = new NumberFormatInfo() { CurrencyGroupSeparator = "." };

        public static NumberFormatInfo NFI => nfi;
    }
}
