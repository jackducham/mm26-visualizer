
namespace MM26.Utilities
{
    public struct Hash
    {
        public int Value;

        public void Add(int component)
        {
            unchecked
            {
                this.Value = ((this.Value << 5) + this.Value) ^ component;
            }
        }

        public static Hash Default
        {
            get
            {
                return new Hash()
                {
                    Value = 5381
                };
            }
        }
    }
}
