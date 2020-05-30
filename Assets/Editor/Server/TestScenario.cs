using System;
using System.Reflection;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class TestScenarioAttribute : Attribute
{
    public string Name { get; set; }
}

public class TestScenario
{
}
