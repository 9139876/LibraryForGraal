using Graal.Library.Common.Enums;
using Graal.Library.Common.Quotes;
using System;
using System.Collections.Generic;

namespace Graal.Library.Common.Interfaces
{
    /// <summary>
    /// Общий интерфейс менеджера котировок
    /// </summary>
    public interface IQuotesManager
    {
        void OpenWindow();

        void ReloadTickersInfo(ref int max, ref int cur, ref string text);

        void RefreshQuoteInDB(TickerTF ttf, ref int max, ref int cur, ref string text);

        List<TickerTF> GetAllTickerTFs(TickerInfo tickerInfo);

        TickerTF GetTickerTF(TickerInfo tickerInfo, TimeFrame tf);
    }
}