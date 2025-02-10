using _project.Scripts.Core.Turn;
using Cysharp.Threading.Tasks;
using VampireSquid.Common.CompositeRoot;

namespace _project.Scripts.Game.Compositions
{
    public class ServerGameTurnPipelineComposition : CompositionBase
    {
        private MonoEntitiesData _entitiesData;
        private TurnPipelineRunner _pipelineRunner;

        public override async UniTask InstallBindings()
        {
            _entitiesData = Get<MonoEntitiesData>();
            var turnPipeline = Get<ITurnPipeline>();

            _pipelineRunner = new TurnPipelineRunner(turnPipeline, _entitiesData.Player, _entitiesData.Enemy);

            BindAsLocal<ITurnPipelineRunner>(_pipelineRunner);
        }

        public override async UniTask Initialize()
        {
            _pipelineRunner.OnEnable();

            await UniTask.WaitUntil(() => _entitiesData.Player.IsInitialized && _entitiesData.Enemy.IsInitialized);

            _pipelineRunner.Run();
        }

        public override void OnBeforeDisposed()
        {
            _pipelineRunner.OnDisable();
        }
    }
}