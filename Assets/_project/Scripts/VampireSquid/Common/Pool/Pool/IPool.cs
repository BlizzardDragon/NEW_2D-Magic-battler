namespace VampireSquid.Common.Pool.Pool
{
    public interface IPool<TItem> : IPoolReturn<TItem>
    {
        TItem Get();
        public void ReturnAll();
    }
}