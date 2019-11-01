using Microsoft.EntityFrameworkCore;
using PetShopApp.Core.DomainService;
using PetShopApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetShopApp.Infrastructure.SQLData.Repos
{

    public class PetRepository : IPetRepository
{
        readonly PetShopAppContext context;
        
        public PetRepository(PetShopAppContext ctx)
        {
            context = ctx;
        }
        
        public Pet Create(Pet p)
        {
            context.Attach(p).State = EntityState.Added;
            context.SaveChanges();
            return p;
        }

        public Pet Delete(int id)
        {
            var pet = FindPetWithID(id);
            context.Pets.Remove(pet);
            context.SaveChanges();
            return null;
        }

        public IEnumerable<Pet> ReadPets(Filter filter)
        {
            /*
            if (filter!= null && filter.ItemsPrPage>0 && filter.CurrentPage>0)
            {
                var paging =  context.pets.Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                     .Take(filter.ItemsPrPage);
                if (FieldToSort(null,filter) != null)
                    return paging.OrderBy(p => FieldToSort(p,filter));
                else return paging;
            }
            return context.pets.Include(p => p.PreviousOwner).ToList();
           */
            var paging = new List<Pet>();
            if (filter != null &&  filter.CurrentPage > 0 && filter.ItemsPrPage > 1)
            {
                paging = context.Pets
                    .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                    .Take(filter.ItemsPrPage).ToList();
            }


            if (filter != null && filter.CurrentPage > 0 && filter.ItemsPrPage > 1 && FieldToSort(null, filter) != null && filter.SortOrder == "desc")
            {
                return  paging.OrderByDescending(p => FieldToSort(p, filter));
            }
            else if (filter != null && filter.CurrentPage > 0 && filter.ItemsPrPage > 1 && FieldToSort(null, filter) != null)
            {
                return paging.OrderBy(p => FieldToSort(p, filter));
            }
            else if (filter != null && filter.CurrentPage > 0 && filter.ItemsPrPage > 1 )
            {
                return paging;
            }
            else return context.Pets.Include(p => p.PreviousOwner).ToList();

        }
        public object FieldToSort(Pet p, Filter filter)
        {
            if (filter.SortBy == null)
                return null;

            if(p==null)
            p = new Pet();

            if (filter.SortBy.ToLower().Equals("id"))
                return p.ID;
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
            else if (filter.SortBy.ToLower().Equals("price"))
                return p.Price;
            else return null;
        }


        public Pet Update(Pet petUpdate)
        {
            /*
            if (petUpdate.PreviousOwner != null  && context.ChangeTracker.Entries<Pet>().FirstOrDefault(ce => ce.Entity.ID == petUpdate.ID) == null)
            {
                context.Attach(petUpdate.PreviousOwner);
            }
            else
            {
             
                context.Entry(petUpdate).Reference(p => p.PreviousOwner).IsModified = true;
            }
            var pet = context.pets.Update(petUpdate).Entity;
            context.SaveChanges();
    */
            context.Attach(petUpdate).State = EntityState.Modified;
            context.Entry(petUpdate).Reference(p => p.PreviousOwner).IsModified = true;
            context.SaveChanges();
        return petUpdate;
        }

        public Pet FindPetWithID(int id)
        {
            return context.Pets.FirstOrDefault(p => p.ID == id);
        }

        public Pet FindPetWithIdIncludingOwner(int id)
        { 
            return context.Pets.Include(p => p.PreviousOwner).FirstOrDefault(p => p.ID == id);
            
        }

        public int Count()
        {
            return context.Pets.Count();
        }
        
    }
}
