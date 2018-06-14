using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Zhiji.Test.Common
{
    public class ExplicitTestCaseOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) 
            where TTestCase : ITestCase
        {
            return from c in testCases
                   let n = c.TestMethod.Method.ToRuntimeMethod().GetCustomAttribute<TestOrderAttribute>()
                   orderby n is null ? 0 : n.Order
                   select c;
        }
    }
}
