using System;
using System.Collections.Generic;
using System.Text;

namespace Zhiji.Test.Common
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class TestOrderAttribute : Attribute
    {
        public int Order { get; }

        public TestOrderAttribute(int order) => this.Order = order;
    }
}
