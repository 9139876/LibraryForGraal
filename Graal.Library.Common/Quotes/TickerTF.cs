using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graal.Library.Common.Enums;
using Graal.Library.Common.Storage;

namespace Graal.Library.Common.Quotes
{
    /// <summary>
    /// Таймфрейм тикера
    /// </summary>
    public class TickerTF
    {
        //public static readonly DateTime FirstDate = new DateTime(1995, 1, 1);

        //public Storage.StorageId StorageId { get; } = new Storage.StorageId();

        public TimeFrame TimeFrame { get; private set; }

        public TickerInfo ParentTickerInfo { get; private set; }

        public List<Quote> Quotes { get; private set; }

        public DateTime QuotesDateOfLastUpdate { get; set; }

        public TickerTF(int timeFrame, TickerInfo tickerInfo, IEnumerable<Quote> quotes, string tradingTimeRulesSerialize, int storageId = Storage.StorageId.DefaultValue, DateTime? quotesDateOfLastUpdate = null)
        {
            TimeFrame = (TimeFrame)timeFrame;

            ParentTickerInfo = tickerInfo ?? throw new ArgumentNullException(nameof(tickerInfo), "Ошибка создания таймфрейма тикера - вместо информации о тикере пришел null");

            Quotes = quotes?.OrderBy(q => q.Date).ToList() ?? new List<Quote>();

            QuotesDateOfLastUpdate = quotesDateOfLastUpdate ?? DateTime.MinValue;

            if (!string.IsNullOrEmpty(tradingTimeRulesSerialize))
                TradingTimeRules = new TradingTimeRules(this, tradingTimeRulesSerialize);

            //if (storageId >= 0)
            //    StorageId.Set(storageId);
        }

        public void RefreshQuotes(IEnumerable<Quote> storageQuotes)
        {
            Quotes = Quotes.Union(storageQuotes).OrderBy(q => q.Date).ToList();
        }

        public override bool Equals(object obj)
        {
            return obj is TickerTF tF &&
                   TimeFrame == tF.TimeFrame &&
                   ParentTickerInfo.Equals(tF.ParentTickerInfo);
        }

        public override int GetHashCode()
        {
            var hashCode = -1134543184;
            hashCode = hashCode * -1521134295 + TimeFrame.GetHashCode();
            hashCode = hashCode * -1521134295 + ParentTickerInfo.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"{ParentTickerInfo.MarketTitle}_{ParentTickerInfo.Title}_{TimeFrame.ToString()}";
        }

        public DateTime GetFirstDate() => Quotes.FirstOrDefault()?.Date ?? AppGlobal.FIRST_DATE;

        public DateTime GetLastDate() => Quotes.LastOrDefault()?.Date ?? AppGlobal.FIRST_DATE;

        public decimal GetMinPrice() => Quotes.Min(q => q.Low);

        public decimal GetMaxPrice() => Quotes.Max(q => q.Hi);

        public int GetQuotesCount() => Quotes.Count;

        public DateTime GetFirstDateInStorage(IStorageQuotesManager manager) => manager.TickerTF_GetFirstDateInStorage(this);

        public DateTime GetLastDateInStorage(IStorageQuotesManager manager) => manager.TickerTF_GetLastDateInStorage(this);

        public double GetMinPriceInStorage(IStorageQuotesManager manager) => manager.TickerTF_GetMinPriceInStorage(this);

        public double GetMaxPriceInStorage(IStorageQuotesManager manager) => manager.TickerTF_GetMaxPriceInStorage(this);

        public int GetQuotesCountInStorage(IStorageQuotesManager manager) => manager.TickerTF_GetQuotesCountInStorage(this);

        #region TradingTimeRules

        public void RefreshTradingTimeRules(IStorageQuotesManager manager)
        {
            if (TradingTimeRules == null)
                TradingTimeRules = new TradingTimeRules(this);

            TradingTimeRules.Refresh();

            manager.TickerTF_UpdateTradingTimeRules(this);
        }

        public TradingTimeRules TradingTimeRules { get; private set; }

        public string TradingTimeRulesSerialize() => TradingTimeRules?.Serialize() ?? string.Empty;

        #endregion
    }
}
