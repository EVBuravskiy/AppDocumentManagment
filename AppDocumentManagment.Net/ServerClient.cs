using System.ServiceModel;

namespace AppDocumentManagment.Net
{
    public class ServerClient
    {
        public int ClientID { get; set; }
        public OperationContext OperationContext { get; set; }
    }
}
