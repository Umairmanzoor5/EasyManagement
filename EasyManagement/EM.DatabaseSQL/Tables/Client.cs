using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class Client
    {
        public Client()
        {
            Projects = new HashSet<Project>();
            SupplierClientMovements = new HashSet<SupplierClientMovement>();
        }

        public string Nif { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? NameResponsible { get; set; }
        public string? ContactResponsible { get; set; }
        public string? EmailResponsible { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<SupplierClientMovement> SupplierClientMovements { get; set; }
    }
}
