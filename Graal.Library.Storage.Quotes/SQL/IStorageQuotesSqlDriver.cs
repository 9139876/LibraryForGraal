using System;
using System.Collections.Generic;
using System.Data;
using Graal.Library.Common.Quotes;
using Graal.Library.Common.Enums;
using Graal.Library.Storage.Common;

namespace Graal.Library.Storage.Quotes
{
    public interface IStorageQuotesSqlDriver : IStorageSqlDriver
    {
        void TickerInfosToStorage(IEnumerable<TickerInfo> tickerInfos);

        void TickerInfoToStorage(TickerInfo tickerInfo);

        TickerInfo TickerInfo_Get(string id);

        List<TickerInfo> GetAllTickerInfos();

        void TickerTFsToStorage(IEnumerable<TickerTF> tickerTFs);

        void TickerTF_UpdateTradingTimeRules(TickerTF tickerTF);

        TickerTF GetTickerTF(TickerInfo tickerInfo, int timeFrame);

        void QuotesToStorage(IEnumerable<Quote> quotes);

        List<Quote> GetQuotes(TickerTF tickerTF, Quote lastQuote = null);

        DateTime GetLastDate(TickerTF tickerTF);

        DateTime GetFirstDate(TickerTF tickerTF);

        double GetMinPrice(TickerTF tickerTF);

        double GetMaxPrice(TickerTF tickerTF);

        int GetQuotesCount(TickerTF tickerTF);
    }
}
