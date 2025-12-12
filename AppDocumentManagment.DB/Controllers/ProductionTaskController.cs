using AppDocumentManagment.DB.Models;

namespace AppDocumentManagment.DB.Controllers
{
    public class ProductionTaskController
    {
        public bool AddProductionTask(ProductionTask productionTask)
        {
            if (productionTask == null) return false;
            bool result = false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    ExternalDocument externalDocument = new ExternalDocument();
                    if (productionTask.ExternalDocumentID != 0)
                    {
                        externalDocument = context.ExternalDocuments.SingleOrDefault(exd => exd.ExternalDocumentID == productionTask.ExternalDocumentID);
                    }
                    InternalDocument internalDocument = new InternalDocument();
                    if (productionTask.InternalDocumentID != 0)
                    {
                        internalDocument = context.InternalDocuments.SingleOrDefault(ind => ind.InternalDocumentID == productionTask.InternalDocumentID);
                    }
                    if (externalDocument != null)
                    {
                        productionTask.ExternalDocument = externalDocument;
                    }
                    if (internalDocument != null)
                    {
                        productionTask.InternalDocument = internalDocument;
                    }
                    context.ProductionTasks.Add(productionTask);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в сохранении задачи в базу данных");
                result = false;
            }
            return result;
        }

        public List<ProductionTask> GetProductionTasks()
        {
            List<ProductionTask> productionTasks = new List<ProductionTask>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    productionTasks = context.ProductionTasks.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении списка задач из базы данных");
            }
            return productionTasks;
        }

        public List<ProductionTask> GetProductionTasksByEmployeeID(int employeeID)
        {
            List<ProductionTask> allProductionTasks = new List<ProductionTask>();
            List<ProductionTask> productionTasks = new List<ProductionTask>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    allProductionTasks = context.ProductionTasks.ToList();
                    foreach (ProductionTask productionTask in allProductionTasks)
                    {
                        foreach (Employee employee in productionTask.Employees)
                        {
                            if (employee.EmployeeID == employeeID)
                            {
                                productionTasks.Add(productionTask);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении списка задач по идентификатору сотрудника");
            }
            return productionTasks;
        }
    }
}
