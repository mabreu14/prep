using System;
using System.Data;

namespace prep.learning_mspec
{
  public class Calculator
  {
    IDbConnection connection;

    public Calculator(IDbConnection connection, IDbConnection other)
    {
      this.connection = connection;
    }

    public int add(int first, int second)
    {
      if (first < 0 || second < 0) throw new ArgumentException();

      connection.Open();
	    connection.CreateCommand().ExecuteNonQuery();
      return first + second;
    }

    public void shut_off()
    {
      throw new NotImplementedException();
    }
  }
}