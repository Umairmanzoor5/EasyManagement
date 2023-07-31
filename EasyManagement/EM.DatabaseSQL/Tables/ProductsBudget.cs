using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class ProductsBudget
    {
        public int IdBudjet { get; set; }
        public string IdProduct { get; set; } = null!;
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public double? Discount { get; set; }

        public virtual Budget IdBudjetNavigation { get; set; } = null!;
        public virtual Product IdProductNavigation { get; set; } = null!;
    }
}
