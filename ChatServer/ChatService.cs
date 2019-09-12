using ChatInterfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChatServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)] //change this to single if it doesnt work
    public class ChatService : IChatService
    {
        public ConcurrentDictionary<string, ConnectedClient> connectedClients = new ConcurrentDictionary<string, ConnectedClient>();

        public int Login(string userName)
        {

            foreach (var client in connectedClients)   //Login with a already logged-in name
            {
                if(client.Key.ToLower() == userName.ToLower())
                {
                    return 1; 
                }
            }

            var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClient>(); //callback  

            ConnectedClient newClient = new ConnectedClient();
            newClient.connection = establishedUserConnection;
            newClient.UserName = userName;
            connectedClients.TryAdd(userName, newClient);

            return 0; //successfully logged in
        }

        public void sendMessage(string Message, string userName)   //username - senders username 
        {
            foreach (var client in connectedClients)
            {
                if(client.Key.ToLower() != userName.ToLower())
                {
                    client.Value.connection.GetMessage(Message, userName);
                }
            }
        }
    }
}
