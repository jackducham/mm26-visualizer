using UnityEngine;
using MM26.ECS;
using MM26.Utilities;

namespace MM26.Tasks
{
    public sealed class UpdateInventoryTask : Task
    {
        public bool hat_changed = false;
        public bool clothes_changed = false;
        public bool shoes_changed = false;
        public bool weapon_changed = false;
        public bool accesory_changed = false;
        
        public string Head = "_";
        public string Top = "_";
        public string Bottom = "_"; 
        public string Weapon = "_";
        public string Accessory = "_";

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

            if(this.Head != null)
                hash.Add(this.Head.GetHashCode());
            if (this.Top != null)
                hash.Add(this.Top.GetHashCode());
            if(this.Bottom != null)
                hash.Add(this.Bottom.GetHashCode());
            if(this.Weapon != null)
                hash.Add(this.Weapon.GetHashCode());
            if(this.Accessory != null)
                hash.Add(this.Accessory.GetHashCode());

            return hash.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is UpdateInventoryTask))
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            UpdateInventoryTask other = (UpdateInventoryTask)obj;

            bool eq = this.hat_changed == other.hat_changed
                && this.clothes_changed == other.clothes_changed
                && this.shoes_changed == other.shoes_changed
                && this.weapon_changed == other.weapon_changed
                && this.accesory_changed == other.accesory_changed;

            //return this.hat_changed == other.hat_changed
            //    && this.clothes_changed == other.clothes_changed
            //    && this.shoes_changed == other.shoes_changed
            //    && this.weapon_changed == other.weapon_changed
            //    && this.accesory_changed == other.accesory_changed
            //    && this.Head == other.Head
            //    && this.Top == other.Top
            //    && this.Bottom == other.Bottom
            //    && this.Weapon == other.Weapon
            //    && this.Accessory == other.Accessory;

            if (!eq) return false;

            if (this.hat_changed)
                eq = eq && (this.Head == other.Head);
            if (this.shoes_changed)
                eq = eq && (this.Bottom == other.Bottom);
            if (this.clothes_changed)
                eq = eq && (this.Top == other.Top);
            if (this.accesory_changed)
                eq = eq && (this.Accessory == other.Accessory);
            if (this.weapon_changed)
                eq = eq && (this.Weapon == other.Weapon);

            Debug.Log(eq);
            return eq;
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
