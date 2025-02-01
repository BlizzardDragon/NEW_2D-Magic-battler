using UnityEngine;
using System;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace VampireSquid.Common.Version
{
    [CreateAssetMenu(menuName = "Create AppVersionConfig", fileName = "AppVersionConfig", order = 51)]
    public class AppVersionConfig : ScriptableObject, IAppVersion
    {
#if ODIN_INSPECTOR
        [field: SerializeField, ReadOnly, HideLabel, SuffixLabel("Version", true)]
#endif
        public string VersionText { get; private set; }
        
#if ODIN_INSPECTOR
        [SerializeField, VerText]  private string versionPrefix  = "v ";
        [SerializeField, VerParam] private int    globalReleases = 0;
        [SerializeField, VerParam] private int    breakUpdates   = 1;
        [SerializeField, VerParam] private int    smallUpdates   = 0;
        [SerializeField, VerText]  private string versionType    = " [Beta]";

        private void UpdateVersion() => VersionText = $"{versionPrefix}{globalReleases}.{breakUpdates}.{smallUpdates}{versionType}";

        private void OnValidate()
        {
            if (globalReleases < 0) globalReleases = 0;
            if (breakUpdates   < 0) breakUpdates   = 0;
            if (smallUpdates   < 0) smallUpdates   = 0;
            
            UpdateVersion();
#if UNITY_EDITOR
            UnityEditor.PlayerSettings.bundleVersion = VersionText;
#endif
        }

        #region Header

        [IncludeMyAttributes, HorizontalGroup("v"), HideLabel]
        private class VerParamAttribute : Attribute { }
        [IncludeMyAttributes, SuffixLabel("text", true), HorizontalGroup("v"), HideLabel]
        private class VerTextAttribute : Attribute { }

        #endregion
#endif
    }
}