using AppDocumentManagment.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDocumentManagment.DB.Controllers
{
    public class ProductionTaskFileController
    {
        public bool AddProductionTaskFile(ProductionTaskFile productionTaskFile, int productionTaskID)
        {
            bool result = false;
            if (productionTaskFile == null || productionTaskID == 0) return result;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    ProductionTask aviableProductionTask = context.ProductionTasks.SingleOrDefault(t => t.ProductionTaskID == productionTaskID);
                    if (aviableProductionTask == null) return false;
                    productionTaskFile.ProductionTask = aviableProductionTask;
                    context.ProductionTaskFiles.Add(productionTaskFile);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в добавлении файла задачи в базу данных");
            }
            return result;
        }

        public bool AddProductionTaskFiles(List<ProductionTaskFile> productionTaskFiles, int productionTaskID)
        {
            bool result = false;
            if (productionTaskFiles == null || productionTaskFiles.Count == 0 || productionTaskID == 0) return result;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    ProductionTask aviableProductionTask = context.ProductionTasks.SingleOrDefault(t => t.ProductionTaskID == productionTaskID);
                    if (aviableProductionTask == null) return false;
                    foreach (ProductionTaskFile file in productionTaskFiles)
                    {
                        file.ProductionTask = aviableProductionTask;
                    }
                    context.ProductionTaskFiles.AddRange(productionTaskFiles);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в сохранении списка файлов задачи в базу данных");
            }
            return result;
        }

        public List<ProductionTaskFile> GetProductionTaskFiles(int productionTaskID)
        {
            if (productionTaskID == 0) return null;
            List<ProductionTaskFile> productionTaskFiles = new List<ProductionTaskFile>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    productionTaskFiles = context.ProductionTaskFiles.Where(tf => tf.ProductionTaskID == productionTaskID).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в получении файлов задачи из базы данных");
            }
            return productionTaskFiles;
        }

        public bool RemoveExternalDocumentFile(int productionTaskFileID)
        {
            bool result = false;
            if (productionTaskFileID == 0) return result;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    ProductionTaskFile aviableProductionTaskFile = context.ProductionTaskFiles.SingleOrDefault(tf => tf.ProductionTaskFileID == productionTaskFileID);
                    if (aviableProductionTaskFile == null) return false;
                    context.ProductionTaskFiles.Remove(aviableProductionTaskFile);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в удалении файла задачи из базы данных");
            }
            return result;
        }
    }
}
