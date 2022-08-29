using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;

namespace Chat
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service : IService
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextID = -1;
        String path = "..\\Test.txt";
        public int Connect(string name)
        {
            ServerUser user = new ServerUser()
            {
                ID = nextID++,
                Name = name,
                operationContext = OperationContext.Current
            };
            SendMsg(": "+user.Name+" подключился к чату!", 0);
            users.Add(user);
            return user.ID;
            
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if (user != null)
            {
                users.Remove(user);
                SendMsg(": " + user.Name + " покинул чат!", 0);
            }
        }
       

        public void SendMsg(string msg, int id)
        {
            foreach (var item in users)
            {
                string answer = DateTime.Now.ToShortDateString();
                var user = users.FirstOrDefault(i => i.ID == id);
                if (user != null)
                {
                    answer += ": " + user.Name + "- ";
                }
                answer += msg;

                item.operationContext.GetCallbackChannel<IServerCallback>().MsgCallback(answer);
            }
        }

        public Stream Download(string File)
        {
            throw new NotImplementedException();
        }
        public string Upload(Stream input)
        {
            throw new NotImplementedException();
        }
    }
}
