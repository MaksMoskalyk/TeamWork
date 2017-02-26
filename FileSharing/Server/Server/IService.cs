using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace FileSharing.Server
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        FileTransferResponse Receive(FileTransferRequest request, int projectId, int issueId = -1, bool tempFolder = false);

        [OperationContract]
        FileDescription[] GetFilesInfo(bool tempFolder);


        [OperationContract]
        FileTransferRequest GetFile(string name, int projectId, int issueId = -1);


        [OperationContract]
        FileTransferResponse DeleteFile(string name, int projectId, int issueId = -1, bool tempFolder = false);
    }
}
