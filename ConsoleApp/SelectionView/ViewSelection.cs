
using Microsoft.Extensions.DependencyInjection;
using PetShopApp.Core.ApplicationService;
using PetShopApp.Core.ApplicationService.Impl;
using PetShopApp.Core.DomainService;
using PetShopApp.Infrastructure.SQLData.Repos;
using System;

namespace ConsoleApp
{
    class ViewSelection : IViewSelection
    {

        public void SelectView()
        {
            var serviceCollection = new ServiceCollection();
            // SERVICES
            serviceCollection.AddScoped<IOwnerService, OwnerService>();
            serviceCollection.AddScoped<IPetService, PetService>();
            // VIEWS
            serviceCollection.AddScoped<IPetView, PetView>();
            serviceCollection.AddScoped<IOwnerView, OwnerView>();
            // REPOS
            serviceCollection.AddScoped<IPetRepository, PetRepository>();
            serviceCollection.AddScoped<IOwnerRepository, OwnerRepository>();
            //View Selection
            serviceCollection.AddScoped<IViewSelection, ViewSelection>();

            serviceCollection.AddScoped<IConsoleSupport, ConsoleSupport>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var petView = serviceProvider.GetRequiredService<IPetView>();
            var ownerView = serviceProvider.GetRequiredService<IOwnerView>();

            if (GetSelection() == 1)
            {
                Console.Clear();
                petView.BeginProgram();
            }

            else
            {
                Console.Clear();
                ownerView.BeginProgram();
            }


        }

        private int GetSelection()
        {
            Console.WriteLine("Select:");
            Console.WriteLine("1.Pet View.");
            Console.WriteLine("2.Owner View.");
            int result;
            while(!int.TryParse(Console.ReadLine(), out result) || result<1 || result>2)
            {
                Console.WriteLine("Provide proper number.");
            }
            return result;
        }
    }
}
