using System.Reflection;
using XwRemote;

[assembly: AssemblyVersion(Version.AssemblyVersion)]
[assembly: AssemblyFileVersion(Version.AssemblyVersion)]

namespace XwRemote
{
    class Version
    {
#if DEBUG
        internal const string AssemblyVersion = "0.0.0.0";
#else
        internal const string AssemblyVersion = "3.1.3.8";        
#endif
    }
}
