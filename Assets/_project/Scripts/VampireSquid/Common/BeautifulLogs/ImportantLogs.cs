using System;
using UnityEngine;

namespace VampireSquid.Common.BeautifulLogs
{
    public static class BLog
    {
        [HideInCallstack]
        public static void LogImportant(string text, Type classType = null)
        {
#if DEBUG_IMPORTANT
            string warn = GetWarn("red", classType);
            UnityEngine.Debug.Log(string.Format("{0} {1}", warn, text));
#endif
        }
        
        [HideInCallstack]
        public static void LogWarning(string text, Type classType = null)
        {
#if DEBUG_WARNING
            string warn = GetWarn("yellow", classType);
            UnityEngine.Debug.Log(string.Format("{0} {1}", warn, text));
#endif
        }
        
        [HideInCallstack]
        public static void Log(string text, bool positive = false, Type classType = null)
        {
#if DEBUG_LOG
            if (positive)
            {
#if !STRIP_GOODLOG
                string warn = GetWarn("#008000", classType);
                UnityEngine.Debug.Log(string.Format("{0} {1}", warn, text));
#endif
            }
            else
            {
#if !STRIP_BADLOG
                string warn = GetWarn("#E34234", classType);
                UnityEngine.Debug.Log(string.Format("{0} {1}", warn, text));
#endif
            }
#endif
        }

        public static string GetTypeName<T>() => GetTypeName(typeof(T));
        public static string GetTypeName(Type type) => type.Name;

        private static string GetWarn(string color, Type classType = null)
        {
            var className = classType != null ? $"{GetTypeName(classType)}" : "!!!";
            return $"<color={color}>[{className}]</color>";
        }
    }
}