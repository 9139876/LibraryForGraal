using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graal.Library.Storage.Common
{
    public class SQLNames
    {
        #region QuotesGetter
        public static readonly string Tbl_QuotesParserExpressions = "QuotesParserExpressions";

        public static readonly string QuotesParserExpressions_Id = "id";

        public static readonly string QuotesParserExpressions_Specification = "specification";

        #endregion

        #region TickerInfo
        public static readonly string Tbl_TickersInfoes = "TickersInfoes";

        public static readonly string TickersInfoes_Id = "id";

        public static readonly string TickersInfoes_TickerId = "ticker_id";

        public static readonly string TickersInfoes_Code = "code";

        public static readonly string TickersInfoes_TickerTitle = "ticker_title";

        public static readonly string TickersInfoes_MarketId = "market_id";

        public static readonly string TickersInfoes_MarketTitle = "market_title";

        public static readonly string TickersInfoes_Currency = "currency";

        public static readonly string TickersInfoes_VolumeCode = "volume_code";

        public static readonly string TickersInfoes_AutoUpdate = "auto_update";

        public static readonly string TickersInfoes_GetUrl = "get_url";
        #endregion

        #region TickerTF
        public static readonly string Tbl_TickerTFs = "TickerTFs";

        public static readonly string TickerTFs_Id = "id";

        public static readonly string TickerTFs_Period = "period";

        public static readonly string TickerTFs_TickerInfoId = "tickerinfo_id";

        public static readonly string TickerTFs_TradingTimeRules = "trading_time_rules";

        public static readonly string TickerTFs_QuotesDateOfLastUpdate = "quotes_date_of_last_update";
        #endregion

        #region Quotes
        public static readonly string Tbl_Quotes = "Quotes";

        public static readonly string Quotes_Id = "id";

        public static readonly string Quotes_TickerTFId = "tickertf_id";

        public static readonly string Quotes_Date = "datetime";

        public static readonly string Quotes_Open = "open";

        public static readonly string Quotes_High = "high";

        public static readonly string Quotes_Low = "low";

        public static readonly string Quotes_Close = "close";

        public static readonly string Quotes_Volume = "volume";
        #endregion

        #region Tendentions
        public static readonly string Tbl_Tendentions = "Tendentions";

        public static readonly string Tendentions_Id = "id";

        public static readonly string Tendentions_TickerTFId = "tickertf_id";

        public static readonly string Tendentions_Type = "type";

        public static readonly string Tendentions_DateOfChange = "date_of_change";
        #endregion

        #region SimplePatterns
        public static readonly string Tbl_SimplePatterns = "SimplePatterns";

        public static readonly string SimplePatterns_Id = "id";

        public static readonly string SimplePatterns_Serialize = "serialize";

        public static readonly string SimplePatterns_Length = "length";

        public static readonly string SimplePatterns_Type = "type";

        public static readonly string SimplePatterns_TendentionId = "tendention_id";
        #endregion
    }
}
