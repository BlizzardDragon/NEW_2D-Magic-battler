using _project.Scripts.Entities.Health;
using Entity.Core;
using UnityEngine;

public class HealthRoot : EntityModuleCompositeRootBase
{
    [SerializeField] private int _startHealth = 100;

    public override void Create(IEntity entity)
    {
        var entityHealth = new EntityHealth(_startHealth);

        entity.AddModule<IHealth>(entityHealth);
    }
}