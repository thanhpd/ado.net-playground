using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlPlayground.Worker;

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
                InterfaceInit();
            }

            private void InterfaceInit()
            {
                Console.WriteLine();
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
                        interfaceXDocument();
                        break;
                    case "3":
                        interfaceDataSet();
                        break;
                    case "4":
                        interfaceLinqXElement();
                        break;
                }
            }

            private void interfaceXmlReader()
            {
                Console.WriteLine();
                Console.WriteLine("Reading XML > Read using XMLReader Class");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Simple Processing");
                Console.WriteLine("2. Using MoveToContent");
                Console.WriteLine("3. Using Read Start Element");
                Console.WriteLine("4. Using Read Element String");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        XmlReaderHelper.GetInstance().UsingSimpleProcessing();
                        break;
                    case "2":
                        XmlReaderHelper.GetInstance().UsingMoveToContent();
                        break;
                    case "3":
                        XmlReaderHelper.GetInstance().UsingReadStartElement();
                        break;
                    case "4":
                        XmlReaderHelper.GetInstance().UsingReadElementString();
                        break;
                }
            }

            private void interfaceXDocument()
            {
                Console.WriteLine();
                Console.WriteLine("Reading XML > Read using XDocument Class");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Simple Processing");
                Console.WriteLine("2. Read Employee Nodes");
                Console.WriteLine("3. Read All Nodes");
                Console.WriteLine("4. Load XML");
                Console.WriteLine("5. Get Attributes");
                Console.WriteLine("6. Find by Attribute");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        XDocumentHelper.GetInstance().UsingSimpleProcessing();
                        break;
                    case "2":
                        XDocumentHelper.GetInstance().ReadEmployeeNodes();
                        break;
                    case "3":
                        XDocumentHelper.GetInstance().ReadAllNodes();
                        break;
                    case "4":
                        XDocumentHelper.GetInstance().LoadXml();
                        break;
                    case "5":
                        XDocumentHelper.GetInstance().GetAttributes();
                        break;
                    case "6":
                        XDocumentHelper.GetInstance().FindByAttribute();
                        break;
                }
            }

            private void interfaceDataSet()
            {
                Console.WriteLine();
                Console.WriteLine("Reading XML > Read using DataSet Class");
                Console.WriteLine("======================================");
                Console.WriteLine("1. Simple Processing");
                Console.WriteLine("2. Using Select Method");
                Console.WriteLine("3. Reading a Schema");
                Console.WriteLine("4. Reading a Schema (BAD)");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        DataSetHelper.GetInstance().UsingSimpleProcessing();
                        break;
                    case "2":
                        DataSetHelper.GetInstance().UsingSelectMethod();
                        break;
                    case "3":
                        DataSetHelper.GetInstance().ReadingSchema();
                        break;
                    case "4":
                        DataSetHelper.GetInstance().ReadingSchemaBad();
                        break;
                }
            }

            private void interfaceLinqXElement()
            {
                Console.WriteLine();
                Console.WriteLine("Reading XML > Read using LINQ and XElement Class");
                Console.WriteLine("================================================");
                Console.WriteLine("1. Select XPath (Single)");
                Console.WriteLine("2. Select LINQ (Single)");
                Console.WriteLine("3. Select XPath (Multiple)");
                Console.WriteLine("4. Select LINQ (Multiple)");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        LinqXElementHelper.GetInstance().SelectSingleXPath();
                        break;
                    case "2":
                        LinqXElementHelper.GetInstance().SelectSingleLinq();
                        break;
                    case "3":
                        LinqXElementHelper.GetInstance().SelectMultipleXPath();
                        break;
                    case "4":
                        LinqXElementHelper.GetInstance().SelectMultipleLinq();
                        break;
                }
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
