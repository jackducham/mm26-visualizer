using System;
using UnityEngine;

namespace MM26.Tests
{
    /// <summary>
    /// Mock game state
    /// </summary>
    [CreateAssetMenu(fileName = "New Test Board", menuName = "Board/Test Board")]
    public class TestGameData : ScriptableObject
    {
        public TestGameState State = null;
        public TestGameChange[] Changes = null;
    }
}
