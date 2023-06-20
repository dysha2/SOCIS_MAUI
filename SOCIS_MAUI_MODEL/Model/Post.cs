using System;
using System.Collections.Generic;

namespace SOCIS_MAUI_MODEL.Model
{
    public partial class Post
    {
        public Post()
        {
            People = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Person> People { get; set; }
    }
}
