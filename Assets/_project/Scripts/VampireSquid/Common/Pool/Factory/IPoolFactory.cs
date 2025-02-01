namespace VampireSquid.Common.Pool.Factory
{
    public interface IPoolFactory<out T>
    {
        T Create();
    }
}