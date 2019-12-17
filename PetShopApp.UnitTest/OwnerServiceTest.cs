using Moq;
using PetShopApp.Core.ApplicationService.Impl;
using PetShopApp.Core.DomainService;
using PetShopApp.Core.Entities;
using System;
using System.IO;
using Xunit;

namespace PetShopApp.UnitTest
{
    public class OwnerServiceTest
    {
        [Fact]
        public void CreateOwnerRepositoryCalls()
        {
            var ownerRepo = new Mock<IOwnerRepository>();
            var petRepo = new Mock<IPetRepository>();
            var service = new OwnerService(ownerRepo.Object, petRepo.Object);

            var owner = new Owner 
            {
                FirstName = "fname",
                LastName = "lname"
            };

            service.CreateOwner(owner);

            ownerRepo.Verify(repo => repo.Create(It.IsAny<Owner>()),Times.Once());
            petRepo.Verify(repo => repo.Create(It.IsAny<Pet>()), Times.Never());
        }

        [Fact]
        public void CreateOwnerNullOwnerThrowsException()
        {
            var ownerRepo = new Mock<IOwnerRepository>();
            var petRepo = new Mock<IPetRepository>();
            var service = new OwnerService(ownerRepo.Object, petRepo.Object);

            var ex = Assert.Throws<NullReferenceException>(() => service.CreateOwner(null));
            Assert.Equal("Cannot create null owner", ex.Message);
        }

        [Fact]
        public void CreateOwnerEmptyFirstNameThrowsException()
        {
            var ownerRepo = new Mock<IOwnerRepository>();
            var petRepo = new Mock<IPetRepository>();
            var service = new OwnerService(ownerRepo.Object,petRepo.Object);

            var owner = new Owner { };

            var ex = Assert.Throws<InvalidDataException>(() => service.CreateOwner(owner));
            Assert.Equal("Owner needs a FirstName to be created",ex.Message);
        }

        [Fact]
        public void CreateOwnerEmptyLastNameThrowsException()
        {
            var ownerRepo = new Mock<IOwnerRepository>();
            var petRepo = new Mock<IPetRepository>();
            var service = new OwnerService(ownerRepo.Object, petRepo.Object);

            var owner = new Owner { FirstName = "Owner1" };

            var ex = Assert.Throws<InvalidDataException>(() => service.CreateOwner(owner));
            Assert.Equal("Owner needs a LastName to be created", ex.Message);
        }
    }
}
