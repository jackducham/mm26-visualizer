using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MM26.Components
{
    public class Inventory : MonoBehaviour
    {
        [Header("Equipment Slots")]
        [SerializeField]
        private SpriteRenderer _bottom = null;

        [SerializeField]
        private SpriteRenderer _top = null;

        [SerializeField]
        private SpriteRenderer _head = null;

        [SerializeField]
        private SpriteRenderer _accessory = null;

        [SerializeField]
        private SpriteRenderer _weapon = null;

        public Sprite Bottom
        {
            get => _bottom.sprite;
            set => _bottom.sprite = value;
        }

        public Sprite Top
        {
            get => _top.sprite;
            set => _top.sprite = value;
        }

        public Sprite Head
        {
            get => _head.sprite;
            set => _head.sprite = value;
        }

        public Sprite Accessory
        {
            get => _accessory.sprite;
            set => _accessory.sprite = value;
        }

        public Sprite Weapon
        {
            get => _weapon.sprite;
            set => _weapon.sprite = value;
        }
    }
}