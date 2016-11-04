using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaseOnRoslyn
{
    public class ReferenceInfo
    {
        public string Name
            {
                get;
                set;
            }
        public bool IsProjectReference
            {
                get;
                set;
            }
        public bool IsTeamCode
            {
                get;
                set;
            }
        public string AssemblyName
            {
                get;
                set;
            }
        public string AssemblyLocationForDebug
            {
                get;
                set;
            }
        public string AssemblyLocationForRelease
            {
                get;
                set;
            }
        public MetadataReference CurrentReferenceMetadata
            {
                get;
                set;
            }
        public Assembly LoadedAssembly
            {
                get;
                set;
            }
    }
}
