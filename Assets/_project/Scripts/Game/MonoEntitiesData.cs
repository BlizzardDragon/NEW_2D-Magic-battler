using Entity.Core;

namespace _project.Scripts.Game
{
    public class MonoEntitiesData
    {
        public MonoEntitiesData(MonoEntity player, MonoEntity enemy)
        {
            Player = player;
            Enemy = enemy;
        }

        public MonoEntity Player { get; }
        public MonoEntity Enemy { get; }
    }
}