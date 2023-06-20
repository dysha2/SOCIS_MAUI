using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SOCIS_MAUI.ViewModel
{
    public partial class FullNameUnitVM : BaseVM
    {
        private const int _pageSize=10;
        [ObservableProperty]
        private int _currentPage=0;
        [ObservableProperty]
        private int _pages;
        public static ObservableCollection<FullNameUnit> SearchFullNameUnits { get; private set; }
        public static Action<FullNameUnit> FullNameUnitAction { get; private set; }
        public ObservableCollection<FullNameUnit> FullNameUnits { get; private set; }
        public FullNameUnitVM(INavigationService navigationService, MainModel MainModel, DisplayService displayService, DataTransferService dataTransferService) : base(navigationService, MainModel, displayService, dataTransferService)
        {
            FullNameUnitAction = FullNameUnitUpdate;
            UpdateCollection();
            SearchFullNameUnits = _mainModel.GetAllFullNameUnits();
        }
        [RelayCommand]
        private void FullNameUnitUpdate(FullNameUnit FullNameUnit)
        {
            _dataTransferService.TransferFullNameUnit = FullNameUnit;
            _navigationService.NavigateToAsync(nameof(FullNameUnitEditPage));
        }
        [RelayCommand]
        private void AddFullNameUnit()
        {
            _dataTransferService.TransferFullNameUnit = new FullNameUnit();
            _navigationService.NavigateToAsync(nameof(FullNameUnitEditPage));
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
            Pages = _mainModel.GetAllFullNameUnits().Count / _pageSize;
            FullNameUnits = _mainModel.GetAllFullNameUnits()
                .Skip(CurrentPage*_pageSize)
                .Take(_pageSize)
                .ToObservableCollection();
            OnPropertyChanged(nameof(FullNameUnits));
        }
    }
}
