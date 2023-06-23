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
    public partial class UnitPlaceVM:BaseVM
    {
        private const int _pageSize = 10;
        [ObservableProperty]
        private int _currentPage = 0;
        [ObservableProperty]
        private int _pages;

        private AccountingUnit _Unit;
        private Place _Place;
        [ObservableProperty]
        private ObservableCollection<UnitPlace> _UnitPlaces;
        private bool _IsActiveOnly = true;
        public UnitPlaceVM(INavigationService navigationService, MainModel MainModel, DisplayService displayService, DataTransferService dataTransferService) : base(navigationService, MainModel, displayService, dataTransferService)
        {
            _Unit = _dataTransferService.TransferAccountingUnit;
            _dataTransferService.TransferAccountingUnit = null;
            _Place = _dataTransferService.TransferPlace;
            _dataTransferService.TransferPlace = null;
            if (_Unit is not null)
            {
                IsUnitMode = true;
            }
            else
            {
                IsUnitMode = false;
            }
        }
        public bool IsActiveOnly
        {
            get => _IsActiveOnly;
            set
            {
                _IsActiveOnly = value;
                OnPropertyChanged();
                CurrentPage = 0;
                UpdateCollection();
            }
        }
        public bool IsUnitMode { get; private set; }
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
        private void UnitPlaceUpdate(UnitPlace UnitPlace)
        {
            _dataTransferService.TransferUnitPlace = UnitPlace;
            _navigationService.NavigateToAsync(nameof(UnitPlaceEditPage));
        }
        [RelayCommand]
        private void AddUnitPlace()
        {
            _dataTransferService.TransferUnitPlace = new UnitPlace() {Unit=_Unit,UnitId=_Unit.Id };
            _navigationService.NavigateToAsync(nameof(UnitPlaceEditPage));
        }
        [RelayCommand]
        private void Appearing()
        {
            try
            {
                if (_Place is not null)
                {
                    _Place.UnitPlaces = _mainModel.GetAllUnitPlaceByPlace(_Place.Id);
                }
                else
                {
                    _Unit.UnitPlaces = _mainModel.GetAllUnitPlaceByUnit(_Unit.Id);
                }
                UpdateCollection();
            }
            catch (Exception ex)
            {
                Errors = ValidateAndErrorsTools.GetInfo(ex);
                OnPropertyChanged(nameof(Errors));
            }
        }
        private void UpdateCollection()
        {
            try
            {
                ICollection<UnitPlace> unitPlaces;
                if (_Place is not null)
                {
                    unitPlaces = _Place.UnitPlaces;
                }
                else
                {
                    unitPlaces = _Unit.UnitPlaces;
                }
                if (IsActiveOnly) unitPlaces = unitPlaces.Where(y => y.DateEnd == null).ToList();
                unitPlaces = unitPlaces.OrderBy(x => x.Unit.FullNameUnit.UnitType.Name).ToList();
                Pages = unitPlaces.Count() % _pageSize==0?unitPlaces.Count()/_pageSize-1:unitPlaces.Count()/_pageSize;
                UnitPlaces = unitPlaces
                    .Skip(CurrentPage * _pageSize)
                    .Take(_pageSize)
                    .ToObservableCollection();
                OnPropertyChanged(nameof(UnitPlaces));
            }
            catch (Exception ex)
            {
                Errors = ValidateAndErrorsTools.GetInfo(ex);
                OnPropertyChanged(nameof(Errors));
            }
        }
    }
}
