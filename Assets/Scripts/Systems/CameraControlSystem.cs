using UnityEngine;
using Unity.Entities;
using MM26.Components;

namespace MM26.Systems
{
    /// <summary>
    /// Implement user controlling the camera
    /// </summary>
    public class CameraControlSystem : SystemBase
    {
        Configuration.Input _input;

        protected override void OnCreate()
        {
            base.OnCreate();

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
                .ForEach((Transform transform, CameraSettings cameraSetting, CameraControl control) =>
                {
                    float deltaTime = Time.DeltaTime;
                    var input = _input.Camera.Movement.ReadValue<Vector2>();

                    Vector3 translation = new Vector3(input.x, input.y);
                    translation *= deltaTime;

                    translation.x *= cameraSetting.MoveSpeed.x;
                    translation.y *= cameraSetting.MoveSpeed.y;
                    translation.z *= cameraSetting.MoveSpeed.z;

                    transform.Translate(translation);
                })
                .Run();
        }
    }
}
