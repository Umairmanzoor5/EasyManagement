using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            SupplierClientMovements = new HashSet<SupplierClientMovement>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool? Active { get; set; }

        public virtual ICollection<SupplierClientMovement> SupplierClientMovements { get; set; }
    }
}
