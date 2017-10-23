using System;

namespace GREEDY.Models
{
    public class Item : IEquatable<Item>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        //maybe this should compare not objects by reference, but Item.Name by value?
        public bool Equals(Item other) 
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Item) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        public bool Equals(string name)
        {
            if (Name.Equals(name)) return true;
            return false;
        }
    }
}