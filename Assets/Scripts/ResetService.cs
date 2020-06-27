using UnityEngine;

namespace MM26
{
    [CreateAssetMenu(menuName = "Services/Reset", fileName = "Reset")]
    public class ResetService : StatefulService
    {
        [SerializeField]
        StatefulService[] _resets = null;

        public override void Reset()
        {
            for (int i = 0; i < _resets.Length; i++)
            {
                _resets[i].Reset();
            }
        }
    }
}