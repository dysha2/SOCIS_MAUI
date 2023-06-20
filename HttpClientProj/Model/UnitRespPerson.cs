using System;
using System.Collections.Generic;

namespace SOCIS_MAUI_MODEL.Model
{
    public partial class UnitRespPerson
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public int PersonId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        public virtual Person Person { get; set; } = null!;
        public virtual AccountingUnit Unit { get; set; } = null!;
    }
}
