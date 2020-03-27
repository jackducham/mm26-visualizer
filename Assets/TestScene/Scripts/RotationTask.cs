using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTask : Task
{
    public float rotationAmount;
    public Vector3 rotationAxis;
    
    public RotationTask(int id, float rotationAmt, Vector3 axis) : base(id) {
        rotationAmount = rotationAmt;
        rotationAxis = axis;
    }
    
    public override string GetTaskType() {
        return "Rotation";
    }
    
    public static string TYPE_STRING = "Rotation";
}
