
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PetShopApp.Core.DomainService;
using PetShopApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetShopApp.Infrastructure.SQLData.Repos

{
  public  class OwnerRepository : IOwnerRepository
    {
       

        readonly PetShopAppContext context;

        public OwnerRepository(PetShopAppContext ctx)
        {
            context = ctx;
        }

        public Owner Create(Owner o)
        {

            context.Attach(o).State = EntityState.Added;
            context.SaveChanges();
            return o;
        }

        public Owner Delete(int id)
        {
            var owner = FindOwnerWithID(id);
            context.Owners.Remove(owner);
            context.SaveChanges();
            return null;
        }
        public IEnumerable<Owner> ReadOwners()
        {
            return context.Owners;
        }

        public Owner UpdateOwner(Owner ownerUpdate)
        {
            context.Attach(ownerUpdate).State = EntityState.Modified;
            context.Entry(ownerUpdate).Collection(o => o.pets).IsModified = true;
            context.SaveChanges();
            return ownerUpdate;
        }
       
        public Owner FindOwnerWithID(int id)
        { 
            
            return context.Owners.FirstOrDefault(o => o.Id == id);
        }

        public List<Pet> GetPetsForThisOwner(int id)
        {
            return context.Pets.Where(p => p.PreviousOwner.Id == id).ToList(); ;
        }

        public IEnumerable<Owner> ReadOwners(Filter filter)
        {
            var paging = new List<Owner>();
            if (filter !=null && filter.CurrentPage > 0 && filter.ItemsPrPage > 1)
            {
                paging = context.Owners
                    .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                    .Take(filter.ItemsPrPage).ToList();
            }


            if (filter != null &&  filter.CurrentPage > 0 && filter.ItemsPrPage > 1 && FieldToSort(null, filter) != null && filter.SortOrder == "desc")
            {
                return paging.OrderByDescending(o => FieldToSort(o, filter));
            }
            else if (filter != null &&  filter.CurrentPage > 0 && filter.ItemsPrPage > 1 && FieldToSort(null, filter) != null)
            {
                return paging.OrderBy(o => FieldToSort(o, filter));
            }
            else if (filter != null &&  filter.CurrentPage > 0 && filter.ItemsPrPage > 1)
            {
                return paging;
            }
            else return context.Owners;
        }
        public object FieldToSort(Owner o, Filter filter)
        {
            if (filter.SortBy == null)
                return null;

            if (o == null)
                o = new Owner();

            if (filter.SortBy.ToLower().Equals("id"))
                return o.Id;
            //       else if (filter.SortBy.ToLower().Equals("name"))
            //           return p.Name;
            //       else if (filter.SortBy.ToLower().Equals("type"))
            //           return p.Type;
            //       else if (filter.SortBy.ToLower().Equals("birthdate"))
            //           return p.Birthdate;
            //       else if (filter.SortBy.ToLower().Equals("solddate"))
            //           return p.SoldDate;
            //        else if (filter.SortBy.ToLower().Equals("color"))
            //            return p.Color;
            else if (filter.SortBy.ToLower().Equals("phonenumber"))
                return o.PhoneNumber;
            else return null;
        }
        public int Count()
        {
            return context.Owners.Count();
        }
    }
}