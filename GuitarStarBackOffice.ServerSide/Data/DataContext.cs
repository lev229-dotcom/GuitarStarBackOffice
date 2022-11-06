using GuitarStarBackOffice.Shared;
using Microsoft.EntityFrameworkCore;

namespace GuitarStarBackOffice.ServerSide.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Post
        modelBuilder.Entity<Post>().HasKey(i => i.IdPost);

        modelBuilder.Entity<Post>()
            .HasMany(i => i.PostEmployees)
            .WithOne(i => i.Post)
            .HasForeignKey(i => i.PostId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        #endregion

        #region Employee

        modelBuilder.Entity<Employee>().HasKey(i => i.IdEmployee);
        modelBuilder.Entity<Employee>()
            .HasMany(i => i.PostEmployees)
            .WithOne(i => i.Employee)
            .HasForeignKey(i => i.EmployeeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Employee>()
            .HasMany(i => i.Orders)
            .WithOne(i => i.Employee)
            .HasForeignKey(i => i.EmployeeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        #endregion

        #region PostEmployee

        modelBuilder.Entity<PostEmployee>().HasKey(i => i.IdPostEmployee);

        modelBuilder.Entity<PostEmployee>()
            .HasOne(i => i.Post)
            .WithMany(i => i.PostEmployees)
            .HasForeignKey(i => i.PostId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PostEmployee>()
            .HasOne(i => i.Employee)
            .WithMany(i => i.PostEmployees)
            .HasForeignKey(i => i.EmployeeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        #endregion

        #region Category

        modelBuilder.Entity<Category>().HasKey(i => i.IdCategory);

        modelBuilder.Entity<Category>()
            .HasMany(i => i.Products)
            .WithOne(i => i.Category)
            .HasForeignKey(i => i.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        #endregion

        #region WareHouse

        modelBuilder.Entity<WareHouse>().HasKey(i => i.IdEWareHouse);

        modelBuilder.Entity<WareHouse>()
           .HasMany(i => i.Products)
           .WithOne(i => i.WareHouse)
           .HasForeignKey(i => i.WareHouseId)
           .IsRequired()
           .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WareHouse>()
          .HasMany(i => i.Shipments)
          .WithOne(i => i.Warehouse)
          .HasForeignKey(i => i.WareHouseId)
          .IsRequired()
          .OnDelete(DeleteBehavior.Cascade);

        #endregion

        #region Supplier

        modelBuilder.Entity<Supplier>().HasKey(i => i.IdSupplier);
        modelBuilder.Entity<Supplier>()
            .HasMany(i => i.Shipmnets)
            .WithOne(i => i.Supplier)
            .HasForeignKey(i => i.SupplierId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        #endregion

        #region Shipment

        modelBuilder.Entity<Shipment>().HasKey(i => i.IdShipment);
        modelBuilder.Entity<Shipment>()
            .HasOne(i => i.Warehouse)
            .WithMany(i => i.Shipments)
            .HasForeignKey(i => i.WareHouseId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Shipment>()
           .HasOne(i => i.Supplier)
           .WithMany(i => i.Shipmnets)
           .HasForeignKey(i => i.SupplierId)
           .IsRequired()
           .OnDelete(DeleteBehavior.Cascade);

        #endregion

        #region Product

        modelBuilder.Entity<Product>().HasKey(i => i.IdProduct);
        modelBuilder.Entity<Product>()
            .HasMany(i => i.OrderElemnts)
            .WithOne(i => i.Product)
            .HasForeignKey(i => i.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>()
               .HasOne(i => i.Category)
               .WithMany(i => i.Products)
               .HasForeignKey(i => i.CategoryId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
             .HasOne(i => i.WareHouse)
             .WithMany(i => i.Products)
             .HasForeignKey(i => i.WareHouseId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);


        #endregion

        #region Order

        modelBuilder.Entity<Order>().HasKey(i => i.IdOrder);
        modelBuilder.Entity<Order>()
            .HasOne(i => i.Employee)
            .WithMany(i => i.Orders)
            .HasForeignKey(i => i.EmployeeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
               .HasMany(i => i.OrderElements)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        #endregion

        #region OrderElement

        modelBuilder.Entity<OrderElement>().HasKey(i => i.IdOrderElement);

        modelBuilder.Entity<OrderElement>()
                    .HasOne(i => i.Order)
                    .WithMany(i => i.OrderElements)
                    .HasForeignKey(i => i.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderElement>()
                .HasOne(i => i.Product)
                .WithMany(i => i.OrderElemnts)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderElement> OrderElements { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostEmployee> PostEmployees { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<Supplier> Supplier { get; set; }
    public DbSet<WareHouse> WareHouses { get; set; }
}
