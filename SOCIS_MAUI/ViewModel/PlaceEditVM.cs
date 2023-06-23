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
    public partial class PlaceEditVM : BaseVM
    {
        private Place _place;
        [ObservableProperty]
        [Required(ErrorMessage = "Поле название обязательно для заполнения")]
        [MinLength(1, ErrorMessage = "Название не может быть пустым")]
        [MaxLength(5, ErrorMessage = "Название не может быть больше 5 символов")]
        private string _name;
        [ObservableProperty]
        [MaxLength(50,ErrorMessage ="Описание иммет ограничение 50 символов")]
        private string _description;
        [ObservableProperty]
        private int _id;
        public PlaceEditVM(INavigationService navigationService, MainModel MainModel, DisplayService displayService, DataTransferService dataTransferService) : base(navigationService, MainModel, displayService, dataTransferService)
        {
            _place = _dataTransferService.TransferPlace;
            _dataTransferService.TransferPlace = null;
            _name = _place.Name;
            _description= _place.Description;
            _id = _place.Id;
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
        private void UnitPlacesShow()
        {
            _dataTransferService.TransferPlace = _place;
            _navigationService.NavigateToAsync(nameof(UnitPlacePage));
        }
        [RelayCommand]
        private void ShortTermMovesShow()
        {
            _dataTransferService.TransferPlace = _place;
            _navigationService.NavigateToAsync(nameof(ShortTermMovePage));
        }
        [RelayCommand]
        private async void Delete()
        {
            try
            {
                bool res = await _displayService.ShowAlert("Предупреждение", "Вы уверены?", "Да", "Нет");
                if (res)
                {
                    _mainModel.PlaceDelete(_place);
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
                    _place.Name = Name;
                    _place.Description = Description;
                    if (_place.Id == 0)
                    {
                        _mainModel.PlaceAdd(_place);
                        await _displayService.ShowAlert("Succes", "Значение добавлено");
                        _navigationService.NavigateBack();
                    }
                    else
                    {
                        _mainModel.PlaceUpdate(_place.Id,_place);
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
