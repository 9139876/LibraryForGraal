using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graal.Library.Common.Enums
{
    /// <summary>
    /// Таймфрейм
    /// </summary>
    public enum TimeFrame
    {
        /// <summary>
        /// Годовой
        /// </summary>
        [Description("Годовой")]
        Y1 = 102,
        /// <summary>
        /// Сезонный
        /// </summary>
        [Description("Сезонный")]
        Seasonly = 101,
        /// <summary>
        /// Месячный
        /// </summary>
        [Description("Месячный")]
        M1 = 10,
        /// <summary>
        /// Недельный
        /// </summary>
        [Description("Недельный")]
        W1 = 9,
        /// <summary>
        /// Дневной
        /// </summary>
        [Description("Дневной")]
        D1 = 8,
        /// <summary>
        /// Часовой
        /// </summary>
        [Description("Часовой")]
        H1 = 7,
        /// <summary>
        /// 4-минутный
        /// </summary>
        [Description("4-минутный")]
        min4 = 100,
        /// <summary>
        /// 1-минутный
        /// </summary>
        [Description("1-минутный")]
        min1 = 2
    }

    public static class TimeFrameExtensions
    {
        readonly static Dictionary<TimeFrame, int> TimeFrameDuration = new Dictionary<TimeFrame, int>
        {
            { TimeFrame.min1,       1},
            { TimeFrame.min4,       2},
            { TimeFrame.H1,         3},
            { TimeFrame.D1,         4},
            { TimeFrame.W1,         5},
            { TimeFrame.M1,         6},
            { TimeFrame.Seasonly,   7},
            { TimeFrame.Y1,         8}
        };

        /// <summary>
        /// Возможна ли конвертация котировок первого таймфрейма во второй.
        /// </summary>
        /// <param name="tf1">Первый таймфрейм.</param>
        /// <param name="tf2">Второй таймфрейм.</param>
        /// <returns>Признак возможности конвертации.</returns>
        public static bool PossibilityConvert(TimeFrame tf1, TimeFrame tf2) => TimeFrameDuration[tf1] < TimeFrameDuration[tf2];

        /// <summary>
        /// Целое число со знаком, представляющее результат сравнения значений x и y:
        /// -1, если x меньше y;
        /// 0, если x равно y;
        /// 1, если x больше y.
        /// </summary>
        /// <returns></returns>
        public static int Compare(TimeFrame x, TimeFrame y)
        {
            if (TimeFrameDuration[x] < TimeFrameDuration[y])
                return -1;
            else if (TimeFrameDuration[x] > TimeFrameDuration[y])
                return 1;
            else
                return 0;
        }

        public static TimeFrame Max(TimeFrame x, TimeFrame y) => Compare(x, y) > 0 ? x : y;

        public static TimeFrame Min(TimeFrame x, TimeFrame y) => Compare(x, y) < 0 ? x : y;
    }
}
