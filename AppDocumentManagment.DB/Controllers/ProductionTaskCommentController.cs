using AppDocumentManagment.DB.Models;

namespace AppDocumentManagment.DB.Controllers
{
    public class ProductionTaskCommentController
    {
        public bool AddProductionTaskComment(ProductionTaskComment productionTaskComment)
        {
            if (productionTaskComment == null) return false;
            bool result = false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    context.ProductionTaskComments.Add(productionTaskComment);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в сохранении комментария к задаче в базу данных");
                result = false;
            }
            return result;
        }

        public List<ProductionTaskComment> GetProductionTaskComments(int productionTaskID)
        {
            List<ProductionTaskComment> productionTaskComments = new List<ProductionTaskComment>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    productionTaskComments = context.ProductionTaskComments.Where(c => c.ProductionTaskID == productionTaskID).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении комментариев к задаче из базы данных");
            }
            return productionTaskComments;
        }
    }
}
