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

        public Canvas Canvas = null;

        public string Name
        {
            set => this.NameLabel.text = value;
        }

        public int Health
        {
            set => this.HealthLabel.text = $"Health: {value}";
        }

        public int Level
        {
            set => this.LevelLabel.text = $"Level: {value}";
        }

        public int Experience
        {
            set => this.ExperienceLabel.text = $"Experience: {value}";
        }
    }
}
