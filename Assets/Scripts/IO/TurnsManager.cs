using System;
using UnityEngine;
using MM26.IO.Models;

namespace MM26.IO
{
    public class TurnsManager : MonoBehaviour
    {
        enum DataSource
        {
            Web,
            Local,
        }

        enum Mode
        {
            Listen,
            Replay
        }

        public NetworkEndpoints Endpoints
        {
            get
            {
#if UNITY_EDITOR
                return _debugEndpoints;
#else
                return _endpoints;
#endif
            }
        }

        [Header("Settings")]
        [SerializeField]
        DataSource _dataSource = DataSource.Web;

        [SerializeField]
        Mode _mode = Mode.Listen;

        [SerializeField]
        NetworkEndpoints _endpoints;

        [SerializeField]
        NetworkEndpoints _debugEndpoints;

        IDataProvider _dataProvider;

        private void Awake()
        {
            switch (_dataSource)
            {
                case DataSource.Web:
                    IWebDataProvider dataProvider = DataProvider.CreateWebDataProvider();
                    dataProvider.NewChange += this.OnNewChange;
                    dataProvider.UseEndpoints(this.Endpoints, this.OnConnection, this.OnFailure);

                    _dataProvider = dataProvider;
                    break;
                case DataSource.Local:
                    _dataProvider = DataProvider.CreateFileSystemDataProvider();
                    break;
            }
        }

        private void OnDestroy()
        {
            _dataProvider.Dispose();
        }

        void OnConnection()
        {
            Debug.Log("On connection");
        }

        void OnFailure()
        {
            Debug.LogError("Websocket connection failed");
        }

        void OnNewChange(object sender, GameChange change)
        {
            Debug.Log("New change received");
        }
    }
}
