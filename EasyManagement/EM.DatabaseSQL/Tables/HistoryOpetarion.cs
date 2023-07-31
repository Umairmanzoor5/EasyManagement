using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class HistoryOpetarion
    {
        public string IdUser { get; set; } = null!;
        public int IdTypeOperation { get; set; }
        public double Time { get; set; }
        public int IdBudget { get; set; }

        public virtual Budget IdBudgetNavigation { get; set; } = null!;
        public virtual TypeOperation IdTypeOperationNavigation { get; set; } = null!;
    }
}
