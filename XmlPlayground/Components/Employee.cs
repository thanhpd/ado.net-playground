using System;

namespace XML_Processing
{
  [Serializable()]
  public class Employee
  {
    private int _Id = default(int);
    private string _FirstName = default(string);
    private string _LastName = default(string);

    public int Id
    {
      get { return _Id; }
      set { _Id = value; }
    }

    public string FirstName
    {
      get { return _FirstName; }
      set { _FirstName = value; }
    }

    public string LastName
    {
      get { return _LastName; }
      set { _LastName = value; }
    }
  }
}
