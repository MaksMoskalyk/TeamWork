using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileSharing.Server
{
    public class Service : IService
    {
        public FileTransferResponse Receive(FileTransferRequest fileToPush, int projectId, int issueId = -1, bool tempFolder = false)
        {
            FileTransferResponse fileTransferResponse = this.CheckFileTransferRequest(fileToPush);
            string appSettingsSavedLocation = "SavedLocation";
            if (tempFolder)
                appSettingsSavedLocation = "SavedLocationTemp";
            if (fileTransferResponse.ResponseStatus == "FileIsValed")
            {
                try
                {

                    string path = System.Configuration.ConfigurationManager.AppSettings[appSettingsSavedLocation].ToString() + projectId.ToString();
                    path = Path.GetFullPath(path);
                    if (issueId != -1)
                        path = path + "\\" + issueId.ToString();
                    if (!Directory.Exists(path))
                        System.IO.Directory.CreateDirectory(path);
                    this.SaveFileStream(path + "\\" + fileToPush.FileName, new MemoryStream(fileToPush.Content));
                    return new FileTransferResponse
                    {
                        CreateAt = DateTime.Now,
                        FileName = fileToPush.FileName,
                        Message = "File was transfered",
                        ResponseStatus = "Successful"
                    };
                }
                catch (Exception ex)
                {
                    return new FileTransferResponse
                    {
                        CreateAt = DateTime.Now,
                        FileName = fileToPush.FileName,
                        Message = ex.Message,
                        ResponseStatus = "Error"
                    };
                }
            }
            return fileTransferResponse;
        }

        public FileDescription[] GetFilesInfo(bool tempFolder)
        {
            List<FileDescription> descriptions = new List<FileDescription>();
            string appSettingsSavedLocation = "SavedLocation";
            if (tempFolder)
                appSettingsSavedLocation = "SavedLocationTemp";
            string[] pathes = Directory.GetFiles(System.Configuration.ConfigurationManager.AppSettings[appSettingsSavedLocation].ToString());
            foreach (string item in pathes)
            {
                FileDescription f = new FileDescription();
                f.Name = item.Substring(item.LastIndexOf('\\') + 1);
                f.SizeBytes = new FileInfo(item).Length;
                descriptions.Add(f);
            }
            return descriptions.ToArray();
        }


        public FileTransferRequest GetFile(string name, int projectId, int issueId = -1)
        {

            string path = System.Configuration.ConfigurationManager.AppSettings["SavedLocation"].ToString();
            path = path + "\\" + projectId;
            if (issueId != -1)
                path = path + "\\" + issueId;
            path = path + "\\" + name;
            

            if (!File.Exists(path))
                return null;

            byte[] bytes = null;
            try
            {
                bytes = File.ReadAllBytes(path);
            }
            catch
            {
                name = "";
            }
            return new FileTransferRequest()
            {
                Content = bytes,
                FileName = name
            };
        }

        public FileTransferResponse DeleteFile(string name, int projectId, int issueId = -1, bool tempFolder = false)
        {
            string appSettingsSavedLocation = "SavedLocation";
            if (tempFolder)
                appSettingsSavedLocation = "SavedLocationTemp";
            string path = System.Configuration.ConfigurationManager.AppSettings[appSettingsSavedLocation].ToString() + projectId.ToString();
            if (issueId != -1)
                path = path + "\\" + issueId.ToString();
            if (!File.Exists(path + "\\" + name))
                return new FileTransferResponse
                {
                    CreateAt = DateTime.Now,
                    FileName = name,
                    Message = "File not exists at currect path",
                    ResponseStatus = "Error"
                };
            try
            {
                File.Delete(path + "\\" + name);
                return new FileTransferResponse
                {
                    CreateAt = DateTime.Now,
                    FileName = name,
                    Message = "File was Deleted",
                    ResponseStatus = "Successful"
                };
            }
            catch (Exception ex)
            {
                return new FileTransferResponse
                {
                    CreateAt = DateTime.Now,
                    FileName = name,
                    Message = ex.Message,
                    ResponseStatus = "Error"
                };
            }
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

    }
}
