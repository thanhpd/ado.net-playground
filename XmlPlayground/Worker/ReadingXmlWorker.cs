using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using XML_Processing;

namespace XmlPlayground.Worker
{
    public class XmlReaderHelper
    {
        private static XmlReaderHelper _instance;
        private XmlReaderHelper() { }

        public static XmlReaderHelper GetInstance()
        {
            return _instance ?? (_instance = new XmlReaderHelper());
        }

        public void UsingSimpleProcessing()
        {
            StringBuilder builder = new StringBuilder(512);
            // Create XmlReader
            XmlReader reader = XmlReader.Create(AppConfig.GetEmployeesFile());
            // Skip <xml> element
            reader.Read();
            // Skip CRLF
            reader.Read();
            // Move to Employees element
            reader.Read();
            if (reader.LocalName.Equals("Employees"))
            {
                // skip crlf
                reader.Read();
                //move to employee element
                reader.Read();
                if (reader.LocalName.Equals("Employee") && reader.NamespaceURI.Equals(""))
                {
                    // skip crlf
                    reader.Read();
                    reader.Read();
                    if (reader.LocalName.Equals("id") && reader.NamespaceURI.Equals(""))
                    {
                        // read id
                        reader.Read();
                        builder.AppendFormat("id={0}", reader.Value);
                        builder.Append(Environment.NewLine);
                        // move to closing tag
                        reader.Read();
                        // skip crlf
                        reader.Read();
                    }
                    else
                    {
                        builder.Append("Cannot find id elemnent");
                        builder.Append(Environment.NewLine);
                    }
                }
                reader.Read();
                if (reader.LocalName.Equals("FirstName") && reader.NamespaceURI.Equals(""))
                {
                    // read first name
                    reader.Read();
                    builder.AppendFormat("FirstName={0}", reader.Value);
                    builder.Append(Environment.NewLine);
                    // move to closing tag
                    reader.Read();
                    // skip crlf
                    reader.Read();
                }
                else
                {
                    builder.Append("Cannot find FirstName elemnent");
                    builder.Append(Environment.NewLine);
                }
                while (reader.Read()) ;
            }
            else
            {
                builder.Append("Cannot find Employees elemnent");
                builder.Append(Environment.NewLine);
            }
            reader.Close();
            reader.Dispose();
            Console.Write(builder.ToString());
        }

        public void UsingMoveToContent()
        {
            StringBuilder builder = new StringBuilder();
            XmlReader reader = XmlReader.Create(AppConfig.GetEmployeesFile());
            // Move to content
            reader.MoveToContent();
            if (reader.LocalName.Equals("Employees"))
            {
                // Move to next line
                reader.Read();
                // Move to content
                reader.MoveToContent();
                if (reader.LocalName.Equals("Employee") && reader.NamespaceURI.Equals(""))
                {
                    // Move to next line
                    reader.Read();
                    // Move to content
                    reader.MoveToContent();
                    if (reader.LocalName.Equals("id") && reader.NamespaceURI.Equals(""))
                    {
                        reader.Read();
                        builder.AppendFormat("id = {0}", reader.Value);
                        builder.Append(Environment.NewLine);
                        reader.Read();
                    }
                    else
                    {
                        builder.Append("Cannot find id elemnent");
                        builder.Append(Environment.NewLine);
                    }
                    reader.Read();
                    reader.MoveToContent();
                    if (reader.LocalName.Equals("FirstName") && reader.NamespaceURI.Equals(""))
                    {
                        reader.Read();
                        builder.AppendFormat("FirstName = {0}", reader.Value);
                        builder.Append(Environment.NewLine);
                    }
                    else
                    {
                        builder.Append("Cannot find FirstName elemnent");
                        builder.Append(Environment.NewLine);
                    }
                }
                else
                {
                    builder.Append("Cannot find Employees elemnent");
                    builder.Append(Environment.NewLine);
                }
            }
            reader.Close();
            reader.Dispose();
            Console.Write(builder.ToString());
        }

