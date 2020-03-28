using UnityEngine;
using Unity.Entities;
using MM26.Components;

namespace MM26.Systems
{
    public class MovementSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.deltaTime;

            this.Entities.ForEach((IdComponent id, Transform transform, MovementComponent movement) =>
            {
                transform.Translate(new Vector3(movement.Speed * deltaTime, 0.0f, 0.0f));
            });
        }
    }
}