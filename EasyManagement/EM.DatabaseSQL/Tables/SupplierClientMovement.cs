using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class SupplierClientMovement
    {
        public SupplierClientMovement()
        {
            ProductsMovements = new HashSet<ProductsMovement>();
        }

        public int Id { get; set; }
        public string? IdSupplier { get; set; }
        public int? IdWarehouse { get; set; }
        public string? IdClient { get; set; }
        public int? IdTypeDocument { get; set; }
        public bool? IsEntry { get; set; }
        public DateTime Date { get; set; }

        public virtual Client? IdClientNavigation { get; set; }
        public virtual Supplier? IdSupplierNavigation { get; set; }
        public virtual TypeDocument? IdTypeDocumentNavigation { get; set; }
        public virtual Warehouse? IdWarehouseNavigation { get; set; }
        public virtual ICollection<ProductsMovement> ProductsMovements { get; set; }
    }
}
