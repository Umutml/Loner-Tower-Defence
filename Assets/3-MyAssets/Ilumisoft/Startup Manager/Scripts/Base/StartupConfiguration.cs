using UnityEngine;

namespace Ilumisoft.StartupManager
{
    public class StartupConfiguration : ScriptableObject
    {
        public const string ConfigurationPath = "Ilumisoft/Startup Manager/Startup Configuration";

        [SerializeField]
        private StartupProfile startupProfile = null;

        public StartupProfile StartupProfile
        {
            get => startupProfile;
            set => startupProfile = value;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            var startupConfiguration = Find();

            if (startupConfiguration == null)
            {
                Debug.LogWarning($"Could not find {typeof(StartupConfiguration).Name} asset at Resources/{ConfigurationPath}. Did you delete, rename or move it? Please reimport the asset from the Package Manager.");
            }
            else if (startupConfiguration.StartupProfile != null)
            {
                startupConfiguration.StartupProfile.Initialize();
            }
        }

        public static StartupConfiguration Find()
        {
            var result = Resources.Load<StartupConfiguration>(ConfigurationPath);

            return result;
        }
    }
}