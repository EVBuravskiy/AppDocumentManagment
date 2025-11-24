namespace AppDocumentManagment.Net
{
    // ПРИМЕЧАНИЕ. Можно использовать команду "Переименовать" в меню "Рефакторинг", чтобы изменить имя интерфейса "IDocumentManagmentService" в коде и файле конфигурации.
    //Интерфейс работы сервера
    [ServiceContract(CallbackContract = typeof(IServerChatCallback))]
    public interface IDocumentManagmentService
    {
        //Метод подключения к сервису
        [OperationContract]
        int Connect();

        //Метод отключения от сервиса
        [OperationContract]
        void Disconnect(int clientID);

        //Методы работы с отделами / департаментами
        [OperationContract]
        bool AddDepartments(Department department);

        [OperationContract]
        List<Department> GetAllDepartments();

        [OperationContract]
        Department GetDepartmentByID(int departmentID);

        [OperationContract]
        Department GetDepartmentByTitle(string departmentTitle);

        [OperationContract]
        Department GetDepartmentByShortTitle(string departmentShortTitle);

        [OperationContract]
        bool UpdateDepartment(Department inputDepartment);

        [OperationContract]
        bool RemoveDepartment(Department inputDepartment);

        //Методы работы с сотрудниками
        [OperationContract(IsOneWay = true)]
        void AddEmployee(Employee newEmployee);

        [OperationContract]
        List<Employee> GetAllEmployees();

        [OperationContract]
        Employee GetEmployeeByID(int employeeID);

        [OperationContract]
        List<Employee> GetEmployeesByDepartmentID(int departmentID);

        [OperationContract]
        List<Employee> GetPerformersByDepartmentID(int departmentID);

        [OperationContract]
        List<Employee> GetHeadsByDepartmentID(int departmentID);

        [OperationContract]
        Employee GetGeneralDirector();

        [OperationContract]
        bool DeleteEmployee(Employee employee);

        [OperationContract(IsOneWay = true)]
        void UpdateEmployee(Employee employee);

        [OperationContract]
        bool CheckAviableEmployee();

        //Методы работы с фотографиями сотрудников
        [OperationContract]
        bool AddEmployeePhoto(EmployeePhoto employeePhoto);

        [OperationContract]
        List<EmployeePhoto> GetEmployeePhotos();

        [OperationContract]
        EmployeePhoto GetEmployeePhotoByEmployeeID(int employeeID);

        //Методы работы с компаниями-контрагентами
        [OperationContract]
        bool AddContractorCompany(ContractorCompany contractorCompany);

        [OperationContract]
        List<ContractorCompany> GetContractorCompanies();

        [OperationContract]
        ContractorCompany GetContractorCompany(ContractorCompany contractorCompany);

        [OperationContract]
        ContractorCompany GetContractorCompanyByID(int contractorCompanyID);

        [OperationContract]
        bool UpdateContractorCompany(ContractorCompany contractorCompany);

        [OperationContract]
        bool RemoveContractorCompany(ContractorCompany contractorCompany);

        //Методы работы с входящими документами
        [OperationContract]
        bool AddExternalDocument(ExternalDocument externalDocument);

        [OperationContract]
        List<ExternalDocument> GetAllExternalDocuments();

        [OperationContract]
        List<ExternalDocument> GetExternalDocumentsByEmployeeReceivedDocumentID(int employeeReceivedDocumentID);

        [OperationContract]
        bool RemoveExternalDocument(ExternalDocument externalDocument);

        [OperationContract]
        bool UpdateExternalDocument(ExternalDocument externalDocument);

        //Методы работы с файлами входящих документов
        [OperationContract]
        bool AddExternalDocumentFile(ExternalDocumentFile documentFile, ExternalDocument externalDocument);

        [OperationContract]
        bool AddExternalDocumentFiles(List<ExternalDocumentFiles> externalDocumentFiles);

        [OperationContract]
        List<ExternalDocumentFile> GetExternalDocumentFiles(int externalDocumentID);

        [OperationContract]
        bool RemoveExternalDocumentFile(ExternalDocumentFile externalDocumentFile);

        //Методы работы с внутренними документами
        [OperationContract]
        bool AddInternalDocument(InternalDocument internalDocument);

        [OperationContract]
        List<InternalDocument> GetInternalDocuments();

        [OperationContract]
        List<InternalDocument> GetInternalDocumentsByEmployeeRecievedDocumentID(int employeeRecievedDocumentID);

        [OperationContract]
        bool RemoveInternalDocument(InternalDocument internalDocument);

        [OperationContract]
        bool UpdateInternalDocument(InternalDocument internalDocument);

        [OperationContract]
        int GetCountInternalDocumentByType(InternalDocumentType type);

        //Методы работы с файлами внутренних документов
        [OperationContract]
        bool AddInternalDocumentFile(InternalDocumentFile internalDocumentFile, InternalDocument internalDocument);

        [OperationContract]
        bool AddInternalDocumentFiles(List<InternalDocumentFile> internalDocumentFiles);

        [OperationContract]
        List<InternalDocumentFile> GetInternalDocumentFiles(int internalDocumentID);

        [OperationContract]
        bool RemoveInternalDocumentFile(InternalDocumentFile internalDocumentFile);

        //Методы работы с регистрацией пользователей
        [OperationContract]
        bool AddRegistratedUser(RegistratedUser registratedUser);

        [OperationContract]
        List<RegistredUser> GetAllRegistredUsers();

        [OperationContract]
        RegistredUser GetRegistredUser(string login, string password);

        [OperationContract]
        RegistredUser UserAutentication(string login, string password);

        [OperationContract]
        bool UpdateRegistratedUser(RegistratedUser registratedUser);

        [OperationContract]
        bool RemoveRegistratedUser(RegistratedUser registratedUser);

        [OperationContract]
        bool CheckAviableRegistredUsers();
    }

    //Интерфейс работы на стороне клиента
    public interface IServerUpdateDataCallback
    {
        [OperationContract(IsOneWay = true)]
        void UpdateDataCallBack(string message);
    }

}
