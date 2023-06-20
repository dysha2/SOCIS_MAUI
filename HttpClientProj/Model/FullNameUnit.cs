using System;
using System.Collections.Generic;

namespace SOCIS_MAUI_MODEL.Model
{
    public partial class FullNameUnit
    {
        public FullNameUnit()
        {
            AccountingUnits = new HashSet<AccountingUnit>();
        }

        public int Id { get; set; }
        public int? FirmId { get; set; }
        public string Model { get; set; } = null!;
        public int UnitTypeId { get; set; }
        public string? ModelNo { get; set; }

        public virtual Firm? Firm { get; set; }
        public virtual UnitType UnitType { get; set; } = null!;
        public virtual ICollection<AccountingUnit> AccountingUnits { get; set; }
    }
}
