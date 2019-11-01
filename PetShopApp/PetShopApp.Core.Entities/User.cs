using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApp.Core.Entities
{
    public class User
    {
        public String Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public bool IsAdmin { get; set; }
    }
}
