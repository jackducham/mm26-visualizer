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
        private enum State
        {
            Follow,
            Control
        }


        [SerializeField]
        private Canvas _canvas = null;

        [SerializeField]
        private string _introScene = "Intro";

        private Configuration.Input _input = null;

        [SerializeField]
        private TextMeshProUGUI _followField = null;

        private Entity _entity = default;
        private World _world = null;

        private State _state = State.Control;
        private string _target = "";

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
                switch (_state)
                {
                    case State.Follow:
                        ecb.RemoveComponent<FollowTransform>(_entity);
                        break;
                    case State.Control:
                        ecb.RemoveComponent<CameraControl>(_entity);
                        break;
                }
            }
            else
            {
                switch (_state)
                {
                    case State.Control:
                        ecb.AddComponent<CameraControl>(_entity);
                        break;
                    case State.Follow:
                        ecb.AddComponent(_entity, this.MakeFollowTransform(_target));
                        break;
                }
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
            ecb.AddComponent(_entity, this.MakeFollowTransform(name));

            _target = name;
            _state = State.Follow;
        }

        public void OnReleaseCamera()
        {
            var ecb = _world
                .GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>()
                .CreateCommandBuffer();

            ecb.AddComponent<CameraControl>(_entity);
            ecb.RemoveComponent<FollowTransform>(_entity);

            _state = State.Control;
            _target = "";
        }

        private FollowTransform MakeFollowTransform(string name)
        {
            GameObject follow = GameObject.Find(name);

            return new FollowTransform()
            {
                Target = follow.transform,
                Offset = this.transform.position - follow.transform.position
            };
        }
    }
}
