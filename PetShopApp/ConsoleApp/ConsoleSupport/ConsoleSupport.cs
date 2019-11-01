using System;
namespace ConsoleApp
{
   public class ConsoleSupport : IConsoleSupport
    {
        public int validateSelection(int min, int max)
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < min || choice > max)
            {
                Comment($"Error, select something between {min}-{max}\n");
            }
            return choice;
        }
        public int ShowMenu(string[] menu)
        {
            Comment("Select an option.\n");
            for (int i = 0; i < menu.Length; i++)
            {
                Comment($"{(i + 1)} : {menu[i]}\n");
            }

            return validateSelection(1, menu.Length);



        }
        public string DataRetriever(string s)
        {
            Comment(s + ": ");
            return Console.ReadLine();
        }

        public int ReturnIdProvided()
        {
            Comment("Provide the ID:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Comment("Error, type a number:");
            }
            return id;
        }
        public void Comment(string com)
        {
            Console.Write(com);
        }
        public void Close()
        {
            Console.Clear();
            Comment("Goodbye, see you soon :)");
            Environment.Exit(0);
        }
    }
}
