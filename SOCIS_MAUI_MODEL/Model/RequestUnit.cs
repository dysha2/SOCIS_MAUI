using System;
using System.Collections.Generic;

namespace SOCIS_MAUI_MODEL.Model
{
    public partial class RequestUnit
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public int WorkOnRequestId { get; set; }

        public virtual AccountingUnit Unit { get; set; } = null!;
        public virtual WorkOnRequest WorkOnRequest { get; set; } = null!;
    }
}
