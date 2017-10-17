using System;

namespace Influence.Common.Extensions
{
    public static class GuidExtensions
    {
        public static bool IsValid(this Guid g) => g != Guid.Empty;
        public static bool NotValid(this Guid g) => g == Guid.Empty;
    }
}