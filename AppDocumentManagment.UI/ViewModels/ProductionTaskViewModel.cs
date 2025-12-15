using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Utilities;
using AppDocumentManagment.UI.Views;
using Microsoft.Identity.Client;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AppDocumentManagment.UI.ViewModels
{
    public class ProductionTaskViewModel : BaseViewModelClass
    {
        ProductionTaskWindow ProductionTaskWindow { get; set; }
        Employee CurrentEmployee { get; set; }

        private IFileDialogService fileDialogService;

        ExternalDocument ExternalDocument { get; set; }
        InternalDocument InternalDocument { get; set; }

        private string productionTaskTitle;

        public string ProductionTaskTitle
        {
            get => productionTaskTitle;
            set
            {
                productionTaskTitle = value;
                OnPropertyChanged(nameof(ProductionTaskTitle));
            }
        }

        private string documentTitle = string.Empty;

        public string DocumentTitle
        {
            get => documentTitle;
            set
            {
                documentTitle = value;
                OnPropertyChanged(nameof(DocumentTitle));
            }
        }

        private DateTime productionTaskDueDate;
        public DateTime ProductionTaskDueDate
        {
            get => productionTaskDueDate;
            set
            {
                productionTaskDueDate = value;
                OnPropertyChanged(nameof(ProductionTaskDueDate));
            }
        }

        private string productionTaskDescription = string.Empty;

        public string ProductionTaskDescription
        {
            get => productionTaskDescription;
            set
            {
                productionTaskDescription = value;
                OnPropertyChanged(nameof(ProductionTaskDescription));
            }
        }

        private string subProductionTaskTitle = string.Empty;

        public string SubProductionTaskTitle
        {
            get => subProductionTaskTitle;
            set
            {
                subProductionTaskTitle = value;
                OnPropertyChanged(nameof(SubProductionTaskTitle));
            }
        }
        private bool isImportant = false;
        public bool IsImportant
        {
            get => isImportant;
            set
            {
                isImportant = value;
                OnPropertyChanged(nameof(IsImportant));
            }
        }

        public ObservableCollection<ProductionSubTask> ProductionSubTasks { get; set; }

        public ObservableCollection<Employee> ProductionTaskPerformers {  get; set; }

        public ObservableCollection<ProductionTaskFile> ProductionTaskFiles { get; set; }

        private ProductionTaskFile selectedProductionTaskFile;
        public ProductionTaskFile SelectedProductionTaskFile
        {
            get => selectedProductionTaskFile;
            set
            {
                selectedProductionTaskFile = value;
                OnPropertyChanged(nameof(SelectedProductionTaskFile));
            }
        }

        private List<ProductionTaskComment> ProductionTaskComments { get; set; }

        public ProductionTaskViewModel(ProductionTaskWindow window, Employee currentEmployee, ExternalDocument externalDocument, InternalDocument internalDocument)
        {
            ProductionTaskWindow = window;
            CurrentEmployee = currentEmployee;
            DepartmentController departmentController = new DepartmentController();
            Department department = departmentController.GetDepartmentByID(currentEmployee.DepartmentID);
            CurrentEmployee.Department = department;
            fileDialogService = new WindowsDialogService();
            if (externalDocument != null)
            {
                ExternalDocument = externalDocument;
                DocumentTitle = externalDocument.ExternalDocumentTitle;
            }
            if (internalDocument != null)
            {
                InternalDocument = internalDocument;
                DocumentTitle = internalDocument.InternalDocumentTitle;
            }
            if (DocumentTitle == string.Empty)
            {
            }
            ProductionTaskDueDate = DateTime.Now;
            ProductionTaskComments = new List<ProductionTaskComment>();
            ProductionTaskPerformers = new ObservableCollection<Employee>();
            ProductionTaskFiles = new ObservableCollection<ProductionTaskFile>();
            ProductionSubTasks = new ObservableCollection<ProductionSubTask>();
        }

        public ICommand ISendForExecution => new RelayCommand(sendForExecution => SendForExecution());
        private void SendForExecution()
        {
            AddNewTask();
        }

        private void AddNewTask()
        {
            ProductionTask productionTask = new ProductionTask();
            productionTask.ProductionTaskTitle = ProductionTaskTitle;
            productionTask.Priority = IsImportant;
            if (ExternalDocument != null) 
            {
                productionTask.ExternalDocument = ExternalDocument;
            }
            if (InternalDocument != null)
            {
                productionTask.InternalDocument = InternalDocument;
            }
            productionTask.ProductionTaskCreateDate = DateTime.Now;
            productionTask.ProductionTaskDueDate = ProductionTaskDueDate;
            productionTask.ProductionTaskDescription = ProductionTaskDescription;
            productionTask.Employees = new List<Employee>();
            if (ProductionTaskPerformers.Count > 0)
            {
                productionTask.Employees.AddRange(ProductionTaskPerformers);
            }
            productionTask.ProductionTaskStatus = ProductionTaskStatus.InProgress;
            productionTask.ProductionSubTasks = new List<ProductionSubTask>();
            if (ProductionSubTasks.Count > 0)
            {
                productionTask.ProductionSubTasks.AddRange(ProductionSubTasks);
            }
            //скорей всего это ошибочное решение
            productionTask.ProductionTaskComments = new List<ProductionTaskComment>();
            if (ProductionTaskComments.Count > 0)
            {
                productionTask.ProductionTaskComments.AddRange(ProductionTaskComments);
            }
            productionTask.ProductionTaskFiles = new List<ProductionTaskFile>();
            if (ProductionTaskFiles.Count > 0)
            {
                productionTask.ProductionTaskFiles.AddRange(ProductionTaskFiles);
            }
            ProductionTaskController controller = new ProductionTaskController();
            bool result = controller.AddProductionTask(productionTask);
            if (result)
            {
                MessageBox.Show("Задача успешно сохранена");
            }
            else
            {
                MessageBox.Show("Ошибка в сохранении задачи");
            }
            ProductionTaskWindow.Close();
        }

        public ICommand IAddNewSubTask => new RelayCommand(addNewSubTask => AddNewSubTask());
        private void AddNewSubTask()
        {
            if (string.IsNullOrEmpty(SubProductionTaskTitle)) return;
            ProductionSubTask productionSubTask = new ProductionSubTask();
            productionSubTask.ProductionSubTaskDescription = SubProductionTaskTitle;
            productionSubTask.ProductionSubTaskCreateTime = DateTime.Now;
            ProductionSubTasks.Add(productionSubTask);
        }

        public ICommand IBrowseProductionTaskFile => new RelayCommand(browseProductionTaskFile => BrowseProductionTaskFile());
        private void BrowseProductionTaskFile()
        {
            var filePath = fileDialogService.OpenFile();
            if (filePath == null) return;
            string fileName = FileProcessing.GetFileName(filePath);
            string fileExtension = FileProcessing.GetFileExtension(filePath);
            byte[] fileData = FileProcessing.GetFileData(filePath);
            ProductionTaskFile productionTaskFile = new ProductionTaskFile();
            productionTaskFile.ProductionTaskFileName = fileName;
            productionTaskFile.ProductionTaskFileExtension = fileExtension;
            productionTaskFile.ProductionTaskFileData = fileData;
            ProductionTaskFiles.Add(productionTaskFile);
        }

        public ICommand IOpenListPerformers => new RelayCommand(openListPerformers => OpenListPerformers());
        private void OpenListPerformers()
        {
            ExaminingPersonsWindow examiningPersonsWindow = new ExaminingPersonsWindow(false, CurrentEmployee.Department);
            examiningPersonsWindow.ShowDialog();
            if (examiningPersonsWindow.viewModel.SelectedEmployee != null)
            {
                Employee employee = examiningPersonsWindow.viewModel.SelectedEmployee;
                ProductionTaskPerformers.Add(employee);
            }
        }

        public ICommand IOpenProductionTaskCommentWindow => new RelayCommand(openProductionTaskCommentWindow => OpenProductionTaskCommentWindow());
        private void OpenProductionTaskCommentWindow()
        {
            ProductionTaskCommentWindow productionTaskCommentWindow = new ProductionTaskCommentWindow(null, CurrentEmployee);
            productionTaskCommentWindow.ShowDialog();
            ProductionTaskComments = productionTaskCommentWindow.viewModel.ProductionTaskCommentsList;
        }
    }
}
