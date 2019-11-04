using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApp.Core.Entities
{
    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PetColor> PetColors { get; set; }

    }
}
