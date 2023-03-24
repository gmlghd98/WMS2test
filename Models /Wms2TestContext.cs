using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Models;

public partial class Wms2TestContext : DbContext
{
    public Wms2TestContext()
    {
    }

    public Wms2TestContext(DbContextOptions<Wms2TestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Board> Boards { get; set; }

    public virtual DbSet<BoardField> BoardFields { get; set; }

    public virtual DbSet<BoardFieldType> BoardFieldTypes { get; set; }

    public virtual DbSet<BoardList> BoardLists { get; set; }

    public virtual DbSet<BoardListValue> BoardListValues { get; set; }

    public virtual DbSet<HandlingUnit> HandlingUnits { get; set; }

    public virtual DbSet<HandlingUnitType> HandlingUnitTypes { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<InventoryComplex> InventoryComplexes { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductVariant> ProductVariants { get; set; }

    public virtual DbSet<ProductVariantComplex> ProductVariantComplexes { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TransactionHandlingUnit> TransactionHandlingUnits { get; set; }

    public virtual DbSet<Variant> Variants { get; set; }

    public virtual DbSet<VariantValue> VariantValues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=wms2_test;trusted_connection=false;persist security info=false;user id=sa;password=Crossing3305;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Board>(entity =>
        {
            entity.ToTable("Board");

            entity.Property(e => e.BoardId).HasMaxLength(50);
            entity.Property(e => e.BoardName).HasMaxLength(50);
            entity.Property(e => e.BoardTypeId).HasMaxLength(50);
            entity.Property(e => e.ParentBoardId).HasMaxLength(50);
        });

        modelBuilder.Entity<BoardField>(entity =>
        {
            entity.ToTable("BoardField");

            entity.Property(e => e.BoardFieldId).HasMaxLength(50);
            entity.Property(e => e.BoardFieldName).HasMaxLength(50);
            entity.Property(e => e.BoardFieldType).HasMaxLength(50);
            entity.Property(e => e.BoardId).HasMaxLength(50);
            entity.Property(e => e.RefBoardId).HasMaxLength(50);

            entity.HasOne(d => d.Board).WithMany(p => p.BoardFields)
                .HasForeignKey(d => d.BoardId)
                .HasConstraintName("FK_BoardField_Board");
        });

        modelBuilder.Entity<BoardFieldType>(entity =>
        {
            entity.ToTable("BoardFieldType");

            entity.Property(e => e.BoardFieldTypeId).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(50);
        });

        modelBuilder.Entity<BoardList>(entity =>
        {
            entity.ToTable("BoardList");

            entity.Property(e => e.BoardListId).HasMaxLength(50);
            entity.Property(e => e.BoardId).HasMaxLength(50);
            entity.Property(e => e.BoardListName).HasMaxLength(50);
            entity.Property(e => e.GroupName).HasMaxLength(50);
            entity.Property(e => e.ParentBoardListId).HasMaxLength(50);

            entity.HasOne(d => d.Board).WithMany(p => p.BoardLists)
                .HasForeignKey(d => d.BoardId)
                .HasConstraintName("FK_BoardList_Board");
        });

        modelBuilder.Entity<BoardListValue>(entity =>
        {
            entity.ToTable("BoardListValue");

            entity.Property(e => e.BoardListValueId).HasMaxLength(50);
            entity.Property(e => e.BoardFieldId).HasMaxLength(50);
            entity.Property(e => e.BoardListId).HasMaxLength(50);
            entity.Property(e => e.Value).HasMaxLength(50);

            entity.HasOne(d => d.BoardField).WithMany(p => p.BoardListValues)
                .HasForeignKey(d => d.BoardFieldId)
                .HasConstraintName("FK_BoardListValue_BoardField");

            entity.HasOne(d => d.BoardList).WithMany(p => p.BoardListValues)
                .HasForeignKey(d => d.BoardListId)
                .HasConstraintName("FK_BoardListValue_BoardList");
        });

        modelBuilder.Entity<HandlingUnit>(entity =>
        {
            entity.ToTable("HandlingUnit");

            entity.Property(e => e.HandlingUnitId).HasMaxLength(50);
            entity.Property(e => e.CurrentBin).HasMaxLength(50);
            entity.Property(e => e.HandlingUnitNumber).HasMaxLength(50);
            entity.Property(e => e.HandlingUnitTypeId).HasMaxLength(50);
            entity.Property(e => e.ParentHandlingUnitId).HasMaxLength(50);

            entity.HasOne(d => d.HandlingUnitType).WithMany(p => p.HandlingUnits)
                .HasForeignKey(d => d.HandlingUnitTypeId)
                .HasConstraintName("FK_HandlingUnit_HandlingUnitType");
        });

        modelBuilder.Entity<HandlingUnitType>(entity =>
        {
            entity.ToTable("HandlingUnitType");

            entity.Property(e => e.HandlingUnitTypeId).HasMaxLength(50);
            entity.Property(e => e.Depth).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.HandlingUnitTypeName).HasMaxLength(50);
            entity.Property(e => e.Height).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.LabelFormat).HasMaxLength(50);
            entity.Property(e => e.Width).HasColumnType("decimal(18, 4)");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.ToTable("Inventory");

            entity.Property(e => e.InventoryId).HasMaxLength(50);
            entity.Property(e => e.Barcode).HasMaxLength(50);
            entity.Property(e => e.ProductId).HasMaxLength(50);
            entity.Property(e => e.Sku)
                .HasMaxLength(50)
                .HasColumnName("SKU");

            entity.HasOne(d => d.Product).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Inventory_Product");
        });

        modelBuilder.Entity<InventoryComplex>(entity =>
        {
            entity.HasKey(e => e.InventoryComplexId).HasName("InventoryComplex_PK");

            entity.ToTable("InventoryComplex");

            entity.Property(e => e.InventoryId).HasMaxLength(50);
            entity.Property(e => e.VariantComplexId).HasMaxLength(50);

            entity.HasOne(d => d.Inventory).WithMany(p => p.InventoryComplexes)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InventoryComplex_Inventory");

            entity.HasOne(d => d.VariantComplex).WithMany(p => p.InventoryComplexes)
                .HasForeignKey(d => d.VariantComplexId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InventoryComplex_ProductVariantComplex");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Location");

            entity.Property(e => e.LocationId).HasMaxLength(50);
            entity.Property(e => e.Bin)
                .HasMaxLength(50)
                .HasColumnName("BIN");
            entity.Property(e => e.Depth).HasMaxLength(50);
            entity.Property(e => e.Height).HasMaxLength(50);
            entity.Property(e => e.LocationType).HasMaxLength(50);
            entity.Property(e => e.Memo).HasMaxLength(50);
            entity.Property(e => e.Width).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CDE86F6E5A");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasMaxLength(50);
            entity.Property(e => e.ItemCode).HasMaxLength(50);
            entity.Property(e => e.ProductCategoryId).HasMaxLength(50);
            entity.Property(e => e.ProductName).HasMaxLength(50);

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategoryId)
                .HasConstraintName("FK_Product_ProductCategory");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryId).HasName("PK__ProductC__3224ECCE7D47A2E1");

            entity.ToTable("ProductCategory");

            entity.Property(e => e.ProductCategoryId).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.ParentProductCategoryId).HasMaxLength(50);
            entity.Property(e => e.ProductCategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductVariant>(entity =>
        {
            entity.ToTable("ProductVariant");

            entity.Property(e => e.ProductVariantId).HasMaxLength(50);
            entity.Property(e => e.ProductId).HasMaxLength(50);
            entity.Property(e => e.VariantId).HasMaxLength(50);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductVariant_Product");

            entity.HasOne(d => d.Variant).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.VariantId)
                .HasConstraintName("FK_ProductVariant_Variant");
        });

        modelBuilder.Entity<ProductVariantComplex>(entity =>
        {
            entity.HasKey(e => e.VariantComplexId);

            entity.ToTable("ProductVariantComplex");

            entity.Property(e => e.VariantComplexId).HasMaxLength(50);
            entity.Property(e => e.ProductVariantId).HasMaxLength(50);
            entity.Property(e => e.VariantValueId).HasMaxLength(50);

            entity.HasOne(d => d.ProductVariant).WithMany(p => p.ProductVariantComplexes)
                .HasForeignKey(d => d.ProductVariantId)
                .HasConstraintName("FK_ProductVariantComplex_ProductVariant");

            entity.HasOne(d => d.VariantValue).WithMany(p => p.ProductVariantComplexes)
                .HasForeignKey(d => d.VariantValueId)
                .HasConstraintName("FK_ProductVariantComplex_VariantValue");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK_Transaction");

            entity.Property(e => e.TransactionId).HasMaxLength(50);
            entity.Property(e => e.BoardId).HasMaxLength(50);
            entity.Property(e => e.InventoryId).HasMaxLength(50);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.TransactionType).HasMaxLength(50);

            entity.HasOne(d => d.Board).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.BoardId)
                .HasConstraintName("FK_Transaction_Board");

            entity.HasOne(d => d.Inventory).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.InventoryId)
                .HasConstraintName("FK_Transaction_Inventory");
        });

        modelBuilder.Entity<TransactionHandlingUnit>(entity =>
        {
            entity.ToTable("TransactionHandlingUnit");

            entity.Property(e => e.TransactionHandlingUnitId).HasMaxLength(50);
            entity.Property(e => e.Bin)
                .HasMaxLength(50)
                .HasColumnName("BIN");
            entity.Property(e => e.HandlingUnitId).HasMaxLength(50);
            entity.Property(e => e.ParentHandlingUnitId).HasMaxLength(50);
            entity.Property(e => e.TransactionId).HasMaxLength(50);
            entity.Property(e => e.TransactionType).HasMaxLength(50);

            entity.HasOne(d => d.HandlingUnit).WithMany(p => p.TransactionHandlingUnits)
                .HasForeignKey(d => d.HandlingUnitId)
                .HasConstraintName("TransactionHandlingUnit_FK");

            entity.HasOne(d => d.Transaction).WithMany(p => p.TransactionHandlingUnits)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FK_TransactionHandlingUnit_Transaction");
        });

        modelBuilder.Entity<Variant>(entity =>
        {
            entity.ToTable("Variant");

            entity.Property(e => e.VariantId).HasMaxLength(50);
            entity.Property(e => e.VariantName).HasMaxLength(50);
        });

        modelBuilder.Entity<VariantValue>(entity =>
        {
            entity.ToTable("VariantValue");

            entity.Property(e => e.VariantValueId).HasMaxLength(50);
            entity.Property(e => e.Value).HasMaxLength(50);
            entity.Property(e => e.VariantId).HasMaxLength(50);

            entity.HasOne(d => d.Variant).WithMany(p => p.VariantValues)
                .HasForeignKey(d => d.VariantId)
                .HasConstraintName("FK_VariantValue_Variant");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
