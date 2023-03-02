using System;
using System.IO;
using System.Reflection;
using Autodesk.Revit.DB;
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


        [RevitTestMethod]
        public void Revit_CreateNewProjectDocument(UIApplication uia)
        {
            var document = uia.Application.NewProjectDocument(UnitSystem.Metric);
            Console.WriteLine(document.Title);
            document.Close(false);
        }


        [RevitTestMethod]
        public void Revit_OpenProjectDocument(UIApplication uia)
        {
            var path = Path.Combine(GetTestDirectory(), @"..\..\assets\rst_basic_sample_project.rvt");
            var document = uia.Application.OpenDocumentFile(path);

            var collector = new FilteredElementCollector(document).WhereElementIsNotElementType().OfCategory(BuiltInCategory.OST_Walls);
            var count = collector.GetElementCount();

            Assert.AreEqual(9, count);
            document.Close(false);
        }


        [RevitTestMethod]
        [DataRow(BuiltInCategory.OST_Walls, 9)]
        [DataRow(BuiltInCategory.OST_Columns, 0)]
        public void Revit_DataDrivenTest(UIApplication uia, BuiltInCategory inputCategory, int expectedCount)
        {
            var path = Path.Combine(GetTestDirectory(), @"..\..\assets\rst_basic_sample_project.rvt");
            var document = uia.Application.OpenDocumentFile(path);

            var collector = new FilteredElementCollector(document).WhereElementIsNotElementType().OfCategory(inputCategory);
            var count = collector.GetElementCount();

            Assert.AreEqual(expectedCount, count);
            document.Close(false);
        }


        private static string GetTestDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}