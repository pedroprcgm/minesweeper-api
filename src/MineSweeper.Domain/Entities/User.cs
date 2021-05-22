using AspNetCore.Identity.MongoDbCore.Models;
using MineSweeper.Domain.Entities.Base;
using System;

namespace MineSweeper.Domain.Entities
{
    public class User : MongoIdentityUser, IEntity
    {
        public User()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.UtcNow;
        }

        public User(string email)
        {
            Email = email;
            UserName = email;
            NormalizedEmail = email.ToUpper();
            NormalizedUserName = email.ToUpper();
        }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }
    }


    public class Role : MongoIdentityRole
    {
    }
}
