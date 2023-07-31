using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class ProductsProject
    {
        public string IdProject { get; set; } = null!;
        public string IdProduct { get; set; } = null!;
        public double Quantity { get; set; }

        public virtual Product IdProductNavigation { get; set; } = null!;
        public virtual Project IdProjectNavigation { get; set; } = null!;
    }
}
