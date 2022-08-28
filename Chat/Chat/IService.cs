using System.ServiceModel;

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
    }

    public interface IServerCallback
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallback(string msg);
    }
}
