using Graal.Library.Common.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graal.Library.Common.Quotes
{
    public class TradingTimeRules
    {
        readonly TickerTF parentTickerTF;

        Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>> Rules;

        internal TradingTimeRules(TickerTF _parentTickerTF, string serialize = null)
        {
            parentTickerTF = _parentTickerTF ?? throw new ArgumentException($"Невозможно создать правила торгового времени - вместо таймфрема тикера пришел null.", nameof(_parentTickerTF));

            Rules = new Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>>();

            if (!string.IsNullOrEmpty(serialize))
                Deserialize(serialize);
        }

        internal void Refresh()
        {
            if (parentTickerTF.TimeFrame == TimeFrame.Y1 || parentTickerTF.TimeFrame == TimeFrame.Seasonly || parentTickerTF.TimeFrame == TimeFrame.M1 || parentTickerTF.TimeFrame == TimeFrame.W1)
                return;

            var quotes = parentTickerTF.Quotes;

            if (quotes == null
                || (parentTickerTF.TimeFrame == TimeFrame.D1 && quotes.Count < 100)
                || (parentTickerTF.TimeFrame == TimeFrame.H1 && quotes.Count < 1200)
                || (parentTickerTF.TimeFrame == TimeFrame.min4 && quotes.Count < 15000)
                || (parentTickerTF.TimeFrame == TimeFrame.min1 && quotes.Count < 15000)
                )
                throw new InvalidOperationException($"Невозможно обновить правила торгового времени - у таймфрема тикера слишком мало котировок.");

            Rules = new Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>>();

            foreach (var dow in quotes.Select(q => q.Date.DayOfWeek).Distinct().OrderBy(d => (int)d))
                if (quotes.Where(q => q.Date.DayOfWeek == dow).Count() >= quotes.Count / 21) //21==7(дней в неделе)*3(~30%)
                    Rules.Add(dow, null);

            if (parentTickerTF.TimeFrame == TimeFrame.D1) //Для дней часы работы не нужны
                return;

            foreach (var dow in Rules.Keys.ToList())
            {
                var groupQuotes = new Dictionary<int, List<DateTime>>();

                foreach (var q in quotes.Where(q => q.Date.DayOfWeek == dow))
                {
                    var i = q.Date.Year * 10000 + q.Date.Month * 100 + q.Date.Day;

                    if (!groupQuotes.ContainsKey(i))
                        groupQuotes.Add(i, new List<DateTime>());

                    groupQuotes[i].Add(q.Date);
                }

                var startLimits = new Dictionary<TimeSpan, int>();
                var endLimits = new Dictionary<TimeSpan, int>();

                //Берем самые популярные
                foreach (var date in groupQuotes.Keys)
                {
                    var slim = groupQuotes[date].Min().TimeOfDay;
                    var elim = groupQuotes[date].Max().TimeOfDay;

                    if (!startLimits.ContainsKey(slim))
                        startLimits.Add(slim, 1);
                    else
                        startLimits[slim]++;

                    if (!endLimits.ContainsKey(elim))
                        endLimits.Add(elim, 1);
                    else
                        endLimits[elim]++;
                }

                Rules[dow] = new Tuple<TimeSpan, TimeSpan>(startLimits.OrderByDescending(sl => sl.Value).First().Key, endLimits.OrderByDescending(el => el.Value).First().Key);
            }
        }

        public DateTime GetNextDate(DateTime dt)
        {
            switch (parentTickerTF.TimeFrame)
            {
                case TimeFrame.Y1:
                case TimeFrame.Seasonly:
                case TimeFrame.M1:
                case TimeFrame.W1:
                    return Auxiliary.AddDate(dt, parentTickerTF.TimeFrame);

                case TimeFrame.D1:
                    {
                        dt = dt.AddDays(1);

                        while (!Rules.ContainsKey(dt.DayOfWeek))
                            dt = dt.AddDays(1);

                        return dt;
                    }

                case TimeFrame.H1:
                    {
                        dt = dt.AddHours(1);

                        while (!Rules.ContainsKey(dt.DayOfWeek) || dt.TimeOfDay < Rules[dt.DayOfWeek].Item1 || dt.TimeOfDay > Rules[dt.DayOfWeek].Item2)
                            dt = dt.AddHours(1);

                        return dt;
                    }

                case TimeFrame.min4:
                    {
                        dt = dt.AddMinutes(4);

                        while (!Rules.ContainsKey(dt.DayOfWeek) || dt.TimeOfDay < Rules[dt.DayOfWeek].Item1 || dt.TimeOfDay > Rules[dt.DayOfWeek].Item2)
                            dt = dt.AddMinutes(4);

                        return dt;
                    }

                case TimeFrame.min1:
                    {
                        dt = dt.AddMinutes(1);

                        while (!Rules.ContainsKey(dt.DayOfWeek) || dt.TimeOfDay < Rules[dt.DayOfWeek].Item1 || dt.TimeOfDay > Rules[dt.DayOfWeek].Item2)
                            dt = dt.AddMinutes(1);

                        return dt;
                    }

                default: throw new ArgumentException($"Неизвестный таймфрем - '{ parentTickerTF.TimeFrame }'");
            }
        }

        void Deserialize(string serialize)
        {
            if (parentTickerTF.TimeFrame == TimeFrame.Y1 || parentTickerTF.TimeFrame == TimeFrame.Seasonly || parentTickerTF.TimeFrame == TimeFrame.M1 || parentTickerTF.TimeFrame == TimeFrame.W1)
                return;

            Rules = JsonConvert.DeserializeObject<Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>>>(serialize);
        }

        internal string Serialize()
        {
            if (parentTickerTF.TimeFrame == TimeFrame.Y1 || parentTickerTF.TimeFrame == TimeFrame.Seasonly || parentTickerTF.TimeFrame == TimeFrame.M1 || parentTickerTF.TimeFrame == TimeFrame.W1)
                return "default";

            return JsonConvert.SerializeObject(Rules, Formatting.Indented);
        }
    }
}
