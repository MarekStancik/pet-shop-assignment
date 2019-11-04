
namespace ConsoleApp
{
    public interface IConsoleSupport
    {
        int validateSelection(int min, int max);
        int ShowMenu(string[] menu);

         string DataRetriever(string s);
         int ReturnIdProvided();

         

         void Close();

    }
}