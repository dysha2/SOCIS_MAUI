using System;
using System.Collections.Generic;

namespace SOCIS_MAUI_MODEL.Model
{
    public partial class AccountingUnit
    {
        public AccountingUnit()
        {
            RequestUnits = new HashSet<RequestUnit>();
            ShortTermMoves = new HashSet<ShortTermMove>();
            UnitPlaces = new HashSet<UnitPlace>();
            UnitRespPeople = new HashSet<UnitRespPerson>();
        }

        public int Id { get; set; }
        public string? Mac { get; set; }
        public string? SerNum { get; set; }
        public string? NetName { get; set; }
        public DateTime? ManufDate { get; set; }
        public string? InvNum { get; set; }
        public int FullNameUnitId { get; set; }
        public string? Comment { get; set; }

        public virtual FullNameUnit FullNameUnit { get; set; } = null!;
        public virtual ICollection<RequestUnit> RequestUnits { get; set; }
        public virtual ICollection<ShortTermMove> ShortTermMoves { get; set; }
        public virtual ICollection<UnitPlace> UnitPlaces { get; set; }
        public virtual ICollection<UnitRespPerson> UnitRespPeople { get; set; }
    }
}
