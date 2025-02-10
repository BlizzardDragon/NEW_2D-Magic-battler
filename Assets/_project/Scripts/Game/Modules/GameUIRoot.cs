using _project.Scripts.Core;
using _project.Scripts.Game.Compositions;
using _project.Scripts.Game.UI.GameOver;
using _project.Scripts.Game.UI.RestartButton;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VampireSquid.Common.CompositeRoot;

namespace _project.Scripts.Game.Modules
{
    public class GameUIRoot : CompositeRootBase
    {
        [SerializeField] private GameOverView _gameOverView;
        [SerializeField] private RestartButtonView _restartButtonView;

        public override async UniTask InstallBindings()
        {
            if (NetworkManager.Instance.IsRemote)
            {
                BindAsLocal<GameOverView>(_gameOverView);
                BindAsLocal<RestartButtonView>(_restartButtonView);

                CreateComposition<ClientGameUIComposition>();
            }
        }
    }
}