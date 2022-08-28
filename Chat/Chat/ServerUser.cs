using System.ServiceModel;

namespace Chat
{
    internal class ServerUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public OperationContext operationContext { get; set; }
    }
}
