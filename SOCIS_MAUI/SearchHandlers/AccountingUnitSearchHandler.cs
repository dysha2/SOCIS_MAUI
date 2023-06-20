using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIS_MAUI.SearchHandlers
{
    class AccountingUnitSearchHandler:SearchHandler
    {
        public IList<AccountingUnit> AccountingUnits { get; set; }
        public Action<AccountingUnit> ItemSelectedAction { get; set; }
        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);
            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                var invNumSearch = AccountingUnits
                    .Where(x => x.InvNum is not null)
                    .Where(x => x.InvNum.Contains(newValue, StringComparison.OrdinalIgnoreCase))
                    .Take(5);
                if (invNumSearch.Count() > 0) ItemsSource = invNumSearch.ToList();
                else
                {
                    var serNumSearch = AccountingUnits
                    .Where(x => x.SerNum is not null)
                    .Where(x => x.SerNum.Contains(newValue, StringComparison.OrdinalIgnoreCase))
                    .Take(5);
                    if (serNumSearch.Count() > 0) ItemsSource = serNumSearch.ToList();
                    else
                    {
                        ItemsSource = AccountingUnits
                            .Where(x => x.Mac is not null)
                            .Where(x => x.Mac.Contains(newValue, StringComparison.OrdinalIgnoreCase))
                            .Take(5)
                            .ToList();
                    }
                }
            }
        }
        protected override void OnItemSelected(object item)
        {
            base.OnItemSelected(item);
            ItemSelectedAction(item as AccountingUnit);
        }
    }
}
