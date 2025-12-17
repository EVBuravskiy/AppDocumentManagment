using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Utilities;
using AppDocumentManagment.UI.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AppDocumentManagment.UI.ViewModels
{
    public class ProductionTaskShowViewModel : BaseViewModelClass
    {
        private ProductionTaskShowWindow ProductionTaskShowWindow { get; set; }
        private Employee CurrentEmployee { get; set; }
        private ProductionTask CurrentProductionTask { get; set; }

        private IFileDialogService fileDialogService;

        private ExternalDocument ExternalDocument { get; set; }
        private InternalDocument InternalDocument { get; set; }

        private string productionTaskTitle = string.Empty;

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

        public bool IsImportance {  get; set; }

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
        private List<ProductionSubTask> ProductionSubTasksList { get; set; }
        public ObservableCollection<ProductionSubTask> ProductionSubTasks {  get; set; }

        public ObservableCollection<Employee> ProductionTaskPerformers { get; set; }
        private List<ProductionTaskFile> ProductionTaskFilesList { get; set; }
        public ObservableCollection<ProductionTaskFile> ProductionTaskFiles { get; set; }

        private ProductionTaskFile selectedProductionTaskFile;
        public ProductionTaskFile SelectedProductionTaskFile
        {
            get => selectedProductionTaskFile;
            set
            {
                selectedProductionTaskFile = value;
                OnPropertyChanged(nameof(SelectedProductionTaskFile));
                if(value != null)
                {
                    BrowseToSaveProductionTaskFile();
                }
            }
        }

        private List<ProductionTaskComment> ProductionTaskComments { get; set; }

        public ProductionTaskShowViewModel(ProductionTaskShowWindow window, Employee currentEmployee, ProductionTask currentProductionTask)
        {
            ProductionTaskShowWindow = window;
            CurrentEmployee = currentEmployee;
            CurrentProductionTask = currentProductionTask;
            fileDialogService = new WindowsDialogService();
            if(currentProductionTask.ExternalDocumentID != 0)
            {
                ExternalDocumentController controller = new ExternalDocumentController();
                ExternalDocument = controller.GetExternalDocumentByExternalDocumentID(currentProductionTask.ExternalDocumentID);
                DocumentTitle = ExternalDocument.ExternalDocumentTitle;
                ProductionTaskShowWindow.ExternalDocumentBtn.Visibility = Visibility.Visible;
                ProductionTaskShowWindow.InternalDocumentBtn.Visibility = Visibility.Hidden;
            }
            if (currentProductionTask.InternalDocumentID != 0)
            {
                InternalDocumentController controller = new InternalDocumentController();
                InternalDocument = controller.GetInternalDocumentByInternalDocumentID(currentProductionTask.InternalDocumentID);
                DocumentTitle = InternalDocument.InternalDocumentTitle;
                ProductionTaskShowWindow.InternalDocumentBtn.Visibility = Visibility.Visible;
                ProductionTaskShowWindow.ExternalDocumentBtn.Visibility = Visibility.Hidden;
            }
            if (DocumentTitle == string.Empty)
            {
                ProductionTaskShowWindow.DocumentInfo.Visibility = System.Windows.Visibility.Hidden;
            }
            IsImportance = CurrentProductionTask.Priority;
            ProductionTaskTitle = CurrentProductionTask.ProductionTaskTitle;
            ProductionTaskDueDate = currentProductionTask.ProductionTaskDueDate;
            ProductionTaskDescription = currentProductionTask.ProductionTaskDescription ?? string.Empty;
            ProductionSubTasks = new ObservableCollection<ProductionSubTask>();
            GetProductionSubTasks();
            ProductionTaskPerformers = new ObservableCollection<Employee>();
            GetProductionTaskPerformers();
            ProductionTaskFilesList = new List<ProductionTaskFile>();
            GetProductionTaskFiles();
            ProductionTaskFiles = new ObservableCollection<ProductionTaskFile>();
            InitializeProductionTaskFiles();
        }

        private void GetProductionSubTasks()
        {
            ProductionSubTasks.Clear();
            ProductionSubTaskController controller = new ProductionSubTaskController();
            ProductionSubTasksList = controller.GetProductionSubTasks(CurrentProductionTask.ProductionTaskID);
            if (ProductionSubTasksList != null && ProductionSubTasksList.Count > 0)
            {
                foreach (ProductionSubTask productionSubTask in ProductionSubTasksList)
                {
                    ProductionSubTasks.Add(productionSubTask);
                }
            }
        }

        private void GetProductionTaskPerformers()
        {
            ProductionTaskPerformers.Clear();
            if (CurrentProductionTask.Employees.Count > 0)
            {
                foreach (Employee performer in CurrentProductionTask.Employees)
                {
                    ProductionTaskPerformers.Add(performer);
                }
            }
        }
        private void GetProductionTaskFiles()
        {
            ProductionTaskFilesList.Clear();
            ProductionTaskFileController controller = new ProductionTaskFileController();
            ProductionTaskFilesList = controller.GetProductionTaskFiles(CurrentProductionTask.ProductionTaskID);
        }

        private void InitializeProductionTaskFiles()
        {
            ProductionTaskFiles.Clear();
            if (ProductionTaskFilesList.Count > 0)
            {
                foreach(ProductionTaskFile productionTaskFile in ProductionTaskFilesList)
                {
                    ProductionTaskFiles.Add(productionTaskFile);
                }
            }
        }

        public ICommand ILoadProductionTaskFiles => new RelayCommand(loadProductionTaskFiles => LoadProductionTaskFiles());
        private void LoadProductionTaskFiles()
        {
            string directoryPath = string.Empty;
            foreach (ProductionTaskFile file in ProductionTaskFiles)
            {
                directoryPath = FileProcessing.SaveProductionTaskFileFromDB(file, "Tasks");
            }
            if (string.IsNullOrEmpty(directoryPath))
            {
                MessageBox.Show("Не удалось сохранить файлы");
            }
            else
            {
                DirectoryProcessing.OpenDirectory(directoryPath);
            }
        }

        public ICommand IBrowseToSaveProductionTaskFile => new RelayCommand(browseToSaveProductionTaskFile => BrowseToSaveProductionTaskFile());
        private void BrowseToSaveProductionTaskFile()
        {
            var filePath = fileDialogService.SaveFile(SelectedProductionTaskFile.ProductionTaskFileExtension, SelectedProductionTaskFile.ProductionTaskFileName);
            bool result = FileProcessing.SaveProductionTaskFileToPath(filePath, SelectedProductionTaskFile);
            if (result)
            {
                MessageBox.Show($"Файл {SelectedProductionTaskFile.ProductionTaskFileName} сохранен");
            }
            else
            {
                MessageBox.Show($"Файл {SelectedProductionTaskFile.ProductionTaskFileName} уже имеется, либо не был сохранен");
            }
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
            productionTaskFile.ProductionTask = CurrentProductionTask;
            productionTaskFile.ProductionTaskID = CurrentProductionTask.ProductionTaskID;
            SaveProductionTaskFileToBD(productionTaskFile);
        }

        private void SaveProductionTaskFileToBD(ProductionTaskFile newFile)
        {
            ProductionTaskFileController controller = new ProductionTaskFileController();
            bool result = controller.AddProductionTaskFile(newFile, CurrentProductionTask.ProductionTaskID);
            if (result)
            {
                MessageBox.Show("Добавление файла выполнено успешно");
                ProductionTaskFiles.Add(newFile);
            }
            else
            {
                MessageBox.Show("Ошибка! Добавление файла не выполнено");
            }
        }

        public ICommand IOpenProductionTaskCommentWindow => new RelayCommand(openProductionTaskCommentWindow => OpenProductionTaskCommentWindow());
        private void OpenProductionTaskCommentWindow()
        {
            ProductionTaskCommentWindow productionTaskCommentWindow = new ProductionTaskCommentWindow(CurrentProductionTask, CurrentEmployee);
            productionTaskCommentWindow.ShowDialog();
            ProductionTaskComments = productionTaskCommentWindow.viewModel.ProductionTaskCommentsList;
            ProductionTaskCommentController controller = new ProductionTaskCommentController();
            bool result = controller.AddProductionTaskComments(ProductionTaskComments);
            if(!result)
            {
                MessageBox.Show("Ошибка! Комментарии не были сохранены");
            }
        }

        public ICommand IUpdateSubTasks => new RelayCommand(updateSubTasks => UpdateProductionSubTaskStatus());
        private void UpdateProductionSubTaskStatus()
        {
            foreach (ProductionSubTask productionSubTask in ProductionSubTasks)
            {
                UpdateProductionSubTask(productionSubTask);
            }
        }
        private void UpdateProductionSubTask(ProductionSubTask productionSubTask)
        {
            if (productionSubTask != null)
            {
                ProductionSubTaskController controller = new ProductionSubTaskController();
                bool result = controller.UpdateProductionSubTask(productionSubTask);
                if (!result)
                {
                    MessageBox.Show("Ошибка в обновлении данных");
                }
            }
        }

        public ICommand IOpenExternalDocumentWindow => new RelayCommand(openExternalDocumentWindow => OpenExternalDocumentWindow());
        private void OpenExternalDocumentWindow()
        {
            ExternalDocumentShowWindow externalDocumentShowWindow = new ExternalDocumentShowWindow(ExternalDocument, ExternalDocument.ContractorCompany, CurrentEmployee);
            externalDocumentShowWindow.Show();
        }

        public ICommand IOpenInternalDocumentWindow => new RelayCommand(openInternalDocumentWindow => OpenInternalDocumentWindow());
        private void OpenInternalDocumentWindow()
        {
            InternalDocumentShowWindow internalDocumentShowWindow = new InternalDocumentShowWindow(InternalDocument, CurrentEmployee.EmployeeID);
            internalDocumentShowWindow.Show();
        }

        public ICommand IExit => new RelayCommand(exit => Exit());
        private void Exit()
        {
            ProductionTaskShowWindow.Close();
        }
    }
}
