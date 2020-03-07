using System;
using UnityEngine;

namespace MM26.IO
{
    /// <summary>
    /// Network endpoints
    /// </summary>
    [Serializable]
    public struct NetworkEndpoints
    {
        /// <summary>
        /// A websocket path at where change is observed
        /// </summary>
        [Tooltip("A websocket path at where change is observed")]
        public string ChangeSocket;

        /// <summary>
        /// A http path at which turns are fetched
        /// </summary>
        [Tooltip("A http path at which turns are fetched")]
        public string StateHttp;

        /// <summary>
        /// A http path at which the change is observed
        /// </summary>
        [Tooltip("A http path at which the change is observed")]
        public string ChangeHttp;
    }
}