using System;
using System.Collections.Generic;

namespace SOCIS_MAUI_MODEL.Model
{
    public partial class Service
    {
        public Service()
        {
            WorkOnRequests = new HashSet<WorkOnRequest>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;

        public virtual ICollection<WorkOnRequest> WorkOnRequests { get; set; }
    }
}
