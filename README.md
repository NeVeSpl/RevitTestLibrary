[![Nuget](https://img.shields.io/nuget/v/RevitTestLibrary?color=%23004880&label=RevitTestLibrary%20nugets)](https://www.nuget.org/packages?q=RevitTestLibrary)

# RevitTestLibrary (RTL)
Proof of Concept that it is possible to run and debug unit tests in Visual Studio with remote execution inside Revit.

![proof-of-concept](documentation/proof-of-concept.gif)

The first results are very promising, it may someday be ready to be used on production.

## Origin

- [RevitTestFramework ](https://github.com/DynamoDS/RevitTestFramework)
- [Revit.TestRunner](https://github.com/geberit/Revit.TestRunner)
- [xUnitRevit](https://github.com/specklesystems/xUnitRevit)

## Getting started

1) Add information about Revit location to `.runsettings` file. [How to use `.runsettings` on Microsoft learn.](https://learn.microsoft.com/en-us/visualstudio/test/configure-unit-tests-by-using-a-dot-runsettings-file?view=vs-2022)
```xml
<RunSettings>
  <MSTest> 
    <AssemblyResolution>
      <Directory path="D:\Autodesk\Revit 2023\" includeSubDirectories="false"/>
    </AssemblyResolution>
  </MSTest>
</RunSettings>
```

2) Install nuget

[https://www.nuget.org/packages/RevitTestLibrary.MSTest](https://www.nuget.org/packages/RevitTestLibrary.MSTest)

3) Change test method attribute to `[RevitTestMethod]` and add one input parameter of type `UIApplication`

```csharp
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
    }
}
```

## Current limitations
 - works only with : Revit 2023 and Visual Studio 2022
 - theoretically, it can be used with any test framework, but right now only integration with MSTest v3 is available
 - it is slow and memory-hungry
