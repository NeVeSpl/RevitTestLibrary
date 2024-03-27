[![Nuget](https://img.shields.io/nuget/v/RevitTestLibrary?color=%23004880&label=RevitTestLibrary%20nugets)](https://www.nuget.org/packages?q=RevitTestLibrary)

> **Warning**
Since version v.0.1 the RTL switched to .net 8 (Revit 2025), any further development on .net framework 4.8 (Revit 2023, 2024) is not planned.



# RevitTestLibrary (RTL)
Proof of Concept that it is possible to run and debug unit tests in Visual Studio with remote execution inside Revit.

![proof-of-concept](https://raw.githubusercontent.com/NeVeSpl/RevitTestLibrary/main/documentation/proof-of-concept.gif)


## Origin

- [RevitTestFramework ](https://github.com/DynamoDS/RevitTestFramework)
- [Revit.TestRunner](https://github.com/geberit/Revit.TestRunner)
- [xUnitRevit](https://github.com/specklesystems/xUnitRevit)

There are three established solutions for unit testing in Revit space, but none of them is integrated with the IDE. This makes them good mostly for detecting regressions, which makes QA happy. We, developers, love and need unit tests for different reasons. There is no better way of writing a code, than running a small fragment of it in isolation without the need to start the whole (big (and slow)) application. We need as fast feedback loop as possible. 
<h4 align="center">
Red, Green, Refactor, Repeat
</h4>

## Features
- does not keep dlls locked after conducting a unit test, which means Revit does not have to be closed down in order to recompile dlls
- gives full access to the exception stack trace with interactive links
- allows to mix unit tests that are run inside Revit context with tests that can be run without Revit context
- unit tests are run in a special instance of Revit that does not load any addins.

## Getting started

1) Add information about Revit location to `.runsettings` file. [How to use `.runsettings` on Microsoft learn.](https://learn.microsoft.com/en-us/visualstudio/test/configure-unit-tests-by-using-a-dot-runsettings-file?view=vs-2022)
```xml
<RunSettings>
  <MSTest> 
    <AssemblyResolution>
      <Directory path="D:\Autodesk\Revit 2025\" includeSubDirectories="false"/>
    </AssemblyResolution>
  </MSTest>
</RunSettings>
```

2) Install nuget

[https://www.nuget.org/packages/RevitTestLibrary.MSTest](https://www.nuget.org/packages/RevitTestLibrary.MSTest)

3) Change test method attribute to `[RevitTestMethod]` and add one input parameter of type `RevitContext`
4) Add the RevitPath attribute `[assembly: RevitPath("D:\\Autodesk\\Revit 2025\\Revit.exe")]`.

```csharp
using System;
using System.IO;
using Autodesk.Revit.DB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevitTestLibrary;

[assembly: RevitPath("D:\\Autodesk\\Revit 2025\\Revit.exe")]

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
        public void Revit_TestMethod_WriteLine(RevitContext context)
        {
            Console.WriteLine("This code is running inside Revit.");
        }
    }
}
```

## Demo
[The Demo project is available here: RevitTestLibrary.Demo.MSTestV3](https://github.com/NeVeSpl/RevitTestLibrary/tree/main/sources/RevitTestLibrary.Demo.MSTestV3)

## Current limitations
 - works only with : Revit 2025 and Visual Studio 2022
 - theoretically, it can be used with any test framework, but right now only integration with MSTest v3 is available
