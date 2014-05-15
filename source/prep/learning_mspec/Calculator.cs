using System;
using System.Data;
using System.Security;
using System.Security.Principal;

namespace prep.learning_mspec
{
  public class Calculator
  {
        IDbConnection connection;
        IPrincipal _principal;

      public Calculator(IDbConnection connection, IPrincipal principal)
    {
        this.connection = connection;
        _principal = principal;
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
        throw new SecurityException();
        if (!_principal.IsInRole("hello"))
            throw new SecurityException();
    }
  }
}