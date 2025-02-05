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

            if (_started) return;
            if (_tasks.Count < 2) return;

            _started = true;
            Run();
        }

        public void Run()
        {
            _currentIndex = 0;
            RunNextTask();
        }

        private void RunNextTask()
        {
            if (_currentIndex >= _tasks.Count)
            {
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