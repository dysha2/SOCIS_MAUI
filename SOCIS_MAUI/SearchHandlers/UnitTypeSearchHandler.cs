using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIS_MAUI.SearchHandlers
{
    public class UnitTypeSearchHandler : SearchHandler
    {
        public IList<UnitType> UnitTypes { get; set; }
        public Action<UnitType> ItemSelectedAction { get; set; }
        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);
            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = UnitTypes
                    .Where(x => x.Name.Contains(newValue, StringComparison.OrdinalIgnoreCase))
                    .Take(5)
                    .ToList();
            }
        }
        protected override void OnItemSelected(object item)
        {
            base.OnItemSelected(item);
            ItemSelectedAction(item as UnitType);
        }
    }
}
