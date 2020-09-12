using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MM26.IO.Models;

namespace MM26.IO
{
    public struct Turn
    {
        public readonly GameState State;
        public readonly GameChange Change;

        public Turn(GameState state, GameChange change)
        {
            this.State = state;
            this.Change = change;
        }
    }

    [CreateAssetMenu(menuName = "IO/Data", fileName = "Data")]
    public class Data : ScriptableObject
    {
        public GameState InitialState { get; set; }
        public Queue<Turn> Turns { get; private set; } = new Queue<Turn>();

        public void Reset()
        {
            this.InitialState = null;
            this.Turns = new Queue<Turn>();
        }
    }
}
