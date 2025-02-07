using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _project.Scripts.Core
{
    public interface ISceneLoader
    {
        UniTask RestartAsync();
        void LoadGameScene();
    }

    public class SceneLoader : ISceneLoader
    {
        public async UniTask RestartAsync()
        {
            await SceneManager.UnloadSceneAsync(1);
            SceneManager.LoadScene(0);
        }

        public void LoadGameScene()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
    }
}