using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            raytracer.Class1 c = new raytracer.Class1();
            Assert.AreEqual<int>(5, c.add(2, 3));
        }
    }
}
