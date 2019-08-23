using System;
using System.Collections.Generic;
using System.Linq;

namespace Influence.Common.Extensions
{
    public static class ListExtensions
    {
        static Random _random = new Random();
        public static T Random<T>(this List<T> list)
            => list.Any()
                ? list[_random.Next(list.Count)]
                : default;
    }
}
