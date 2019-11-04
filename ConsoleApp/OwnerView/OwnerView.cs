using PetShopApp.Core.ApplicationService;
using PetShopApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
 public   class OwnerView : IOwnerView
    {
        readonly IViewSelection viewSelection;
        readonly IOwnerService ownerService;
        readonly IConsoleSupport Supp; // ConsoleSupport
        public OwnerView(IViewSelection viewSelection, IOwnerService ownerService,IConsoleSupport consoleSupport)
        {
            this.ownerService = ownerService;
            this.viewSelection = viewSelection;
            Supp = consoleSupport;
        }

        public void BeginProgram()
        {
            var Menu = createMenuSelections();
            var changeView = false;

            var choice = Supp.ShowMenu(Menu);
            while (choice != 6)
            {
                switch (choice)
                {
                    case 1: // PRINT ALL OWNERS
                        PrintAllOwners();
                        break;
                    case 2: // CREATE OWNER
                        CreateOwner();
                        break;
                    case 3: // DEL OWNER
                        DeleteOwner();
                        break;
                    case 4: // UPDATE OWNER
                        UpdateOwner();
                        break;
                    case 5: // CHANGE VIEW
                        changeView = true;
                        choice = 9;
                        viewSelection.SelectView();
                        break;
                }
                if(!changeView)
                choice = Supp.ShowMenu(Menu);

            }
            Supp.Close();


        }


        private void Comment(string com)
        {
            Console.Write(com);
        }
        private void UpdateOwner()
        {
            var IdOfOwner = Supp.ReturnIdProvided();
            Owner o = ownerService.FindOwnerWithId(IdOfOwner);
            if (o != null)
            {
                var newName = Supp.DataRetriever("Name");
                var newSurname = Supp.DataRetriever("Surname");
                var newAddress = Supp.DataRetriever("Address");
                var newPhone = Supp.DataRetriever("Phone number");
                var newEmail = Supp.DataRetriever("Email");



                var newOwn = ownerService.OwnerCreatorHelper(newName, newSurname, newAddress, newPhone, newEmail);
                if (newOwn != null)
                {
                    newOwn.Id = IdOfOwner;
                    ownerService.UpdateOwner(newOwn);
                    Console.Clear();
                    Comment("Owner updated\n");
                }
            }
            else
            {
                Console.Clear();
                Comment("Owner doesn't exist.\n");
            }
        }
  private string[] createMenuSelections()
        {
            string[] Menu = {
                "List All Owners",
                "Add Owner",
                "Delete Owner",
                "Edit Owner",
                "Change View",
                "Exit"
            };
            return Menu;
        }

        private void PrintAllOwners()
        {
            Console.Clear();
            var owners = ownerService.GetOwners();
            PrintOwners(owners);
        }

        private void CreateOwner()
        {
            var firstName = Supp.DataRetriever("Name");
            var surname = Supp.DataRetriever("Surname");
            var address = Supp.DataRetriever("Address");
            var phone = Supp.DataRetriever("Phone");
            var email = Supp.DataRetriever("Email");
            var newOwner = ownerService.OwnerCreatorHelper(firstName, surname, address, phone, email);
            if (newOwner != null)
            {
                Console.Clear();
                Comment("Owner created.\n");
                ownerService.CreateOwner(newOwner);
            }
        }

        private void DeleteOwner()
        {
            if (ownerService.DeleteOwner(Supp.ReturnIdProvided()))
            {
                Console.Clear();
                Comment("Owner deleted.\n");
                Comment("");
            }
            else
            {
                Console.Clear();
                Comment("Owner not found.\n");
                Comment("");

            }
        }

        private void PrintOwners(List<Owner> owners)
        {
            foreach (var owner in owners)
            {

                Comment($"ID: {owner.Id} Name: {owner.FirstName} " +
                                  $" Surname: { owner.LastName} " +
                                  $" Address: {owner.Address} " +
                                  $" Phone: {owner.PhoneNumber} " +
                                  $" E-mail: {owner.Email}\n"
                                );
            }
            Comment("\n");
        }

    }
}
