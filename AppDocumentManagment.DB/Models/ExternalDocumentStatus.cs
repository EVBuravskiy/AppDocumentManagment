using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDocumentManagment.DB.Models
{
    public enum ExternalDocumentStatus
    {
        NewExternalDocument,    //Новый документ
        UnderConsideration,     //На рассмотрении
        Considered,             //Рассмотрен
    }
}
