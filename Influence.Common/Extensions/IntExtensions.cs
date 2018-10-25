namespace Influence.Common.Extensions
{
    public static class IntExtensions
    {
        public static bool IsBetween(this int i, int lowerBoundInc, int upperBoundInc) 
            => i >= lowerBoundInc && i <= upperBoundInc;
    }
}