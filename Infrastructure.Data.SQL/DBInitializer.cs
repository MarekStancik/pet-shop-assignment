using PetShopApp.Core.Entities;
using PetShopApp.Core.Helpers;
using PetShopApp.Infrastructure.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.SQLData
{
    public static class DBInitializer
    {
        private static readonly int SeedCount = 20;
        public static void SeedDB(PetShopAppContext ctx,IAuthenticationHelper authenticationHelper)
        {

            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

             if (ctx.Owners.Any())
             {
                 return;   // DB has been seeded
             }

            var petList = new List<Pet>();
            var colorList = new List<Color>();
            var ownersList = new List<Owner>();

            //Add Owners
            for (int i = 0; i < SeedCount; ++i)
                ownersList.Add(new Owner { FirstName = $"First Name{i}", LastName = $"Last Name{i}" });
            
            ctx.Owners.AddRange(ownersList);

            //Add Pets
            for (int i = 0; i < SeedCount; ++i)
            {
                var owner = ownersList[i];
                petList.Add(new Pet
                {
                    Name = $"Pet name {i}",
                    Type = $"Pet type {i}",
                    BirthDate = new DateTime(2000 + i, i < 11 ? 1 + i : 4, 1),
                    SoldDate = new DateTime(2001 + i, 1, 1),
                    PreviousOwner = owner,
                    Price = new Random().Next(1,5000)
                }) ;
            }

            ctx.AddRange(petList);
           
            //Add Colors
            for(int i =0; i < SeedCount; ++i)
            {
                colorList.Add(new Color{Name = "White"});
            }

            ctx.AddRange(colorList);

            //Add PetColor relation
            for(int i = 0; i < SeedCount; ++i)
            {
                ctx.PetColors.Add(new PetColor{ Pet = petList[i],Color = colorList[i] });
            }

            //Add Users

            byte[] passwordHashUser1, passwordSaltUser1, passwordHashUser2, passwordSaltUser2;
            authenticationHelper.CreatePasswordHash("12345", out passwordHashUser1, out passwordSaltUser1);
            authenticationHelper.CreatePasswordHash("ahojky", out passwordHashUser2, out passwordSaltUser2);

            List<User> users = new List<User>
            {
                new User
                {
                    IsAdmin = false,
                    Username = "Jano",
                    PasswordSalt = passwordSaltUser1,
                    PasswordHash = passwordHashUser1
                },
                new User
                {
                    IsAdmin = true,
                    Username = "Marek",
                    PasswordSalt = passwordSaltUser2,
                    PasswordHash = passwordHashUser2
                }
            };

            ctx.AddRange(users);


            ctx.SaveChanges();
        }
    }
}
