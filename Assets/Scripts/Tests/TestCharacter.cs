using System;

namespace MM26.Tests
{
    public enum CharacterType
    {
        Player,
        Monster
    }

    /// <summary>
    /// Mock player
    /// </summary>
    [Serializable]
    public class TestCharacter
    {
        public string Name;
        public CharacterType CharacterType = CharacterType.Player;
        public string SpritePath;

        public string Board = "pvp";
        public int X;
        public int Y;
    }
}
