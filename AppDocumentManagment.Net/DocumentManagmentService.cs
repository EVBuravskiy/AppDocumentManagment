using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.DB.Controllers;


namespace AppDocumentManagment.Net
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "DocumentManagmentService" в коде и файле конфигурации.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class DocumentManagmentService : IDocumentManagmentService
    {
        private List<ServerClient> clients = new List<ServerClient>();
        int nextClientID = 1;

        public int Connect()
        {
            ServerClient newClient = new ServerClient()
            {
                ClientID = nextClientID,
                OperationContext = OperationContext.Current,
            };
            nextClientID ++;
            clients.Add(newClient);
            return newClient.ClientID;
        }

        public void Disconnect(int clientID)
        {
            ServerClient client = clients.SingleOrDefault(c => c.ClientID == clientID);
            if (client != null)
            {
                clients.Remove(client);
            }
            if(clients.Count == 0) nextClientID = 1;
        }

    }
}
