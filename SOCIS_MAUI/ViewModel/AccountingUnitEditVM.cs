using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelpfulProjectCSharp.ASP;
using IntelliJ.Lang.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIS_MAUI.ViewModel
{
    public partial class AccountingUnitEditVM : BaseVM
    {
        private AccountingUnit _AccountingUnit;
        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        [StringLength(17,ErrorMessage ="MAC адрес должен содержать 17 симлволов")]
        [RegularExpression("([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})",ErrorMessage ="Значение не является MAC адресом")]
        private string? _Mac;
        [ObservableProperty]
        [MaxLength(50,ErrorMessage ="Поле серийного номера имеет ограничение 50 символов")]
        private string? _SerNum;
        [ObservableProperty]
        [MaxLength(15, ErrorMessage = "Поле сетевого имени имеет ограничение 15 символов")]
        private string? _NetName;
        [ObservableProperty]
        [DataType(DataType.DateTime,ErrorMessage ="В поле дата производства указана не дата")]
        private string? _ManufDate;
        [ObservableProperty]
        [MaxLength(15, ErrorMessage = "Поле инвантарного номера имеет ограничение 15 символов")]
        private string? _InvNum;
        [ObservableProperty]
        [MaxLength(512, ErrorMessage = "Поле сетевого имени имеет ограничение 512 символов")]
        private string? _Comment;
        [ObservableProperty]
        private FullNameUnit _FullNameUnit = null!;
        public AccountingUnitEditVM(INavigationService navigationService, MainModel MainModel, DisplayService displayService, DataTransferService dataTransferService) : base(navigationService, MainModel, displayService, dataTransferService)
        {
            _AccountingUnit = _dataTransferService.TransferAccountingUnit;
            _dataTransferService.TransferAccountingUnit = null;
            _id = _AccountingUnit.Id;
            _Mac = _AccountingUnit.Mac;
            _SerNum = _AccountingUnit.SerNum;
            _NetName = _AccountingUnit.NetName;
            _ManufDate = _AccountingUnit.ManufDate is null?null:_AccountingUnit.ManufDate.ToString();
            _InvNum = _AccountingUnit.InvNum;
            _Comment = _AccountingUnit.Comment;
            _FullNameUnit = _AccountingUnit.FullNameUnit;
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
                    _mainModel.AccountingUnitDelete(_AccountingUnit);
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
                    _AccountingUnit.Mac=_Mac;
                    _AccountingUnit.SerNum = _SerNum;
                    _AccountingUnit.NetName = _NetName;
                    _AccountingUnit.ManufDate = String.IsNullOrEmpty(_ManufDate) ? null : DateTime.Parse(_ManufDate);
                    _AccountingUnit.InvNum = _InvNum;
                    _AccountingUnit.Comment = _Comment;
                    if (_AccountingUnit.Id == 0)
                    {
                        _mainModel.AccountingUnitAdd(_AccountingUnit);
                        await _displayService.ShowAlert("Succes", "Значение добавлено");
                        _navigationService.NavigateBack();
                    }
                    else
                    {
                        _mainModel.AccountingUnitUpdate(_AccountingUnit.Id, _AccountingUnit);
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
