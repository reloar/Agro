using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Entities
{
    public class User : IdentityUser
    {
        //public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BankAccountNumber { get; set; }
        public string ContactAddress { get; set; }
        public string WalletCode { get; set; }
        public string UserType { get; set; }
        public string Accountname { get; set; }
      //  public double percentage_charge { get; set; }
        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; } = new List<IdentityUserRole<string>>();
        public bool IsDeleted { get; set; } = false;
        /// <summary>
        /// Navigation property for the claims this user possesses.
        /// </summary>
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; } = new List<IdentityUserClaim<string>>();

    }
    public class Role : IdentityRole
    {
        public Role()
        {

        }

        public Role(string name, string descriptions) : base(name)
        {
            Descriptions = descriptions;
        }
        public string Descriptions { get; set; }
        //add permission


        /// <summary>   
        /// Navigation property for the users in this role.
        /// </summary>
        public virtual ICollection<IdentityUserRole<string>> Users { get; set; } = new List<IdentityUserRole<string>>();

        /// <summary>
        /// Navigation property for claims in this role.
        /// </summary>
        public virtual ICollection<IdentityRoleClaim<string>> Claims { get; set; } = new List<IdentityRoleClaim<string>>();
    }
}
