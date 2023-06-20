using System;
using System.Collections.Generic;

namespace SOCIS_MAUI_MODEL.Model
{
    public partial class Request
    {
        public Request()
        {
            WorkOnRequests = new HashSet<WorkOnRequest>();
        }

        public int Id { get; set; }
        public int DeclarantId { get; set; }
        public DateTime DateTimeStart { get; set; }
        public string Description { get; set; } = null!;
        public DateTime? DateTimeEnd { get; set; }
        public int PlaceId { get; set; }
        public bool IsComplete { get; set; }
        public int RequestStatusId { get; set; }

        public virtual Person Declarant { get; set; } = null!;
        public virtual Place Place { get; set; } = null!;
        public virtual RequestStatus RequestStatus { get; set; } = null!;
        public virtual ICollection<WorkOnRequest> WorkOnRequests { get; set; }
    }
}
