using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdComponent : MonoBehaviour
{
    private static int _nextId;
    public int id = _nextId++;
}
