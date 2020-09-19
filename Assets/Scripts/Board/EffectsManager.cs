using UnityEngine;
using UnityEngine.Tilemaps;

namespace MM26.Board
{
    /// <summary>
    /// Manages effects used by board updator
    /// </summary>
    public class EffectsManager : MonoBehaviour
    {
        [SerializeField]
        private Tilemap _tilemap = null;
        
        [Header("Prefabs")]
        [SerializeField]
        private GameObject _deathEffect = null;

        [SerializeField]
        private GameObject _spawnEffect = null;

        [SerializeField]
        private GameObject _portalEffect = null;

        [SerializeField]
        private GameObject _attackEffect = null;

        public void CreateDeathEffect(Vector3Int position)
        {
            this.CreateEffect(_deathEffect, position);
        }

        public void CreateSpawnEffect(Vector3Int position)
        {
            this.CreateEffect(_spawnEffect, position);
        }

        public void CreatePortalEffect(Vector3Int position)
        {
            this.CreateEffect(_portalEffect, position);
        }

        public void CreateAttackEffect(Vector3Int position)
        {
            this.CreateEffect(_attackEffect, position);
        }

        private void CreateEffect(GameObject prefab, Vector3Int position)
        {
            GameObject effect = Instantiate(prefab);
            effect.transform.position = _tilemap.GetCellCenterWorld(position);
        }
    }
}