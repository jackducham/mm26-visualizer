using UnityEngine;
using Unity.Entities;
using MM26.Components;

namespace MM26.Systems
{
    /// <summary>
    /// Implements camera zoom
    /// </summary>
    public class CameraZoomSystem : SystemBase
    {
        Configuration.Input _input = null;

        protected override void OnCreate()
        {
            _input = new Configuration.Input();
            _input.Enable();
        }

        protected override void OnDestroy()
        {
            _input.Dispose();
        }

        protected override void OnUpdate()
        {
            this.Entities
                .WithoutBurst()
                .ForEach((Camera camera, CameraSettings cameraSettings) =>
                {
                    float dt = Time.DeltaTime;
                    var input = _input.Camera.Zoom.ReadValue<Vector2>();

                    camera.orthographicSize += input.y * dt * cameraSettings.ZoomSpeed;
                    camera.orthographicSize = Mathf.Clamp(
                        camera.orthographicSize,
                        cameraSettings.MinZoom,
                        cameraSettings.MaxZoom);
                })
                .Run();
        }
    }
}
