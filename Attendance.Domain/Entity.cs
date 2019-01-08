using System;

namespace Attendance.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public bool Available { get; set; } = true;

/*        public override bool Equals(object obj)
        {
            var other = obj as Entity;

            if (ReferenceEquals(other, null) || GetType() != other.GetType() || 
                Id == Guid.Empty || other.Id == Guid.Empty)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Id == other.Id;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }*/
    }
}
