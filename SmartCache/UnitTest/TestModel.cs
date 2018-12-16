using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    public class TestModel
    {
        public string first { get; set; }
        public string second { get; set; }
        public string third { get; set; }

        public override bool Equals(object obj)
        {
            var m = (TestModel)obj;
            return m.first == first && m.second == second && m.third == third;
        }
    }
}
