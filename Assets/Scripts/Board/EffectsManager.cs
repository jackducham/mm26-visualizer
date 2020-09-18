using UnityEngine;

namespace MM26.Board
{
    /// <summary>
    /// Manages effects used by board updator
    /// </summary>
    public class EffectsManager : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField]
        private GameObject _deathEffect = null;

        [SerializeField]
        private GameObject _spawnEffect = null;

        [SerializeField]
        private GameObject _portalEffect = null;
    }
}