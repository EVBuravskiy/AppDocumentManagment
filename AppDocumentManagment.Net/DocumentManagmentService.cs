using System;
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
            nextClientID ++;
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
            if(clients.Count == 0) nextClientID = 1;
        }

        //Методы отправления сообщений на сервер
        void SendMessage(string message, int userID)
        {

        }

        //Методы работы с отделами / департаментами
        bool AddDepartments(Department department)
        {
            DeparmentController deparmentController = new DeparmentController();
            return departmentController.AddDepartments(department);
        }

        List<Department> GetAllDepartments()
        {
            DeparmentController deparmentController = new DeparmentController();
            deparmentController.GetAllDepartments();
        }

        Department GetDepartmentByID(int departmentID)
        {
            DeparmentController deparmentController = new DeparmentController();
            return deparmentController.GetDepartmentByID(departmentID);
        }

        Department GetDepartmentByTitle(string departmentTitle)
        {
            DeparmentController deparmentController = new DeparmentController();
            return deparmentController.GetDepartmentByTitle(departmentTitle);
        }

        Department GetDepartmentByShortTitle(string departmentShortTitle)
        {
            DeparmentController deparmentController = new DeparmentController();
            return deparmentController.GetDepartmentByShortTitle(departmentShortTitle);
        }

        bool UpdateDepartment(Department inputDepartment)
        {
            DeparmentController deparmentController = new DeparmentController();
            return deparmentController.UpdateDepartment(inputDepartment);
        }

        bool RemoveDepartment(Department inputDepartment)
        {
            DeparmentController deparmentController = new DeparmentController();
            return deparmentController.RemoveDepartment(inputDepartment);
        }

        //Методы работы с сотрудниками
        void AddEmployee(Employee newEmployee)
        {
            EmployeeController employeeController = new EmployeeController();
            employeeController.AddEmployee(newEmployee);
        }

        List<Employee> GetAllEmployees()
        {
            EmployeeController employeeController = new EmployeeController();
            return employeeController.GetAllEmployees();
        }

        Employee GetEmployeeByID(int employeeID)
        {
            EmployeeController employeeController = new EmployeeController();
            return employeeController.GetEmployeeByID(employeeID);
        }

        List<Employee> GetEmployeesByDepartmentID(int departmentID)
        {
            EmployeeController employeeController = new EmployeeController();
            return employeeController.GetEmployeesByDeparmentID(departmentID);
        }

        List<Employee> GetPerformersByDepartmentID(int departmentID)
        {
            EmployeeController employeeController = new EmployeeController();
            return employeeController.GetPerformers(departmentID);
        }

        List<Employee> GetHeadsByDepartmentID(int departmentID)
        {
            EmployeeController employeeController = new EmployeeController();
            return employeeController.GetHeads(departmentID);
        }

        Employee GetGeneralDirector()
        {
            EmployeeController employeeController = new EmployeeController();
            return employeeController.GetGeneralDirector();
        }

        bool DeleteEmployee(Employee employee)
        {
            EmployeeController employeeController = new EmployeeController();
            return employeeController.DeleteEmployee(employee);
        }

        void UpdateEmployee(Employee employee)
        {
            EmployeeController employeeController = new EmployeeController();
            employeeController.UpdateEmployee(employee);
        }

        bool CheckAviableEmployee()
        {
            EmployeeController employeeController = new EmployeeController();
            return employeeController.CheckAviableEmployee();
        }

        //Методы работы с фотографиями сотрудников
        bool AddEmployeePhoto(EmployeePhoto employeePhoto)
        {
            EmployeePhotoController employeePhotoController = new EmployeePhotoController();
            return employeePhotoController.AddEmployeePhoto(employeePhoto);
        }

        List<EmployeePhoto> GetEmployeePhotos()
        {
            EmployeePhotoController employeePhotoController = new EmployeePhotoController();
            return employeePhotoController.GetEmployeePhotos();
        }

        EmployeePhoto GetEmployeePhotoByEmployeeID(int employeeID)
        {
            EmployeePhotoController employeePhotoController = new EmployeePhotoController();
            return employeePhotoController.GetEmployeePhotoByID(employeeID);
        }

        //Методы работы с компаниями-контрагентами
        bool AddContractorCompany(ContractorCompany contractorCompany)
        {
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            return contractorCompanyController.AddContractorCompany(contractorCompany);
        }

        List<ContractorCompany> GetContractorCompanies()
        {
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            return contractorCompanyController.GetContractorCompanies();
        }

        ContractorCompany GetContractorCompany(ContractorCompany contractorCompany)
        {
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            return contractorCompanyController.GetContractorCompany(contractorCompany);
        }

        ContractorCompany GetContractorCompanyByID(int contractorCompanyID)
        {
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            return contractorCompanyController.GetContractorCompanyByID(contractorCompanyID);
        }

        bool UpdateContractorCompany(ContractorCompany contractorCompany)
        {
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            return contractorCompanyController.UpdateContractorCompany(contractorCompany);
        }

        bool RemoveContractorCompany(ContractorCompany contractorCompany)
        {
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            return contractorCompanyController.RemoveContractorCompany(contractorCompany);
        }

        //Методы работы с входящими документами
        bool AddExternalDocument(ExternalDocument externalDocument)
        {
            ExternalDocumentController externalDocumentController = new ExternalDocumentController();
            return externalDocumentController.AddExternalDocument(externalDocument);
        }

        List<ExternalDocument> GetAllExternalDocuments()
        {
            ExternalDocumentController externalDocumentController = new ExternalDocumentController();
            return externalDocumentController.GetAllDocuments();
        }

        List<ExternalDocument> GetExternalDocumentsByEmployeeReceivedDocumentID(int employeeReceivedDocumentID)
        {
            ExternalDocumentController externalDocumentController = new ExternalDocumentController();
            return externalDocumentController.GetExternalDocumentsByEmployeeReceivedDocumentID(employeeReceivedDocumentID);
        }

        bool RemoveExternalDocument(ExternalDocument externalDocument)
        {
            ExternalDocumentController externalDocumentController = new ExternalDocumentController();
            return externalDocumentController.RemoveDocument(externalDocument);
        }

        bool UpdateExternalDocument(ExternalDocument externalDocument)
        {
            ExternalDocumentController externalDocumentController = new ExternalDocumentController();
            return externalDocumentController.UpdateDocument(externalDocument);
        }

        //Методы работы с файлами входящих документов
        bool AddExternalDocumentFile(ExternalDocumentFile documentFile, ExternalDocument externalDocument)
        {
            ExternalDocumentFileController externalDocumentFileController = new ExternalDocumentFileController();
            return externalDocumentFileController.AddDocumentFile(documentFile, externalDocument);
        }

        bool AddExternalDocumentFiles(List<ExternalDocumentFiles> externalDocumentFiles)
        {
            ExternalDocumentFileController externalDocumentFileController = new ExternalDocumentFileController();
            return externalDocumentFileController.AddDocumentFiles(externalDocumentFiles);
        }

        List<ExternalDocumentFile> GetExternalDocumentFiles(int externalDocumentID)
        {
            ExternalDocumentFileController externalDocumentFileController = new ExternalDocumentFileController();
            return externalDocumentFileController.GetDocumentFiles(externalDocumentID);
        }

        bool RemoveExternalDocumentFile(ExternalDocumentFile externalDocumentFile)
        {
            ExternalDocumentFileController externalDocumentFileController = new ExternalDocumentFileController();
            return externalDocumentFileController.RemoveDocumentFile(externalDocumentFile);
        }

        //Методы работы с внутренними документами
        bool AddInternalDocument(InternalDocument internalDocument)
        {
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            return internalDocumentController.AddInternalDocument(internalDocument);
        }

        List<InternalDocument> GetInternalDocuments()
        {
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            return internalDocumentController.GetInternalDocuments();
        }

        List<InternalDocument> GetInternalDocumentsByEmployeeRecievedDocumentID(int employeeRecievedDocumentID)
        {
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            return internalDocumentController.GetAllDocumentsByEmployeeRecievedDocumentID(employeeRecievedDocumentID);
        }

        bool RemoveInternalDocument(InternalDocument internalDocument)
        {
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            return internalDocumentController.RemoveInternalDocument(internalDocument);
        }

        bool UpdateInternalDocument(InternalDocument internalDocument)
        {
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            internalDocumentController.UpdateInternalDocument(internalDocument);
        }

        int GetCountInternalDocumentByType(InternalDocumentType type)
        {
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            return internalDocumentController.GetCountInternalDocumentByType(type);
        }

        //Методы работы с файлами внутренних документов
        bool AddInternalDocumentFile(InternalDocumentFile internalDocumentFile, InternalDocument internalDocument)
        {
            InternalDocumentFileController internalDocumentFileController = new InternalDocumentFileController();
            return internalDocumentFileController.AddInternalDocumentFile(internalDocumentFile, internalDocument);
        }

        bool AddInternalDocumentFiles(List<InternalDocumentFile> internalDocumentFiles)
        {
            InternalDocumentFileController internalDocumentFileController = new InternalDocumentFileController();
            return internalDocumentFileController.AddItemInternalDocumentFiles(internalDocumentFiles);
        }

        List<InternalDocumentFile> GetInternalDocumentFiles(int internalDocumentID)
        {
            InternalDocumentFileController internalDocumentFileController = new InternalDocumentFileController();
            return internalDocumentFileController.GetInternalDocumentFiles(internalDocumentID);
        }

        bool RemoveInternalDocumentFile(InternalDocumentFile internalDocumentFile)
        {
            InternalDocumentFileController internalDocumentFileController = new InternalDocumentFileController();
            return internalDocumentFileController.RemoveInternalDocumentFile(internalDocumentFile);
        }

        //Методы работы с регистрацией пользователей
        bool AddRegistratedUser(RegistratedUser registratedUser)
        {
            RegistredUserController registredUserController = new RegistredUserController();
            registredUserController.AddRegistratedUser(registratedUser);
        }

        List<RegistredUser> GetAllRegistredUsers()
        {
            RegistredUserController registredUserController = new RegistredUserController();
            return registredUserController.GetAllRegisteredUsers();
        }

        RegistredUser GetRegistredUser(string login, string password)
        {
            RegistredUserController registredUserController = new RegistredUserController();
            return registredUserController.GetRegistratedUser(login, password);
        }

        RegistredUser UserAutentication(string login, string password)
        {
            RegistredUserController registredUserController = new RegistredUserController();
            return registredUserController.UserAutentication(login, password);
        }

        bool UpdateRegistratedUser(RegistratedUser registratedUser)
        {
            RegistredUserController registredUserController = new RegistredUserController();
            return registredUserController.UpdateRegistratedUser(registratedUser);
        }

        bool RemoveRegistratedUser(RegistratedUser registratedUser)
        {
            RegistredUserController registredUserController = new RegistredUserController();
            return registredUserController.RemoveRegistratedUser(registratedUser);
        }

        bool CheckAviableRegistredUsers()
        {
            RegistredUserController registredUserController = new RegistredUserController();
            return registredUserController.CheckAviableRegistredUsers();
        }
    }
}
