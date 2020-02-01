using System;
using UnityEngine;

namespace MM26.IO
{
    public class TurnsManager : MonoBehaviour
    {
        [SerializeField]
        string _changeSocket;

        [SerializeField]
        string _editorChangeSocket;

        DataProvider _dataProvider;

        private void Awake()
        {
#if UNITY_EDITOR
            _dataProvider = new DataProvider(_editorChangeSocket);
#else
            _dataProvider = new DataProvider(_changeSocket);
#endif
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
    }
}
