using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class Project
    {
        public Project()
        {
            Budgets = new HashSet<Budget>();
            ProductsProjects = new HashSet<ProductsProject>();
            ProjectOperations = new HashSet<ProjectOperation>();
        }

        public string Reference { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int IdState { get; set; }
        public decimal? Value { get; set; }
        public string IdClient { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Client IdClientNavigation { get; set; } = null!;
        public virtual StatesProject IdStateNavigation { get; set; } = null!;
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<ProductsProject> ProductsProjects { get; set; }
        public virtual ICollection<ProjectOperation> ProjectOperations { get; set; }
    }
}
