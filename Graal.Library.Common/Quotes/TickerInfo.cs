using Graal.Library.Common.Enums;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graal.Library.Common.Quotes
{
    /// <summary>
    /// Информация о тикере
    /// </summary>
    public class TickerInfo
    {
        //public Storage.StorageId StorageId { get; } = new Storage.StorageId();

        //public string TickerId { get; private set; }

        //public string Code { get; private set; }

        //public string MarketId { get; private set; }

        public string MarketTitle { get; private set; }

        public string Title { get; private set; }

        public string Currency { get; private set; }

        public string VolumeCode { get; private set; }

        //public string GetUrl { get; private set; }

        //public TickerInfo(string tickerId, string code, string title, string marketId, string marketTitle, string currency, string volumeCode, string getUrl, int storageId = Storage.StorageId.DefaultValue)
        //{
        //    TickerId = tickerId;
        //    Code = code;
        //    Title = title;
        //    MarketId = marketId;
        //    MarketTitle = marketTitle;
        //    Currency = currency;
        //    VolumeCode = volumeCode;
        //    GetUrl = getUrl;

        //    if (storageId >= 0)
        //        StorageId.Set(storageId);
        //}

        public TickerInfo(string title, string marketTitle, string currency, string volumeCode)
        {
            MarketTitle = marketTitle;
            Title = title;
            Currency = currency;
            VolumeCode = volumeCode;
        }

        public IEnumerable<TickerTF> GetTickerTFs() => Enum.GetValues(typeof(TimeFrame)).Cast<int>().Select(tf => new TickerTF(tf, this, null, null));

        public override string ToString()
        {
            //return "id".PadRight(12) + " = " + TickerId + "\r\n" +
            //        "code".PadRight(12) + " = " + Code + "\r\n" +
            //        "title".PadRight(12) + " = " + Title + "\r\n" +
            //        "market_id".PadRight(12) + " = " + MarketId + "\r\n" +
            //        "market_title".PadRight(12) + " = " + MarketTitle + "\r\n" +
            //        "currency".PadRight(12) + " = " + Currency + "\r\n" +
            //        "volume_code".PadRight(12) + " = " + VolumeCode;

            return "Рынок".PadRight(15) + " - " + MarketTitle + "\r\n" +
                    "Тикер".PadRight(15) + " - " + Title + "\r\n" +
                    "Валюта".PadRight(15) + " - " + Currency + "\r\n" +
                    "Ед. измерения".PadRight(15) + " - " + VolumeCode;
        }

        public override bool Equals(object obj)
        {
            return obj is TickerInfo info
                && MarketTitle == info.MarketTitle
                && Title == info.Title;
        }

        public override int GetHashCode()
        {
            var hashCode = 289181645;
            hashCode = hashCode * -1521134295 + MarketTitle.GetHashCode();
            hashCode = hashCode * -1521134295 + Title.GetHashCode();

            return hashCode;
        }

        public string ToJson() => ((JObject)JToken.FromObject(this)).ToString();
    }
}

