using System.Collections.Generic;
using UnityEngine;
using MM26.IO.Models;

namespace MM26.IO
{
    [CreateAssetMenu(menuName = "IO/Data", fileName = "Data")]
    public class Data : ScriptableObject
    {
        public VisualizerInitial Initial { get; set; }
        public Queue<VisualizerTurn> Turns { get; private set; } = new Queue<VisualizerTurn>();

        public void Reset()
        {
            this.Initial = null;
            this.Turns = new Queue<VisualizerTurn>();
        }
    }
}
