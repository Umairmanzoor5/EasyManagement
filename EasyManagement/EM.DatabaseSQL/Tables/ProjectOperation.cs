using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class ProjectOperation
    {
        public string IdProject { get; set; } = null!;
        public int IdOpetarion { get; set; }
        public double Time { get; set; }

        public virtual TypeOperation IdOpetarionNavigation { get; set; } = null!;
        public virtual Project IdProjectNavigation { get; set; } = null!;
    }
}
