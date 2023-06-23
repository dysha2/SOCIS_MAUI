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
    public partial class UnitRespPersonVM : BaseVM
    {
        private const int _pageSize = 10;
        [ObservableProperty]
        private int _currentPage = 0;
        [ObservableProperty]
        private int _pages;
        private Person _CurrentPerson;
        private AccountingUnit _Unit;
        [ObservableProperty]
        private ObservableCollection<UnitRespPerson> _UnitRespPersons;
        private bool _IsActiveOnly = true;
        public ObservableCollection<Person> People { get; set; }
        public UnitRespPersonVM(INavigationService navigationService, MainModel MainModel, DisplayService displayService, DataTransferService dataTransferService) : base(navigationService, MainModel, displayService, dataTransferService)
        {
            _Unit = _dataTransferService.TransferAccountingUnit;
            _dataTransferService.TransferAccountingUnit = null;
            if(_Unit is not null)
            {
                IsUnitMode = true;
            }
            else
            {
                IsUnitMode = false;
                People = _mainModel.GetAllPersons();
                CurrentPerson = People.FirstOrDefault();
            }
        }
        public bool IsUnitMode { get; private set; }
        public Person CurrentPerson
        {
            get => _CurrentPerson;
            set {
                _CurrentPerson = value;
                OnPropertyChanged();
                UpdateCollection();
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
        private void UnitRespPersonUpdate(UnitRespPerson UnitRespPerson)
        {
            _dataTransferService.TransferUnitRespPerson = UnitRespPerson;
            _navigationService.NavigateToAsync(nameof(UnitRespPersonEditPage));
        }
        [RelayCommand]
        private void AddUnitRespPerson()
        {
            _dataTransferService.TransferUnitRespPerson = new UnitRespPerson() { Unit = _Unit, UnitId = _Unit.Id };
            _navigationService.NavigateToAsync(nameof(UnitRespPersonEditPage));
        }
        [RelayCommand]
        private void Appearing()
        {
            try
            {
                _Unit.UnitRespPeople = _mainModel.GetAllUnitRespPersonByUnit(_Unit.Id);
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
                ICollection<UnitRespPerson> UnitRespPersonsCol;
                if (IsUnitMode)
                    UnitRespPersonsCol = _Unit.UnitRespPeople;
                else
                    UnitRespPersonsCol = _mainModel.GetAllUnitRespPersonByPerson(CurrentPerson.Id);
                if (IsActiveOnly) UnitRespPersonsCol = UnitRespPersonsCol.Where(y => y.DateEnd == null).ToList();
                UnitRespPersonsCol = UnitRespPersonsCol.OrderBy(x => x.Unit.FullNameUnit.UnitType.Name).ToList();
                Pages = UnitRespPersonsCol.Count() % _pageSize == 0 ? UnitRespPersonsCol.Count() / _pageSize - 1 : UnitRespPersonsCol.Count() / _pageSize;
                UnitRespPersons = UnitRespPersonsCol
                    .Skip(CurrentPage * _pageSize)
                    .Take(_pageSize)
                    .ToObservableCollection();
                OnPropertyChanged(nameof(UnitRespPersons));
            }
            catch (Exception ex)
            {
                Errors = ValidateAndErrorsTools.GetInfo(ex);
                OnPropertyChanged(nameof(Errors));
            }
        }
    }
}
