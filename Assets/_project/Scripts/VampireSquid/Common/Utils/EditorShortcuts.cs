namespace VampireSquid.Common.Utils
{
    public static class EditorShortcuts
    {
#if  ODIN_INSPECTOR
        
        public static bool TableRefreshButton
        {
#if UNITY_EDITOR
            get => Sirenix.Utilities.Editor.SirenixEditorGUI.IconButton(Sirenix.Utilities.Editor.EditorIcons.Refresh);
#else 
            get => false;
#endif
        }

        public static void BeginBoxForListObject(string boxName)
        {
#if UNITY_EDITOR
            Sirenix.Utilities.Editor.SirenixEditorGUI.BeginBox(boxName);
#endif
        }
        public static void EndBoxCoverForListObject()
        {
#if UNITY_EDITOR
            Sirenix.Utilities.Editor.SirenixEditorGUI.EndBox();
#endif
        }

        public static void RefreshForPrefabUpdate(UnityEngine.Object gameObject)
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(gameObject);
            UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(gameObject);
#endif
        }
#endif
    }
}