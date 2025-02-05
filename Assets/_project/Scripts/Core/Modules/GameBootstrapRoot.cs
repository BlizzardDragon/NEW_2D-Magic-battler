using _project.Scripts.Core.Turn;
using Cysharp.Threading.Tasks;
using VampireSquid.Common.CompositeRoot;

namespace _project.Scripts.Core.Modules
{
    public class GameBootstrapRoot : CompositeRootBase
    {
        public override async UniTask InstallBindings()
        {
            var turnPipeline = new TurnPipeline();

            BindAsGlobal<ITurnPipeline>(turnPipeline);
        }
    }
}