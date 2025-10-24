using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppDocumentManagment.UI.ViewModels
{
    public class ContractorCompanyListViewModel : BaseViewModelClass
    {
        ContractorСompanyListWindow Window;

        private List<ContractorCompany> ContractorCompaniesList {  get; set; }
        public ObservableCollection<ContractorCompany> ContractorCompanies {  get; set; }

        private ContractorCompany selectedContractorCompany;
        public ContractorCompany SelectedContractorCompany
        {
            get => selectedContractorCompany;
            set
            {
                selectedContractorCompany = value;
                OnPropertyChanged(nameof(SelectedContractorCompany));
            }
        }

        private string _searchString;
        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                OnPropertyChanged(nameof(SearchString));
                GetContractorCompanyBySearchString(value);
            }
        }

        public ContractorCompanyListViewModel(ContractorСompanyListWindow window) 
        {
            Window = window;
            ContractorCompanies = new ObservableCollection<ContractorCompany>();
            GetListOfContractorCompanies();
            InitializeContractorCompanies();
        }

        private void GetListOfContractorCompanies()
        {
            if(ContractorCompaniesList == null)
            {
                ContractorCompaniesList = new List<ContractorCompany>();
            }
            else
            {
                ContractorCompaniesList.Clear();
            }
            ContractorCompanyController controller = new ContractorCompanyController();
            ContractorCompaniesList = controller.GetContractorCompanies();
        }

        private void InitializeContractorCompanies()
        {
            if (ContractorCompaniesList == null)
            {
                ContractorCompanies = new ObservableCollection<ContractorCompany>();
            }
            else
            {
                ContractorCompanies.Clear();
            }
            if (ContractorCompaniesList?.Count > 0)
            {
                foreach (ContractorCompany company in ContractorCompaniesList)
                {
                    ContractorCompanies.Add(company);
                }
            }
        }

        public void GetContractorCompanyBySearchString(string searchingString)
        {
            string searchString = searchingString.Trim();
            if (string.IsNullOrWhiteSpace(searchString) || string.IsNullOrEmpty(searchString))
            {
                InitializeContractorCompanies();
                return;
            }   
            ContractorCompanies.Clear();
            if (ContractorCompaniesList?.Count > 0)
            {
                foreach (var company in ContractorCompaniesList)
                {
                    if (company.ContractorCompanyTitle.ToLower().Contains(searchString.ToLower()))
                    {
                        ContractorCompanies.Add(company);
                    }
                }
            }
        }

        public ICommand IAddNewContractorCompany => new RelayCommand(addNewContractorCompany => AddNewContractorCompany());
        private void AddNewContractorCompany()
        {
            ContractorCompanyWindow contractorCompanyWindow = new ContractorCompanyWindow(null);
            contractorCompanyWindow.ShowDialog();
            GetListOfContractorCompanies();
            InitializeContractorCompanies();
        }

        public ICommand IEditContractorCompany => new RelayCommand(editContractorCompany => EditContractorCompany());
        private void EditContractorCompany()
        {
            if (SelectedContractorCompany != null)
            {
                ContractorCompanyWindow contractorCompanyWindow = new ContractorCompanyWindow(SelectedContractorCompany);
                contractorCompanyWindow.ShowDialog();
                GetListOfContractorCompanies();
                InitializeContractorCompanies();
            }
        }
    }
}
