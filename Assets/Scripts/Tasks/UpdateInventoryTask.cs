using UnityEngine;
using MM26.ECS;
using MM26.Utilities;

namespace MM26.Tasks
{
    public sealed class UpdateInventoryTask : Task
    {
        public bool? hat_changed;
        public bool? clothes_changed;
        public bool? shoes_changed;
        public bool? weapon_changed;
        public bool? accesory_changed;
        
        public string Head = "";
        public string Top = "";
        public string Bottom = ""; 
        public string Weapon = "";
        public string Accessory = "";

        public UpdateInventoryTask(string entity) : base(entity)
        {
        }

        public override int GetHashCode()
        {
            Hash hash = Hash.Default;

            hash.Add(base.GetHashCode());
            hash.Add(this.hat_changed.GetHashCode());
            hash.Add(this.clothes_changed.GetHashCode());
            hash.Add(this.shoes_changed.GetHashCode());
            hash.Add(this.weapon_changed.GetHashCode());
            hash.Add(this.accesory_changed.GetHashCode());

            hash.Add(this.Head.GetHashCode());
            hash.Add(this.Top.GetHashCode());
            hash.Add(this.Bottom.GetHashCode());
            hash.Add(this.Weapon.GetHashCode());
            hash.Add(this.Accessory.GetHashCode());

            return hash.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is UpdateInventoryTask))
            {
                //Debug.LogError(obj);
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            UpdateInventoryTask other = (UpdateInventoryTask)obj;

            return this.hat_changed == other.hat_changed
                && this.clothes_changed == other.clothes_changed
                && this.shoes_changed == other.shoes_changed
                && this.weapon_changed == other.weapon_changed
                && this.accesory_changed == other.accesory_changed
                && this.Head == other.Head
                && this.Top == other.Top
                && this.Bottom == other.Bottom
                && this.Weapon == other.Weapon
                && this.Accessory == other.Accessory;
        }

        //public override string ToString()
        //{
        //    return JsonUtility.ToJson(this);
        //}
    }
}