        public void UsingReadStartElement()
        {
            StringBuilder builder = new StringBuilder();
            XmlReader reader = XmlReader.Create(AppConfig.GetEmployeesFile());

            // Open XmlReader
            reader.ReadStartElement("Employees");
            reader.ReadStartElement("Employee");
            reader.ReadStartElement("id");

            builder.AppendFormat("id = {0}", reader.Value);
            builder.Append(Environment.NewLine);
            // Read Text Node
            reader.Read();
            // Move past closing tag
            reader.Read();

            // Read FirstName element
            reader.ReadStartElement("FirstName");
            builder.AppendFormat("FirstName = {0}", reader.Value);
            builder.Append(Environment.NewLine);

            reader.Close();
            reader.Dispose();
            Console.Write(builder.ToString());
        }

        public void UsingReadElementString()
        {
            StringBuilder builder = new StringBuilder();
            using (XmlReader reader = XmlReader.Create(AppConfig.GetEmployeesFile()))
            {
                reader.ReadStartElement("Employees");
                reader.ReadStartElement("Employee");
                builder.AppendFormat("id = {0}", reader.ReadElementString("id"));
                builder.Append(Environment.NewLine);
                builder.AppendFormat("FirstName = {0}", reader.ReadElementString("FirstName"));
                builder.Append(Environment.NewLine);
            }                       
            Console.Write(builder.ToString());
        }
    }

    public class XDocumentHelper
    {
        private static XDocumentHelper _instance;
        private XDocumentHelper() { }

        public static XDocumentHelper GetInstance()
        {
            return _instance ?? (_instance = new XDocumentHelper());
        }

        public void UsingSimpleProcessing()
        {
            StringBuilder builder = new StringBuilder();
            XDocument xd = XDocument.Load(AppConfig.GetEmployeesFile());
            XElement node = xd.XPathSelectElement("/Employees/Employee");
            builder.AppendFormat("id = {0}", node.Element("id").Value);
            builder.Append(Environment.NewLine);
            builder.AppendFormat("FirstName = {0}", node.Element("FirstName").Value);
            builder.Append(Environment.NewLine);
            Console.Write(builder.ToString());
        }

        public void ReadEmployeeNodes()
        {
            StringBuilder builder = new StringBuilder();
            XDocument xd = XDocument.Load(AppConfig.GetEmployeesFile());
            var list = xd.XPathSelectElements("//Employee");
            foreach (XElement node in list)
            {
                builder.AppendFormat("id = {0}", node.Element("id").Value);
                builder.Append(Environment.NewLine);
                builder.AppendFormat("FirstName = {0}", node.Element("FirstName").Value);
                builder.Append(Environment.NewLine);
            }            
            Console.Write(builder.ToString());
        }

        public void ReadAllNodes()
        {
            StringBuilder builder = new StringBuilder();
            XDocument xd = XDocument.Load(AppConfig.GetEmployeesFile());
            var list = xd.XPathSelectElements("//Employee");
            foreach (XElement node in list)
            {
                builder.AppendFormat("Name={0}", node.Name);
                builder.Append(Environment.NewLine);
                foreach (XElement item in node.Elements())
                {
                    builder.AppendFormat("{0}={1}", item.Name, node.Element(item.Name).Value);
                    builder.Append(Environment.NewLine);
                }                
            }
            Console.Write(builder.ToString());
        }

        public void LoadXml()
        {

        }

        public void GetAttributes()
        {

        }

        public void FindByAttribute()
        {

        }
    }

    public class DataSetHelper
    {
        private static DataSetHelper _instance;
        private DataSetHelper() { }

        public static DataSetHelper GetInstance()
        {
            return _instance ?? (_instance = new DataSetHelper());
        }

        public void UsingSimpleProcessing()
        {

        }

        public void UsingSelectMethod()
        {

        }

        public void ReadingSchema()
        {

        }

        public void ReadingSchemaBad()
        {

        }
    }

    public class LinqXElementHelper
    {
        private static LinqXElementHelper _instance;
        private LinqXElementHelper() { }

        public static LinqXElementHelper GetInstance()
        {
            return _instance ?? (_instance = new LinqXElementHelper());
        }

        public void SelectSingleXPath()
        {

        }

        public void SelectSingleLinq()
        {

        }

        public void SelectMultipleXPath()
        {

        }

        public void SelectMultipleLinq()
        {

        }
    }
}
