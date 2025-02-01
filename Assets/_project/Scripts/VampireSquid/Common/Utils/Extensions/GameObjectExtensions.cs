using UnityEngine;

namespace VampireSquid.Scripts.Common
{
    public static class GameObjectExtensions
    {
        public static bool IsOfLayer(this Collider collider, string layer) =>
            collider.gameObject.layer == LayerMask.NameToLayer(layer);

        public static bool Matches(this Collider collider, LayerMask layerMask) =>
            ((1 << collider.gameObject.layer) & layerMask) != 0;

        public static bool Matches(this Collision collision, LayerMask layerMask) =>
            ((1 << collision.gameObject.layer) & layerMask) != 0;

        public static void SetLayer(this GameObject parent, string layer, bool includeChildren = false)
        {
            int unmaskedLayer = LayerMask.NameToLayer(layer);
            parent.layer = unmaskedLayer;

            if (includeChildren)
            {
                //for some reasons this is faster than direct transform enumeration
                foreach (var transform in parent.transform.GetComponentsInChildren<Transform>(includeInactive: true))
                    transform.gameObject.layer = unmaskedLayer;
            }
        }
    }
}