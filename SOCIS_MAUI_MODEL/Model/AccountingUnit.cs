using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOCIS_MAUI_MODEL.Model
{
    public partial class AccountingUnit:ObservableObject
    {
        public AccountingUnit()
        {
            RequestUnits = new HashSet<RequestUnit>();
            ShortTermMoves = new HashSet<ShortTermMove>();
            UnitPlaces = new HashSet<UnitPlace>();
            UnitRespPeople = new HashSet<UnitRespPerson>();
        }

        public int Id { get; set; }
        [ObservableProperty]
        private string? _Mac;
        [ObservableProperty]
        private string? _SerNum;
        [ObservableProperty]
        private string? _NetName;
        [ObservableProperty]
        private DateTime? _ManufDate;
        [ObservableProperty]
        private string? _InvNum;
        [ObservableProperty]
        private int _FullNameUnitId;
        [ObservableProperty]
        private string? _Comment;
        [NotMapped]
        public Place? CurrentPlace { get; set; }
        [ObservableProperty]
        private FullNameUnit _FullNameUnit = null!;
        public virtual ICollection<RequestUnit> RequestUnits { get; set; }
        public virtual ICollection<ShortTermMove> ShortTermMoves { get; set; }
        public virtual ICollection<UnitPlace> UnitPlaces { get; set; }
        public virtual ICollection<UnitRespPerson> UnitRespPeople { get; set; }
    }
}
