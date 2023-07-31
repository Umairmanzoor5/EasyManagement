using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class Product
    {
        public Product()
        {
            ProductsBudgets = new HashSet<ProductsBudget>();
            ProductsManufacturings = new HashSet<ProductsManufacturing>();
            ProductsMovements = new HashSet<ProductsMovement>();
            ProductsProjects = new HashSet<ProductsProject>();
        }

        public string Reference { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int IdFamily { get; set; }
        public int IdUnit { get; set; }
        public int? Barcode { get; set; }
        public int IdTypeProduct { get; set; }
        public bool StockControl { get; set; }
        public double? StockAlert { get; set; }
        public int IdTax { get; set; }
        public decimal Sale { get; set; }
        public decimal Purchase { get; set; }
        public string IdSuppliers { get; set; } = null!;

        public virtual FamiliesProduct IdFamilyNavigation { get; set; } = null!;
        public virtual Supplier IdSuppliersNavigation { get; set; } = null!;
        public virtual TaxesProduct IdTaxNavigation { get; set; } = null!;
        public virtual TypesProduct IdTypeProductNavigation { get; set; } = null!;
        public virtual UnitiesProduct IdUnitNavigation { get; set; } = null!;
        public virtual ICollection<ProductsBudget> ProductsBudgets { get; set; }
        public virtual ICollection<ProductsManufacturing> ProductsManufacturings { get; set; }
        public virtual ICollection<ProductsMovement> ProductsMovements { get; set; }
        public virtual ICollection<ProductsProject> ProductsProjects { get; set; }
    }
}
