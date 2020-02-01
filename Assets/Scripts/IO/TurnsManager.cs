using System;
using UnityEngine;

namespace MM26.IO
{
    public class TurnsManager : MonoBehaviour
    {
        [SerializeField]
        string _changeSocket = "";

        [SerializeField]
        string _editorChangeSocket = "";

        DataProvider _dataProvider;

        private void Awake()
        {
            _dataProvider = new DataProvider();
            _dataProvider.NewChange += OnNewChange;

#if UNITY_EDITOR
            _dataProvider.Connect(_editorChangeSocket, this.OnConnection, this.OnFailure);
#else
            _dataProvider.Connect(_changeSocket, this.OnConnection, this.OnFailure);
#endif
            
        }

        private void OnDestroy()
        {
            _dataProvider.Dispose();
        }

        /// <summary>
        /// Receive new change data.
        ///
        /// This method is intended to be called by the JavaScript plugin
        /// </summary>
        /// <param name="bytes"></param>
        public void TakeChangeData(byte[] bytes)
        {
            _dataProvider.OnChangeData(this, bytes);
        }

        void OnConnection()
        {

        }

        void OnFailure()
        {
            Debug.LogError("Websocket connection failed");
        }

        void OnNewChange(object sender, EventArgs e)
        {

        }
    }
}
