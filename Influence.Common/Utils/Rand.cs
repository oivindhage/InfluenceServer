using System;
using System.Collections.Generic;

namespace Influence.Common.Utils
{
    public static class Rng
    {
        private static readonly object RngLock = new object();
        private static readonly Random Rand = new Random();

        public static List<T> ShuffleList<T>(List<T> list)
        {
            int n = list.Count;

            lock (RngLock)
            {
                while (n > 1)
                {
                    n--;
                    int k = Rand.Next(0, n);
                    T value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }
            }

            return list;
        }

        public static bool Chance(int percent)
        {
            if (percent < 1)
                return false;

            if (percent > 99)
                return true;

            lock(RngLock)
                return Rand.Next(1, 101) <= percent;
        }

        public static bool Chance(double percent)
        {
            lock (RngLock)
                return Rand.NextDouble() <= percent * 0.01;
        }

        public static int NumberBetween(int minInclusive, int maxInclusive)
        {
            lock (RngLock)
                return Rand.Next(minInclusive, maxInclusive + 1);
        }
    }
}
