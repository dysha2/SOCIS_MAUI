using System;
using System.Collections.Generic;

namespace SOCIS_MAUI_MODEL.Model
{
    public partial class WorkOnRequest
    {
        public WorkOnRequest()
        {
            RequestUnits = new HashSet<RequestUnit>();
            ShortTermMoves = new HashSet<ShortTermMove>();
            UnitPlaces = new HashSet<UnitPlace>();
        }

        public int Id { get; set; }
        public int RequestId { get; set; }
        public int ServiceId { get; set; }
        public DateTime DateTime { get; set; }
        public string? Comment { get; set; }
        public int ImplementerId { get; set; }

        public virtual Person Implementer { get; set; } = null!;
        public virtual Request Request { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
        public virtual ICollection<RequestUnit> RequestUnits { get; set; }
        public virtual ICollection<ShortTermMove> ShortTermMoves { get; set; }
        public virtual ICollection<UnitPlace> UnitPlaces { get; set; }
    }
}
