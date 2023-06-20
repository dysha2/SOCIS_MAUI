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
    public partial class FirmEditVM : BaseVM
    {
        private Firm _Firm;
        [ObservableProperty]
        [Required(ErrorMessage = "Поле название обязательно для заполнения")]
        [MinLength(1, ErrorMessage = "Название не может быть пустым")]
        [MaxLength(30, ErrorMessage = "Название не может быть больше 30 символов")]
        private string _name;
        [ObservableProperty]
        private int _id;
        public FirmEditVM(INavigationService navigationService, MainModel MainModel, DisplayService displayService, DataTransferService dataTransferService) : base(navigationService, MainModel, displayService, dataTransferService)
        {
            _Firm = _dataTransferService.TransferFirm;
            _dataTransferService.TransferFirm = null;
            _name = _Firm.Name;
            _id = _Firm.Id;
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
        private async void Delete()
        {
            try
            {
                bool res = await _displayService.ShowAlert("Предупреждение", "Вы уверены?", "Да", "Нет");
                if (res)
                {
                    _mainModel.FirmDelete(_Firm);
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
                    _Firm.Name = Name;
                    if (_Firm.Id == 0)
                    {
                        _mainModel.FirmAdd(_Firm);
                        await _displayService.ShowAlert("Succes", "Значение добавлено");
                        _navigationService.NavigateBack();
                    }
                    else
                    {
                        _mainModel.FirmUpdate(_Firm.Id, _Firm);
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
