using System;
using System.Collections.Generic;
using System.Linq;
using Graal.Library.Common;
using Graal.Library.Common.Storage;
using Npgsql;
using Graal.Library.Common.Quotes;
using Graal.Library.Common.Enums;
using System.Data;

namespace Graal.Library.Storage.Quotes
{
    public class PostgresStorageManager : Graal.Library.Storage.Common.StorageManager, IQuotesStorageManager
    {
        readonly IStorageQuotesSqlDriver quotesSqlDriver;

        public PostgresStorageManager(Action<string> message, Action<string> debug) : base(message, debug)
        {
            quotesSqlDriver = new QuotesPostgresSqlDriver();

            if (sqlDriver.Connection != null)
                quotesSqlDriver.Connection = sqlDriver.Connection;
            else if (GetConnection(out NpgsqlConnection connection))
                quotesSqlDriver.Connection = connection;
        }

        #region TickersToStorage

        public void TickerInfo_AddToStorage(TickerInfo storedTickerInfo)
        {
            quotesSqlDriver.TickerInfoToStorage(storedTickerInfo);
        }

        public void Quotes_AddToStorage(IEnumerable<Quote> quotes)
        {
            quotesSqlDriver.QuotesToStorage(quotes);
        }

        public void TickerTF_UpdateTradingTimeRules(TickerTF tickerTF) => quotesSqlDriver.TickerTF_UpdateTradingTimeRules(tickerTF);

        #endregion

        #region TickersFromStorage

        public IEnumerable<TickerInfo> TickerInfo_GetAll()
        {
            return quotesSqlDriver.GetAllTickerInfos();
        }

        public TickerTF TickerTF_Get(TickerInfo tickerInfo, TimeFrame tf)
        {
            return quotesSqlDriver.GetTickerTF(tickerInfo, (int)tf);
        }

        //public TickerInfo TickerInfo_Get(string id) => sqlDriver.TickerInfo_Get(id);

        public TickerInfo TickerInfo_Get(string market, string ticker)
        {
            return TickerInfo_GetAll().Where(ti => ti.MarketTitle == market && ti.Title == ticker).FirstOrDefault();
        }

        public DateTime TickerTF_GetFirstDateInStorage(TickerTF tickerTF)
        {
            return quotesSqlDriver.GetFirstDate(tickerTF);
        }

        public DateTime TickerTF_GetLastDateInStorage(TickerTF tickerTF)
        {
            return quotesSqlDriver.GetLastDate(tickerTF);
        }

        public double TickerTF_GetMinPriceInStorage(TickerTF tickerTF)
        {
            return quotesSqlDriver.GetMinPrice(tickerTF);
        }

        public double TickerTF_GetMaxPriceInStorage(TickerTF tickerTF)
        {
            return quotesSqlDriver.GetMaxPrice(tickerTF);
        }

        public int TickerTF_GetQuotesCountInStorage(TickerTF tickerTF)
        {
            return quotesSqlDriver.GetQuotesCount(tickerTF);
        }

        #endregion

        #region static

        public static string CreateConnectionString(string host, string port, string user, string password, string dataBase)
        {
            return new NpgsqlConnectionStringBuilder()
            {
                Host = host,
                Port = int.Parse(port),
                Username = user,
                Password = password,
                Database = dataBase
            }
            .ConnectionString;
        }

        public static bool TryGetConnection(string connectionString, out NpgsqlConnection connection, out string error)
        {
            try
            {
                connection = new NpgsqlConnection(connectionString);
                connection.Open();
                error = string.Empty;
                return true;
            }
            catch (Exception e)
            {
                error = e.Message;
                connection = null;
                return false;
            }
        }
        #endregion
    }
}
