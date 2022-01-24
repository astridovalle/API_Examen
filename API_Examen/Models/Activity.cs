using System;
using System.Collections.Generic;

namespace API_Examen
{
    public partial class Activity
    {
        public Activity()
        {
            Surveys = new HashSet<Survey>();
        }

        public int Id { get; set; }
        public int PropertyId { get; set; }
        public DateTime Schedule { get; set; }
        public string Title { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Status { get; set; } = null!;

        public virtual Property Property { get; set; } = null!;
        public virtual ICollection<Survey> Surveys { get; set; }
    }
}
