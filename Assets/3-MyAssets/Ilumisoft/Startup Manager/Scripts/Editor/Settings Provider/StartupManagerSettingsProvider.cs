using UnityEditor;
using UnityEngine;

namespace Ilumisoft.StartupManager.Editor
{
    class StartupManagerSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateStartupProfileConfigurationProvider() => CreateProvider("Project/Startup Manager", StartupConfiguration.Find());

        static SettingsProvider CreateProvider(string settingsWindowPath, Object asset)
        {
            var provider = AssetSettingsProvider.CreateProviderFromObject(settingsWindowPath, asset);

            provider.keywords = SettingsProvider.GetSearchKeywordsFromSerializedObject(new SerializedObject(asset));
            return provider;
        }
    }
}