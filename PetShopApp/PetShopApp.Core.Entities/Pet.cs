using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApp.Core.Entities
{
    public class Pet
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime SoldDate { get; set; }
        public ICollection<PetColor> PetColors { get; set; }
        public Owner PreviousOwner { get; set; }
        public double Price { get; set; }

        

    }
}
