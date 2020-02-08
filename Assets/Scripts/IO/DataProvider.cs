using System;
using System.Collections.Generic;
using Google.Protobuf;

namespace MM26.IO
{
    internal class DataProvider : IDataProvider
    {
        public long LatestChangeNumber { get; protected set; }
        protected Dictionary<long, VisualizerChange> Changes { get; set; } = new Dictionary<long, VisualizerChange>();
        protected Dictionary<long, VisualizerTurn> Turns { get; set; } = new Dictionary<long, VisualizerTurn>();

        protected MessageParser<VisualizerChange> ChangeParser { get; set; }
        protected MessageParser<VisualizerTurn> TurnParser { get; set; }

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
            this.ChangeParser = VisualizerChange.Parser;
            this.TurnParser = VisualizerTurn.Parser;
        }

        public virtual void Dispose() { }

        public virtual void GetChange(long change, Action<VisualizerChange> callback)
        {
            callback(this.Changes[change]);
        }

        public virtual void GetTurn(long turn, Action<VisualizerTurn> callback)
        {
            callback(this.Turns[turn]);
        }
    }
}
