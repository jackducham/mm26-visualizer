﻿using UnityEngine;

namespace MM26
{
    /// <summary>
    /// Drives various stages of a life cycle object
    /// </summary>
    public class SceneDriver : MonoBehaviour
    {
        [SerializeField]
        SceneLifeCycle _sceneLifeCycle = null;

        [Header("Stages")]
        [SerializeField]
        bool _play = true;

        private void OnEnable()
        {
            _sceneLifeCycle.BoardCreated.AddListener(this.OnBoardCreated);
            _sceneLifeCycle.DataFetched.AddListener(this.OnDataFetched);
        }

        private void OnDisable()
        {
            _sceneLifeCycle.BoardCreated.RemoveListener(this.OnBoardCreated);
            _sceneLifeCycle.DataFetched.RemoveListener(this.OnDataFetched);
            _sceneLifeCycle.Reset.Invoke();
        }

        private void Start()
        {
            _sceneLifeCycle.FetchData.Invoke();
        }

        private void Update()
        {
            _sceneLifeCycle.Update.Invoke();
        }

        private void OnDataFetched()
        {
            _sceneLifeCycle.CreateBoard.Invoke();
        }

        private void OnBoardCreated()
        {
            if (_play)
            {
                _sceneLifeCycle.Play.Invoke();
            }
        }
    }
}
