using PetShopApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApp.Core.DomainService
{
    public interface IPetRepository
    {
        IEnumerable<Pet> ReadPets(Filter filter = null);

        
        Pet Create(Pet pet);
        Pet Delete(int id);
        Pet Update(Pet petUpdate);
        Pet FindPetWithID(int id);
        Pet FindPetWithIdIncludingOwner(int id);
         int Count();
    }
}
