namespace Entity.Core
{
    public interface IEntityModuleInitializable
    {
        void Initialize(IEntity entity);
    }
}