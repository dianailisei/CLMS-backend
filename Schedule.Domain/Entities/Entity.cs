using System;

namespace Schedule.Domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public bool Available { get; set; }

        public Entity()
        {
            Available = true;
        }
    }
}
