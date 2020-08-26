using UnityEngine;

namespace MM26
{
    /// <summary>
    /// Describes how the scene can be loaded. Should be configured
    /// after <c>DataFetched</c> event
    /// </summary>
    [CreateAssetMenu(menuName = "Scene Configuration", fileName = "Scene Configuration")]
    public class SceneConfiguration : ScriptableObject
    {
        /// <summary>
        /// Name of the board
        /// </summary>
        public string BoardName;

        /// <summary>
        /// URL to connect websocket to
        /// </summary>
        public string WebSocketURL;
    }
}


