using System;
using System.Data;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;
using prep.learning_mspec;

namespace prep.specs
{
  public class CalculatorSpecs
  {
    public class concern_for_calculator : Observes<Calculator>
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

    public class when_adding_two_numbers : concern_for_calculator
    {
      public class and_they_are_both_positive
      {
        Establish c = () =>
        {
          connection = depends.on<IDbConnection>();          
        };

        Because b = () =>
          result = sut.add(2, 3);

        It should_return_the_sum = () =>
          result.ShouldEqual(5);

        It should_open_a_connection_to_the_database = () =>
          connection.received(x => x.Open());

        static int result;
        static IDbConnection connection;
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