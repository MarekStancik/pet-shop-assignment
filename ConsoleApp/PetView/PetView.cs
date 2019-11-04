using PetShopApp.Core.ApplicationService;
using System;
using System.Collections.Generic;
using PetShopApp.Core.Entities;

namespace ConsoleApp
{
  public  class PetView : IPetView
    {
         readonly IPetService petService;
         readonly IOwnerService ownerService;
         readonly IViewSelection viewSelection;
         readonly IConsoleSupport Sup;

        public PetView(IPetService petService,IOwnerService ownerService,IViewSelection viewSelection,IConsoleSupport consoleSupport)
          {
              this.petService = petService;
            this.ownerService = ownerService;
            this.viewSelection = viewSelection;
            Sup = consoleSupport;
        }




       public void BeginProgram()
        {
            var Menu = createMenuSelections();
            var changeView = false;

            var choice = Sup.ShowMenu(Menu);
            while (choice != 9)
            {
                changeView = false;
                switch (choice)
                {
                    case 1: // PRINT ALL PETS
                        Console.Clear();
                        var pets = petService.GetPets();
                        PrintPets(pets);
                        break;
                    case 2: //Search pet by its type
                        SearchPetByType();
                        break;
                    case 3: // CREATE PET
                        CreatePet();
                        break;
                    case 4: // DELETE PET
                        DeletePet();
                        break;
                    case 5: // UPDATE PET
                        UpdatePet();
                        break;
                    case 6: // Sort pets by price
                        SortPetsByPrice();
                        break;
                    case 7: // Get 5 cheapest pets
                        Console.Clear();
                        PrintPets(petService.GetCheapestPets());
                        break;
                    case 8: // Change View
                        changeView = true;
                        choice = 9;
                        viewSelection.SelectView();
                        break;

                }
                if(!changeView)
                choice = Sup.ShowMenu(Menu);
                
            }
            Sup.Close();

        }

        

        private string[] createMenuSelections()
        {
            string[] Menu = {
                "List All Pets",
                "Search Pet by type",
                "Add Pet",
                "Delete Pet",
                "Edit Pet",
                "Sort Pets by price",
                "Get 5 cheapest Pets",
                "Change View",
                "Exit"
            };
            return Menu;
        }

        

        private void SortPetsByPrice()
        {
            Comment("Do you want to sort Pets ASC or DESC (A/D)?");
            var l = Console.ReadLine().ToLower();
            if (l.Equals("a"))
            {
                Console.Clear();
                PrintPets(petService.SortPetsByPriceASC());
            }
            else if (l.Equals("d"))
            {
                Console.Clear();
                PrintPets(petService.SortPetsByPriceDESC());
            }
            else Comment("Wrong selection.");
        }

        private void UpdatePet()
        {
            int IdOfPet = Sup.ReturnIdProvided();
            Pet p = petService.FindPetWithId(IdOfPet);
            if (p != null)
            {
                var newName = Sup.DataRetriever("Name");
                var newType = Sup.DataRetriever("Type");
                var newBirthdate = Sup.DataRetriever("Birth date(i.e \"YYYY.M.D\")");
                var newSoldDate = Sup.DataRetriever("Sold date(i.e \"YYYY.M.D\")");
                var newColor = Sup.DataRetriever("Color");
                var newPreviousOwner = assignOwnerToPet();
                var newPrice = Sup.DataRetriever("Price");



                var newPet = petService.PetCreatorHelper(newName, newType, newBirthdate, newSoldDate, newColor, newPreviousOwner, newPrice);
                if (newPet != null)
                {
                    newPet.ID = IdOfPet;
                    petService.UpdatePet(newPet);
                    Console.Clear();
                    Comment("Pet updated\n");
                }
            }
            else
            {
                Console.Clear();
                Comment("Pet doesn't exist.\n");
            }
        }

        private void CreatePet()
        {
            var name = Sup.DataRetriever("Name");
            var type = Sup.DataRetriever("Type");
            var birthdate = Sup.DataRetriever("Birth date(i.e \"YYYY.M.D\")");
            var soldDate = Sup.DataRetriever("Sold date(i.e \"YYYY.M.D\")");
            var color = Sup.DataRetriever("Color");
            var previousOwner = assignOwnerToPet();
            var price = Sup.DataRetriever("Price");
            var pet = petService.PetCreatorHelper(name, type, birthdate, soldDate, color, previousOwner, price);
            if (pet != null)
            {
                Console.Clear();
                Comment("Pet created.\n");
                petService.CreatePet(pet);
            }
        }

        private void SearchPetByType()
        {
            Comment("What type of pet are you looking for?");
            var typee = Console.ReadLine();
            var typePets = petService.FindPetsByType(typee);
            if (typePets.Count > 0)
            {
                Console.Clear();
                PrintPets(typePets);
            }
            else
                Comment("No such pet with this type.");
        }
        private void DeletePet()
        {
            int id = Sup.ReturnIdProvided();
            if (petService.DeletePet(id))
            {
                Console.Clear();
                Comment("Pet deleted.\n");
                Comment("");
            }
            else
            {
                Console.Clear();
                Comment("Pet not found.\n");
                Comment("");

            }
        }

        private Owner assignOwnerToPet()
        {
            Owner own;
            string[] Menu = {
                
                "Leave pet without owner",
                "Assign existing owner"
            };
            var selection = Sup.ShowMenu(Menu);

            if (selection == 2)
            {
                own = ownerService.FindOwnerWithId(Sup.ReturnIdProvided());
                if (own == null) Comment("No such owner.\n");
                return own;
            }
            else
            {
                return null;
            }
        }

        private void Comment(string com)
        {
            Console.Write(com);
        }

        private void PrintPets(List<Pet> pets)
        {
            foreach (var pet in pets)
            {
                Comment($"ID: {pet.ID} Name: {pet.Name} " +
                                  $" Type: { pet.Type} " +
                                  $" Birth Date: {pet.Birthdate.ToString("dd/MM/yyyy")} "+
                                  $" Sold Date: {pet.SoldDate.ToString("dd/MM/yyyy")} " +
                                  $" Color: {pet.Color} "+
                                  $" Previous owner: {pet.PreviousOwner} " +
                                  $" Price: {pet.Price}\n"
                                );
            }
            Comment("\n");
        }


    }
}
