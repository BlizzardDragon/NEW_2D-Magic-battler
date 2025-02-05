using System;

namespace _project.Scripts.Core.Turn.Tasks
{
    public abstract class Task
    {
        private Action<Task> _callback;

        public void Run(Action<Task> callback)
        {
            _callback = callback;
            OnRun();
        }

        protected virtual void Finish()
        {
            if (_callback is not null)
            {
                var callback = _callback;
                _callback = null;

                callback?.Invoke(this);
            }

            OnFinish();
        }

        protected abstract void OnRun();

        protected virtual void OnFinish()
        {
        }
    }
}