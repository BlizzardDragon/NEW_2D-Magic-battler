using UnityEngine;

namespace VampireSquid.Common.Utils
{
    public static class ColorExt
    {
        public static Color WithAlpha(this Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }
    }
}