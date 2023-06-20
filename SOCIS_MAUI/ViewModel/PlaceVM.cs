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
    public partial class PlaceVM : BaseVM
    {
        public static ObservableCollection<Place> SearchPlaces { get; private set; }
        public static Action<Place> PlaceAction { get; private set; }
        public ObservableCollection<Place> Places { get; private set; }
        public PlaceVM(INavigationService navigationService, MainModel MainModel,DisplayService displayService, DataTransferService dataTransferService) : base(navigationService, MainModel, displayService, dataTransferService)
        {
            PlaceAction = PlaceUpdate;
            Places = _mainModel.GetAllPlaces();
            SearchPlaces = Places;
        }
        [RelayCommand]
        private void PlaceUpdate(Place place)
        {
            _dataTransferService.TransferPlace = place;
            _navigationService.NavigateToAsync(nameof(PlaceEditPage));
        }
        [RelayCommand]
        private void AddPlace()
        {
            _dataTransferService.TransferPlace = new Place();
            _navigationService.NavigateToAsync(nameof(PlaceEditPage));
        }
    }
}
