using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class Budget
    {
        public Budget()
        {
            HistoryOpetarions = new HashSet<HistoryOpetarion>();
            Manufacturings = new HashSet<Manufacturing>();
            ProductsBudgets = new HashSet<ProductsBudget>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public int IdState { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public decimal Value { get; set; }
        public string? IdProject { get; set; }

        public virtual Project? IdProjectNavigation { get; set; }
        public virtual StatesBudget IdStateNavigation { get; set; } = null!;
        public virtual ICollection<HistoryOpetarion> HistoryOpetarions { get; set; }
        public virtual ICollection<Manufacturing> Manufacturings { get; set; }
        public virtual ICollection<ProductsBudget> ProductsBudgets { get; set; }
    }
}
