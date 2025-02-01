using UnityEngine;

namespace VampireSquid.Scripts.Common.GizomsDrawers
{
    public class SphereColliderGizmosDrawer : MonoBehaviour
    {
        public bool IsEnableDrawing;

        [SerializeField] private Color _color;
        [SerializeField] private SphereCollider _sphereCollider;
        
        public void OnDrawGizmos()
        {
            if (_sphereCollider == null && !IsEnableDrawing)
                return;

            Gizmos.color = _color;
            Gizmos.DrawSphere(transform.position + _sphereCollider.center, _sphereCollider.radius);
        }
    }
}