using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

namespace SOCIS_MAUI_MODEL.Model
{
    public partial class FullNameUnit:ObservableObject
    {
        public FullNameUnit()
        {
            AccountingUnits = new HashSet<AccountingUnit>();
        }

        public int Id { get; set; }
        [ObservableProperty]
        private int? _FirmId;
        [ObservableProperty]
        private string _Model = null!;
        [ObservableProperty]
        private int _UnitTypeId;
        [ObservableProperty]
        private string? _ModelNo;
        [ObservableProperty]
        private Firm? _Firm;
        [ObservableProperty]
        private UnitType _UnitType  = null!;
        public virtual ICollection<AccountingUnit> AccountingUnits { get; set; }
    }
}
