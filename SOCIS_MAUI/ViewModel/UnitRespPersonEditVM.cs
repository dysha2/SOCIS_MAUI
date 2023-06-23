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
    public partial class UnitRespPersonEditVM : BaseVM
    {
        private UnitRespPerson _UnitRespPerson;
        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        [Required(ErrorMessage = "Поле дата начала обязательно")]
        [DataType(DataType.DateTime, ErrorMessage = "Поле дата начала не является датой")]
        private string _DateStart;
        [ObservableProperty]
        [DataType(DataType.DateTime, ErrorMessage = "Поле дата окончания не является датой")]
        private string? _DateEnd;
        [ObservableProperty]
        [Required(ErrorMessage = "Поле ответственного лица обязательно")]
        private Person _Person = null!;
        [ObservableProperty]
        private AccountingUnit _Unit = null!;
        [ObservableProperty]
        private List<Person> _Persons;
        public UnitRespPersonEditVM(INavigationService navigationService, MainModel MainModel, DisplayService displayService, DataTransferService dataTransferService) : base(navigationService, MainModel, displayService, dataTransferService)
        {
            _UnitRespPerson = _dataTransferService.TransferUnitRespPerson;
            _dataTransferService.TransferUnitRespPerson = null;
            _id = _UnitRespPerson.Id;
            _DateStart = _UnitRespPerson.Id == 0 ? DateTime.Today.ToShortDateString() : _UnitRespPerson.DateStart.ToShortDateString();
            _DateEnd = _UnitRespPerson.DateEnd.ToString();
            _Unit = _UnitRespPerson.Unit;
            _Persons = _mainModel.GetAllPersons().ToList();
            _Person = _Persons.FirstOrDefault(x => x.Id == _UnitRespPerson.PersonId);
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
                    _mainModel.UnitRespPersonDelete(_UnitRespPerson);
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
                    _UnitRespPerson.PersonId = Person.Id;
                    _UnitRespPerson.DateStart = DateTime.Parse(DateStart);
                    _UnitRespPerson.DateEnd = String.IsNullOrEmpty(DateEnd) ? null : DateTime.Parse(DateEnd);
                    if (_UnitRespPerson.Id == 0)
                    {
                        _mainModel.UnitRespPersonAdd(_UnitRespPerson);
                        await _displayService.ShowAlert("Succes", "Значение добавлено");
                        _navigationService.NavigateBack();
                    }
                    else
                    {
                        _mainModel.UnitRespPersonUpdate(_UnitRespPerson.Id, _UnitRespPerson);
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
