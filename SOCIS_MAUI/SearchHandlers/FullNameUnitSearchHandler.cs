using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIS_MAUI.SearchHandlers
{
    public class FullNameUnitSearchHandler : SearchHandler
    {
        public IList<FullNameUnit> FullNameUnits { get; set; }
        public Action<FullNameUnit> ItemSelectedAction { get; set; }
        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);
            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                var modelSearch = FullNameUnits
                    .Where(x => x.Model.Contains(newValue, StringComparison.OrdinalIgnoreCase))
                    .Take(5);
                if (modelSearch.Count()>0) ItemsSource= modelSearch.ToList();
                else
                {
                    ItemsSource = FullNameUnits
                    .Where(x=>x.ModelNo is not null)
                    .Where(x => x.ModelNo.Contains(newValue, StringComparison.OrdinalIgnoreCase))
                    .Take(5)
                    .ToList();
                }
            }
        }
        protected override void OnItemSelected(object item)
        {
            base.OnItemSelected(item);
            ItemSelectedAction(item as FullNameUnit);
        }
    }
}
