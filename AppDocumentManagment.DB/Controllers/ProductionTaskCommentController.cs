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
                    Employee employee = context.Employees.SingleOrDefault(e => e.EmployeeID == productionTaskComment.EmployeeID);
                    if(employee != null)
                    {
                        productionTaskComment.Employee = employee;
                        productionTaskComment.EmployeeID = employee.EmployeeID;
                    }
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

        public bool AddProductionTaskComments(List<ProductionTaskComment> productionTaskComments)
        {
            if (productionTaskComments == null || productionTaskComments.Count == 0) return false;
            bool result = false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    List<ProductionTaskComment> notSaveTaskComments = productionTaskComments.Where(ptc => ptc.ProductionTaskCommentID == 0).ToList();
                    foreach (var aviableTaskComment in notSaveTaskComments)
                    {
                        Employee employee = context.Employees.SingleOrDefault(e => e.EmployeeID == aviableTaskComment.EmployeeID);
                        aviableTaskComment.Employee = employee;
                        aviableTaskComment.EmployeeID = employee.EmployeeID;
                        ProductionTask productionTask = context.ProductionTasks.SingleOrDefault(t => t.ProductionTaskID == aviableTaskComment.ProductionTaskID);
                        aviableTaskComment.ProductionTask = productionTask;
                        aviableTaskComment.ProductionTaskID = productionTask.ProductionTaskID;
                    }
                    context.ProductionTaskComments.AddRange(notSaveTaskComments);
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
