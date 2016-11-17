using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlPlayground
{
    public class ConsoleInterface
    {
        public void InterfaceMain()
        {
            Console.WriteLine("XML Processing");
            Console.WriteLine("==============");
            Console.WriteLine("1. Reading XML");
            Console.WriteLine("2. Writing XML");
            Console.WriteLine("3. Samples");
            Console.Write("Select: ");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    var interfaceReading = new InterfaceReading();
                    break;
                case "2":
                    InterfaceWriting();
                    break;
                case "3":
                    InterfaceSamples();
                    break;
            }
        }

        public class InterfaceReading
        {
            public InterfaceReading()
            {
                interfaceInit();
            }

            private void interfaceInit()
            {
                Console.WriteLine("Reading XML");
                Console.WriteLine("===========");
                Console.WriteLine("1. XmlReader");
                Console.WriteLine("2. XDocument");
                Console.WriteLine("3. DataSet");
                Console.WriteLine("4. LINQ/XElement");
                Console.Write("Select: ");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        interfaceXmlReader();
                        break;
                    case "2":
                        interfaceMoveToContent();
                        break;
                    case "3":
                        interfaceReadStartElement();
                        break;
                    case "4":
                        interfaceReadElementString();
                        break;
                }
            }

            private void interfaceXmlReader()
            {
                Console.WriteLine("Reading XML > Read using XMLReader Class");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Simple Processing");
                Console.WriteLine("2. Using MoveToContent");
                Console.WriteLine("3. Using Read Start Element");
                Console.WriteLine("4. Using Read Element String");
                int input = Int32.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                }
            }

            private void interfaceMoveToContent()
            {
                
            }

            private void interfaceReadStartElement()
            {
                
            }

            private void interfaceReadElementString()
            {
                
            }
        }

        public static void InterfaceWriting()
        {
            Console.WriteLine("Writing XML");
            Console.WriteLine("===========");
            Console.WriteLine("1. XmlWriter");
            Console.WriteLine("2. XDocument");
            Console.WriteLine("3. DataSet");
            Console.WriteLine("4. LINQ/XElement");
            Console.Write("Select: ");
            var input = Console.ReadLine();
            switch (input)
            {
                    
            }
        }

        public static void InterfaceSamples()
        {
            Console.WriteLine("Samples");
            Console.WriteLine("=======");
            Console.WriteLine("1. Samples");
            Console.WriteLine("Select: ");
            var input = Console.ReadLine();
            switch (input)
            {
                
            }
        }
    }
}
