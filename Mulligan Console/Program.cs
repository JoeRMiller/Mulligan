using Mulligan.Core.Data;
using Mulligan.Core.Models;

namespace Mulligan.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //@"D:\Development\source\repos\Mulligan\Mulligan Console\states.csv"
            var importer = new Importer(@"D:\Development\source\repos\Mulligan\Mulligan Console\states.csv");
            importer.Import();
            System.Console.WriteLine("Finished");
        }
    }


}
