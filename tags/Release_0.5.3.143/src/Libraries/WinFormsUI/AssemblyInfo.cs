using System;
using System.Reflection;

[assembly : CLSCompliant(true)]
#if DEBUG
[assembly : AssemblyVersion("0.1.0.*")]
#else
[assembly : AssemblyVersion("1.0.2005.1013")]
[assembly : AssemblyKeyFile("..\\..\\..\\..\\Ch3Etah.snk")]
#endif
