using _project.Scripts.Core;
using Cysharp.Threading.Tasks;

namespace _project.Scripts.Game.UI.RestartButton
{
    public class RestartButtonPresenter
    {
        private readonly ISceneLoader _model;
        private readonly RestartButtonView _view;

        public RestartButtonPresenter(ISceneLoader model, RestartButtonView view)
        {
            _model = model;
            _view = view;
        }

        public void OnEnable()
        {
            _view.ButtonClicked += OnButtonClicked;
        }

        public void OnDisable()
        {
            _view.ButtonClicked -= OnButtonClicked;
        }

        private void OnButtonClicked()
        {
            _model.RestartAsync().Forget();
        }
    }
}