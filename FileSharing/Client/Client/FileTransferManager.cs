using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ServiceReference1;
using System.ServiceModel;
using System.Configuration;
using System.IO;



namespace FileSharing.Client
{
    public class FileTransferManager
    {
        ServiceClient proxy;
        public FileTransferManager()
        {
            this.InitializeProxy();
        }

        private void InitializeProxy()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = System.ServiceModel.Configuration.ServiceModelSectionGroup.GetSectionGroup(config);
            string endpointAddress = section.Client.Endpoints[0].Address.ToString();

            if (string.IsNullOrEmpty(endpointAddress))
                throw new Exception("Incorrect endpoint address");

            EndpointAddress httpAdr = new EndpointAddress(endpointAddress);
            NetTcpBinding binding = new NetTcpBinding();
            proxy = new ServiceClient(binding, httpAdr);
        }

        private void SaveFileStream(string filePath, Stream stream)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                stream.CopyTo(fileStream);
                fileStream.Dispose();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                throw ex;
            }
        }

        private FileTransferResponse CheckFileTransferRequest(FileTransferRequest fileToPush)
        {
            if (fileToPush != null)
            {
                if (!string.IsNullOrEmpty(fileToPush.FileName))
                {
                    if (fileToPush.Content != null)
                    {
                        return new FileTransferResponse
                        {
                            CreateAt = DateTime.Now,
                            FileName = fileToPush.FileName,
                            Message = string.Empty,
                            ResponseStatus = "FileIsValed"
                        };
                    }

                    return new FileTransferResponse
                    {
                        CreateAt = DateTime.Now,
                        FileName = "No Name",
                        Message = " File Content is null",
                        ResponseStatus = "Error"
                    };
                }

                return new FileTransferResponse
                {
                    CreateAt = DateTime.Now,
                    FileName = "No Name",
                    Message = " File Name Can't be Null",
                    ResponseStatus = "Error"
                };
            }

            return new FileTransferResponse
            {
                CreateAt = DateTime.Now,
                FileName = "No Name",
                Message = " File Can't be Null",
                ResponseStatus = "Error"
            };
        }

        public FileTransferResponse SendFileToServer(string fileName, byte[] bytes)
        {
            FileTransferRequest createdFile = new FileTransferRequest()
            {
                FileName = fileName,
                Content = bytes
            };

            return proxy.Receive(createdFile, false);
        }

        public FileTransferResponse SendFileToServerTempFolder(string fileName, byte[] bytes)
        {
            FileTransferRequest createdFile = new FileTransferRequest()
            {
                FileName = fileName,
                Content = bytes
            };
            return proxy.Receive(createdFile, true);
        }

        public FileDescription[] GetServerFilesInfo()
        {
            return proxy.GetFilesInfo(false);
        }

        public FileDescription[] GetServerFilesInfoFromTempFolder()
        {
            return proxy.GetFilesInfo(true);
        }

        public FileTransferResponse GetFile(string name)
        {
            FileTransferRequest fileToDownload = proxy.GetFile(name);
            FileTransferResponse fileTransferResponse = this.CheckFileTransferRequest(fileToDownload);
            if (fileTransferResponse.ResponseStatus == "FileIsValed")
            {
                try
                {
                    this.SaveFileStream(System.Configuration.ConfigurationManager.AppSettings["SavedLocation"].ToString() + "\\" + fileToDownload.FileName, new MemoryStream(fileToDownload.Content));
                    return new FileTransferResponse
                    {
                        CreateAt = DateTime.Now,
                        FileName = fileToDownload.FileName,
                        Message = "File was downloaded",
                        ResponseStatus = "Successful"
                    };
                }
                catch (Exception ex)
                {
                    return new FileTransferResponse
                    {
                        CreateAt = DateTime.Now,
                        FileName = fileToDownload.FileName,
                        Message = ex.Message,
                        ResponseStatus = "Error"
                    };
                }
            }

            return fileTransferResponse;

        }

        public FileTransferResponse DeleteFileFromServer(string name)
        {
            return proxy.DeleteFile(name, false);
        }

        public FileTransferResponse DeleteFileFromServerTempFolder(string name)
        {
            return proxy.DeleteFile(name, true);
        }


    }
}
