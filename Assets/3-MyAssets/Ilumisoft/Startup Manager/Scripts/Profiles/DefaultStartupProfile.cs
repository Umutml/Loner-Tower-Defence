using System.Collections.Generic;
using UnityEngine;

namespace Ilumisoft.StartupManager
{
    /// <summary>
    /// Default implementation of the Startup Profile Asset.
    /// The startup profile is executed automatically at startup and creates all the persistent systems of the game.
    /// You can create your own custom startup profiles and set them in the project settings.
    /// </summary>
    [CreateAssetMenu(menuName = "Startup Manager/Startup Profile", fileName = "Startup Profile")]
    public class DefaultStartupProfile : StartupProfile
    {
        [Tooltip("All prefabs in this list will automatically be instantiated on start up and marked as DontDestroyOnLoad")]
        public List<GameObject> Prefabs;

        public override void Initialize()
        {
            InitializePersistentObjects();
        }

        void InitializePersistentObjects()
        {
            foreach (var prefab in Prefabs)
            {
                var instance = Instantiate(prefab);

                instance.name = prefab.name;

                DontDestroyOnLoad(instance);
            }
        }
    }
}