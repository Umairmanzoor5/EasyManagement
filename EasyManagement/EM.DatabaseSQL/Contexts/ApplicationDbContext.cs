using EM.DatabaseSQL.Tables;
using EM.DatabaseSQL.Views;
using Microsoft.EntityFrameworkCore;

namespace EM.DatabaseSQL.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; } = null!;
    public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
    public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
    public virtual DbSet<Budget> Budgets { get; set; } = null!;
    public virtual DbSet<Client> Clients { get; set; } = null!;
    public virtual DbSet<FamiliesProduct> FamiliesProducts { get; set; } = null!;
    public virtual DbSet<HistoryOpetarion> HistoryOpetarions { get; set; } = null!;
    public virtual DbSet<Manufacturing> Manufacturings { get; set; } = null!;
    public virtual DbSet<Product> Products { get; set; } = null!;
    public virtual DbSet<ProductsBudget> ProductsBudgets { get; set; } = null!;
    public virtual DbSet<ProductsManufacturing> ProductsManufacturings { get; set; } = null!;
    public virtual DbSet<ProductsMovement> ProductsMovements { get; set; } = null!;
    public virtual DbSet<ProductsProject> ProductsProjects { get; set; } = null!;
    public virtual DbSet<Project> Projects { get; set; } = null!;
    public virtual DbSet<ProjectOperation> ProjectOperations { get; set; } = null!;
    public virtual DbSet<StatesBudget> StatesBudgets { get; set; } = null!;
    public virtual DbSet<StatesManufacturing> StatesManufacturings { get; set; } = null!;
    public virtual DbSet<StatesProject> StatesProjects { get; set; } = null!;
    public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
    public virtual DbSet<SupplierClientMovement> SupplierClientMovements { get; set; } = null!;
    public virtual DbSet<TaxesProduct> TaxesProducts { get; set; } = null!;
    public virtual DbSet<TypeDocument> TypeDocuments { get; set; } = null!;
    public virtual DbSet<TypeMovement> TypeMovements { get; set; } = null!;
    public virtual DbSet<TypeOperation> TypeOperations { get; set; } = null!;
    public virtual DbSet<TypesProduct> TypesProducts { get; set; } = null!;
    public virtual DbSet<UnitiesProduct> UnitiesProducts { get; set; } = null!;
    public virtual DbSet<ViewCurrentStock> ViewCurrentStocks { get; set; } = null!;
    public virtual DbSet<ViewProject> ViewProjects { get; set; } = null!;
    public virtual DbSet<ViewStockRef> ViewStockRefs { get; set; } = null!;
    public virtual DbSet<ViewStockWarehouse> ViewStockWarehouses { get; set; } = null!;
    public virtual DbSet<Warehouse> Warehouses { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("pi_em");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Email).HasMaxLength(50);

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.Property(e => e.Password).HasMaxLength(64);

            entity.Property(e => e.Salary).HasColumnType("money");
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.ToTable("AspNetRoles", "dbo");

            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);

            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.ToTable("AspNetRoleClaims", "dbo");

            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role)
                .WithMany(p => p.AspNetRoleClaims)
                .HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.ToTable("AspNetUsers", "dbo");

            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);

            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles)
                .WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");

                        j.ToTable("AspNetUserRoles", "dbo");

                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.ToTable("AspNetUserClaims", "dbo");

            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User)
                .WithMany(p => p.AspNetUserClaims)
                .HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.ToTable("AspNetUserLogins", "dbo");

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);

            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User)
                .WithMany(p => p.AspNetUserLogins)
                .HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.ToTable("AspNetUserTokens", "dbo");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);

            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User)
                .WithMany(p => p.AspNetUserTokens)
                .HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Budget>(entity =>
        {
            entity.Property(e => e.IdProject).HasMaxLength(50);

            entity.Property(e => e.Value).HasColumnType("money");

            entity.HasOne(d => d.IdProjectNavigation)
                .WithMany(p => p.Budgets)
                .HasForeignKey(d => d.IdProject)
                .HasConstraintName("FK_Budgets_Projects");

            entity.HasOne(d => d.IdStateNavigation)
                .WithMany(p => p.Budgets)
                .HasForeignKey(d => d.IdState)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Budgets_StatesBudget");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Nif);

            entity.Property(e => e.Nif).HasMaxLength(50);

            entity.Property(e => e.Address1)
                .HasMaxLength(50)
                .HasColumnName("Address_1");

            entity.Property(e => e.Address2)
                .HasMaxLength(50)
                .HasColumnName("Address_2");

            entity.Property(e => e.City).HasMaxLength(50);

            entity.Property(e => e.ContactResponsible).HasMaxLength(50);

            entity.Property(e => e.EmailResponsible).HasMaxLength(50);

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.Property(e => e.NameResponsible).HasMaxLength(50);

            entity.Property(e => e.PostalCode).HasMaxLength(50);
        });

        modelBuilder.Entity<FamiliesProduct>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<HistoryOpetarion>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.Property(e => e.IdUser).HasMaxLength(50);

            entity.HasOne(d => d.IdBudgetNavigation)
                .WithMany(p => p.HistoryOpetarions)
                .HasForeignKey(d => d.IdBudget)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HistoryOpetarions_Budgets");

            entity.HasOne(d => d.IdTypeOperationNavigation)
                .WithMany(p => p.HistoryOpetarions)
                .HasForeignKey(d => d.IdTypeOperation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HistoryOpetarions_TypeOperations");
        });

        modelBuilder.Entity<Manufacturing>(entity =>
        {
            entity.HasKey(e => e.Reference);

            entity.ToTable("Manufacturing");

            entity.Property(e => e.Reference).HasMaxLength(50);

            entity.Property(e => e.Description).HasMaxLength(50);

            entity.HasOne(d => d.IdBudgetNavigation)
                .WithMany(p => p.Manufacturings)
                .HasForeignKey(d => d.IdBudget)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Manufacturing_Budgets");

            entity.HasOne(d => d.IdFamilyNavigation)
                .WithMany(p => p.Manufacturings)
                .HasForeignKey(d => d.IdFamily)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Manufacturing_FamiliesProducts");

            entity.HasOne(d => d.IdStateNavigation)
                .WithMany(p => p.Manufacturings)
                .HasForeignKey(d => d.IdState)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Manufacturing_StatesManufacturing");

            entity.HasOne(d => d.IdUnitNavigation)
                .WithMany(p => p.Manufacturings)
                .HasForeignKey(d => d.IdUnit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Manufacturing_UnitiesProducts");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Reference);

            entity.Property(e => e.Reference).HasMaxLength(50);

            entity.Property(e => e.IdSuppliers).HasMaxLength(50);

            entity.Property(e => e.Purchase).HasColumnType("money");

            entity.Property(e => e.Sale).HasColumnType("money");

            entity.HasOne(d => d.IdFamilyNavigation)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.IdFamily)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_FamiliesProducts");

            entity.HasOne(d => d.IdSuppliersNavigation)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.IdSuppliers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Suppliers");

            entity.HasOne(d => d.IdTaxNavigation)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.IdTax)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_TaxesProducts");

            entity.HasOne(d => d.IdTypeProductNavigation)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.IdTypeProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_TypesProducts");

            entity.HasOne(d => d.IdUnitNavigation)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.IdUnit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_UnitiesProducts");
        });

        modelBuilder.Entity<ProductsBudget>(entity =>
        {
            entity.HasKey(e => new { e.IdBudjet, e.IdProduct });

            entity.Property(e => e.IdProduct).HasMaxLength(50);

            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.IdBudjetNavigation)
                .WithMany(p => p.ProductsBudgets)
                .HasForeignKey(d => d.IdBudjet)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductsBudgets_Budgets");

            entity.HasOne(d => d.IdProductNavigation)
                .WithMany(p => p.ProductsBudgets)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductsBudgets_Products");
        });

        modelBuilder.Entity<ProductsManufacturing>(entity =>
        {
            entity.HasKey(e => new { e.IdManufacturing, e.IdProducts });

            entity.ToTable("ProductsManufacturing");

            entity.Property(e => e.IdManufacturing).HasMaxLength(50);

            entity.Property(e => e.IdProducts).HasMaxLength(50);

            entity.Property(e => e.Quantity)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.IdManufacturingNavigation)
                .WithMany(p => p.ProductsManufacturings)
                .HasForeignKey(d => d.IdManufacturing)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductsManufacturing_Manufacturing");

            entity.HasOne(d => d.IdProductsNavigation)
                .WithMany(p => p.ProductsManufacturings)
                .HasForeignKey(d => d.IdProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductsManufacturing_Products");
        });

        modelBuilder.Entity<ProductsMovement>(entity =>
        {
            entity.HasKey(e => new { e.RefProduct, e.IdMovement });

            entity.ToTable("ProductsMovement");

            entity.Property(e => e.RefProduct).HasMaxLength(50);

            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.IdMovementNavigation)
                .WithMany(p => p.ProductsMovements)
                .HasForeignKey(d => d.IdMovement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductsMovement_SupplierClientMovements");

            entity.HasOne(d => d.RefProductNavigation)
                .WithMany(p => p.ProductsMovements)
                .HasForeignKey(d => d.RefProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductsMovement_Products");
        });

        modelBuilder.Entity<ProductsProject>(entity =>
        {
            entity.HasKey(e => new { e.IdProject, e.IdProduct });

            entity.Property(e => e.IdProject).HasMaxLength(50);

            entity.Property(e => e.IdProduct).HasMaxLength(50);

            entity.HasOne(d => d.IdProductNavigation)
                .WithMany(p => p.ProductsProjects)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductsProjects_Products");

            entity.HasOne(d => d.IdProjectNavigation)
                .WithMany(p => p.ProductsProjects)
                .HasForeignKey(d => d.IdProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductsProjects_Projects");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Reference);

            entity.Property(e => e.Reference).HasMaxLength(50);

            entity.Property(e => e.IdClient).HasMaxLength(50);

            entity.Property(e => e.Value).HasColumnType("money");

            entity.HasOne(d => d.IdClientNavigation)
                .WithMany(p => p.Projects)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_Clients");

            entity.HasOne(d => d.IdStateNavigation)
                .WithMany(p => p.Projects)
                .HasForeignKey(d => d.IdState)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_StatesProject");
        });

        modelBuilder.Entity<ProjectOperation>(entity =>
        {
            entity.HasKey(e => new { e.IdProject, e.IdOpetarion });

            entity.Property(e => e.IdProject).HasMaxLength(50);

            entity.HasOne(d => d.IdOpetarionNavigation)
                .WithMany(p => p.ProjectOperations)
                .HasForeignKey(d => d.IdOpetarion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectOperations_TypeOperations");

            entity.HasOne(d => d.IdProjectNavigation)
                .WithMany(p => p.ProjectOperations)
                .HasForeignKey(d => d.IdProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectOperations_Projects");
        });

        modelBuilder.Entity<StatesBudget>(entity =>
        {
            entity.ToTable("StatesBudget");

            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<StatesManufacturing>(entity =>
        {
            entity.ToTable("StatesManufacturing");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<StatesProject>(entity =>
        {
            entity.ToTable("StatesProject");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Nif);

            entity.Property(e => e.Nif).HasMaxLength(50);

            entity.Property(e => e.Address1)
                .HasMaxLength(50)
                .HasColumnName("Address_1");

            entity.Property(e => e.Address2)
                .HasMaxLength(50)
                .HasColumnName("Address_2");

            entity.Property(e => e.City).HasMaxLength(50);

            entity.Property(e => e.ContactComercial).HasMaxLength(50);

            entity.Property(e => e.EmailComercial).HasMaxLength(50);

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.Property(e => e.NameComercial).HasMaxLength(50);

            entity.Property(e => e.PostalCode).HasMaxLength(50);
        });

        modelBuilder.Entity<SupplierClientMovement>(entity =>
        {
            entity.Property(e => e.IdClient).HasMaxLength(50);

            entity.Property(e => e.IdSupplier).HasMaxLength(50);

            entity.HasOne(d => d.IdClientNavigation)
                .WithMany(p => p.SupplierClientMovements)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_SupplierClientMovements_Clients");

            entity.HasOne(d => d.IdSupplierNavigation)
                .WithMany(p => p.SupplierClientMovements)
                .HasForeignKey(d => d.IdSupplier)
                .HasConstraintName("FK_SupplierClientMovements_Suppliers");

            entity.HasOne(d => d.IdTypeDocumentNavigation)
                .WithMany(p => p.SupplierClientMovements)
                .HasForeignKey(d => d.IdTypeDocument)
                .HasConstraintName("FK_SupplierClientMovements_TypeDocument");

            entity.HasOne(d => d.IdWarehouseNavigation)
                .WithMany(p => p.SupplierClientMovements)
                .HasForeignKey(d => d.IdWarehouse)
                .HasConstraintName("FK_SupplierClientMovements_Warehouses");
        });

        modelBuilder.Entity<TaxesProduct>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<TypeDocument>(entity =>
        {
            entity.ToTable("TypeDocument");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<TypeMovement>(entity =>
        {
            entity.ToTable("TypeMovement");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<TypeOperation>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<TypesProduct>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<UnitiesProduct>(entity =>
        {
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<ViewCurrentStock>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("View_CurrentStock");

            entity.Property(e => e.NameFamily).HasMaxLength(50);

            entity.Property(e => e.Pl)
                .HasColumnType("money")
                .HasColumnName("PL");

            entity.Property(e => e.Pvp)
                .HasColumnType("money")
                .HasColumnName("PVP");

            entity.Property(e => e.Reference).HasMaxLength(50);

            entity.Property(e => e.Unit).HasMaxLength(50);
        });

        modelBuilder.Entity<ViewProject>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("ViewProjects");

            entity.Property(e => e.Client).HasMaxLength(50);

            entity.Property(e => e.Reference).HasMaxLength(50);

            entity.Property(e => e.State).HasMaxLength(50);

            entity.Property(e => e.Value).HasColumnType("money");
        });

        modelBuilder.Entity<ViewStockRef>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("View_Stock_Ref");

            entity.Property(e => e.Diferenca).HasColumnName("diferenca");

            entity.Property(e => e.RefProduct).HasMaxLength(50);
        });

        modelBuilder.Entity<ViewStockWarehouse>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("View_StockWarehouse");

            entity.Property(e => e.Reference).HasMaxLength(50);
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

    }
}