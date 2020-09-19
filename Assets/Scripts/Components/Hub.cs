using UnityEngine;
using TMPro;

namespace MM26.Components
{
    /// <summary>
    /// Helps controls the hub of a player. Should be attached to the
    /// root of player prefab
    /// </summary>
    public class Hub : MonoBehaviour
    {
        public TextMeshProUGUI NameLabel = null;
        public TextMeshProUGUI HealthLabel = null;
        public TextMeshProUGUI LevelLabel = null;
        public TextMeshProUGUI ExperienceLabel = null;
    }
}
