using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlPlayground.Worker
{
    public class XmlWorkerHelper
    {
        private static XmlWorkerHelper _instance;
        private XmlWorkerHelper() { }

        public static XmlWorkerHelper GetInstance()
        {
            return _instance ?? (_instance = new XmlWorkerHelper());
        }
    }

    public class 
}
