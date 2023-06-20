using System;
using System.Collections.Generic;

namespace SOCIS_MAUI_MODEL.Model
{
    public partial class Place
    {
        public Place()
        {
            Requests = new HashSet<Request>();
            ShortTermMoves = new HashSet<ShortTermMove>();
            UnitPlaces = new HashSet<UnitPlace>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<ShortTermMove> ShortTermMoves { get; set; }
        public virtual ICollection<UnitPlace> UnitPlaces { get; set; }
    }
}
