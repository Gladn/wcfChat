using System.IO;
using System.Linq;
using System.ServiceModel;


namespace Chat
{
    [MessageContract]
    public class FileUploadMessage
    {
        [MessageHeader(MustUnderstand = true)]
        public string VirtualPath { get; set; }

        [MessageBodyMember(Order = 1)]
        public Stream DataStream { get; set; }
    }
}
