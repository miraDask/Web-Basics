using SIS.MvcFramework;
using System;
using System.Collections.Generic;

namespace SharedTrip.Models
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString(); 
        }

        public virtual ICollection<UserTrip> UsersTrips { get; set; }
    }
}
