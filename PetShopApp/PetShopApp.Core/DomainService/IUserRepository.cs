using PetShopApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApp.Core.DomainService
{
    public interface IUserRepository
    {
        User Create(User user);

        User GetByName(String name);

        User Update(User user);

        void Delete(String userName);
    }
}
