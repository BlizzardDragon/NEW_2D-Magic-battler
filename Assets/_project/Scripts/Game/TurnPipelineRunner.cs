using _project.Scripts.Core.Turn;
using _project.Scripts.Core.Turn.Tasks;
using Entity.Core;
using UnityEngine;

namespace _project.Scripts.Game
{
    public interface ITurnPipelineRunner
    {
        void Run();
    }

    public class TurnPipelineRunner : ITurnPipelineRunner
    {
        private readonly ITurnPipeline _turnPipeline;
        private readonly MonoEntity[] _units;

        public TurnPipelineRunner(ITurnPipeline turnPipeline, params MonoEntity[] units)
        {
            _turnPipeline = turnPipeline;
            _units = units;
        }

        public void Run()
        {
            foreach (var unit in _units)
            {
                if (unit.TryGetModule(out Task turnTask))
                {
                    _turnPipeline.AddTask(turnTask);
                }
            }
            
            _turnPipeline.Run();
        }

        public void OnEnable()
        {
            _turnPipeline.Finished += _turnPipeline.Run;
        }

        public void OnDisable()
        {
            _turnPipeline.Finished -= _turnPipeline.Run;
        }
    }
}