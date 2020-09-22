using System;
using UnityEngine;
using Unity.Entities;

public class FollowTransform : IComponentData, IEquatable<FollowTransform>
{
    public Transform Target;

    public override int GetHashCode()
    {
        return this.Target.GetHashCode();
    }

    public bool Equals(FollowTransform other)
    {
        return this.Target.Equals(other.Target);
    }
}
