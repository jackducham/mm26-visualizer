using UnityEngine;
using MM26.Board;

namespace MM26.Play.Tests
{
    public class MockPositionLookUp : BoardPositionLookUp
    {
        public override Vector3 Translate(Vector3Int position)
        {
            return new Vector3(position.x, position.y, position.z);
        }
    }
}