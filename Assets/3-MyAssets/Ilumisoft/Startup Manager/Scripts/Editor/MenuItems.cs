using UnityEditor;

namespace Ilumisoft.StartupManager.Editor
{
    internal class MenuItems
    {
        /// <summary>
        /// Opens the online documentation
        /// </summary>
        [MenuItem("Help/Ilumisoft/Startup Manager")]
        private static void ShowOnlineDocumentation()
        {
            Help.BrowseURL("https://ilumisoft.gitbook.io/documentation/tools/startup-manager");
        }
    }
}