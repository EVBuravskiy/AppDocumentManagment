using AppDocumentManagment.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDocumentManagment.DB.Controllers
{
    public class ProductionSubTaskController
    {
        public bool AddProductionSubTask(ProductionSubTask productionSubTask)
        {
            if (productionSubTask == null) return false;
            bool result = false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    context.ProductionSubTasks.Add(productionSubTask);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в сохранении подзадачи в базу данных");
                result = false;
            }
            return result;
        }

        public bool AddProductionSubTasks(List<ProductionSubTask> productionSubTasks)
        {
            if (productionSubTasks == null || productionSubTasks.Count == 0) return false;
            bool result = false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    context.ProductionSubTasks.AddRange(productionSubTasks);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в сохранении подзадач в базу данных");
                result = false;
            }
            return result;
        }

        public List<ProductionSubTask> GetProductionSubTasks(int productionTaskID)
        {
            List<ProductionSubTask> productionSubTasks = new List<ProductionSubTask>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    productionSubTasks = context.ProductionSubTasks.Where(s => s.ProductionTaskID == productionTaskID).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении подзадач из базы данных");
            }
            return productionSubTasks;
        }
    }
}
