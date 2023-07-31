using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class StatesProject
    {
        public StatesProject()
        {
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string Type { get; set; } = null!;

        public virtual ICollection<Project> Projects { get; set; }
    }
}
