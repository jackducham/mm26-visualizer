using System;
using System.Collections.Generic;
using Google.Protobuf;
using MM26.IO.Models;

namespace MM26.IO
{
    internal class DataProvider : IDataProvider
    {
        public long LatestChangeNumber { get; protected set; }
        protected Dictionary<long, GameChange> Changes { get; set; } = new Dictionary<long, GameChange>();
        protected Dictionary<long, GameState> Turns { get; set; } = new Dictionary<long, GameState>();

        protected MessageParser<GameChange> ChangeParser { get; set; }
        protected MessageParser<GameState> TurnParser { get; set; }

        internal static IWebDataProvider CreateWebDataProvider()
        {
#if UNITY_STANDALONE
            return new StandAloneWebDataProvider();
#elif UNITY_WEBGL
            return new WebGLDataProvider();
#endif
        }

        internal static IDataProvider CreateFileSystemDataProvider()
        {
            return new StandAloneFileSystemDataProvider();
        }

        internal DataProvider()
        {
            this.ChangeParser = GameChange.Parser;
            this.TurnParser = GameState.Parser;
        }

        public virtual void Dispose() { }

        public virtual void GetChange(long change, Action<GameChange> callback)
        {
            callback(this.Changes[change]);
        }

        public virtual void GetTurn(long turn, Action<GameState> callback)
        {
            callback(this.Turns[turn]);
        }
    }
}
