using _project.Scripts.Entities.Health;

namespace _project.Scripts.Entities.Unit.UI.HealthBar
{
    public class HealthBarViewPresenter
    {
        private readonly IHealth _model;
        private readonly HealthBarView _view;

        public HealthBarViewPresenter(IHealth model, HealthBarView view)
        {
            _model = model;
            _view = view;
        }

        public void OnEnable()
        {
            UpdateHealthBar();
            _model.HealthChanged += UpdateHealthBar;
        }

        public void OnDisable()
        {
            _model.HealthChanged -= UpdateHealthBar;
        }

        private void UpdateHealthBar()
        {
            _view.RenderHealth(_model.CurrentHealth.ToString());
            _view.RenderFillAmount((float) _model.CurrentHealth / _model.MaxHealth);
        }
    }
}