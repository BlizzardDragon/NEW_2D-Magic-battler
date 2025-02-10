using _project.Scripts.Core;
using _project.Scripts.Game.Compositions;
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

        public override async UniTask InstallBindings()
        {
            BindAsLocal<MonoEntitiesData>(new MonoEntitiesData(_player, _enemy));

            if (NetworkManager.Instance.OnServer)
            {
                CreateComposition<ServerGameTurnPipelineComposition>();
                CreateComposition<ServerGameRestartComposition>();
            }
        }
    }
}