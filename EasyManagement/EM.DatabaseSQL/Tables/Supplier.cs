using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
            SupplierClientMovements = new HashSet<SupplierClientMovement>();
        }

        public string Nif { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? NameComercial { get; set; }
        public string? ContactComercial { get; set; }
        public string? EmailComercial { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<SupplierClientMovement> SupplierClientMovements { get; set; }
    }
}
