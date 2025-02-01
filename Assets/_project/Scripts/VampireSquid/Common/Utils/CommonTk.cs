using System;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;

namespace _3rdParty.Utils
{
    public static class CommonTk
    {
        [IncludeMyAttributes]
        [FoldoutGroup("Info")] public class Info : Attribute { }
        
        [IncludeMyAttributes]
        [FoldoutGroup("References")] public class References : Attribute { }

        [IncludeMyAttributes]
        [FoldoutGroup("Settings")] public class Settings : Attribute { }
        
        [IncludeMyAttributes]
        [FoldoutGroup("Physics")] public class Physics : Attribute { }
        
        [IncludeMyAttributes]
        [InlineProperty, HideLabel] public class Property : Attribute { }
        
        [IncludeMyAttributes]
        [FoldoutGroup("Features")] public class Features : Attribute { }
    }
}
#endif