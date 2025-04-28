---
title: Current state of .NET suite for Apache Maven™
_description: Describes the current development state of .NET suite for Apache Maven™
---

# Naven development state

This release comes with some ready made classes:

* Naven:
  * Reflected almost all classes of a Temurin JDK 11 with the limits imposed from JNetReflector
  * Manually made some classes, or extended some of reflected one, due to limitations of JNetReflector
  * If something is not available use [API extensibility](API_extensibility.md) to cover missing classes.
* NavenCLI: added REPL shell, run Main-Class and execute C# scripts
* NavenPS: some PowerShell cmdlets