using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelpfulProjectCSharp.ASP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIS_MAUI.ViewModel
{
    public partial class AccountingUnitVM : BaseVM
    {
        private const int _pageSize = 10;
        [ObservableProperty]
        private int _currentPage = 0;
        [ObservableProperty]
        private int _pages;
        public static ObservableCollection<AccountingUnit> SearchAccountingUnits { get; private set; }
        public static Action<AccountingUnit> AccountingUnitAction { get; private set; }
        public ObservableCollection<AccountingUnit> AccountingUnits { get; private set; }
        public AccountingUnitVM(INavigationService navigationService, MainModel MainModel, DisplayService displayService, DataTransferService dataTransferService) : base(navigationService, MainModel, displayService, dataTransferService)
        {
            AccountingUnitAction = AccountingUnitUpdate;
            UpdateCollection();
            SearchAccountingUnits = _mainModel.GetAllAccountingUnits();
        }
        [RelayCommand]
        private void AccountingUnitUpdate(AccountingUnit AccountingUnit)
        {
            _dataTransferService.TransferAccountingUnit = AccountingUnit;
            _navigationService.NavigateToAsync(nameof(AccountingUnitEditPage));
        }
        [RelayCommand]
        private void NextPage()
        {
            if (CurrentPage < Pages)
            {
                CurrentPage++;
                UpdateCollection();
            }
        }
        [RelayCommand]
        private void PreviousPage()
        {
            if (CurrentPage > 0)
            {
                CurrentPage--;
                UpdateCollection();
            }
        }
        [RelayCommand]
        private void UpdateCollection()
        {
            try
            {
                var units = _mainModel.GetAllAccountingUnits();
                Pages = units.Count() % _pageSize == 0 ? units.Count() / _pageSize - 1 : units.Count() / _pageSize;
                AccountingUnits = _mainModel.GetAllAccountingUnits()
                    .Skip(CurrentPage * _pageSize)
                    .Take(_pageSize)
                    .ToObservableCollection();
                OnPropertyChanged(nameof(AccountingUnits));
            }
            catch (Exception ex)
            {
                Errors = ValidateAndErrorsTools.GetInfo(ex);
                OnPropertyChanged(Errors);
            }
        }
    }
}
