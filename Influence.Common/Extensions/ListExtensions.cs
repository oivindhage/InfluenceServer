using System;
using System.Collections.Generic;
using System.Linq;

namespace Influence.Common.Extensions
{
    public static class ListExtensions
    {
        static readonly Random Rand = new Random();
        public static T Random<T>(this List<T> list)
            => list.Any()
                ? list[Rand.Next(list.Count)]
                : default;
    }
}
