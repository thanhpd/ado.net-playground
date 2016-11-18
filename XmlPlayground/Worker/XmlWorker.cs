using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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


    public class XmlWriterHelper
    {
        private static XmlWriterHelper _instance;
        private XmlWriterHelper() { }

        public static XmlWriterHelper GetInstance()
        {
            return _instance ?? (_instance = new XmlWriterHelper());
        }

        public void SimpleWriting()
        {
            // Create XmlWriter
            XmlWriter writer = XmlWriter.Create(AppConfig.XmlFile);

            // Write a start element (Root)
            writer.WriteStartElement("Employees");
            // Write a start element (Parent)
            writer.WriteStartElement("Employee");
            
            writer.WriteStartElement("id");
            writer.WriteString("1");
            writer.WriteEndElement();

            writer.WriteStartElement("FirstName");
            writer.WriteString("Bruce");
            writer.WriteEndElement();

            writer.WriteStartElement("LastName");
            writer.WriteString("Jones");
            writer.WriteEndElement();

            // Write a closing element (Parent)
            writer.WriteEndElement();
            // Write a closing element (Root)
            writer.WriteEndElement();
            writer.Close();

            StringBuilder builder = new StringBuilder();
            builder.Append(File.ReadAllText(AppConfig.XmlFile));
            Console.Write(builder.ToString());
        }

        public void WritingAttributes()
        {
            // Create XmlWriter
            XmlWriter writer = XmlWriter.Create(AppConfig.XmlFile);

            // Write a start element (Root)
            writer.WriteStartElement("Employees");
            // Write a start element (Parent)
            writer.WriteStartElement("Employee");

            writer.WriteAttributeString("id", "1");

            writer.WriteStartElement("FirstName");
            writer.WriteString("Bruce");
            writer.WriteEndElement();

            writer.WriteStartElement("LastName");
            writer.WriteString("Jones");
            writer.WriteEndElement();

            // Write a closing element (Parent)
            writer.WriteEndElement();
            // Write a closing element (Root)
            writer.WriteEndElement();
            writer.Close();

            StringBuilder builder = new StringBuilder();
            builder.Append(File.ReadAllText(AppConfig.XmlFile));
            Console.Write(builder.ToString());
        }

        public void Formatting()
        {
            // Create XmlWriter
            XmlWriter writer;
            XmlWriterSettings settings = new XmlWriterSettings();

            // Set the Format options
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;

            writer = XmlWriter.Create(AppConfig.XmlFile, settings);

            // Write a start element (Root)
            writer.WriteStartElement("Employees");
            // Write a start element (Parent)
            writer.WriteStartElement("Employee");

            writer.WriteAttributeString("id", "1");

            writer.WriteStartElement("FirstName");
            writer.WriteString("Bruce");
            writer.WriteEndElement();

            writer.WriteStartElement("LastName");
            writer.WriteString("Jones");
            writer.WriteEndElement();

            // Write a closing element (Parent)
            writer.WriteEndElement();
            // Write a closing element (Root)
            writer.WriteEndElement();
            writer.Close();

            StringBuilder builder = new StringBuilder();
            builder.Append(File.ReadAllText(AppConfig.XmlFile));
            Console.Write(builder.ToString());
        }

        public void WriteToStringBuilder()
        {
            // Create XmlWriter
            XmlWriter writer;
            XmlWriterSettings settings = new XmlWriterSettings();
            StringBuilder builder = new StringBuilder();            

            // Set the Format options
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;

            writer = XmlWriter.Create(builder, settings);

            // Write a start element (Root)
            writer.WriteStartElement("Employees");
            // Write a start element (Parent)
            writer.WriteStartElement("Employee");

            writer.WriteAttributeString("id", "1");

            writer.WriteStartElement("FirstName");
            writer.WriteString("Bruce");
            writer.WriteEndElement();

            writer.WriteStartElement("LastName");
            writer.WriteString("Jones");
            writer.WriteEndElement();

            // Write a closing element (Parent)
            writer.WriteEndElement();
            // Write a closing element (Root)
            writer.WriteEndElement();
            writer.Close();
            
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
            var xml = "<Employees> <Employee> <id>1</id> <FirstName>Bruce</FirstName> <LastName>Jones</LastName> </Employee> <Employee> <id>2</id> <FirstName>Ken</FirstName> <LastName>Getz</LastName> </Employee> <Employee> <id>3</id> <FirstName>Rob</FirstName> <LastName>Howard</LastName> </Employee> <Employee> <id>4</id> <FirstName>Rocky</FirstName> <LastName>Lhotka</LastName> </Employee> <Employee> <id>5</id> <FirstName>Miguel</FirstName> <LastName>Castro</LastName> </Employee> <Employee> <id>6</id> <FirstName>Bill</FirstName> <LastName>Gates</LastName> </Employee> <Employee> <id>7</id> <FirstName>Jerry</FirstName> <LastName>Seinfeld</LastName> </Employee> <Employee> <id>8</id> <FirstName>Beth</FirstName> <LastName>Massi</LastName> </Employee> <Employee> <id>9</id> <FirstName>Jay</FirstName> <LastName>Roxe</LastName> </Employee> <Employee> <id>10</id> <FirstName>Andrew</FirstName> <LastName>Brust</LastName> </Employee> <Employee> <id>11</id> <FirstName>Benjamin</FirstName> <LastName>Day</LastName> </Employee> <Employee> <id>12</id> <FirstName>Jackie</FirstName> <LastName>Goldstein</LastName> </Employee> <Employee> <id>13</id> <FirstName>Robert </FirstName> <LastName>Green</LastName> </Employee> <Employee> <id>14</id> <FirstName>Fritz</FirstName> <LastName>Onion</LastName> </Employee> <Employee> <id>15</id> <FirstName>Brian</FirstName> <LastName>Randell</LastName> </Employee> <Employee> <id>16</id> <FirstName>Richard</FirstName> <LastName>Hale Shaw</LastName> </Employee> <Employee> <id>17</id> <FirstName>Scott</FirstName> <LastName>Guthrie</LastName> </Employee> <Employee> <id>18</id> <FirstName>Scott</FirstName> <LastName>Hanselman</LastName> </Employee> <Employee> <id>19</id> <FirstName>Jim</FirstName> <LastName>Ruhl</LastName> </Employee> <Employee> <id>20</id> <FirstName>John</FirstName> <LastName>Kuhn</LastName> </Employee> <Employee> <id>21</id> <FirstName>John</FirstName> <LastName>Brongo</LastName> </Employee> <Employee> <id>22</id> <FirstName>David</FirstName> <LastName>Takigawa</LastName> </Employee> <Employee> <id>23</id> <FirstName>Paul</FirstName> <LastName>Sheriff</LastName> </Employee> <Employee> <id>24</id> <FirstName>James</FirstName> <LastName>Byrd</LastName> </Employee></Employees>";
            XDocument xd = XDocument.Parse(xml);
            Console.Write(xd.ToString());
        }

        public void GetAttributes()
        {
            StringBuilder builder = new StringBuilder();
            XDocument xd = XDocument.Load(AppConfig.GetEmployeesFileWithAttributes());
            var list = xd.XPathSelectElements("//Employee");
            foreach (XElement node in list)
            {
                builder.AppendFormat("id={0}", node.Attribute("id").Value);
                builder.Append(Environment.NewLine);
                builder.AppendFormat("FirstName = {0}", node.Element("FirstName").Value);
                builder.Append(Environment.NewLine);
            }
            Console.Write(builder.ToString());
        }

        public void FindByAttribute()
        {

        }

        public void SimpleWritingVerbose()
        {
            XDocument doc;
            XElement root, child, child2;

            doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XComment("Employee Records"), new XElement("Employees"));
            root = doc.XPathSelectElement("//Employees");

            child = new XElement("Employee");
            root.Add(child);

            child2 = new XElement("id", 1);
            child.Add(child2);
            child2 = new XElement("FirstName", "Bruce");
            child.Add(child2);
            child2 = new XElement("LastName", "Jones");
            child.Add(child2);

            doc.Save(AppConfig.XmlFile);
            StringBuilder builder = new StringBuilder();
            builder.Append(File.ReadAllText(AppConfig.XmlFile));
            Console.Write(builder.ToString());
        }

        public void SimpleWriting()
        {
            XDocument doc;
            XElement root, child;

            doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XComment("Employee Records"), new XElement("Employees"));
            root = doc.XPathSelectElement("//Employees");

            child = new XElement("Employee", new XElement("id", "1"), new XElement("FirstName", "Bruce"), new XElement("LastName", "Jones"));
            root.Add(child);            

            doc.Save(AppConfig.XmlFile);
            StringBuilder builder = new StringBuilder();
            builder.Append(File.ReadAllText(AppConfig.XmlFile));
            Console.Write(builder.ToString());
        }

        public void WritingAttributes()
        {
            XDocument doc;
            XElement root, child;

            doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XComment("Employee Records"), new XElement("Employees"));
            root = doc.XPathSelectElement("//Employees");

            child = new XElement("Employee", new XAttribute("id", "1"), new XElement("FirstName", "Bruce"), new XElement("LastName", "Jones"));
            root.Add(child);

            doc.Save(AppConfig.XmlFile);
            StringBuilder builder = new StringBuilder();
            builder.Append(File.ReadAllText(AppConfig.XmlFile));
            Console.Write(builder.ToString());
        }

        public void NoFormatting()
        {
            XDocument doc;
            XElement root, child;

            doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XComment("Employee Records"), new XElement("Employees"));
            root = doc.XPathSelectElement("//Employees");

            child = new XElement("Employee", new XAttribute("id", "1"), new XElement("FirstName", "Bruce"), new XElement("LastName", "Jones"));
            root.Add(child);

            doc.Save(AppConfig.XmlFile, SaveOptions.DisableFormatting);
            StringBuilder builder = new StringBuilder();
            builder.Append(File.ReadAllText(AppConfig.XmlFile));
            Console.Write(builder.ToString());
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
            DataSet ds = new DataSet();
            StringBuilder builder = new StringBuilder(1024);

            ds.ReadXml(AppConfig.GetEmployeesFile());
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                builder.AppendFormat("{0} {1} {2}", dataRow["id"], dataRow["FirstName"], dataRow["LastName"]);
                builder.Append(Environment.NewLine);
            }
            Console.Write(builder.ToString());
        }

        public void UsingSelectMethod()
        {
            DataSet ds = new DataSet();
            StringBuilder builder = new StringBuilder(1024);
            DataRow[] rows;

            ds.ReadXml(AppConfig.GetEmployeesFile());
            rows = ds.Tables[0].Select("LastName LIKE 's%'");
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                builder.AppendFormat("{0} {1} {2}", dataRow["id"], dataRow["FirstName"], dataRow["LastName"]);
                builder.Append(Environment.NewLine);
            }
            Console.Write(builder.ToString());
        }

        public void ReadingSchema()
        {
            DataSet ds = new DataSet();
            StringBuilder builder = new StringBuilder(1024);

            ds.ReadXmlSchema(AppConfig.GetEmployeesSchemaFile());
            ds.ReadXml(AppConfig.GetEmployeesFile());

            Console.WriteLine("done");
        }

        public void ReadingSchemaBad()
        {
            DataSet ds = new DataSet();
            StringBuilder builder = new StringBuilder(1024);

            try
            {
                ds.ReadXmlSchema(AppConfig.GetEmployeesSchemaFile());
                ds.ReadXml(AppConfig.GetEmployeesBadFile());
            }
            catch (ConstraintException e)
            {
                Console.WriteLine("Constraint Violation");
            }

            Console.WriteLine("done");
        }

        public void InferSchemaWrite()
        {
            
        }

        public void StringToDataSet()
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
            StringBuilder builder = new StringBuilder(1024);
            XElement xe = XElement.Load(AppConfig.GetEmployeesFile());
            var emp = xe.XPathSelectElement("//Employee[id='1']");
            if (emp != null)
            {
                builder.AppendFormat("{0} {1} {2}", emp.Element("id").Value, emp.Element("FirstName").Value,
                    emp.Element("LastName").Value);
                builder.Append(Environment.NewLine);
            }
            Console.Write(builder.ToString());
        }

        public void SelectSingleLinq()
        {
            StringBuilder builder = new StringBuilder(1024);
            XElement xe = XElement.Load(AppConfig.GetEmployeesFile());
            var emp =
                (from e in xe.Descendants("Employee") where e.Element("id").Value == "1" select e).SingleOrDefault();
            if (emp != null)
            {
                builder.AppendFormat("{0} {1} {2}", emp.Element("id").Value, emp.Element("FirstName").Value,
                    emp.Element("LastName").Value);
                builder.Append(Environment.NewLine);
            }
            Console.Write(builder.ToString());
        }

        public void SelectMultipleXPath()
        {
            StringBuilder builder = new StringBuilder(1024);
            XElement xe = XElement.Load(AppConfig.GetEmployeesFile());
            var emp = xe.XPathSelectElements("//Employee[LastName[starts-with(.,'S')]]");
            foreach (XElement element in emp)
            {
                builder.AppendFormat("{0} {1} {2}", element.Element("id").Value, element.Element("FirstName").Value,
                    element.Element("LastName").Value);
                builder.Append(Environment.NewLine);
            }
            
            Console.Write(builder.ToString());
        }

        public void SelectMultipleLinq()
        {
            StringBuilder builder = new StringBuilder(1024);
            XElement xe = XElement.Load(AppConfig.GetEmployeesFile());
            var emp =
                (from e in xe.Descendants("Employee") where e.Element("LastName").Value.StartsWith("S") select e);
            foreach (XElement element in emp)
            {
                builder.AppendFormat("{0} {1} {2}", element.Element("id").Value, element.Element("FirstName").Value,
                    element.Element("LastName").Value);
                builder.Append(Environment.NewLine);
            }
            Console.Write(builder.ToString());
        }

        public void WriteXDocument()
        {
            
        }

        public void WriteXElement()
        {
            
        }

        public void AddElementConstructor()
        {
            
        }

        public void AddElementCloning()
        {
            
        }

        public void UpdateElement()
        {
            
        }

        public void DeleteElement()
        {
            
        }
    }
}
