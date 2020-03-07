using System;
using System.Collections.Generic;
using Google.Protobuf;
using MM26.IO.Models;

namespace MM26.IO
{
    /// <summary>
    /// Base class of all data providers
    /// </summary>
    internal class DataProvider : IDataProvider
    {
        /// <summary>
        /// Lastest change number
        /// </summary>
        /// <value>some change number</value>
        public long LatestChangeId { get; protected set; }

        /// <summary>
        /// A record of all the changes up to this point
        /// </summary>
        /// <returns>A reference to the changes</returns>
        protected Dictionary<long, GameChange> Changes { get; set; } = new Dictionary<long, GameChange>();

        /// <summary>
        /// A record of all the states up to this point
        /// </summary>
        /// <returns>A reference to the states</returns>
        protected Dictionary<long, GameState> States { get; set; } = new Dictionary<long, GameState>();

        /// <summary>
        /// Parser for changes
        /// </summary>
        /// <value>an instance of change parser</value>
        protected MessageParser<GameChange> ChangeParser { get; set; }

        /// <summary>
        /// Parser for states
        /// </summary>
        /// <value>an instance of state parser</value>
        protected MessageParser<GameState> StateParser { get; set; }

        /// <summary>
        /// Create a web-based data provider
        ///
        /// - On standalone platforms, this returns an instance of
        ///   StandAloneWebDataProvider
        /// - On WebGL platform, this returns an instance of WebDataProvider
        /// </summary>
        /// <returns>
        /// A type that implement IWebDataProvider
        /// </returns>
        internal static IWebDataProvider CreateWebDataProvider()
        {
#if UNITY_STANDALONE
            return new StandAloneWebDataProvider();
#elif UNITY_WEBGL
            return new WebGLDataProvider();
#endif
        }

        /// <summary>
        /// Create a file system data provider. Always returns an instance of
        /// StandAloneFileSystemDataProvider
        /// </summary>
        /// <returns>
        /// A type that implement IDataProvider
        /// </returns>
        internal static IDataProvider CreateFileSystemDataProvider()
        {
            return new StandAloneFileSystemDataProvider();
        }

        /// <summary>
        /// Create an empty data provider
        /// </summary>
        internal DataProvider()
        {
            this.ChangeParser = GameChange.Parser;
            this.StateParser = GameState.Parser;
        }

        /// <summary>
        /// Does nothing, but can be overriden by children
        /// </summary>
        public virtual void Dispose() { }

        /// <summary>
        /// Get the change with an identifier
        /// </summary>
        /// <param name="change">the change identifier</param>
        /// <param name="callback">
        /// once the change is available, the call back would be fired
        /// </param>
        public virtual void GetChange(long change, Action<GameChange> callback)
        {
            callback(this.Changes[change]);
        }

        /// <summary>
        /// Get the change with an identifier
        /// </summary>
        /// <param name="state">the state identifier</param>
        /// <param name="callback">
        /// once the state is available, the call back would be fired
        /// </param>
        public virtual void GetState(long state, Action<GameState> callback)
        {
            callback(this.States[state]);
        }
    }
}
