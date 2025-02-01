using Entity.Core;
using UnityEngine;

public class HealthRoot : EntityModuleCompositeRootBase
{
    [SerializeField] private float _startHealth = 100;

    public override void Create(IEntity entity)
    {
        throw new System.NotImplementedException();
    }
}