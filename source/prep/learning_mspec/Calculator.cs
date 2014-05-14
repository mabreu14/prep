using System;
using System.Data;

namespace prep.learning_mspec
{
  public class Calculator
  {
      public IDbConnection Connection { get; set; }

      public Calculator(IDbConnection connection)
      {
          Connection = connection;
      }

      public int add(int first, int second)
    {
        Connection.Open();
        if(first <0 || second <0) throw new ArgumentException();
	    return first + second;
    }
  }
}