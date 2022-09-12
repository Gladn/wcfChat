using Chat;
using System;
using System.ServiceModel;

namespace ChatClient
{
    public class FileRepositoryServiceClient : ClientBase<IService>, IService, IDisposable
    {
        public FileRepositoryServiceClient()
            : base("Service")
        {
        }

        #region IFileRepositoryService Members

        public System.IO.Stream GetFile(string virtualPath)
        {
            return base.Channel.GetFile(virtualPath);
        }

        public void PutFile(FileUploadMessage msg)
        {
            base.Channel.PutFile(msg);
        }

        public void DeleteFile(string virtualPath)
        {
            base.Channel.DeleteFile(virtualPath);
        }

        public StorageFileInfo[] List()
        {
            return List(null);
        }

        public StorageFileInfo[] List(string virtualPath)
        {
            return base.Channel.List(virtualPath);
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (this.State == CommunicationState.Opened)
                this.Close();
        }

        public int Connect(string name)
        {
            throw new NotImplementedException();
        }

        public void Disconnect(int id)
        {
            throw new NotImplementedException();
        }

        public void SendMsg(string msg, int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
