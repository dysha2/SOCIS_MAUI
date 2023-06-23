using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

namespace SOCIS_MAUI_MODEL.Model
{
    public partial class ShortTermMove:ObservableObject
    {
        public int ShortTermMoveId { get; set; }
        [ObservableProperty]
        private int _PlaceId;
        [ObservableProperty]
        private int? _WorkOnRequestId;
        [ObservableProperty]
        private DateTime? _DateTimeEndPlan;
        [ObservableProperty]
        private DateTime? _DateTimeEndFact;
        [ObservableProperty]
        private DateTime _DateTimeStart;
        public int UnitId { get; set; }
        [ObservableProperty]
        private Place _Place = null!;
        public virtual AccountingUnit Unit { get; set; } = null!;
        public virtual WorkOnRequest? WorkOnRequest { get; set; }
    }
}
