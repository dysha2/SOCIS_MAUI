using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

namespace SOCIS_MAUI_MODEL.Model
{
    public partial class UnitType:ObservableObject
    {
        public UnitType()
        {
            FullNameUnits = new HashSet<FullNameUnit>();
        }
        
        public int Id { get; set; }
        [ObservableProperty]
        private string _Name = null!;
        public virtual ICollection<FullNameUnit> FullNameUnits { get; set; }
    }
}
