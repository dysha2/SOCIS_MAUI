using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelpfulProjectCSharp.ASP;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIS_MAUI.ViewModel
{
    public partial class ShortTermMoveEditVM : BaseVM
    {
        private ShortTermMove _ShortTermMove;
        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        [Required(ErrorMessage = "Поле дата начала обязательно")]
        [DataType(DataType.DateTime, ErrorMessage = "Поле дата начала не является датой")]
        private string _DateTimeStart;
        [ObservableProperty]
        [DataType(DataType.DateTime, ErrorMessage = "Поле дата окончания по плану не является датой")]
        private string? _DateTimeEndPlan;
        [ObservableProperty]
        [DataType(DataType.DateTime, ErrorMessage = "Поле дата окончания по факту не является датой")]
        private string? _DateTimeEndFact;
        [ObservableProperty]
        [Required(ErrorMessage = "Поле кабинета обязательно")]
        private Place _Place = null!;
        [ObservableProperty]
        private AccountingUnit _Unit = null!;
        [ObservableProperty]
        private List<Place> _Places;
        public ShortTermMoveEditVM(INavigationService navigationService, MainModel MainModel, DisplayService displayService, DataTransferService dataTransferService) : base(navigationService, MainModel, displayService, dataTransferService)
        {
            _ShortTermMove = _dataTransferService.TransferShortTermMove;
            _dataTransferService.TransferShortTermMove = null;
            _id = _ShortTermMove.ShortTermMoveId;
            _DateTimeStart = _ShortTermMove.ShortTermMoveId == 0 ? DateTime.Now.ToString(new CultureInfo("de-DE")) : _ShortTermMove.DateTimeStart.ToString(new CultureInfo("de-DE"));
            _DateTimeEndPlan = _ShortTermMove.DateTimeEndPlan.ToString();
            _DateTimeEndFact = _ShortTermMove.DateTimeEndFact.ToString();
            _Unit = _ShortTermMove.Unit;
            _Places = _mainModel.GetAllPlaces().OrderBy(x => x.Name).ToList();
            _Place = _Places.FirstOrDefault(x => x.Id == _ShortTermMove.PlaceId);
        }
        public bool IsAdminMode
        {
            get
            {
                try
                {
                    return _mainModel.IsAdminMode();
                }
                catch (Exception ex)
                {
                    Errors = ValidateAndErrorsTools.GetInfo(ex);
                    OnPropertyChanged(nameof(Errors));
                    return false;
                }
            }
        }
        [RelayCommand]
        private void GoToAccountingUnitEditPage()
        {
            _dataTransferService.TransferAccountingUnit = _Unit;
            _navigationService.NavigateToAsync(nameof(AccountingUnitEditPage));
        }
        [RelayCommand]
        private async void Delete()
        {
            try
            {
                bool res = await _displayService.ShowAlert("Предупреждение", "Вы уверены?", "Да", "Нет");
                if (res)
                {
                    _mainModel.ShortTermMoveDelete(_ShortTermMove);
                    await _displayService.ShowAlert("Succes", "Значение удалено");
                    _navigationService.NavigateBack();
                }
            }
            catch (Exception ex)
            {
                Errors = ValidateAndErrorsTools.GetInfo(ex);
                OnPropertyChanged(nameof(Errors));
            }
        }
        [RelayCommand]
        private async void Save()
        {
            try
            {
                ValidateAllProperties();
                OnPropertyChanged(nameof(Errors));
                if (HasErrors == false)
                {
                    _ShortTermMove.PlaceId = Place.Id;
                    _ShortTermMove.DateTimeStart = DateTime.Parse(DateTimeStart);
                    _ShortTermMove.DateTimeEndPlan = String.IsNullOrEmpty(DateTimeEndPlan) ? null : DateTime.Parse(DateTimeEndPlan);
                    _ShortTermMove.DateTimeEndFact = String.IsNullOrEmpty(DateTimeEndFact) ? null : DateTime.Parse(DateTimeEndFact);
                    if (_ShortTermMove.ShortTermMoveId == 0)
                    {
                        _mainModel.ShortTermMoveAdd(_ShortTermMove);
                        await _displayService.ShowAlert("Succes", "Значение добавлено");
                        _navigationService.NavigateBack();
                    }
                    else
                    {
                        _mainModel.ShortTermMoveUpdate(_ShortTermMove.ShortTermMoveId, _ShortTermMove);
                        await _displayService.ShowAlert("Succes", "Значение обновлено");
                        _navigationService.NavigateBack();
                    }
                }
            }
            catch (Exception ex)
            {
                Errors = ValidateAndErrorsTools.GetInfo(ex);
                OnPropertyChanged(nameof(Errors));
            }
        }
    }
}
