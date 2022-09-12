using System.ServiceModel;
using System.IO;
using System;

namespace Chat
{
       
    [ServiceContract(CallbackContract =typeof(IServerCallback))]
    public interface IService
    {
        [OperationContract]
        int Connect(string name);

        [OperationContract]
        void Disconnect(int id);

        [OperationContract(IsOneWay = true)]
        void SendMsg(string msg, int id);

        [OperationContract]
        Stream GetFile(string virtualPath);

        [OperationContract]
        void PutFile(FileUploadMessage msg);

        [OperationContract]
        void DeleteFile(string virtualPath);

        [OperationContract]
        StorageFileInfo[] List(string virtualPath);

    }

    public interface IServerCallback
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallback(string msg);
    }

    

}
