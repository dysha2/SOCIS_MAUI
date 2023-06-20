using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIS_MAUI.Services
{
    public class NavigationService : INavigationService
    {
        public void NavigateBack()
        {
            Shell.Current.SendBackButtonPressed();
        }

        public Task NavigateToAsync(string route, IDictionary<string, object> parameters=null)
        {
            if(parameters is not null)
            {
                return Shell.Current.GoToAsync(route, parameters);
            }
            else
            {
                return Shell.Current.GoToAsync(route);
            }
        }
    }
}
