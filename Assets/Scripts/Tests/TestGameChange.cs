using System;
using MM26.IO.Models;

namespace MM26.Tests
{
    [Serializable]
    public struct TestPosition
    {
        public int X;
        public int Y;
        public string BoardID;
    }

    [Serializable]
    public struct TestCharacterChange
    {
        public string Entity;
        public bool Died;
        public bool Respawned;
        public DecisionType DecisionType;
        public TestPosition[] Path;
    }

    [Serializable]
    public class TestGameChange
    {
        public TestCharacterChange[] CharacterChanges = null;
    }
}