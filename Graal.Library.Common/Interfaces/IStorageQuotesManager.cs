using System;
using System.Collections.Generic;
using Graal.Library.Common.Quotes;
using Graal.Library.Common.Enums;

namespace Graal.Library.Common
{
    /// <summary>
    /// ОБщий интерфейс менеджера хранилища данных
    /// </summary>
    public interface IStorageQuotesManager : IStorageManager
    {
        #region TickerInfo
        /// <summary>
        /// Возращает коллекцию объектов <see cref="TickerInfo"/> из хранилища.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TickerInfo> TickerInfo_GetAll();

        /// <summary>
        /// Возращает объект <see cref="TickerInfo"/> по имени тикера и рынка
        /// </summary>
        /// <param name="market">Имя рынка</param>
        /// <param name="ticker">Имя тикера</param>
        /// <returns></returns>
        TickerInfo TickerInfo_Get(string market, string ticker);

        /// <summary>
        /// Возращает объект <see cref="TickerInfo"/> по id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns></returns>
        //TickerInfo TickerInfo_Get(string id);

        /// <summary>
        /// Сохраняет коллекцию объектов <see cref="TickerInfo"/> в хранилище.
        /// </summary>
        /// <param name="storedTickerInfos">The stored ticker infos.</param>
        void TickerInfo_AddToStorage(TickerInfo storedTickerInfo);
        #endregion

        #region TickerTF
        void TickerTF_UpdateTradingTimeRules(TickerTF tickerTF);

        /// <summary>
        /// Возвращает объект <see cref="TickerTF"/> из хранилища
        /// </summary>
        /// <param name="tickerInfo">Объект <see cref="TickerInfo"/></param>
        /// <param name="tf">Таймфрейм</param>
        /// <returns></returns>
        TickerTF TickerTF_Get(TickerInfo tickerInfo, Library.Common.Enums.TimeFrame tf);

        /// <summary>
        /// Возвращает дату самой ранней котировки для указанного тикера и таймфрейма.
        /// </summary>
        /// <param name="tickerTF">Объект <see cref="TickerTF"/>.</param>
        /// <returns></returns>
        DateTime TickerTF_GetFirstDateInStorage(TickerTF tickerTF);

        /// <summary>
        /// Возвращает дату самой поздней котировки для указанного тикера и таймфрейма.
        /// </summary>
        /// <param name="tickerTF">Объект <see cref="TickerTF"/>.</param>
        /// <returns></returns>
        DateTime TickerTF_GetLastDateInStorage(TickerTF tickerTF);

        /// <summary>
        /// Возвращает самую низкую цену для указанного тикера и таймфрейма.
        /// </summary>
        /// <param name="tickerTF">Объект <see cref="TickerTF"/>.</param>
        /// <returns></returns>
        double TickerTF_GetMinPriceInStorage(TickerTF tickerTF);

        /// <summary>
        /// Возвращает самую высокую цену для указанного тикера и таймфрейма.
        /// </summary>
        /// <param name="tickerTF">Объект <see cref="TickerTF"/>.</param>
        /// <returns></returns>
        double TickerTF_GetMaxPriceInStorage(TickerTF tickerTF);

        /// <summary>
        /// Возвращает количество котировок в хранилище для указанного тикера и таймфрейма
        /// </summary>
        /// <param name="tickerTF">Объект <see cref="TickerTF"/>.</param>
        /// <returns></returns>
        int TickerTF_GetQuotesCountInStorage(TickerTF tickerTF);
        #endregion

        #region Quote
        /// <summary>
        /// Сохраняет коллекцию котировок в хранилище.
        /// </summary>
        /// <param name="quotes">Коллекция котировок</param>
        void Quotes_AddToStorage(IEnumerable<Quote> quotes);
        #endregion
    }
}
