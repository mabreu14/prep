using System;
using System.Data;
using System.Security;
using System.Security.Principal;
using System.Threading;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;
using prep.learning_mspec;
using Rhino.Mocks;

namespace prep.specs
{
  public class CalculatorSpecs
  {
    public class concern_for_calculator : Observes<ICalculate, Calculator>
    {
    }

    public class when_the_calculator_is_instantiated : concern_for_calculator
    {
      Establish c = () =>
        connection = depends.on<IDbConnection>();

      It should_not_have_opened_its_connection = () =>
        connection.never_received(x => x.Open());

      static IDbConnection connection;
    }

    public class when_shutting_off_the_calculator : concern_for_calculator
    {
      public class and_they_are_not_in_the_correct_security_group
      {
        Establish c = () =>
        {
          principal = fake.an<IPrincipal>();
          principal.setup(x => x.IsInRole(Arg<string>.Is.Anything)).Return(false);

          spec.change(() => Thread.CurrentPrincipal).to(principal);
        };

        Because b = () =>
          spec.catch_exception(() => sut.shut_off());

        It should_report_inability_to_shut_off = () =>
          spec.exception_thrown.ShouldBeAn<SecurityException>();

        static IPrincipal principal;
      }

      public class and_they_are_in_the_correct_security_group
      {
        Establish c = () =>
        {
          principal = fake.an<IPrincipal>();
          principal.setup(x => x.IsInRole(Arg<string>.Is.Anything)).Return(true);

          spec.change(() => Thread.CurrentPrincipal).to(principal);
        };

        Because b = () =>
          sut.shut_off();

        It should_not_do_anything = () =>
        {

        };
        static IPrincipal principal;
      }
    }
    public class when_adding_two_numbers : concern_for_calculator
    {
      public class and_they_are_both_positive
      {
        Establish c = () =>
        {
          connection = depends.on<IDbConnection>();
          command = fake.an<IDbCommand>();

          connection.setup(x => x.CreateCommand()).Return(command);
        };

        Because b = () =>
          result = sut.add(2, 3);

        It should_return_the_sum = () =>
          result.ShouldEqual(5);

        It should_open_a_connection_to_the_database = () =>
          connection.received(x => x.Open());

        It should_run_a_query = () =>
          command.received(x => x.ExecuteNonQuery());

        static int result;
        static IDbConnection connection;
        static IDbCommand command;
      }

      public class and_there_are_negative_numbers_involved
      {
        Because b = () =>
          spec.catch_exception(() => sut.add(2, -3));

        It should_throw_an_argument_exception = () =>
          spec.exception_thrown.ShouldBeAn<ArgumentException>();
      }
    }
  }
}
