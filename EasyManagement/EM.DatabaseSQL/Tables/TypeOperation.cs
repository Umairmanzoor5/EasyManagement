using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class TypeOperation
    {
        public TypeOperation()
        {
            HistoryOpetarions = new HashSet<HistoryOpetarion>();
            ProjectOperations = new HashSet<ProjectOperation>();
        }

        public int Id { get; set; }
        public string Type { get; set; } = null!;

        public virtual ICollection<HistoryOpetarion> HistoryOpetarions { get; set; }
        public virtual ICollection<ProjectOperation> ProjectOperations { get; set; }
    }
}
