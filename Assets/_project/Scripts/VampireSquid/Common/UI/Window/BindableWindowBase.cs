namespace VampireSquid.Common.UI.Window
{
    public abstract class BindableWindowBase<TBindableModel> : WindowBase
    {
        public void Bind(TBindableModel bindableModel)
        {
            if (bindableModel == null)
                throw new System.NullReferenceException(nameof(bindableModel));

            OnBind(bindableModel);
        }

        protected abstract void OnBind(TBindableModel bindableModel);
    }
}