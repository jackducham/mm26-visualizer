using UnityEngine;
using Unity.Entities;

namespace MM26.Components
{
    /// <summary>
    /// When the game object is destroyed, also destroys the entity (ECS)
    /// associated with the game object
    /// </summary>
    public class EntityCleanup : MonoBehaviour, IConvertGameObjectToEntity
    {
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

        private void OnDestroy()
        {
            if (_world.EntityManager.Exists(_entity))
            {
                _world.EntityManager.DestroyEntity(_entity);
            }
        }
    }
}
