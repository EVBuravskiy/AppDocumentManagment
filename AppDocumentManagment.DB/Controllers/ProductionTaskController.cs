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
                    //if (productionTask.ExternalDocumentID != 0)
                    //{
                    //    productionTask.ExternalDocument = context.ExternalDocuments.SingleOrDefault(exd => exd.ExternalDocumentID == productionTask.ExternalDocumentID);
                    //}
                    //if (productionTask.InternalDocumentID != 0)
                    //{
                    //    productionTask.InternalDocument = context.InternalDocuments.SingleOrDefault(ind => ind.InternalDocumentID == productionTask.InternalDocumentID);
                    //}
                    //List<Employee> employees = new List<Employee>();
                    //employees.AddRange(productionTask.Employees);
                    //productionTask.Employees.Clear();
                    //EmployeeController employeeController = new EmployeeController();
                    //foreach (Employee employee in employees) 
                    //{
                    //    Employee aviableEmployee = employeeController.GetEmployeeByID(employee.EmployeeID);
                    //    productionTask.Employees.Add(aviableEmployee);
                    //}
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
                        //foreach (Employee employee in productionTask.Employees)
                        //{
                        //    if (employee.EmployeeID == employeeID)
                        //    {
                        //        productionTasks.Add(productionTask);
                        //    }
                        //}
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
