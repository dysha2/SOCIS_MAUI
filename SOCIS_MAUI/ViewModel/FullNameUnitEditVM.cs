using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelpfulProjectCSharp.ASP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIS_MAUI.ViewModel
{
    public partial class FullNameUnitEditVM : BaseVM
    {
        private FullNameUnit _FullNameUnit;
        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        [Required(ErrorMessage = "Поле модели обязательно для заполнения")]
        [MinLength(1, ErrorMessage = "Поле модели не может быть пустым")]
        [MaxLength(50, ErrorMessage = "Поле модели не может быть больше 50 символов")]
        private string _model;
        [ObservableProperty]
        private Firm? _firm;
        [ObservableProperty]
        [Required(ErrorMessage = "Поле типа устройства обязательно для заполнения")]
        private UnitType _unitType;
        [ObservableProperty]
        [MaxLength(30, ErrorMessage = "Поле номера модели не может быть больше 30 символов")]
        private string _modelNo;
        public FullNameUnitEditVM(INavigationService navigationService, MainModel MainModel, DisplayService displayService, DataTransferService dataTransferService) : base(navigationService, MainModel, displayService, dataTransferService)
        {
            _FullNameUnit = _dataTransferService.TransferFullNameUnit;
            _dataTransferService.TransferFullNameUnit = null;
            _id = _FullNameUnit.Id;
            _model = _FullNameUnit.Model;
            _modelNo = _FullNameUnit.ModelNo;
            Firms = _mainModel.GetAllFirms();
            UnitTypes = _mainModel.GetAllUnitTypes();
            _unitType = UnitTypes.FirstOrDefault(x=>x.Id==_FullNameUnit.UnitTypeId);
            _firm = Firms.FirstOrDefault(x => x.Id == _FullNameUnit.FirmId);
        }
        public ObservableCollection<Firm> Firms { get; set; }
        public ObservableCollection<UnitType> UnitTypes { get; set; }
        public int UnitTypeIndex { get; private set; }
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
                    _mainModel.FullNameUnitDelete(_FullNameUnit);
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
        private void AddAccountingUnit()
        {
            var AcUn = new AccountingUnit
            {
                FullNameUnit = _FullNameUnit,
                FullNameUnitId = _FullNameUnit.Id
            };
            _dataTransferService.TransferAccountingUnit = AcUn;
            _navigationService.NavigateToAsync(nameof(AccountingUnitEditPage));
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
                    _FullNameUnit.Model = Model;
                    _FullNameUnit.ModelNo = ModelNo;
                    _FullNameUnit.Firm = Firm;
                    _FullNameUnit.UnitType = UnitType;
                    _FullNameUnit.FirmId = Firm is null?null:Firm.Id;
                    _FullNameUnit.UnitTypeId = UnitType.Id;
                    if (_FullNameUnit.Id == 0)
                    {
                        _mainModel.FullNameUnitAdd(_FullNameUnit);
                        await _displayService.ShowAlert("Succes", "Значение добавлено");
                        _navigationService.NavigateBack();
                    }
                    else
                    {
                        _mainModel.FullNameUnitUpdate(_FullNameUnit.Id, _FullNameUnit);
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
