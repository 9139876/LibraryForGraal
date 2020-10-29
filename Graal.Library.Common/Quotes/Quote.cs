using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Graal.Library.Common.Quotes
{
    public class Quote
    {
        public Storage.StorageId StorageId { get; } = new Storage.StorageId();

        public TickerTF ParentTickerTF { get; }

        public DateTime Date { get; }

        public string DateS => Date.ToString("G");

        public decimal Open { get; }

        public string OpenS => Open.ToString(AppGlobal.NFI);

        public decimal Hi { get; }

        public string HiS => Hi.ToString(AppGlobal.NFI);

        public decimal Low { get; }

        public string LowS => Low.ToString(AppGlobal.NFI);

        public decimal Close { get; }

        public string CloseS => Close.ToString(AppGlobal.NFI);

        public decimal Volume { get; }

        public string VolumeS => Volume.ToString(AppGlobal.NFI);

        public Quote(TickerTF tickerTF, DateTime date, decimal open, decimal hi, decimal low, decimal close, decimal volume, int storageId = Storage.StorageId.DefaultValue)
        {
            ParentTickerTF = tickerTF;
            Date = date;
            Open = open;
            Hi = hi;
            Low = low;
            Close = close;
            Volume = volume;

            if (storageId >= 0)
                StorageId.Set(storageId);
        }

        public override string ToString()
        {
            return string.Format("[{0, -12}]   o:{1, -8} h:{2, -8} l:{3, -8} c:{4, -8}", Date.ToString("g"), Formatting(Open), Formatting(Hi), Formatting(Low), Formatting(Close));
        }

        string Formatting(decimal number) => number > 5 ? number.ToString("f2") : number.ToString("f4");

        public override bool Equals(object obj)
        {
            return obj is Quote quote &&
                   ParentTickerTF == quote.ParentTickerTF &&
                   Date == quote.Date;
        }

        public override int GetHashCode()
        {
            var hashCode = 302178880;
            hashCode = hashCode * -1521134295 + ParentTickerTF.GetHashCode();
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            return hashCode;
        }
    }
}

