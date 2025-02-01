namespace VampireSquid.Common.CompositeRoot
{
    public interface ILateUpdateLoop
    {
        void OnLateUpdate(float deltaTime);
    }
}