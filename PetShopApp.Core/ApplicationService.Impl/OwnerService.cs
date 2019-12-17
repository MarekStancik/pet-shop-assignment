using PetShopApp.Core.DomainService;
using PetShopApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace PetShopApp.Core.ApplicationService.Impl
{
   public class OwnerService : IOwnerService
    {
        private IOwnerRepository ownerRepos;
        private IPetRepository petRepos;

        public OwnerService(IOwnerRepository ownerRepository,IPetRepository petRepository)
        {
            ownerRepos = ownerRepository;
            petRepos = petRepository;
        }

        public int Count()
        {
            return ownerRepos.Count();
        }

        public Owner CreateOwner(Owner owner)
        {
            if(owner == null)
                throw new NullReferenceException("Cannot create null owner");

            if (String.IsNullOrEmpty(owner.FirstName))
                throw new InvalidDataException("Owner needs a FirstName to be created");

            if (String.IsNullOrEmpty(owner.LastName))
                throw new InvalidDataException("Owner needs a LastName to be created");

            return ownerRepos.Create(owner);
        }

        public Owner DeleteOwner(int id)
        {
            return ownerRepos.Delete(id);
        }

        public Owner FindOwnerWithId(int id)
        {
            return ownerRepos.FindOwnerWithID(id);
        }
        public Owner FindOwnerWithIDincludingPets(int id)
        {
            var owner = ownerRepos.FindOwnerWithID(id);
            owner.pets = petRepos.ReadPets()
           .Where(pet => 
           pet.PreviousOwner!=null &&
           pet.PreviousOwner.Id == owner.Id)
           .ToList();
            return owner;
        }

        public List<Owner> GetFilteredOwners(Filter filter)
        {
            return ownerRepos.ReadOwners(filter).ToList();
        }

        public List<Owner> GetOwners()
        {
            return ownerRepos.ReadOwners().ToList();
        }

        public List<Pet> GetPetsForThisOwner(int id)
        {
            return ownerRepos.GetPetsForThisOwner(id);
        }

        public Owner OwnerCreatorHelper(string firstName, string lastName,string address,string phoneNumber,string email)
        {
            Owner owner = null;


            try
            {
                if (String.IsNullOrEmpty(firstName)) throw new Exception("First name required.");
                if (String.IsNullOrEmpty(lastName)) throw new Exception("Surname required.");
                if (String.IsNullOrEmpty(address)) throw new Exception("Address required.");
                if (String.IsNullOrEmpty(email)) throw new Exception("Surname required.");
                phoneNumber = phoneNumber.Replace(" ", String.Empty);
                int.Parse(phoneNumber);
                owner = new Owner()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Address = address,
                    PhoneNumber = phoneNumber,
                    Email = email
                };
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("Something went wrong --> " + ex.Message);
            }


            return owner;

        }

        public Owner UpdateOwner(Owner ownerUpdate)
        {
            return ownerRepos.UpdateOwner(ownerUpdate);
        }


    }
}
