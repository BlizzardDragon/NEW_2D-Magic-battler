namespace _project.Scripts.Entities.Unit.Network.Health
{
    public readonly struct HealthData
    {
        public HealthData(int currentHealth, int maxHealth)
        {
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
        }

        public int CurrentHealth { get; }
        public int MaxHealth { get; }
    }
}