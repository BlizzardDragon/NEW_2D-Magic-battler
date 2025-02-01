using UnityEngine;

namespace VampireSquid.Common.Utils
{
    public static class LayersExt
    {
        public static bool Contains(this LayerMask mask, GameObject go)       => ((1 << go.layer) & mask) != 0;
        public static bool Contains(this LayerMask mask, Collider   collider) => mask.Contains(collider.gameObject);
        
        public static bool Excludes(this LayerMask mask, GameObject go)       => ((LayerMask)~mask).Contains(go);
        public static bool Excludes(this LayerMask mask, Collider   collider) => ((LayerMask)~mask).Contains(collider.gameObject);
    }
}