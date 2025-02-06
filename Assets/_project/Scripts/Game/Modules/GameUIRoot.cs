using _project.Scripts.Game.Compositions;
using _project.Scripts.Game.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VampireSquid.Common.CompositeRoot;

namespace _project.Scripts.Game.Modules
{
    public class GameUIRoot : CompositeRootBase
    {
        [SerializeField] private GameOverView _gameOverView;

        public override async UniTask InstallBindings()
        {
            BindAsLocal<GameOverView>(_gameOverView);
            
            CreateComposition<GameUIComposition>();
        }
    }
}