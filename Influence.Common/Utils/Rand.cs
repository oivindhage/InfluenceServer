using System;
using System.Collections.Generic;

namespace Influence.Common.Utils
{
    public static class Rng
    {
        public static readonly Random Rand = new Random();

        public static List<T> ShuffleList<T>(List<T> list)
        {
            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = Rand.Next(0, n);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }
    }
}
