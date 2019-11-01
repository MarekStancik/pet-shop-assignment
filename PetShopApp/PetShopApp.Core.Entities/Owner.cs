using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApp.Core.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public List<Pet> pets { get; set; }
        public override string ToString() {
            return FirstName;
        }
    }
}
