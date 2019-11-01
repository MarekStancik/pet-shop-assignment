using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PetShopApp.Core.DomainService;
using PetShopApp.Core.Entities;

namespace PetShopApp.Core.ApplicationService.Impl
{
    public class PetService : IPetService
    {
        private IPetRepository petRepos;
        private IOwnerRepository ownerRepos;

        public PetService(IPetRepository petRepository, IOwnerRepository ownerRepository)
        {
            petRepos = petRepository;
            ownerRepos = ownerRepository;
        }

        public Pet CreatePet(Pet pet)
        {
            return petRepos.Create(pet);
        }

        public Pet DeletePet(int id)
        {
            return petRepos.Delete(id);
        }

        public Pet UpdatePet(Pet petUpdate)
        {
            return petRepos.Update(petUpdate);
        }

        public Pet FindPetWithId(int id)
        {
            return petRepos.FindPetWithID(id);
        }

        public Pet FindPetWithIdIncludingOwner(int id)
        {
            return petRepos.FindPetWithIdIncludingOwner(id);
        }
        public List<Pet> FindPetsByType(String type)
        {
            List<Pet> PetsList = GetPets();
            List<Pet> TypeList = new List<Pet>();
            foreach (var pet in PetsList)
            {
                if (pet.Type.ToLower() == type.ToLower())
                    TypeList.Add(pet);

            }
            return TypeList;
        }
        public List<Pet> SortPetsByPriceASC()
        {
            List<Pet> PetsList = GetPets();
            return PetsList.OrderBy(Pet => Pet.Price).ToList();
        }

        public List<Pet> SortPetsByPriceDESC()
        {
            List<Pet> PetsList = GetPets();
            return PetsList.OrderByDescending(Pet => Pet.Price).ToList();
        }

        public List<Pet> GetPets()
        {
            return petRepos.ReadPets().ToList();
        }

        public int Count()
        {
            return petRepos.Count();
        }


        public List<Pet> GetCheapestPets()
        {
            List<Pet> cheapest = SortPetsByPriceASC();
            return cheapest.GetRange(0, 5);
        }

        public List<Pet> GetFilteredPets(Filter filter)
        {
            return petRepos.ReadPets(filter).ToList();
        }
    }
}
