using Graal.Library.Common.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graal.Library.Common
{
    public static class Auxiliary
    {
        /// <summary>
        /// Возвращает дату, отстоящую от заданной на указанное число единиц времени таймфрейма.
        /// </summary>
        /// <param name="startDate">Заданная дата.</param>
        /// <param name="timeFrame">Таймфрейм.</param>
        /// <param name="incr">Разница между датами в единицах времени таймфрейма.</param>
        /// <returns></returns>
        public static DateTime AddDate(DateTime startDate, TimeFrame timeFrame, int incr = 1)
        {
            switch (timeFrame)
            {
                case (TimeFrame.min1):
                    return startDate.AddMinutes(incr);
                case (TimeFrame.min4):
                    return startDate.AddMinutes(incr * 4);
                case (TimeFrame.H1):
                    return startDate.AddHours(incr);
                case (TimeFrame.D1):
                    return startDate.AddDays(incr);
                case (TimeFrame.W1):
                    return startDate.AddDays(incr * 7);
                case (TimeFrame.M1):
                    return startDate.AddMonths(incr);
                case (TimeFrame.Seasonly):
                    return startDate.AddMonths(incr * 3);
                case (TimeFrame.Y1):
                    return startDate.AddYears(incr);
            }

            throw new ArgumentException(nameof(timeFrame), $"Неизвестный таймфрейм - {timeFrame}");
        }

        /// <summary>
        /// Возвращает разницу между датами в единицах времени таймфрейма.
        /// </summary>
        /// <param name="dt1">Дата 1.</param>
        /// <param name="dt2">Дата 2.</param>
        /// <param name="timeFrame">Таймфрейм.</param>
        /// <returns>Разница между датами в единицах времени таймфрейма.</returns>
        public static int DatesDifferent(DateTime dt1, DateTime dt2, TimeFrame timeFrame)
        {
            var diff = dt2 - dt1;

            switch (timeFrame)
            {
                case (TimeFrame.min1):
                    return diff.Minutes;
                case (TimeFrame.min4):
                    return (int)Math.Ceiling((float)diff.Minutes/4);
                case (TimeFrame.H1):
                    return diff.Hours;
                case (TimeFrame.D1):
                    return diff.Days;
                case (TimeFrame.W1):
                    return (int)Math.Ceiling((float)diff.Days / 7);
                case (TimeFrame.M1):
                    return (int)Math.Ceiling((float)diff.Days / 30);
                case (TimeFrame.Seasonly):
                    return (int)Math.Ceiling((float)diff.Days / 120);
                case (TimeFrame.Y1):
                    return (int)Math.Ceiling((float)diff.Days / 365);
            }

            throw new ArgumentException(nameof(timeFrame), $"Неизвестный таймфрейм - {timeFrame}");
        }
    }

    public class PointAndBrush
    {
        public Point Point { get; private set; }
        public Brush Brush { get; private set; }

        public PointAndBrush(Point point, Brush brush)
        {
            Point = point;
            Brush = brush;
        }
    }
}
