using System;
using System.Collections.Generic;

namespace SOCIS_MAUI_MODEL.Model
{
    public partial class ShortTermMove
    {
        public int ShortTermMoveId { get; set; }
        public int PlaceId { get; set; }
        public int? WorkOnRequestId { get; set; }
        public DateTime? DateTimeEndPlan { get; set; }
        public DateTime? DateTimeEndFact { get; set; }
        public int UnitId { get; set; }

        public virtual Place Place { get; set; } = null!;
        public virtual AccountingUnit Unit { get; set; } = null!;
        public virtual WorkOnRequest? WorkOnRequest { get; set; }
    }
}
