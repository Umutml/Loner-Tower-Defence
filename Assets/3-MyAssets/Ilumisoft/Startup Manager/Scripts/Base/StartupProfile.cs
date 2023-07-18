using UnityEngine;

namespace Ilumisoft.StartupManager
{
    public abstract class StartupProfile : ScriptableObject
    {
        public abstract void Initialize();
    }
}