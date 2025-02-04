using _project.Scripts.Core.UI.Abilities;
using Cysharp.Threading.Tasks;
using VampireSquid.Common.CompositeRoot;

namespace _project.Scripts.Core.Compositions
{
    public class PlayerHUDViewportsComposition : CompositionBase
    {
        public override async UniTask InstallBindings()
        {
            BindAsGlobal<IAbilitiesHUDViewport>(FindMono<AbilitiesHUDViewport>(true));
        }
    }
}