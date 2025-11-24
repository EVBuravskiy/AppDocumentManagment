using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.DB.Controllers;


namespace AppDocumentManagment.Net
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "DocumentManagmentService" в коде и файле конфигурации.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class DocumentManagmentService : IDocumentManagmentService
    {
        private List<ServerClient> clients = new List<ServerClient>();
        int nextClientID = 1;

        public int Connect()
        {
            ServerClient newClient = new ServerClient()
            {
                ClientID = nextClientID,
                OperationContext = OperationContext.Current,
            };
            nextClientID++;
            clients.Add(newClient);
            return newClient.ClientID;
        }

        public void Disconnect(int clientID)
        {
            ServerClient client = clients.SingleOrDefault(c => c.ClientID == clientID);
            if (client != null)
            {
                clients.Remove(client);
            }
            if (clients.Count == 0) nextClientID = 1;
        }

        //Методы работы с отделами / департаментами
        public bool AddDepartments(Department department)
        {
            DepartmentController departmentController = new DepartmentController();
            bool result = departmentController.AddDepartment(department);
            SendMessageAboutUpdate();
            return result;
        }

        public List<Department> GetAllDepartments()
        {
            DepartmentController departmentController = new DepartmentController();
            return departmentController.GetAllDepartments();
        }

        public Department GetDepartmentByID(int departmentID)
        {
            DepartmentController departmentController = new DepartmentController();
            return departmentController.GetDepartmentByID(departmentID);
        }

        public Department GetDepartmentByTitle(string departmentTitle)
        {
            DepartmentController departmentController = new DepartmentController();
            return departmentController.GetDepartmentByTitle(departmentTitle);
        }

        public Department GetDepartmentByShortTitle(string departmentShortTitle)
        {
            DepartmentController departmentController = new DepartmentController();
            return departmentController.GetDepartmentByShortTitle(departmentShortTitle);
        }

        public bool UpdateDepartment(Department inputDepartment)
        {
            DepartmentController departmentController = new DepartmentController();
            bool result = departmentController.UpdateDepartment(inputDepartment);
            SendMessageAboutUpdate();
            return result;
        }

        public bool RemoveDepartment(Department inputDepartment)
        {
            DepartmentController departmentController = new DepartmentController();
            bool result = departmentController.RemoveDepartment(inputDepartment);
            SendMessageAboutUpdate();
            return result;
        }

        //Методы работы с сотрудниками
        public bool AddEmployee(Employee newEmployee)
        {
            EmployeeController employeeController = new EmployeeController();
            bool result = employeeController.AddEmployee(newEmployee);
            SendMessageAboutUpdate();
            return result;
        }

        public List<Employee> GetAllEmployees()
        {
            EmployeeController employeeController = new EmployeeController();
            return employeeController.GetAllEmployees();
        }

        public Employee GetEmployeeByID(int employeeID)
        {
            EmployeeController employeeController = new EmployeeController();
            return employeeController.GetEmployeeByID(employeeID);
        }

        public List<Employee> GetEmployeesByDepartmentID(int departmentID)
        {
            EmployeeController employeeController = new EmployeeController();
            return employeeController.GetEmployeesByDeparmentID(departmentID);
        }

        public List<Employee> GetPerformersByDepartmentID(int departmentID)
        {
            EmployeeController employeeController = new EmployeeController();
            return employeeController.GetPerformers(departmentID);
        }

        public List<Employee> GetHeadsByDepartmentID(int departmentID)
        {
            EmployeeController employeeController = new EmployeeController();
            return employeeController.GetHeads(departmentID);

        }

        public Employee GetGeneralDirector()
        {
            EmployeeController employeeController = new EmployeeController();
            return employeeController.GetGeneralDirector();
        }

        public bool DeleteEmployee(Employee employee)
        {
            EmployeeController employeeController = new EmployeeController();
            bool result = employeeController.DeleteEmployee(employee);
            SendMessageAboutUpdate();
            return result;
        }

        public bool UpdateEmployee(Employee employee)
        {
            EmployeeController employeeController = new EmployeeController();
            bool result = employeeController.UpdateEmployee(employee);
            SendMessageAboutUpdate();
            return result;
        }

        public bool CheckAviableEmployee()
        {
            EmployeeController employeeController = new EmployeeController();
            return employeeController.CheckAviableEmployee();
        }

        //Методы работы с фотографиями сотрудников
        public bool AddEmployeePhoto(EmployeePhoto employeePhoto)
        {
            EmployeePhotoController employeePhotoController = new EmployeePhotoController();
            bool result = employeePhotoController.AddEmployeePhoto(employeePhoto);
            SendMessageAboutUpdate();
            return result;
        }

        public List<EmployeePhoto> GetEmployeePhotos()
        {
            EmployeePhotoController employeePhotoController = new EmployeePhotoController();
            return employeePhotoController.GetEmployeePhotos();
        }

        public EmployeePhoto GetEmployeePhotoByEmployeeID(int employeeID)
        {
            EmployeePhotoController employeePhotoController = new EmployeePhotoController();
            return employeePhotoController.GetEmployeePhotoByID(employeeID);
        }

        //Методы работы с компаниями-контрагентами
        public bool AddContractorCompany(ContractorCompany contractorCompany)
        {
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            bool result = contractorCompanyController.AddContractorCompany(contractorCompany);
            SendMessageAboutUpdate();
            return result;
        }

        public List<ContractorCompany> GetContractorCompanies()
        {
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            return contractorCompanyController.GetContractorCompanies();
        }

        public ContractorCompany GetContractorCompany(ContractorCompany contractorCompany)
        {
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            return contractorCompanyController.GetContractorCompany(contractorCompany);
        }

        public ContractorCompany GetContractorCompanyByID(int contractorCompanyID)
        {
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            return contractorCompanyController.GetContractorCompanyByID(contractorCompanyID);
        }

        public bool UpdateContractorCompany(ContractorCompany contractorCompany)
        {
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            bool result = contractorCompanyController.UpdateContractorCompany(contractorCompany);
            SendMessageAboutUpdate();
            return result;
        }

        public bool RemoveContractorCompany(ContractorCompany contractorCompany)
        {
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            bool result = contractorCompanyController.RemoveContractorCompany(contractorCompany);
            SendMessageAboutUpdate();
            return result;
        }


        //Методы работы с входящими документами
        public bool AddExternalDocument(ExternalDocument externalDocument)
        {
            ExternalDocumentController externalDocumentController = new ExternalDocumentController();
            bool result = externalDocumentController.AddDocument(externalDocument);
            SendMessageAboutUpdate();
            return result;
        }

        public List<ExternalDocument> GetAllExternalDocuments()
        {
            ExternalDocumentController externalDocumentController = new ExternalDocumentController();
            return externalDocumentController.GetAllDocuments();
        }

        public List<ExternalDocument> GetExternalDocumentsByEmployeeReceivedDocumentID(int employeeReceivedDocumentID)
        {
            ExternalDocumentController externalDocumentController = new ExternalDocumentController();
            return externalDocumentController.GetExternalDocumentsByEmployeeReceivedDocumentID(employeeReceivedDocumentID);
        }

        public bool RemoveExternalDocument(ExternalDocument externalDocument)
        {
            ExternalDocumentController externalDocumentController = new ExternalDocumentController();
            bool result = externalDocumentController.RemoveDocument(externalDocument);
            SendMessageAboutUpdate();
            return result;
        }

        public bool UpdateExternalDocument(ExternalDocument externalDocument)
        {
            ExternalDocumentController externalDocumentController = new ExternalDocumentController();
            bool result = externalDocumentController.UpdateDocument(externalDocument);
            SendMessageAboutUpdate();
            return result;
        }

        //Методы работы с файлами входящих документов
        public bool AddExternalDocumentFile(ExternalDocumentFile documentFile, ExternalDocument externalDocument)
        {
            ExternalDocumentFileController externalDocumentFileController = new ExternalDocumentFileController();
            bool result = externalDocumentFileController.AddDocumentFile(documentFile, externalDocument);
            SendMessageAboutUpdate();
            return result;
        }

        public bool AddExternalDocumentFiles(List<ExternalDocumentFile> externalDocumentFiles)
        {
            ExternalDocumentFileController externalDocumentFileController = new ExternalDocumentFileController();
            bool result = externalDocumentFileController.AddDocumentFiles(externalDocumentFiles);
            SendMessageAboutUpdate();
            return result;
        }

        public List<ExternalDocumentFile> GetExternalDocumentFiles(int externalDocumentID)
        {
            ExternalDocumentFileController externalDocumentFileController = new ExternalDocumentFileController();
            return externalDocumentFileController.GetDocumentFiles(externalDocumentID);
        }

        public bool RemoveExternalDocumentFile(ExternalDocumentFile externalDocumentFile)
        {
            ExternalDocumentFileController externalDocumentFileController = new ExternalDocumentFileController();
            bool result = externalDocumentFileController.RemoveDocumentFile(externalDocumentFile);
            SendMessageAboutUpdate();
            return result;
        }

        //Методы работы с внутренними документами
        public bool AddInternalDocument(InternalDocument internalDocument)
        {
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            bool result = internalDocumentController.AddInternalDocument(internalDocument);
            SendMessageAboutUpdate();
            return result;
        }

        public List<InternalDocument> GetInternalDocuments()
        {
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            return internalDocumentController.GetInternalDocuments();
        }

        public List<InternalDocument> GetInternalDocumentsByEmployeeRecievedDocumentID(int employeeRecievedDocumentID)
        {
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            return internalDocumentController.GetInternalDocumentsByEmployeeRecievedDocumentID(employeeRecievedDocumentID);
        }

        public bool RemoveInternalDocument(InternalDocument internalDocument)
        {
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            bool result = internalDocumentController.RemoveInternalDocument(internalDocument);
            SendMessageAboutUpdate();
            return result;
        }

        public bool UpdateInternalDocument(InternalDocument internalDocument)
        {
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            bool result = internalDocumentController.UpdateInternalDocument(internalDocument);
            SendMessageAboutUpdate();
            return result;
        }

        public int GetCountInternalDocumentByType(InternalDocumentType type)
        {
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            return internalDocumentController.GetCountInternalDocumentByType(type);
        }

        //Методы работы с файлами внутренних документов
        public bool AddInternalDocumentFile(InternalDocumentFile internalDocumentFile, InternalDocument internalDocument)
        {
            InternalDocumentFileController internalDocumentFileController = new InternalDocumentFileController();
            bool result = internalDocumentFileController.AddInternalDocumentFile(internalDocumentFile, internalDocument);
            SendMessageAboutUpdate();
            return result;
        }

        public bool AddInternalDocumentFiles(List<InternalDocumentFile> internalDocumentFiles)
        {
            InternalDocumentFileController internalDocumentFileController = new InternalDocumentFileController();
            bool result = internalDocumentFileController.AddInternalDocumentFiles(internalDocumentFiles);
            SendMessageAboutUpdate();
            return result;
        }

        public List<InternalDocumentFile> GetInternalDocumentFiles(int internalDocumentID)
        {
            InternalDocumentFileController internalDocumentFileController = new InternalDocumentFileController();
            return internalDocumentFileController.GetInternalDocumentFiles(internalDocumentID);
        }

        public bool RemoveInternalDocumentFile(InternalDocumentFile internalDocumentFile)
        {
            InternalDocumentFileController internalDocumentFileController = new InternalDocumentFileController();
            bool result = internalDocumentFileController.RemoveInternalDocumentFile(internalDocumentFile);
            SendMessageAboutUpdate();
            return result;
        }

        //Методы работы с регистрацией пользователей
        public bool AddRegistratedUser(RegistredUser registredUser)
        {
            RegistredUserController registredUserController = new RegistredUserController();
            bool result = registredUserController.AddRegistratedUser(registredUser);
            SendMessageAboutUpdate();
            return result;
        }

        public List<RegistredUser> GetAllRegistredUsers()
        {
            RegistredUserController registredUserController = new RegistredUserController();
            return registredUserController.GetAllRegistredUsers();
        }

        public RegistredUser GetRegistredUser(string login, string password)
        {
            RegistredUserController registredUserController = new RegistredUserController();
            return registredUserController.GetRegistratedUser(login, password);
        }

        public bool UpdateRegistratedUser(RegistredUser registredUser)
        {
            RegistredUserController registredUserController = new RegistredUserController();
            bool result = registredUserController.UpdateRegistratedUser(registredUser);
            SendMessageAboutUpdate();
            return result;
        }

        public bool RemoveRegistratedUser(RegistredUser registredUser)
        {
            RegistredUserController registredUserController = new RegistredUserController();
            bool result = registredUserController.RemoveRegistratedUser(registredUser);
            SendMessageAboutUpdate();
            return result;
        }

        public bool CheckAviableRegistredUsers()
        {
            RegistredUserController registredUserController = new RegistredUserController();
            return registredUserController.CheckAviableRegistredUsers();
        }

        /// <summary>
        /// /////////////////////////////
        /// </summary>

        private void SendMessageAboutUpdate()
        {
            if (clients.Count > 0)
            {
                foreach (var client in clients)
                {
                    client.OperationContext.GetCallbackChannel<IServerUpdateDataCallback>().UpdateDataCallBack("Обновлена база данных");
                }
            }
        }
    }
}
