namespace BattleCards.Models
{
    using SIS.MvcFramework;
    using System;
    using System.Collections.Generic;

    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public virtual ICollection<UserCard> UsersCards { get; set; }
    }
}
