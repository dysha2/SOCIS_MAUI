using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SOCIS_MAUI_MODEL.Model
{
    public partial class Place:ObservableObject
    {
        public Place()
        {
            Requests = new HashSet<Request>();
            ShortTermMoves = new HashSet<ShortTermMove>();
            UnitPlaces = new HashSet<UnitPlace>();
        }

        public int Id { get; set; }
        [ObservableProperty]
        private string _Name = null!;
        [ObservableProperty]
        private string? _Description;

        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<ShortTermMove> ShortTermMoves { get; set; }
        public virtual ICollection<UnitPlace> UnitPlaces { get; set; }
    }
}
