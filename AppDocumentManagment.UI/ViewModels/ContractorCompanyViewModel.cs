using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Views;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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

        private string contractorCompanyInformation;
        public string ContractorCompanyInformation
        {
            get => contractorCompanyInformation;
            set
            {
                contractorCompanyInformation = value;
                OnPropertyChanged(nameof(ContractorCompanyInformation));
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
                ContractorCompanyInformation = selectedContractorCompany.ContractorCompanyInformation;
            }
        }

        public ICommand ISaveContractorCompany => new RelayCommand(saveContractorCompany => SaveContractorCompany());
        private void SaveContractorCompany()
        {
            if (!ValidateContractorCompany()) return;
            if (isNew)
            {
                ContractorCompany = new ContractorCompany();
            }
            ContractorCompany.ContractorCompanyTitle = ContractorCompanyTitle;
            ContractorCompany.ContractorCompanyShortTitle = ContractorCompanyShortTitle;
            ContractorCompany.ContractorCompanyAddress = ContractorCompanyAddress;
            ContractorCompany.ContractorCompanyPhone = ContractorCompanyPhone;
            ContractorCompany.ContractorCompanyEmail = ContractorCompanyEmail;
            ContractorCompany.ContractorCompanyInformation = ContractorCompanyInformation;
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

        private bool ValidateContractorCompany()
        {
            if (string.IsNullOrEmpty(ContractorCompanyTitle))
            {
                MessageBox.Show("Введите наименование организации");
                ContractorCompanyWindow.ContractorCompanyTitle.BorderThickness = new System.Windows.Thickness(2);
                ContractorCompanyWindow.ContractorCompanyTitle.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                ContractorCompanyWindow.ContractorCompanyTitle.BorderThickness = new System.Windows.Thickness(2);
                ContractorCompanyWindow.ContractorCompanyTitle.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            if (string.IsNullOrEmpty(ContractorCompanyShortTitle))
            {
                MessageBox.Show("Введите сокращенное наименование организации");
                ContractorCompanyWindow.ContractorCompanyShortTitle.BorderThickness = new System.Windows.Thickness(2);
                ContractorCompanyWindow.ContractorCompanyShortTitle.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                ContractorCompanyWindow.ContractorCompanyShortTitle.BorderThickness = new System.Windows.Thickness(2);
                ContractorCompanyWindow.ContractorCompanyShortTitle.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            if (string.IsNullOrEmpty(ContractorCompanyAddress))
            {
                MessageBox.Show("Введите адрес организации");
                ContractorCompanyWindow.ContractorCompanyAddress.BorderThickness = new System.Windows.Thickness(2);
                ContractorCompanyWindow.ContractorCompanyAddress.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                ContractorCompanyWindow.ContractorCompanyAddress.BorderThickness = new System.Windows.Thickness(2);
                ContractorCompanyWindow.ContractorCompanyAddress.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            return true;
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
