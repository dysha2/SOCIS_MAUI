using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIS_MAUI.ViewModel
{
    public partial class UnitTypeVM : BaseVM
    {
        public static ObservableCollection<UnitType> SearchUnitTypes { get; private set; }
        public static Action<UnitType> UnitTypeAction { get; private set; }
        public ObservableCollection<UnitType> UnitTypes { get; private set; }
        public UnitTypeVM(INavigationService navigationService, MainModel MainModel, DisplayService displayService, DataTransferService dataTransferService) : base(navigationService, MainModel, displayService, dataTransferService)
        {
            UnitTypeAction = UnitTypeUpdate;
            UnitTypes = _mainModel.GetAllUnitTypes();
            SearchUnitTypes = UnitTypes;
        }
        [RelayCommand]
        private void UnitTypeUpdate(UnitType UnitType)
        {
            _dataTransferService.TransferUnitType = UnitType;
            _navigationService.NavigateToAsync(nameof(UnitTypeEditPage));
        }
        [RelayCommand]
        private void AddUnitType()
        {
            _dataTransferService.TransferUnitType = new UnitType();
            _navigationService.NavigateToAsync(nameof(UnitTypeEditPage));
        }
    }
}
