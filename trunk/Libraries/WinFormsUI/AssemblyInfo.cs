using System;
using System.Reflection;

[assembly : CLSCompliant(true)]
#if DEBUG
[assembly : AssemblyVersion("0.1.*")]
#else
[assembly : AssemblyVersion("0.1.2005.0822")]
[assembly : AssemblyKeyFile("..\\..\\..\\..\\Ch3Etah.snk")]
#endif
