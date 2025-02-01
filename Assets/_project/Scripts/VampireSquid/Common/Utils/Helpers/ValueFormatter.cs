using System.Collections.Generic;

namespace VampireSquid.Common.Utils.Helpers
{
    public static class ValueFormatter
    {
        private static readonly List<(int, string)> _digitsPostfixes = new ()
        {
            (1, ""),
            (1000, "K"),
            (1000000, "M"),
        };

        public static string Format(int value)
        {
            for (int i = 0; i < _digitsPostfixes.Count - 1; ++i)
            {
                var (currentValue, currentPostfix) = _digitsPostfixes[i];
                var (nextValue, nextPosfix)        = _digitsPostfixes[i + 1];

                bool nextIsLast = i >= _digitsPostfixes.Count - 2;

                if (nextValue > value)
                {
                    float truncedResult = (float)value / currentValue;

                    //return truncedResult.ToString(truncedResult >= 100 ? "0" : "0.0").Replace(".0", "") + currentPostfix;
                    return truncedResult.ToString() + currentPostfix;
                }

                if (nextIsLast)
                {
                    float truncedResult = (float)value / nextValue;

                    //return truncedResult.ToString(truncedResult >= 100 ? "0" : "0.0").Replace(".0", "") + nextPosfix;
                    return truncedResult.ToString() + nextPosfix;
                }
            }

            return string.Empty;
        }
    }
}