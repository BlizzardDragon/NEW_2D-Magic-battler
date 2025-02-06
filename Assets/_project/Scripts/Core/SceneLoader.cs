using UnityEngine.SceneManagement;

namespace _project.Scripts.Core
{
    public interface ISceneLoader
    {
        void LoadGameScene();
    }

    public class SceneLoader : ISceneLoader
    {
        public void LoadGameScene()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
    }
}