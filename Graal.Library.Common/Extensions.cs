using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graal.Library.Common
{
    public static class Extensions
    {
        public static bool EqualsAllElements<T>(this List<T> list1, List<T> list2)
        {
            if (list1 == null || list2 == null || list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
                if (!list1[i].Equals(list2[i]))
                    return false;

            return true;
        }

        public static int GetTrueHashCode<T>(this List<T> list)
        {
            if (list == null)
                return -1;

            var hashCode = 301429547;

            foreach (var l in list)
                hashCode = hashCode * -1521134295 + l.GetHashCode();

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
