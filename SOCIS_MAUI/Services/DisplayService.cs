using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIS_MAUI.Services
{
    public class DisplayService
    {
        public Task ShowAlert(string title,string mes,string ok="OK")
        {
            return Shell.Current.DisplayAlert(title, mes, ok);
        }
        public Task<bool> ShowAlert(string title, string mes, string ok, string cancel)
        {
            return Shell.Current.DisplayAlert(title, mes, ok, cancel);
        }
    }
}
