using PetShopApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApp.Core.ApplicationService
{
    public interface IOwnerService
    {
         Owner CreateOwner(Owner owner);
         Owner DeleteOwner(int id);
         Owner FindOwnerWithId(int id);
         List<Owner> GetOwners();
         Owner OwnerCreatorHelper(string firstName, string lastName, string address, string phoneNumber,string email);
        Owner UpdateOwner(Owner ownerUpdate);
        Owner FindOwnerWithIDincludingPets(int id);

        List<Pet> GetPetsForThisOwner(int id);
        int Count();
        List<Owner> GetFilteredOwners(Filter filter);
    }
}
