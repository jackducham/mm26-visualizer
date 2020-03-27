using UnityEngine;
using Unity.Entities;
using System;
using System.Collections.Generic;

abstract class TaskSystem<T> : ComponentSystem where T : Task
{
    
    protected EntityQuery query = null;
    
    protected TaskSystem() {
        
    }
    
    protected bool ShouldFinish() {
        return true;
    }
    
    protected override void OnUpdate() 
    {
        // Loop through all the entities with ids.
        // Check to see if that id has an associated task
    }
}