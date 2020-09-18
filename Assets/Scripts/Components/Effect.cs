using UnityEngine;

namespace MM26.Components
{
    public class Effect : MonoBehaviour
    {
        public void Remove()
        {
            Destroy(this.gameObject);
        }
    }
}
