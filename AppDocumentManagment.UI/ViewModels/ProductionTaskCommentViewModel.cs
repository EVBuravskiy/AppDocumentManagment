using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace AppDocumentManagment.UI.ViewModels
{
    public class ProductionTaskCommentViewModel : BaseViewModelClass
    {
        private ProductionTaskCommentWindow ProductionTaskCommentWindow { get; set; }
        private ProductionTask CurrentProductionTask { get; set; }

        private Employee CurrentEmployee { get; set; }

        public List<ProductionTaskComment> ProductionTaskCommentsList { get; set; }
        public ObservableCollection<ProductionTaskComment> ProductionTaskComments { get; set; }

        private string productionTaskCommentText = string.Empty;

        public string ProductionTaskCommentText
        {
            get => productionTaskCommentText;
            set
            {
                productionTaskCommentText = value;
                OnPropertyChanged(nameof(ProductionTaskCommentText));
            }
        }
        public ProductionTaskCommentViewModel(ProductionTaskCommentWindow productionTaskCommentWindow, ProductionTask currentProductionTask, Employee currentEmployee)
        {
            ProductionTaskCommentWindow = productionTaskCommentWindow;
            CurrentProductionTask = currentProductionTask;
            CurrentEmployee = currentEmployee;
            ProductionTaskCommentsList = new List<ProductionTaskComment>();
            ProductionTaskComments = new ObservableCollection<ProductionTaskComment>();
            GetAllProductionTaskComments();
            InitializeProductTaskComments();
        }

        private void GetAllProductionTaskComments()
        {
            ProductionTaskCommentsList.Clear();
            if (CurrentProductionTask != null)
            {
                ProductionTaskCommentController productionTaskCommentController = new ProductionTaskCommentController();
                ProductionTaskCommentsList = productionTaskCommentController.GetProductionTaskComments(CurrentProductionTask.ProductionTaskID);
                if (ProductionTaskCommentsList.Count > 0)
                {
                    foreach (ProductionTaskComment productionTaskComment in ProductionTaskCommentsList)
                    {
                        EmployeeController employeeController = new EmployeeController();
                        Employee employee = employeeController.GetEmployeeByID(productionTaskComment.EmployeeID);
                        if (employee != null)
                        {
                            EmployeePhotoController employeePhotoController = new EmployeePhotoController();
                            EmployeePhoto photo = employeePhotoController.GetEmployeePhotoByID(employee.EmployeeID);
                            if (photo != null)
                            {
                                employee.EmployeePhoto = photo;
                            }
                        }
                        productionTaskComment.Employee = employee;
                    }
                }
            }
        }

        private void InitializeProductTaskComments()
        {
            ProductionTaskComments.Clear();
            if(ProductionTaskCommentsList.Count > 0)
            {
                foreach(ProductionTaskComment comment in ProductionTaskCommentsList)
                {
                    ProductionTaskComments.Add(comment);
                }
            }
        }

        public ICommand IAddProductionTaskComment => new RelayCommand(addProductionTaskComment => AddProductionTaskComment());
        private void AddProductionTaskComment()
        {
            ProductionTaskComment productionTaskComment = new ProductionTaskComment();
            productionTaskComment.ProductionTaskCommentText = ProductionTaskCommentText;
            productionTaskComment.ProductionTaskCommentDate = DateTime.Now;
            productionTaskComment.EmployeeID = CurrentEmployee.EmployeeID;
            productionTaskComment.Employee = CurrentEmployee;
            if(CurrentProductionTask != null && CurrentProductionTask.ProductionTaskID != 0)
            {
                productionTaskComment.ProductionTask = CurrentProductionTask;
                productionTaskComment.ProductionTaskID = CurrentProductionTask.ProductionTaskID;
            }
            ProductionTaskCommentsList.Add(productionTaskComment);
            ProductionTaskCommentText = string.Empty;
            InitializeProductTaskComments();
        }

        public ICommand IExit => new RelayCommand(exit => { ProductionTaskCommentWindow.Close(); });
    }
}
