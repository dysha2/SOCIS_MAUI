using CommunityToolkit.Mvvm.ComponentModel;
using SOCIS_MAUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIS_MAUI.ViewModel
{
    public class BaseVM:ObservableValidator
    {
        protected readonly INavigationService _navigationService;
        protected readonly MainModel _mainModel;
        protected readonly DisplayService _displayService;
        protected readonly DataTransferService _dataTransferService;
        private string _errors;
        public string Errors
        {
            get
            {
                StringBuilder s = new StringBuilder();
                foreach (var res in GetErrors())
                {
                    s.Append($"{res.ErrorMessage}\n");
                }
                s.Append(_errors);
                _errors = String.Empty;
                return s.ToString();
            }
            set { _errors = value; }
        }

        public BaseVM(INavigationService navigationService,MainModel MainModel, DisplayService displayService,DataTransferService dataTransferService)
        {
            _navigationService = navigationService;
            _mainModel = MainModel;
            _displayService = displayService;
            _dataTransferService=dataTransferService;
        }
    }
}
