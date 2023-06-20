using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpfulProjectCSharp.ASP;


namespace SOCIS_MAUI.ViewModel
{
    public partial class AppLaunchVM : BaseVM
    {
        private string _login="GarievDenis";
        private string _password="123";
        public AppLaunchVM(INavigationService navigationService, MainModel mainModel,DisplayService displayService,DataTransferService dataTransferService) : base(navigationService, mainModel,displayService,dataTransferService)
        {
            try
            {
                if (_mainModel.IsAuth())
                {
                    GoToMainPage();
                }
            } catch (Exception ex)
            {
                Errors = ValidateAndErrorsTools.GetInfo(ex);
            }
        }
        
        [Required(ErrorMessage ="Введите логин")]
        public string Login { 
            get => _login;
            set { 
                SetProperty(ref _login, value, false); 
            }
        }
        
        [Required(ErrorMessage ="Введите пароль")]
        [CustomValidation(typeof(AppLaunchVM),nameof(ValidatePassword))]
        public string Password { 
            get => _password;
            set {
                SetProperty(ref _password, value, false);
            } 
        }
        [RelayCommand]
        public void Authorize()
        {
            ValidateAllProperties();
            if (HasErrors) OnPropertyChanged(nameof(Errors));
            else
            {
                GoToMainPage();
            }
        }
        public static ValidationResult ValidatePassword(string name, ValidationContext context)
        {
            try
            {
                AppLaunchVM instance = (AppLaunchVM)context.ObjectInstance;
                if (instance._mainModel.Authorize(instance.Login, instance.Password))
                {
                    return ValidationResult.Success;
                }
                return new("Проверьте правильность логина и пароля");
            } catch(Exception ex)
            {
                return new(ValidateAndErrorsTools.GetInfo(ex));
            }

        }
        private void GoToMainPage()
        {
            _navigationService.NavigateToAsync(nameof(PlacePage));
        }
    }
}
