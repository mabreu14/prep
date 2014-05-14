using System;
using System.Data;

namespace prep.learning_mspec
{
  public class Calculator
  {
    IDbConnection connection;

    public Calculator(IDbConnection connection)
    {
      this.connection = connection;
    }

    public int add(int first, int second)
    {
      if (first < 0 || second < 0) throw new ArgumentException();

      connection.Open();
      return first + second;
    }
  }
}