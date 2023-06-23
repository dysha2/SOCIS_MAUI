using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelpfulProjectCSharp.ASP;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIS_MAUI.ViewModel
{
    public partial class UnitPlaceEditVM : BaseVM
    {
        private UnitPlace _UnitPlace;
        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        [MaxLength(512,ErrorMessage ="Поле комментарий имеет ограничение 512 символов")]
        private string? _Comment;
        [ObservableProperty]
        [Required(ErrorMessage ="Поле дата начала обязательно")]
        [DataType(DataType.DateTime,ErrorMessage ="Поле дата начала не является датой")]
        private string _DateStart;
        [ObservableProperty]
        [DataType(DataType.DateTime, ErrorMessage = "Поле дата окончания не является датой")]
        private string? _DateEnd;
        [ObservableProperty]
        [Required(ErrorMessage ="Поле кабинета обязательно")]
        private Place _Place = null!;
        [ObservableProperty]
        private AccountingUnit _Unit = null!;
        [ObservableProperty]
        private List<Place> _Places;
        public UnitPlaceEditVM(INavigationService navigationService, MainModel MainModel, DisplayService displayService, DataTransferService dataTransferService) : base(navigationService, MainModel, displayService, dataTransferService)
        {
            _UnitPlace = _dataTransferService.TransferUnitPlace;
            _dataTransferService.TransferUnitPlace = null;
            _id = _UnitPlace.Id;
            _Comment = _UnitPlace.Comment;
            _DateStart = _UnitPlace.Id==0?DateTime.Today.ToShortDateString(): _UnitPlace.DateStart.ToShortDateString();
            _DateEnd = _UnitPlace.DateEnd.ToString();
            _Unit = _UnitPlace.Unit;
            _Places = _mainModel.GetAllPlaces().OrderBy(x=>x.Name).ToList();
            _Place = _Places.FirstOrDefault(x=>x.Id==_UnitPlace.PlaceId);
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
                    _mainModel.UnitPlaceDelete(_UnitPlace);
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
                    _UnitPlace.PlaceId = Place.Id;
                    _UnitPlace.Comment = Comment;
                    _UnitPlace.DateStart = DateTime.Parse(DateStart);
                    _UnitPlace.DateEnd= String.IsNullOrEmpty(DateEnd) ? null : DateTime.Parse(DateEnd);
                    if (_UnitPlace.Id == 0)
                    {
                        _mainModel.UnitPlaceAdd(_UnitPlace);
                        await _displayService.ShowAlert("Succes", "Значение добавлено");
                        _navigationService.NavigateBack();
                    }
                    else
                    {
                        _mainModel.UnitPlaceUpdate(_UnitPlace.Id, _UnitPlace);
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
