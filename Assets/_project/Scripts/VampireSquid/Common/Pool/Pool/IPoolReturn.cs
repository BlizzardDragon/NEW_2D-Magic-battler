namespace VampireSquid.Common.Pool.Pool
{
	public interface IPoolReturn<in TItem>
	{
		public void Return(TItem item);
	}
}