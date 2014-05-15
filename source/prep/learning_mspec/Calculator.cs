using System;
using System.Data;
using System.Security;
using System.Security.Principal;
using System.Threading;

namespace prep.learning_mspec
{
  public interface ICalculate
  {
    int add(int first, int second);
    void shut_off();
  }

  public class Calculator : ICalculate
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
      connection.CreateCommand().ExecuteNonQuery();
      return first + second;
    }

    public void shut_off()
    {
      if (Thread.CurrentPrincipal.IsInRole("sdfsf")) return;

      throw new SecurityException();
    }
  }
}