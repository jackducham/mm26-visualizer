using UnityEngine;

namespace MM26
{
    [CreateAssetMenu(menuName = "Services/Reset", fileName = "Reset")]
    public class Resets : Resettable
    {
        [SerializeField]
        Resettable[] _resets = null;

        public override void Reset()
        {
            for (int i = 0; i < _resets.Length; i++)
            {
                _resets[i].Reset();
            }
        }
    }
}