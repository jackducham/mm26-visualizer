﻿using System;

namespace MM26.Board.Tests
{
    /// <summary>
    /// Mock player
    /// </summary>
    [Serializable]
    public class TestPlayer
    {
        public string Name;
        public string Board = "pvp";
        public int X;
        public int Y;
    }
}
