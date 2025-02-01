using UnityEngine;

namespace VampireSquid.Scripts.Common.GizomsDrawers
{
    public class BoxColliderGizmosDrawer : MonoBehaviour
    {
        [SerializeField] private Color       color;
        [SerializeField] private BoxCollider boxCollider;

        public void OnDrawGizmos()
        {
            if (boxCollider == null && !isActiveAndEnabled)
                return;

            Gizmos.color = color;
            Gizmos.DrawCube(transform.position + boxCollider.center, boxCollider.size);
        }
    }
}