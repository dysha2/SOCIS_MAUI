using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

namespace SOCIS_MAUI_MODEL.Model
{
    public partial class UnitRespPerson:ObservableObject
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public int PersonId { get; set; }
        [ObservableProperty]
        private DateTime _DateStart;
        [ObservableProperty]
        private DateTime? _DateEnd;
        [ObservableProperty]
        private Person _Person = null!;
        [ObservableProperty]
        private AccountingUnit _Unit = null!;
    }
}
