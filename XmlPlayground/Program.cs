using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleInterface consoleInterface = new ConsoleInterface();
            consoleInterface.InterfaceMain();
            Console.ReadKey();
        }
    }
}
