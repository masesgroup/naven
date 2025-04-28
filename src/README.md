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
  "JVMOptions": [
    {
      "Name": "maven.fontcache",
      "Value": "$(FONTCACHE_FOLDER)"
    }
  ],
  "RelativeDestinationCSharpClassPath": "net\\Naven\\Generated",
  "RelativeDestinationJavaListenerPath": "jvm\\naven\\src\\main\\java",
  "JavaListenerBasePackage": "org.mases.naven.generated",
  "PreferMethodWithSignature": true,
  "OnlyPropertiesForGetterSetter": true,
  "DisableInterfaceMethodGeneration": true,
  "CreateInterfaceInheritance": true,
  "JarList": [
    "maven-3.0.4.jar",
    "maven-io-3.0.4.jar",
    "fontbox-3.0.4.jar"
  ],
  "OriginJavadocJARVersionAndUrls": [
    {
      "Version": 8,
      "Url": "https://www.javadoc.io/doc/org.apache.maven/maven/3.0.4/"
    },
    {
      "Version": 8,
      "Url": "https://www.javadoc.io/doc/org.apache.maven/maven-io/3.0.4/"
    },
    {
      "Version": 8,
      "Url": "https://www.javadoc.io/doc/org.apache.maven/fontbox/3.0.4/"
    }
  ],
  "NamespacesToAvoid": [
    "org.apache.commons.logging"
  ],
  "ClassesToBeListener": [
    "org.apache.maven.pdmodel.common.function.type4.Parser$SyntaxHandler"
  ],
  "ClassesToAvoid": [
    "org.apache.maven.pdmodel.common.function.type4.Parser$AbstractSyntaxHandler"
  ],
  "ClassesManuallyDeveloped": [
    "org.apache.maven.text.TextPositionComparator"
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