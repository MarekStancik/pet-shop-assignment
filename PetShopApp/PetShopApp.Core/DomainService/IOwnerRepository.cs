using PetShopApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApp.Core.DomainService
{
   public interface IOwnerRepository
    {
        Owner Create(Owner owner);
        Owner Delete(int id);
        Owner FindOwnerWithID(int id);
        Owner UpdateOwner(Owner ownerUpdate);
        IEnumerable<Owner> ReadOwners(Filter filter =null);
        List<Pet> GetPetsForThisOwner(int id);
        int Count();
    }
}
