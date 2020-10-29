using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Graal.Library.Common
{
    public static class Extensions
    {
        public static bool EqualsAllElements<T>(this IEnumerable<T> collection1, IEnumerable<T> collection2)
        {
            if (collection1 == null || collection2 == null || collection1.Count() != collection2.Count())
                return false;

            for (int i = 0; i < collection1.Count(); i++)
                if (!collection1.ElementAt(i).Equals(collection2.ElementAt(i)))
                    return false;

            return true;
        }

        public static int GetTrueHashCode<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
                return -1;

            var hashCode = 301429547;

            foreach (var item in collection)
                hashCode = hashCode * -1521134295 + item.GetHashCode();

            return hashCode;
        }

        public static T[,] TryGetPieceOfArray<T>(T[,] array, Point point, Size size)
        {
            return TryGetPieceOfArray(array, point.X, size.Width, point.Y, size.Height);
        }

        public static T[,] TryGetPieceOfArray<T>(T[,] array, int x, int xLen, int y, int yLen)
        {
            var res = new T[Math.Min(xLen, array.GetLength(0) - x), Math.Min(yLen, array.GetLength(1) - y)];

            for (int i = 0; i < res.GetLength(0); i++)
                for (int j = 0; j < res.GetLength(1); j++)
                    res[i, j] = array[i + x, j + y];

            return res;
        }
    }
}
