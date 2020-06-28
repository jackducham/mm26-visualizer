using UnityEngine;
using MM26.IO;

namespace MM26.Map.Tests
{
    [CreateAssetMenu(menuName = "Maps/Tests/Test Map Data Provider", fileName = "TestMapDataProvider")]
    public class TestMapDataProvider : DataProvider
    {
        [SerializeField]
        private string _board = "pvp";

        public override void Start()
        {
            base.Start();


            this.CanStart.Invoke();
        }
    }
}
