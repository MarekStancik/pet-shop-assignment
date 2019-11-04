using Microsoft.EntityFrameworkCore;
using PetShopApp.Core.DomainService;
using PetShopApp.Core.Entities;
using PetShopApp.Infrastructure.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.SQLData.Repos
{
    public class UserRepository : IUserRepository
    {
        readonly PetShopAppContext _appContext;
        public UserRepository(PetShopAppContext context)
        {
            _appContext = context;
        }

        public User Create(User user)
        {
            _appContext.Attach(user).State = EntityState.Added;
            _appContext.SaveChanges();
            return user;
        }

        public User GetByName(String name)
        {
            return _appContext.Users.FirstOrDefault(user => user.Username == name);
        }

        public User Update(User user)
        {
            _appContext.Attach(user).State = EntityState.Modified;
            _appContext.SaveChanges();
            return user;
        }

        public void Delete(String userName)
        {
            var user = GetByName(userName);
            if (user == null)
                return;
            _appContext.Users.Remove(user);
            _appContext.SaveChanges();
        }


    }
}
