using System;

namespace MineSweeper.Domain.Entities.Base
{
    public class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
