using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlPlayground.Worker
{
    public class XmlReaderHelper
    {
        private static XmlReaderHelper _instance;
        private XmlReaderHelper()
        {            
        }

        public static XmlReaderHelper GetInstance()
        {
            return _instance ?? (_instance = new XmlReaderHelper());
        }

        public void UsingSimpleProcessing()
        {
            
        }

        public void UsingMoveToContent()
        {
            
        }

        public void UsingReadStartElement()
        {
            
        }

        public void UsingReadElementString()
        {
            
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
            
        }

        public void ReadEmployeeNodes()
        {
            
        }

        public void ReadAllNodes()
        {
            
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
