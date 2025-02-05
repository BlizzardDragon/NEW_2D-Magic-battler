using _project.Scripts.Core.Compositions;
using Cysharp.Threading.Tasks;
using VampireSquid.Common.CompositeRoot;

namespace _project.Scripts.Core.Modules
{
    public class PlayerBootstrapRoot : CompositeRootBase
    {
        public override async UniTask InstallBindings()
        {
            CreateComposition<PlayerHUDViewportsComposition>();
        }
    }
}