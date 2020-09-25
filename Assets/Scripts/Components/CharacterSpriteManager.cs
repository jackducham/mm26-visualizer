using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MM26.Components
{
    public class CharacterSpriteManager : MonoBehaviour
    {
        [Header("Equipment Slots")]
        [SerializeField]
        private SpriteRenderer _bottom;
        [SerializeField]
        private SpriteRenderer _top;
        [SerializeField]
        private SpriteRenderer _head;
        [SerializeField]
        private SpriteRenderer _accessory;
        [SerializeField]
        private SpriteRenderer _weapon;


        /// <summary>
        /// Sets the sprite of an equipment, and returns the previous sprite in that slot
        /// </summary>
        /// <param name="sprite">new sprite</param>
        /// <param name="slot">the slot of the item to be removed</param>
        /// <returns>the previous item in specified slot</returns>
        public Sprite SetEquipment(Sprite sprite, string slot)
        {
            Sprite pop = null;
            switch (slot)
            {
                case "shoes":
                    pop = _bottom.sprite;
                    _bottom.sprite = sprite;
                    break;
                case "clothes":
                    pop = _top.sprite;
                    _top.sprite = sprite;
                    break;
                case "hats":
                    pop = _head.sprite;
                    _head.sprite = sprite;
                    break;
                case "accessories":
                    pop = _accessory.sprite;
                    _accessory.sprite = sprite;
                    break;
                case "weapons":
                    pop = _weapon.sprite;
                    _weapon.sprite = sprite;
                    break;
                default:
                    Debug.LogError("Non-Classified equipment!");
                    break;
            }
            return pop;
        }

        /// <summary>
        /// Removes an item from equipment and returns the sprite to that item
        /// </summary>
        /// <param name="slot">the slot of the item to be removed</param>
        /// <returns>the previous item in specified slot</returns>
        public Sprite RemoveItem(string slot)
        {
            Sprite pop = null;
            switch (slot)
            {
                case "shoes":
                    pop = _bottom.sprite;
                    _bottom.sprite = null;
                    break;
                case "clothes":
                    pop = _top.sprite;
                    _top.sprite = null;
                    break;
                case "hats":
                    pop = _head.sprite;
                    _head.sprite = null;
                    break;
                case "accessories":
                    pop = _accessory.sprite;
                    _accessory.sprite = null;
                    break;
                case "weapons":
                    pop = _weapon.sprite;
                    _weapon.sprite = null;
                    break;
                default:
                    Debug.LogError("Non-Classified equipment!");
                    break;
            }
            return pop;
        }
    }
}