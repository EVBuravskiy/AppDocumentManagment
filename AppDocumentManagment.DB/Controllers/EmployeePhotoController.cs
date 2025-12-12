using AppDocumentManagment.DB.Models;
using System.Windows;

namespace AppDocumentManagment.DB.Controllers
{
    public class EmployeePhotoController
    {
        public List<EmployeePhoto> GetEmployeePhotos()
        {
            List<EmployeePhoto> employeePhotos = new List<EmployeePhoto>();
            using(ApplicationContext context = new ApplicationContext())
            {
                employeePhotos = context.EmployeePhotos.ToList();
            }
            return employeePhotos;
        }

        public EmployeePhoto GetEmployeePhotoByID(int employeeID)
        {
            EmployeePhoto employeePhoto = new EmployeePhoto();
            using(ApplicationContext context = new ApplicationContext()) 
            {
                employeePhoto = context.EmployeePhotos.Where(e => e.EmployeeID == employeeID).FirstOrDefault();   
            }
            return employeePhoto;
        }

        public bool AddEmployeePhoto(EmployeePhoto employeePhoto)
        {
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    context.EmployeePhotos.Add(employeePhoto);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в сохранении фотографии работника");
            }
            return false;
        }
    }
}
