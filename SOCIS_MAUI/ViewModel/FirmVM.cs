using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIS_MAUI.ViewModel
{
    public partial class FirmVM : BaseVM
    {
        public static ObservableCollection<Firm> SearchFirms { get; private set; }
        public static Action<Firm> FirmAction { get; private set; }
        public ObservableCollection<Firm> Firms { get; private set; }
        public FirmVM(INavigationService navigationService, MainModel MainModel, DisplayService displayService, DataTransferService dataTransferService) : base(navigationService, MainModel, displayService, dataTransferService)
        {
            FirmAction = FirmUpdate;
            Firms = _mainModel.GetAllFirms();
            SearchFirms = Firms;
        }
        [RelayCommand]
        private void FirmUpdate(Firm Firm)
        {
            _dataTransferService.TransferFirm = Firm;
            _navigationService.NavigateToAsync(nameof(FirmEditPage));
        }
        [RelayCommand]
        private void AddFirm()
        {
            _dataTransferService.TransferFirm = new Firm();
            _navigationService.NavigateToAsync(nameof(FirmEditPage));
        }
    }
}
