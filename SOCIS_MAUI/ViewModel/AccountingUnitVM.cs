using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
            Pages = _mainModel.GetAllAccountingUnits().Count / _pageSize;
            AccountingUnits = _mainModel.GetAllAccountingUnits()
                .Skip(CurrentPage * _pageSize)
                .Take(_pageSize)
                .ToObservableCollection();
            OnPropertyChanged(nameof(AccountingUnits));
        }
    }
}
