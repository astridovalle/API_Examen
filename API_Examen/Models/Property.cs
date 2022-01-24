using System;
using System.Collections.Generic;

namespace API_Examen
{
    public partial class Property
    {
        public Property()
        {
            Activities = new HashSet<Activity>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DisabeledAt { get; set; }
        public string Status { get; set; } = null!;

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
