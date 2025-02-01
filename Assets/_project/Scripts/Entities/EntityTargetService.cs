namespace _project.Scripts.Entities
{
    public interface IEntityTargetService
    {
        IEntity Target { get; }

        void SetTarget(IEntity target);
    }

    public class EntityTargetService : IEntityTargetService
    {
        public IEntity Target { get; private set; }

        public void SetTarget(IEntity target)
        {
            Target = target;
        }
    }
}