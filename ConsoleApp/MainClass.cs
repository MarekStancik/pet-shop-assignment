
using System;
using Microsoft.Extensions.DependencyInjection;



namespace ConsoleApp
{
    class MainClass
    {
       
        static void Main(string[] args)
        {
            // CONSOLE SIZE SETTING
            Console.SetBufferSize(Console.LargestWindowWidth - 15, Console.LargestWindowHeight - 10);
            Console.SetWindowSize(Console.LargestWindowWidth-15, Console.LargestWindowHeight-10);
            var serviceCollection = new ServiceCollection();
           
            serviceCollection.AddScoped<IViewSelection, ViewSelection>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var viewSelection = serviceProvider.GetRequiredService<IViewSelection>();
            // DATA INITIATION
            

            viewSelection.SelectView();

            


        }

    }
}
