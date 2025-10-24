using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDocumentManagment.DB.Models
{
    public enum DocumentType
    {
        Contract,           //Договор
        CommercialOffer,    //Коммерческое предложение
        Letter,             //Письмо, сопроводительное письмо
        GovernmentLetter,   //Письмо из гос.органов
    }
}
