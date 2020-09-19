using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Entities;
using MM26.Components;

namespace MM26.Systems
{
    public class HubToggleSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            // FIXME:
            Camera camera = Camera.main;

            var mouseScreenPosition = Mouse.current.position.ReadValue();
            var mouseWorldPosition = camera.ScreenToWorldPoint(mouseScreenPosition);

            this.Entities
                .WithoutBurst()
                .ForEach((Transform transform, Hub hub) =>
                {
                    var rect = new Rect();
                    rect.height = transform.localScale.y;
                    rect.width = transform.localScale.x;

                    rect.x = transform.position.x - (rect.width / 2);
                    rect.y = transform.position.y - (rect.height / 2);

                    hub.Canvas.enabled = rect.Contains(mouseWorldPosition);
                })
                .Run();
        }
    }
}