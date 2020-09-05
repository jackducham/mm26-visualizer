using System;
using UnityEngine;

namespace MM26.Tests
{
    /// <summary>
    /// Mock game state
    /// </summary>
    [CreateAssetMenu(fileName = "Data", menuName = "Tests/Test Data")]
    public class TestGameData : ScriptableObject
    {
        public TestGameState State = null;
        public TestGameChange[] Changes = null;
    }
}
