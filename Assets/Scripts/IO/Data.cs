using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MM26.IO.Models;

namespace MM26.IO
{
    [CreateAssetMenu(menuName = "IO/Data", fileName = "Data")]
    public class Data : Resettable
    {
        public GameState GameState { get; set; }
        public Queue<GameChange> GameChanges { get; private set; } = new Queue<GameChange>();

        public override void Reset()
        {
            this.GameState = null;
            this.GameChanges = new Queue<GameChange>();
        }
    }
}
