using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class ProductsMovement
    {
        public string RefProduct { get; set; } = null!;
        public int IdMovement { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public double? Discount { get; set; }

        public virtual SupplierClientMovement IdMovementNavigation { get; set; } = null!;
        public virtual Product RefProductNavigation { get; set; } = null!;
    }
}
