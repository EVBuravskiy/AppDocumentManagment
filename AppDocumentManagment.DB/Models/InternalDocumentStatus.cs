namespace AppDocumentManagment.DB.Models
{
    public enum InternalDocumentStatus
    {
        NewExternalDocument,    //Новый документ
        UnderConsideration,     //На рассмотрении
        Agreed,                 //Согласован
        Refused                 //Отказан
    }
}
