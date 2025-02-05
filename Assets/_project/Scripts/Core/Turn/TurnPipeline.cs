using System;
using System.Collections.Generic;
using _project.Scripts.Core.Turn.Tasks;

namespace _project.Scripts.Core.Turn
{
    public interface ITurnPipeline
    {
        event Action Finished;

        void AddTask(Task task);
        void Run();
    }

    public class TurnPipeline : ITurnPipeline
    {
        private readonly List<Task> _tasks = new();

        private int _currentIndex = -1;
        private bool _started;

        public event Action Finished;

        public void AddTask(Task task)
        {
            _tasks.Add(task);

            if (_tasks.Count < 2) return;

            Run();
        }

        public void Run()
        {
            if (_started) return;

            _started = true;
            _currentIndex = 0;

            RunNextTask();
        }

        private void RunNextTask()
        {
            if (_currentIndex >= _tasks.Count)
            {
                _started = false;
                Finished?.Invoke();
                return;
            }

            var task = _tasks[_currentIndex];
            task.Run(OnTaskFinished);
        }

        private void OnTaskFinished(Task task)
        {
            _currentIndex++;
            RunNextTask();
        }
    }
}