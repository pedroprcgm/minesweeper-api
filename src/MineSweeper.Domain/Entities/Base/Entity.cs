using System;

namespace MineSweeper.Domain.Entities.Base
{
    public class Entity
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
