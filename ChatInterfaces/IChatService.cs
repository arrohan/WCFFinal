using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChatInterfaces
{
    
    [ServiceContract(CallbackContract =typeof(IClient))]
    public interface IChatService
    {
        [OperationContract]
        int Login(string userName);
        [OperationContract]
        void sendMessage(string Message, string userName); 
       
    }
}
