## ESC BASIC

### Create Entity

```
using Unity.Entities;
using Unity.Collections; //NativeArray
EntityManager entityManager = World.Active.EntityManager;
```
Create single entity with component "LevelComponent"
```
Entity entity = entityManager.CreateEntity(typeof(LevelComponent));
```
Create array of entities with component "LevelComponent"
```
NativeArray<Entity> entityArray = new NativeArray<Entity>(10, Allocator.Temp);
entityManager.CreateEntity(typeof(LevelComponent), entityArray);
```
Using entityArchetpye
```
NativeArray<Entity> entityArray = new NativeArray<Entity>(10, Allocator.Temp);
entityManager.CreateEntity(entityArchetype, entityArray);
```

### Managing Entity Components

Inheriting ComponentSystem allows you to control all entities with certain components
```
using Unity.Entities;
public class LevelUpSystem : ComponentSystem {
  provtected override void OnUpdate() {
    Entities.ForEach((ref LevelComponenet levelComponent) => {
      levelComponent.level += 1f * Time.deltaTime;
    });
  }
}
```

## ECS DataTypes

### entityArchetype

```
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;

EntityManager entityManager = World.Active.EntityManager;
EntityArchetype entityArchetype = entityManager.CreatArchetype(
  typeof(levelComponent),
  typeof(Translation),
  typeof(RenderMesh)
);
//creating single entity with entityArchetype of components: LevelComponent, Translation, RenderMesh
Entity entity = entityManager.CreateEntity(entityArchetype);
```
### Usings List for Components

```
using Unity.Transforms; //Translation
using Unity.Rendering;  //RenderMesh
```
