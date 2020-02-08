using System;

namespace MM26.IO
{
    /// <summary>
    /// Basic definition of a data provider
    /// </summary>
    internal interface IDataProvider : IDisposable
    {
        /// <summary>
        /// Get a change
        /// </summary>
        /// <param name="change">the change number</param>
        /// <param name="callback">
        /// the call back would be invoked when the change is available
        /// </param>
        /// <returns>a change if there is one, null otherwise</returns>
        void GetChange(long change, Action<VisualizerChange> callback);

        /// <summary>
        /// Get a turn
        /// </summary>
        /// <param name="turn">the turn number</param>
        /// <param name="callback">
        /// the call back would be invoked when the turn is available
        /// </param>
        /// <returns>a turn if there is one, null otherwise</returns>
        void GetTurn(long change, Action<VisualizerTurn> callback);
    }

    /// <summary>
    /// A data provider that listens and fetches its data from the network
    /// </summary>
    internal interface IWebDataProvider : IDataProvider
    {
        /// <summary>
        /// The newest change number observed from the network
        /// </summary>
        long LatestChangeNumber { get; }

        NetworkEndpoints Endpoints { get; }

        /// <summary>
        /// This event is fired when a new change is available over the network
        /// </summary>
        event EventHandler<VisualizerChange> NewChange;

        /// <summary>
        /// Use network endpoints
        /// </summary>
        /// <param name="endpoints">the endpoints</param>
        /// <param name="onConnection">Called after connection</param>
        /// <param name="onFailure">Called if failed</param>
        void UseEndpoints(NetworkEndpoints endpoints, Action onConnection, Action onFailure);

        /// <summary>
        /// Process new change data in bytes
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="bytes">bytes representing the change</param>
        void AddChange(byte[] bytes);
    }
}
