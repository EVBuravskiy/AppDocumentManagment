using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Views;
using System.Windows;
using System.Windows.Input;

namespace AppDocumentManagment.UI.ViewModels
{
    public class ContractorCompanyViewModel : BaseViewModelClass
    {
        private ContractorCompanyWindow ContractorCompanyWindow;

        private ContractorCompany? ContractorCompany;

        private string contractorCompanyTitle;
        public string ContractorCompanyTitle
        {
            get => contractorCompanyTitle;
            set
            {
                contractorCompanyTitle = value;
                OnPropertyChanged(nameof(ContractorCompanyTitle));
            }
        }
        private string contractorCompanyShortTitle;
        public string ContractorCompanyShortTitle
        {
            get => contractorCompanyShortTitle;
            set
            {
                contractorCompanyShortTitle = value;
                OnPropertyChanged(nameof(ContractorCompanyShortTitle));
            }
        }
        private string contractorCompanyAddress;
        public string ContractorCompanyAddress
        {
            get => contractorCompanyAddress;
            set
            {
                contractorCompanyAddress = value;
                OnPropertyChanged(nameof(ContractorCompanyAddress));
            }
        }

        private string contractorCompanyPhone;
        public string ContractorCompanyPhone
        {
            get => contractorCompanyPhone;
            set
            {
                contractorCompanyPhone = value;
                OnPropertyChanged(nameof(ContractorCompanyPhone));
            }
        }

        private string contractorCompanyEmail;
        public string ContractorCompanyEmail
        {
            get => contractorCompanyEmail;
            set
            {
                contractorCompanyEmail = value;
                OnPropertyChanged(nameof(ContractorCompanyEmail));
            }
        }

        private bool isNew = true;

        public ContractorCompanyViewModel(ContractorCompanyWindow contractorCompanyWindow, ContractorCompany? selectedContractorCompany)
        {
            ContractorCompanyWindow = contractorCompanyWindow;
            if (selectedContractorCompany != null)
            {
                ContractorCompany = selectedContractorCompany;
                isNew = false;
                ContractorCompanyTitle = selectedContractorCompany.ContractorCompanyTitle;
                ContractorCompanyShortTitle = selectedContractorCompany.ContractorCompanyShortTitle;
                ContractorCompanyAddress = selectedContractorCompany.ContractorCompanyAddress;
                ContractorCompanyPhone = selectedContractorCompany.ContractorCompanyPhone;
                ContractorCompanyEmail = selectedContractorCompany.ContractorCompanyEmail;
            }
        }

        public ICommand ISaveContractorCompany => new RelayCommand(saveContractorCompany => SaveContractorCompany());
        private void SaveContractorCompany()
        {
            if (isNew)
            {
                ContractorCompany = new ContractorCompany();
            }
            ContractorCompany.ContractorCompanyTitle = ContractorCompanyTitle;
            ContractorCompany.ContractorCompanyShortTitle = ContractorCompanyShortTitle;
            ContractorCompany.ContractorCompanyAddress = ContractorCompanyAddress;
            ContractorCompany.ContractorCompanyPhone = ContractorCompanyPhone;
            ContractorCompany.ContractorCompanyEmail = ContractorCompanyEmail;
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            bool result = false;
            if (isNew)
            {
                result = contractorCompanyController.AddContractorCompany(ContractorCompany);
            }
            else
            {
                result = contractorCompanyController.UpdateContractorCompany(ContractorCompany);
            }
            if (result)
            {
                MessageBox.Show($"Контрагент {ContractorCompany.ContractorCompanyTitle} сохранен");
            }
            else
            {
                MessageBox.Show($"Ошибка! Контрагент {ContractorCompany.ContractorCompanyTitle} не сохранен");
            }
            Exit();
        }

        public ICommand IRemoveContractorCompany => new RelayCommand(removeContractorCompany => RemoveContractorCompany());
        private void RemoveContractorCompany()
        {
            bool result = false;
            if (ContractorCompany != null)
            {
                ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
                result = contractorCompanyController.RemoveContractorCompany(ContractorCompany);
                if (result)
                {
                    MessageBox.Show($"Контрагент {ContractorCompany.ContractorCompanyTitle} удален");
                }
                else
                {
                    MessageBox.Show($"Ошибка! Контрагент {ContractorCompany.ContractorCompanyTitle} не удален");
                }
                Exit();
            }
            if (!result)
            {
                MessageBox.Show($"Ошибка! Контрагент не был получен");
            }
            Exit();
        }

        public ICommand IExit => new RelayCommand(exit => Exit());
        private void Exit()
        {
            ContractorCompanyWindow.Close();
        }
    }
}
