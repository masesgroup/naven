using MASES.Naven;
using System;
using System.IO;

namespace MASES.NavenTemplate.NavenApp
{
    class Program
    {
        class MyNavenCore : NavenCore<MyNavenCore> { }

        static void Main(string[] _)
        {
            MyNavenCore.CreateGlobalInstance(); // this call prepares the environment: it is mandatory to initialize the JVM
            var appArgs = MyNavenCore.FilteredArgs; // contains the remaining arguments: the Naven, JNet and JCOBridge arguments are discarded
        }
    }
}
