/*
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

using Java.Io;
using MASES.JCOBridge.C2JBridge;
using MASES.JNet.PowerShell;
using MASES.JNet.PowerShell.Cmdlet;
using MASES.JNet.Specific.Extensions;
using Org.Codehaus.Plexus.Classworlds.Launcher;
using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Reflection;

namespace MASES.Naven.PowerShell.Cmdlet
{
    [Cmdlet(VerbsLifecycle.Start, "Launcher")]
    [JNetPSExternalize]
    public class StartLauncherCmdletCommand : StartNavenPSCmdletCommandBase<StartNavenPSCmdletCommand>
    {
        [Parameter(
            Mandatory = false,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The optional argument for POM file")]
        public string File { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 1,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The stage argument for Maven execution")]
        public string Stage { get; set; }

        [Parameter(
            Mandatory = false,
            Position = 2,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The arguments to use with launcher")]
        public string Arguments { get; set; }

        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            WriteVerbose("Begin StartLauncherCmdletCommand!");
        }

        string[] argsFromArguments()
        {
            string[] arguments = string.IsNullOrWhiteSpace(Arguments) ? Array.Empty<string>() : Arguments.Split(' ');
            return arguments;
        }

        protected override void OnBeforeCreateGlobalInstance()
        {
            base.OnBeforeCreateGlobalInstance();
            var multiModuleProjectDirectory = Environment.CurrentDirectory;
            if (!string.IsNullOrWhiteSpace(File))
            {
                multiModuleProjectDirectory = Path.GetDirectoryName(File);
            }
            NavenPSHelper<NavenPSCore>.SetMultiModuleProjectDirectory(multiModuleProjectDirectory);
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessCommand()
        {
            base.ProcessCommand();
            string[] arguments = argsFromArguments();
            List<string> args = new List<string>(arguments);
            if (!string.IsNullOrWhiteSpace(File))
            {
                args.Add("-f");
                args.Add(File);
            }
            if (!string.IsNullOrWhiteSpace(Stage))
            {
                args.Add(Stage);
            }

            var result = Launcher.MainWithExitCode(args.ToArray().ToJVMArray<Java.Lang.String, string>());
            WriteObject(result);
        }

        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        protected override void EndProcessing()
        {
            WriteVerbose("End StartLauncherCmdletCommand!");
        }
    }
}
