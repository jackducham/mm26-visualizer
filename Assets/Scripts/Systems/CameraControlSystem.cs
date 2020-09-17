using UnityEngine;
using Unity.Entities;
using MM26.Components;
using MM26.Configuration;

namespace MM26.Systems
{
    public class CameraControlSystem : SystemBase
    {
        Configuration.Input _input;

        protected override void OnCreate()
        {
            base.OnCreate();

            _input = new Configuration.Input();
            _input.Enable();
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

                    translation.x *= cameraSetting.Speed.x;
                    translation.y *= cameraSetting.Speed.y;
                    translation.z *= cameraSetting.Speed.z;

                    transform.Translate(translation);
                })
                .Run();
        }
    }
}
