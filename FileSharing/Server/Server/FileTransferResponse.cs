using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSharing.Server
{
    public struct FileTransferResponse
    {
        public DateTime CreateAt { get; set; }
        public string FileName { get; set; }
        public string Message { get; set; }
        public string ResponseStatus { get; set; }
    }
}
