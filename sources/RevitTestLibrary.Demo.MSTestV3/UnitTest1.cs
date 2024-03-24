using System;
using System.IO;
using Autodesk.Revit.DB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevitTestLibrary;


[assembly: RevitPath("D:\\Autodesk\\Revit Preview\\Revit Preview Release\\Revit.exe")]


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
        public void Revit_TestMethod_WriteLine(RevitContext revitContext)
        {
            Console.WriteLine("This code is running inside Revit.");
        }


        [TestMethod]
        public void TestMethod_Fail()
        {
            Assert.Fail("This is a standard test");
        }
        [RevitTestMethod]
        public void Revit_TestMethod_Fail(RevitContext revitContext)
        {
            Assert.Fail("This code is running inside Revit.");
        }


        [RevitTestMethod]
        public void Revit_CreateNewProjectDocument(RevitContext revitContext)
        {
            var document = revitContext.UIApplication.Application.NewProjectDocument(UnitSystem.Metric);
            Console.WriteLine(document.Title);
            document.Close(false);
        }


        [RevitTestMethod]
        public void Revit_OpenProjectDocument(RevitContext revitContext)
        {
            var path = Path.Combine(revitContext.TestAssemblyLocation, @"..\..\..\assets\rst_basic_sample_project.rvt");
            var document = revitContext.UIApplication.Application.OpenDocumentFile(path);

            var collector = new FilteredElementCollector(document).WhereElementIsNotElementType().OfCategory(BuiltInCategory.OST_Walls);
            var count = collector.GetElementCount();

            Assert.AreEqual(9, count);
            document.Close(false);
        }


        [RevitTestMethod]
        [DataRow(BuiltInCategory.OST_Walls, 9)]
        [DataRow(BuiltInCategory.OST_Columns, 0)]
        public void Revit_DataDrivenTest(RevitContext revitContext, BuiltInCategory inputCategory, int expectedCount)
        {
            var path = Path.Combine(revitContext.TestAssemblyLocation, @"..\..\..\assets\rst_basic_sample_project.rvt");
            var document = revitContext.UIApplication.Application.OpenDocumentFile(path);

            var collector = new FilteredElementCollector(document).WhereElementIsNotElementType().OfCategory(inputCategory);
            var count = collector.GetElementCount();

            Assert.AreEqual(expectedCount, count);
            document.Close(false);
        }
    }
}