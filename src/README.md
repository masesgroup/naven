## Generated classes

The command used to build the classes is the following:

1. Download the latest version of reflection utility:

```cmd
dotnet tool update -g MASES.JNetReflector
```

2. Run the reflection utility:

```cmd
jnetreflector -TraceLevel 0 -OriginRootPath .\jars -DestinationRootPath .\src\ -ConfigurationFile .\src\configuration.json
```

The configuration is:

```json
{
  "RelativeDestinationCSharpClassPath": "net\\Naven\\Generated",
  "RelativeDestinationJavaListenerPath": "jvm\\naven\\src\\main\\java",
  "JavaListenerBasePackage": "org.mases.naven.generated",
  "PreferMethodWithSignature": true,
  "OnlyPropertiesForGetterSetter": true,
  "DisableInterfaceMethodGeneration": true,
  "CreateInterfaceInheritance": true,
  "JarList": [
    "maven-embedder-3.9.14.jar"
  ],
  "OriginJavadocJARVersionAndUrls": [
    {
      "Version": 11,
      "Url": "https://www.javadoc.io/doc/org.apache.maven/maven-embedder/3.9.14/"
    }
  ],
  "NamespacesToAvoid": [
    "org.apache.commons.logging",
    "org.apache.commons.cli",
    "org.apache.maven.eventspy",
    "org.apache.maven.execution",
    "org.apache.maven.extension",
    "org.codehaus",
    "org.eclipse",
    "org.slf4j"
  ],
  "ClassesToBeListener": [
  ],
  "ClassesToAvoid": [
  ],
  "ClassesManuallyDeveloped": [
  ],
  "NamespacesInConflict": [
    "java.lang.module",
    "java.awt.color",
    "java.awt.desktop",
    "java.awt.image",
    "java.awt.event",
    "java.awt.font"
  ]
}
```