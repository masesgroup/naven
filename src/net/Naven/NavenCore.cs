﻿/*
*  Copyright 2025 MASES s.r.l.
*
*  Licensed under the Apache License, Version 2.0 (the "License");
*  you may not use this file except in compliance with the License.
*  You may obtain a copy of the License at
*
*  http://www.apache.org/licenses/LICENSE-2.0
*
*  Unless required by applicable law or agreed to in writing, software
*  distributed under the License is distributed on an "AS IS" BASIS,
*  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
*  See the License for the specific language governing permissions and
*  limitations under the License.
*
*  Refer to LICENSE for more information.
*/

using MASES.CLIParser;
using System;
using System.Collections.Generic;
using System.IO;
using MASES.JNet;
using MASES.JCOBridge.C2JBridge;
using System.Linq;

namespace MASES.Naven
{
    /// <summary>
    /// Public entry point of <see cref="NavenCore{T}"/>
    /// </summary>
    /// <typeparam name="T">A class which inherits from <see cref="NavenCore{T}"/></typeparam>
    public class NavenCore<T> : JNetCore<T>
        where T : NavenCore<T>
    {
        /// <inheritdoc cref="JNetCoreBase{T}.CommandLineArguments"/>
        public override IEnumerable<IArgumentMetadata> CommandLineArguments
        {
            get
            {
                var lst = new List<IArgumentMetadata>(base.CommandLineArguments);
                lst.AddRange(new IArgumentMetadata[]
                {
                    new ArgumentMetadata<string>()
                    {
                        Name = CLIParam.ClassToRun,
                        Help = "The class to be instantiated from CLI.",
                    },
                    new ArgumentMetadata<string>()
                    {
                        Name = CLIParam.CommonLoggingConfiguration,
                        Default = DefaultCommonLoggingConfiguration(),
                        Help = "The file containing the configuration of common logging.",
                    },
                    new ArgumentMetadata<string>()
                    {
                        Name = CLIParam.LogPath,
                        Default = Const.DefaultLogPath,
                        Help = "The path where log will be stored.",
                    },
                    new ArgumentMetadata<string>()
                    {
                        Name = CLIParam.FontCachePath,
                        Help = "The path where font cache will be stored.",
                    },
                    new ArgumentMetadata<object>()
                    {
                        Name = CLIParam.UsePureJavaCMYKConversion,
                        Help = "Add on command line to enable pure Java CMYK conversion.",
                    },
                });
                return lst;
            }
        }
        /// <summary>
        /// Returns the default configuration file to use when initializing command line defaults
        /// </summary>
        /// <returns>The configuration file to use for logging</returns>
        /// <remarks>Overrides in derived classes to give another default file</remarks>
        protected virtual string DefaultCommonLoggingConfiguration()
        {
            return Const.DefaultCommonLoggingConfigurationPath;
        }

        /// <summary>
        /// Public initializer
        /// </summary>
        public NavenCore()
        {
            JCOBridge.C2JBridge.JCOBridge.RegisterExceptions(typeof(NavenCore<>).Assembly);
        }

        /// <inheritdoc cref="JNetCoreBase{T}.ProcessCommandLine" />
        protected override string[] ProcessCommandLine()
        {
            var result = base.ProcessCommandLine();

            _classToRun = ParsedArgs.Get<string>(CLIParam.ClassToRun);
            _commonLoggingPath = ParsedArgs.Get<string>(CLIParam.CommonLoggingConfiguration);
            if (!Path.IsPathRooted(_commonLoggingPath)) // it is not a full path
            {
                var absolutePath = Path.Combine(Const.DefaultConfigurationPath, _commonLoggingPath);
                if (File.Exists(absolutePath))
                {
                    _commonLoggingPath = absolutePath;
                }
                else
                {
                    throw new ArgumentException($"{_commonLoggingPath} is not an absolute path and there is no file under {Const.DefaultConfigurationPath} whose absolute path is {absolutePath}");
                }
            }
            _logPath = ParsedArgs.Get<string>(CLIParam.LogPath);
            _fontCachePath = ParsedArgs.Get<string>(CLIParam.FontCachePath);
            _usePureJavaCMYKConversion = ParsedArgs.Exist(CLIParam.UsePureJavaCMYKConversion);
            return result;
        }
        /// <summary>
        /// Prepare <see cref="MainClassToRun"/> property
        /// </summary>
        /// <param name="className">The class to search</param>
        /// <exception cref="ArgumentException">If <paramref name="className"/> does not have a corresponding implemented <see cref="Type"/></exception>
        protected virtual void PrepareMainClassToRun(string className)
        {
            if (string.IsNullOrWhiteSpace(className)) return;
            var invariantLowClassName = className.ToLowerInvariant();
            Type type = null;
            foreach (var item in typeof(NavenCore<>).Assembly.ExportedTypes)
            {
                if (item.Name.ToLowerInvariant() == invariantLowClassName
                    || item.FullName.ToLowerInvariant() == invariantLowClassName)
                {
                    type = item;
                    break;
                }
            }
            MainClassToRun = type ?? throw new ArgumentException($"Requested class {className} is not a valid class name.");
        }

        /// <summary>
        /// Sets the <see cref="Type"/> to be invoked at startup
        /// </summary>
        public static Type MainClassToRun { get; protected set; }

        /// <summary>
        /// Sets the global value of class to run
        /// </summary>
        public static string ApplicationClassToRun { get; set; }

        /// <summary>
        /// Sets the global value of log4j path
        /// </summary>
        public static string ApplicationCommonLoggingPath { get; set; }

        /// <summary>
        /// Sets the global value of log path
        /// </summary>
        public static string ApplicationLogPath { get; set; }

        /// <summary>
        /// Sets the global value of font cache path
        /// </summary>
        public static string ApplicationFontCachePath { get; set; }

        /// <summary>
        /// <see langword="true"/> to enable pure Java CMYK conversion
        /// </summary>
        public static bool? ApplicationUsePureJavaCMYKConversion { get; set; }

        /// <summary>
        /// value can be overridden in subclasses
        /// </summary>
        protected string _classToRun;
        /// <summary>
        /// The class to run in CLI version
        /// </summary>
        public virtual string ClassToRun { get { return ApplicationClassToRun ?? _classToRun; } }

        string _commonLoggingPath;
        /// <summary>
        /// The log4j folder
        /// </summary>
        public virtual string CommonLoggingPath { get { return ApplicationCommonLoggingPath ?? _commonLoggingPath; } }

        string _logPath;
        /// <summary>
        /// The log folder
        /// </summary>
        public virtual string LogDir { get { return ApplicationLogPath ?? _logPath; } }

        string _fontCachePath;
        /// <summary>
        /// The log folder
        /// </summary>
        public virtual string FontCachePath { get { return ApplicationFontCachePath ?? _fontCachePath; } }

        bool _usePureJavaCMYKConversion;
        /// <summary>
        /// <see langword="true"/> to enable pure Java CMYK conversion
        /// </summary>
        public virtual bool UsePureJavaCMYKConversion { get { return !ApplicationUsePureJavaCMYKConversion.HasValue ? _usePureJavaCMYKConversion : ApplicationUsePureJavaCMYKConversion.Value; } }

        /// <summary>
        /// The log4j configuration
        /// </summary>
        public virtual string CommonLoggingOpts { get { return string.Format("file:{0}", Path.Combine(Const.DefaultRootPath, "config", "naven-log4j.properties")); } }

        /// <inheritdoc cref="JNetCore{T}.PerformanceOptions"/>
        protected override IList<string> PerformanceOptions
        {
            get
            {
                var lst = new List<string>(base.PerformanceOptions);
                lst.AddRange(new string[]
                {
                    // "-server", <- Disabled because it avoids starts of embedded JVM
                    "-XX:+UseG1GC",
                    "-XX:MaxGCPauseMillis=20",
                    "-XX:InitiatingHeapOccupancyPercent=35",
                    "-XX:+ExplicitGCInvokesConcurrent",
                });
                return lst;
            }
        }

        /// <inheritdoc cref="JNetCore{T}.Options"/>
        protected override IDictionary<string, string> Options
        {
            get
            {
                if (!Directory.Exists(LogDir)) Directory.CreateDirectory(LogDir);

                IDictionary<string, string> options = new Dictionary<string, string>(base.Options)
                {
                    { "log4j.configuration", string.IsNullOrEmpty(CommonLoggingPath) ? CommonLoggingOpts : $"file:{CommonLoggingPath}"},
                    { "naven.logs.dir", LogDir},
                    { "java.awt.headless", "true" },
                    { "classworlds.conf", Path.Combine(Const.DefaultMavenHomePath, "bin", "m2.conf") },
                    { "maven.home", Const.DefaultMavenHomePath },
                    { "library.jansi.path", Path.Combine(Const.DefaultMavenHomePath, "lib", "jansi-native") },

                };

                return options;
            }
        }

        /// <inheritdoc cref="JNetCore{T}.PathToParse"/>
        protected override IList<string> PathToParse
        {
            get
            {
                var lst = new List<string>(base.PathToParse);
                var assembly = typeof(NavenCore<>).Assembly;
                var version = assembly.GetName().Version.ToString();
                // 1. check first full version
                var navenFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(assembly.Location), JARsSubFolder, $"naven-{version}.jar");
                if (!System.IO.File.Exists(navenFile) && version.EndsWith(".0"))
                {
                    // 2. if not exist remove last part of version
                    version = version.Substring(0, version.LastIndexOf(".0"));
                    navenFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(assembly.Location), JARsSubFolder, $"naven-{version}.jar");
                }
                // 3. check if naven jar exist...
                if (!System.IO.File.Exists(navenFile))
                {
                    throw new System.IO.FileNotFoundException("Unable to identify Naven Jar location", navenFile);
                }
                // 4. add naven jar at this version first...
                lst.Add(navenFile);
                // 5. ...then add everything else
                lst.Add(Path.Combine(Const.DefaultPlexusPath, "*.jar"));
                lst.Add(Path.Combine(Const.DefaultLibPath, "*.jar"));
                return lst;
            }
        }

#if DEBUG
        /// <inheritdoc cref="JNetCoreBase{T}.EnableDebug"/>
        public override bool EnableDebug => true;
#endif
    }
}