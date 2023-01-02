using System;
using Autodesk.Revit.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevitTestLibrary.MSTest;

namespace RevitTestLibrary.Demo.MSTestV3
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod_WriteLine()
        {
            Console.WriteLine("This is a standard test");
        }

        [RevitTestMethod]
        public void Revit_TestMethod_WriteLine(UIApplication uia)
        {
            Console.WriteLine("This code is running inside Revit.");
        }


        [TestMethod]
        public void TestMethod_Fail()
        {
            Assert.Fail("This is a standard test");
        }

        [RevitTestMethod]
        public void Revit_TestMethod_Fail(UIApplication uia)
        {
            Assert.Fail("This code is running inside Revit.");
        }
    }
}
