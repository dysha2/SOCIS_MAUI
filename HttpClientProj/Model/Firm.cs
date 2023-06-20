﻿using System;
using System.Collections.Generic;

namespace SOCIS_MAUI_MODEL.Model
{
    public partial class Firm
    {
        public Firm()
        {
            FullNameUnits = new HashSet<FullNameUnit>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<FullNameUnit> FullNameUnits { get; set; }
    }
}
