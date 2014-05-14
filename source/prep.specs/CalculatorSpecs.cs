using System;
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

    public class when_adding_two_numbers : concern_for_calculator
    {
      public class and_they_are_both_positive
      {
        Because b = () =>
          result = sut.add(2, 3);

        It should_return_the_sum = () =>
          result.ShouldEqual(5);

        static int result;
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