using System;
using System.Collections.Generic;

namespace SOCIS_MAUI_MODEL.Model
{
    public partial class UnitPlace
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public int PlaceId { get; set; }
        public string? Comment { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? WorkOnRequestId { get; set; }

        public virtual Place Place { get; set; } = null!;
        public virtual AccountingUnit Unit { get; set; } = null!;
        public virtual WorkOnRequest? WorkOnRequest { get; set; }
    }
}
