---
title: Naven PowerShell module of .NET suite for Apache Maven™
_description: Describes the PowerShell module to use Apache Maven™ classes from any PowerShell shell
---

# Naven: PowerShell Module

## Installation

To install the tool executes the following command within a PowerShell shell:

```powershell
Install-Module -Name MASES.NavenPS
```

If the above command fails, reporting errors related to _authenticode_, use the following command:

```powershell
Install-Module -Name MASES.NavenPS -SkipPublisherCheck
```

## Usage

To use the PowerShell interface (NavenPS) runs the following commands within a **PowerShell** shell:

### Initialization

* The following cmdlet initialize the environment:

```powershell
Start-NavenPS [arguments]
```

### Execution

Now everything is ready and you can create objects like in the following snippet:

```powershell
$var = Open-PDF -PdfFile <path-to-PDF-file-open>
$pages = $var.NumberOfPages
$var.RemovePage(0)
Save-PDF -PdfDocument $var -OutputFile <path-to-PDF-file-to-save>
```

## Cmdlet available

_navenps_ accepts the following cmdlets:

* **Start-NavenPS**: Initialize the engine and shall be the first command to be invoked. The arguments are:
  * LicensePath
  * JDKHome
  * JVMPath
  * JNIVerbosity
  * JNIOutputFile
  * JmxPort
  * EnableDebug
  * JavaDebugPort
  * DebugSuspendFlag
  * JavaDebugOpts
  * HeapSize
  * InitialHeapSize
  * LogClassPath
* **Open-PDF**: open a PDF file. The arguments are:
  * PdfFile
* **Save-PDF**: Save a previously opened PDF file. The arguments are:
  * PdfDocument
  * OutputFile
  * CompressParameter (optional)

### JVM™ identification

One of the most important command-line switch is **JVMPath**: it can be used to set-up the location of the JVM™ library (jvm.dll/libjvm.so) if JCOBridge is not able to identify a suitable JRE installation.

