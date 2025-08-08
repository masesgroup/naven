﻿/*
*  Copyright (c) 2024-2025 MASES s.r.l.
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
using MASES.JNet;
using MASES.JNet.Specific.CLI;
using System;
using System.Collections.Generic;

namespace MASES.Naven.CLI
{
    /// <summary>
    /// Overridable implementation of <see cref="NavenCore{T}"/>
    /// </summary>
    public class NavenCLICore<T> : NavenCore<T>
        where T : NavenCLICore<T>
    {
        /// <inheritdoc cref="JNetCoreBase{T}.CommandLineArguments"/>
        public override IEnumerable<IArgumentMetadata> CommandLineArguments => base.CommandLineArguments.SetCLICommandLineArguments();

        /// <summary>
        /// Public ctor
        /// </summary>
        public NavenCLICore()
        {
            this.InitCLI();
        }

        protected override string[] ProcessCommandLine()
        {
            JNetCLICoreHelper.DefaultClassToRun = "Launcher";

            var result = base.ProcessCommandLine(); // returns the filtered args till now
            return this.ProcessCLIParsedArgs(result, settingsCallback: (className) =>
            {
                switch (className)
                {
                    default:
                        ApplicationInitialHeapSize = ApplicationHeapSize = Environment.Is64BitOperatingSystem ? "512G" : "256M";
                        break;
                }
            });
        }

        /// <inheritdoc cref="JNetCoreBase{T}.PathToParse"/>
        protected override IList<string> PathToParse => base.PathToParse.SetCLIPathToParse();
    }

    /// <summary>
    /// Directly usable implementation of <see cref="NavenCLICore{T}"/>
    /// </summary>
    internal class NavenCLICore : NavenCLICore<NavenCLICore>
    {
    }
}
