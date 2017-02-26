using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace FileSharing.Server
{
    [DataContract]
    public class FileTransferRequest
    {
        [DataMember]
        public string FileName { get; set; }     

        [DataMember]
        public byte[] Content { get; set; }
    }
}
