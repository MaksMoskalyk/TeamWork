using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace FileSharing.Server
{
    [DataContract]
    public struct FileDescription
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public float SizeBytes { get; set; }
    }
}
