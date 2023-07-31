using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class TypeDocument
    {
        public TypeDocument()
        {
            SupplierClientMovements = new HashSet<SupplierClientMovement>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<SupplierClientMovement> SupplierClientMovements { get; set; }
    }
}
