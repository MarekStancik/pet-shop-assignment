using PetShopApp.Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PetShopApp.Core.ApplicationService
{
    public interface IPetService
    {
        Pet CreatePet(Pet pet);
        
        Pet FindPetWithId(int id);

        Pet FindPetWithIdIncludingOwner(int id);
        List<Pet> FindPetsByType(string type);
        List<Pet> SortPetsByPriceASC();
        List<Pet> SortPetsByPriceDESC();

        List<Pet> GetCheapestPets();

        List<Pet> GetPets();
        
        Pet UpdatePet(Pet petUpdate);

         int Count();
        Pet DeletePet(int id);
        List<Pet> GetFilteredPets(Filter filter);
    }
}
