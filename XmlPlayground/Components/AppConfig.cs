using System;
using System.Configuration;

namespace XML_Processing
{
  public class AppConfig
  {
    public static string ConnectString
    {
      get { return ConfigurationManager.ConnectionStrings["AdvWorks"].ConnectionString; }
    }

    public static string XmlFile
    {
      get { return ConfigurationManager.AppSettings["XmlFile"]; }
    }

    public static string XsdFile
    {
      get { return ConfigurationManager.AppSettings["XsdFile"]; }
    }

    public static string GetCurrentDirectory()
    {
      string file;

      file = AppDomain.CurrentDomain.BaseDirectory;
      if (file.IndexOf(@"\bin") > 0)
      {
        file = file.Substring(0, file.LastIndexOf(@"\bin"));
      }

      return file;
    }

    public static string GetEmployeesFile()
    {
      string file;

      file = GetCurrentDirectory();
      file += @"\XML\Employees.xml";

      return file;
    }

    public static string GetEmployeesSchemaFile()
    {
      string file;

      file = GetCurrentDirectory();
      file += @"\XML\Employees.xsd";

      return file;
    }

    public static string GetEmployeesBadFile()
    {
      string file;

      file = GetCurrentDirectory();
      file += @"\XML\Employees_BAD.xml";

      return file;
    }

    public static string GetEmployeesFileWithAttributes()
    {
      string file;

      file = GetCurrentDirectory();
      file += @"\XML\EmployeesAttributes.xml";

      return file;
    }
    
    public static string GetSampleAppConfig()
    {
      string file;

      file = GetCurrentDirectory();
      file += @"\SampleApp.Config";

      return file;
    }
  }
}
