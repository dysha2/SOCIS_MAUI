using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

namespace SOCIS_MAUI_MODEL.Model
{
    public partial class UnitPlace:ObservableObject
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        [ObservableProperty]
        private int _PlaceId;
        [ObservableProperty]
        private string? _Comment;
        [ObservableProperty]
        private DateTime _DateStart;
        [ObservableProperty]
        private DateTime? _DateEnd;
        public int? WorkOnRequestId { get; set; }
        [ObservableProperty]
        private Place _Place = null!;
        [ObservableProperty]
        private AccountingUnit _Unit = null!;
        public virtual WorkOnRequest? WorkOnRequest { get; set; }
    }
}
