using UnityEngine;

namespace _project.Scripts.Core
{
    public class NetworkManager : MonoBehaviour
    {
        public static NetworkManager Instance { get; private set; }

        [field: SerializeField] public bool OnServer { get; private set; }
        [field: SerializeField] public bool IsRemote { get; private set; }

        public bool IsHost => OnServer && IsRemote;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}