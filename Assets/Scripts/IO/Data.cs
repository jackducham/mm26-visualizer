using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MM26.IO.Models;

namespace MM26.IO
{
    [CreateAssetMenu(menuName = "IO/Data", fileName = "Data")]
    public class Data : ScriptableObject
    {
        public GameState GameState { get; set; }
        public List<GameChange> GameChanges { get; private set; } = new List<GameChange>();

        public void Reset()
        {
            this.GameState = null;
            this.GameChanges = new List<GameChange>();
        }
    }
}
