using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOCIS_MAUI_MODEL.Model
{
    public partial class Person
    {
        public Person()
        {
            Requests = new HashSet<Request>();
            UnitRespPeople = new HashSet<UnitRespPerson>();
            WorkOnRequests = new HashSet<WorkOnRequest>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Lastname { get; set; }
        public int? PostId { get; set; }
        public int? DepartmentId { get; set; }
        public string? Email { get; set; }
        public string? Comment { get; set; }
        public string? UserName { get; set; }
        public int RoleId { get; set; }
        public string? Password { get; set; }
        public string? PasswordSalt { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{Surname} {Name}";
            }
        }
        public virtual Department? Department { get; set; }
        public virtual Post? Post { get; set; }
        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<UnitRespPerson> UnitRespPeople { get; set; }
        public virtual ICollection<WorkOnRequest> WorkOnRequests { get; set; }
    }
}
