using UnityEditor;
using UnityEngine;
using Unity.Entities;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using MM26.Components;
using MM26.Utilities;

namespace MM26.UI
{
    public class PauseMenu : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField]
        private Canvas _canvas = null;

        [SerializeField]
        private string _introScene = "Intro";

        private Configuration.Input _input = null;

        [SerializeField]
        private TextMeshProUGUI _followField = null;

        private Entity _entity = default;
        private World _world = null;

        public void Convert(
            Entity entity,
            EntityManager dstManager,
            GameObjectConversionSystem conversionSystem)
        {
            _entity = entity;
            _world = dstManager.World;
        }

        private void OnEnable()
        {
            _input = new Configuration.Input();
            _input.Enable();

            _input.Camera.Pause.performed += this.OnPause;
        }

        private void OnDisable()
        {
            _input.Dispose();
        }

        private void Start()
        {
            _canvas.enabled = false;
        }

        private void OnPause(InputAction.CallbackContext context)
        {
            _canvas.enabled = !_canvas.enabled;

            var ecb = _world
                .GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>()
                .CreateCommandBuffer();

            if (_canvas.enabled)
            {
                ecb.RemoveComponent<CameraControl>(_entity);
            }
            else
            {
                ecb.AddComponent<CameraControl>(_entity);
            }
            
        }

        public void OnExitBoardClick()
        {
            SceneManager.LoadScene(_introScene);
        }

        public void OnExitVisualizerClick()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void OnFollow()
        {
            var ecb = _world
                .GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>()
                .CreateCommandBuffer();

            string name = _followField.text.KeepVisibles();
            GameObject follow = GameObject.Find(name);

            if (follow == null)
            {
                // TODO: add error in UI
                Debug.LogErrorFormat(
                    "{0} (length = {1}) not found!",
                    name,
                    name.Length);

                return;
            }

            ecb.RemoveComponent<CameraControl>(_entity);
            ecb.AddComponent(_entity, new FollowTransform()
            {
                Target = follow.transform
            });
        }

        public void OnReleaseCamera()
        {
            var ecb = _world
                .GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>()
                .CreateCommandBuffer();

            ecb.AddComponent<CameraControl>(_entity);
            ecb.RemoveComponent<FollowTransform>(_entity);
        }
    }
}
