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
                        foreach (int ID in productionTask.EmployeesID)
                        {
                            if(employeeID == ID)
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

        public bool UpdateProductionTaskStatus(ProductionTask productionTask)
        {
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    ProductionTask aviableProductionTask = context.ProductionTasks.FirstOrDefault(pt => pt.ProductionTaskID == productionTask.ProductionTaskID);
                    if (aviableProductionTask == null) return false;
                    aviableProductionTask.ProductionTaskStatus = productionTask.ProductionTaskStatus;
                    context.ProductionTasks.Update(aviableProductionTask);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в обновлении статуса задачи");
            }
            return false;
        }
    }
}
