namespace VampireSquid.Common.Utils
{
    public static class PrefsExt
    {
        public static int  BoolToInt(this bool b) => b ? 1 : 0;
        public static bool IntToBool(this int  i) => i == 1;
    }
}