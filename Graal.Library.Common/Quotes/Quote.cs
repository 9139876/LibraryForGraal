using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Graal.Library.Common.Quotes
{
    public class Quote
    {
        public Storage.StorageId StorageId { get; } = new Storage.StorageId();

        public TickerTF ParentTickerTF { get; private set; }

        public DateTime Date { get; private set; }

        public string DateS => Date.ToString("G");

        public double Open { get; private set; }

        public string OpenS => Open.ToString(ApplicationGlobal.NFI);

        public double Hi { get; private set; }

        public string HiS => Hi.ToString(ApplicationGlobal.NFI);

        public double Low { get; private set; }

        public string LowS => Low.ToString(ApplicationGlobal.NFI);

        public double Close { get; private set; }

        public string CloseS=> Close.ToString(ApplicationGlobal.NFI);

        public double Volume { get; private set; }

        public string VolumeS => Volume.ToString(ApplicationGlobal.NFI);

        public Quote(TickerTF tickerTF, DateTime date, double open, double hi, double low, double close, double volume, int storageId = Storage.StorageId.DefaultValue)
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

        string Formatting(double number) => number > 5 ? number.ToString("f2") : number.ToString("f4");

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

