using _project.Scripts.Core.Turn;
using Cysharp.Threading.Tasks;
using Entity.Core;
using UnityEngine;
using VampireSquid.Common.CompositeRoot;

namespace _project.Scripts.Game.Modules
{
    public class GameplayRoot : CompositeRootBase
    {
        [SerializeField] private MonoEntity _player;
        [SerializeField] private MonoEntity _enemy;

        private TurnPipelineRunner _pipelineRunner;

        public override async UniTask InstallBindings()
        {
            var turnPipeline = Get<ITurnPipeline>();

            _pipelineRunner = new TurnPipelineRunner(turnPipeline, _player, _enemy);
        }

        public override async UniTask Initialize()
        {
            _pipelineRunner.OnEnable();

            await UniTask.WaitUntil(() => _player.IsInitialized && _enemy.IsInitialized);

            _pipelineRunner.Run();
        }

        public override void OnBeforeDestroyed()
        {
            _pipelineRunner.OnDisable();
        }
    }
}