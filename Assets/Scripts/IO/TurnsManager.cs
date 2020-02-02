using System;
using UnityEngine;

namespace MM26.IO
{
    public class TurnsManager : MonoBehaviour
    {
        enum DataProviderType
        {
            Web,
            Local,
        }

        [SerializeField]
        DataProviderType _dataProviderType = DataProviderType.Web;

        [SerializeField]
        string _changeSocket = "";

        [SerializeField]
        string _editorChangeSocket = "";

        IDataProvider _dataProvider;

        private void Awake()
        {
            switch (_dataProviderType)
            {
                case DataProviderType.Web:
                    IWebDataProvider dataProvider = DataProvider.CreateWebDataProvider();
                    dataProvider.NewChange += this.OnNewChange;
#if UNITY_EDITOR
                    dataProvider.Connect(new Uri(_editorChangeSocket), this.OnConnection, this.OnFailure);
#else
                    _dataProvider.Connect(_changeSocket, this.OnConnection, this.OnFailure);
#endif
                    _dataProvider = dataProvider;
                    break;
                case DataProviderType.Local:
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

        }

        void OnFailure()
        {
            Debug.LogError("Websocket connection failed");
        }

        void OnNewChange(object sender, VisualizerChange change)
        {

        }
    }
}
