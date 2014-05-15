using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;
using prep.collections;
using Rhino.Mocks.Impl;

namespace prep.specs
{
    class ReadOnlySetSpecs
    {
        public class concern_for_ReadOnly : Observes<IEnumerable<double>, ReadOnlySet<double>>
        {
            
        }

        public class when_ReadOnly_is_instantiated : concern_for_ReadOnly
        {
            private Establish c = () => items = depends.on<IEnumerable<double>>();

            It should_have_the_double_set = () => items.Equals(new List<double>(){1,2,3,4});

            static IEnumerable<double> items = new List<double>(){1,2,3,4};
        }

        public class when_getting_the_Enumerable : concern_for_ReadOnly
        {
            
        }
    }
}
