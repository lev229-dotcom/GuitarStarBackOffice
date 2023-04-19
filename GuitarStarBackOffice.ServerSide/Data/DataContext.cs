using GuitarStarBackOffice.ServerSide.Services.Hash;
using GuitarStarBackOffice.Shared;
using Microsoft.EntityFrameworkCore;

namespace GuitarStarBackOffice.ServerSide.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        //Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Post
        modelBuilder.Entity<Post>().HasKey(i => i.IdPost);

        modelBuilder.Entity<Post>()
            .HasMany(i => i.Employees)
            .WithOne(i => i.Post)
            .HasForeignKey(i => i.PostId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        #endregion

        #region Employee

        modelBuilder.Entity<Employee>().HasKey(i => i.IdEmployee);
        
        modelBuilder.Entity<Employee>()
            .HasOne(i => i.Post)
            .WithMany(i => i.Employees)
            .HasForeignKey(i => i.PostId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Employee>()
            .HasMany(i => i.Orders)
            .WithOne(i => i.Employee)
            .HasForeignKey(i => i.EmployeeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Employee>().Property(i => i.Patronymic).HasDefaultValue("");
        #endregion

        #region PostEmployee

        //modelBuilder.Entity<PostEmployee>().HasKey(i => i.IdPostEmployee);

        //modelBuilder.Entity<PostEmployee>()
        //    .HasOne(i => i.Post)
        //    .WithMany(i => i.PostEmployees)
        //    .HasForeignKey(i => i.PostId)
        //    .IsRequired()
        //    .OnDelete(DeleteBehavior.Cascade);

        //modelBuilder.Entity<PostEmployee>()
        //    .HasOne(i => i.Employee)
        //    .WithMany(i => i.PostEmployees)
        //    .HasForeignKey(i => i.EmployeeId)
        //    .IsRequired()
        //    .OnDelete(DeleteBehavior.Cascade);

        #endregion

        #region FileIMG
        modelBuilder.Entity<FileIMG>().HasKey(i => i.Id);

        modelBuilder.Entity<FileIMG>()
            .HasMany(i => i.Products)
            .WithOne(i => i.FileImage)
            .HasForeignKey(i => i.FileImageId)
            .IsRequired(false)
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
               .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>()
             .HasOne(i => i.WareHouse)
             .WithMany(i => i.Products)
             .HasForeignKey(i => i.WareHouseId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>()
            .HasOne(i => i.FileImage)
            .WithMany(i => i.Products)
            .HasForeignKey(i => i.FileImageId)
            .IsRequired(false)
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

        #region OrderElement

        modelBuilder.Entity<OrderElement>().HasKey(i => i.IdOrderElement);

        modelBuilder.Entity<OrderElement>()
                    .HasOne(i => i.Order)
                    .WithMany(i => i.OrderElements)
                    .HasForeignKey(i => i.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderElement>()
                .HasOne(i => i.Product)
                .WithMany(i => i.OrderElemnts)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

        #endregion



        #region Order

        modelBuilder.Entity<Order>().HasKey(i => i.IdOrder);
        modelBuilder.Entity<Order>()
            .HasOne(i => i.Employee)
            .WithMany(i => i.Orders)
            .HasForeignKey(i => i.EmployeeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
               .HasMany(i => i.OrderElements)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
   .HasOne(i => i.Client)
   .WithMany(i => i.Orders)
   .HasForeignKey(i => i.ClientId)
   .IsRequired()
   .OnDelete(DeleteBehavior.Cascade);

        #endregion

        #region Client
        modelBuilder.Entity<Client>().HasKey(i => i.IdClient);

        modelBuilder.Entity<Client>()
              .HasMany(i => i.Orders)
               .WithOne(i => i.Client)
               .HasForeignKey(i => i.ClientId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        #endregion Client

        #region WishlistElements

        modelBuilder.Entity<WishlistElements>().HasKey(i => i.IdWishlistElement);

        modelBuilder.Entity<WishlistElements>()
                    .HasOne(i => i.Product)
                    .WithMany(i => i.WishlistElements)
                    .HasForeignKey(i => i.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WishlistElements>()
                .HasOne(i => i.Client)
                .WithMany(i => i.WishlistElements)
                .HasForeignKey(i => i.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

        #endregion

        #region DefaultRecords
        modelBuilder.Entity<Post>()
            .HasData(
            new Post { IdPost = Guid.NewGuid(), PostName = "Сотрудник отдела продаж", Salary = 45_000 },
            new Post { IdPost = Guid.NewGuid(), PostName = "Сотрудник склада", Salary = 34_000 },
            new Post { IdPost = Guid.NewGuid(), PostName = "Сотрудник отдела кадров", Salary = 150_000 },
            new Post { IdPost = Guid.NewGuid(), PostName = "Бухгалтер", Salary = 65_000 },
            new Post { IdPost = Guid.Parse("07CEFFEA-914D-4509-B21D-E3A8042B6BF9"), PostName = "Администратор", Salary = 1 });

        modelBuilder.Entity<Employee>()
            .HasData(
            new Employee
            {
                IdEmployee = Guid.Parse("CAF7B5FF-56CF-40D7-93D2-685191C8774A"),
                Email = "admin@admin.com",
                Password = HashHelper.GetHashString("admin"),
                AccountCreateDate = DateTime.Now,
                Surname = "Иванов",
                Name = "Иван",
                Patronymic = "Иванович",
                PasportNumber = "111111",
                PasportSeries = "1111",
                DateOfBirth = DateTime.Parse("13.12.2002"),
                DateOfEmployment = DateTime.Parse("13.11.2022"),
                PostId = Guid.Parse("07CEFFEA-914D-4509-B21D-E3A8042B6BF9")
            });

        modelBuilder.Entity<Client>()
             .HasData(
            new Client
            {
                IdClient = Guid.Parse("2F036EBB-A7AE-43A2-9838-3DE45598F2F7"),
                ClientEmail = "client@client.com",
                ClientName = "Ivan",
                ClientNumber = "+796715257899",
                ClientLastName = "Ivanov",
                ClientPassword = HashHelper.GetHashString("client"),
                ClientAddress = "Москва, ул. Пролетарская, д. 65А, к. 2"

            });



        modelBuilder.Entity<Category>()
            .HasData(
            new Category
            {
                IdCategory = Guid.Parse("11A0D7CE-007C-44AC-9DA6-FE333BA042BE"),
                CategoryName = "Игровые консоли"
            },
            new Category
            {
                IdCategory = Guid.Parse("4B94A8DB-AC66-4A7F-9045-59672FF62E64"),
                CategoryName = "Наушники"
            },
            new Category
            {
                IdCategory = Guid.Parse("DA7D35D3-735C-40F7-B16C-CB65C1AC0D3F"),
                CategoryName = "Часы"
            },
            new Category
             {
                 IdCategory = Guid.Parse("497D6809-488D-4252-9CCA-D04956BAA87B"),
                 CategoryName = "Планшеты"
             },
            new Category
             {
                 IdCategory = Guid.Parse("E571E876-CCE6-4CDE-ADE1-D36732F3761D"),
                 CategoryName = "Ноутбуки"
             });

        modelBuilder.Entity<WareHouse>()
            .HasData(
            new WareHouse
            {
                IdEWareHouse = Guid.Parse("0C43BA79-F67A-46FA-A7AD-BEF8DD1CFF30"),
                Address = "Ольховская ул., 14, стр. 1, Москва"

            });

        modelBuilder.Entity<Product>()
            .HasData(
            new Product
            {
                IdProduct = Guid.Parse("5E19D537-D1EB-4A96-97EC-008B1874EC80"),
                ProductName = "Игровая консоль Playstation 5",
                ProductPrice = 45999.99,
                CategoryId = Guid.Parse("11A0D7CE-007C-44AC-9DA6-FE333BA042BE"),
                WareHouseId = Guid.Parse("0C43BA79-F67A-46FA-A7AD-BEF8DD1CFF30"),
                FileImageId = null
            },
            new Product
            {
                IdProduct = Guid.Parse("A53B5791-5769-4723-8996-5839ADD00BD3"),
                ProductName = "Игровая консоль Xbox series X",
                ProductPrice = 50999.99,
                CategoryId = Guid.Parse("11A0D7CE-007C-44AC-9DA6-FE333BA042BE"),
                WareHouseId = Guid.Parse("0C43BA79-F67A-46FA-A7AD-BEF8DD1CFF30"),
                FileImageId = null
            },
            new Product
            {
                IdProduct = Guid.Parse("7E3A380B-4EB3-4081-8872-B9FA93B2910F"),
                ProductName = "Наушники беспроводные Sony WHX-4",
                ProductPrice = 16999.99,
                CategoryId = Guid.Parse("4B94A8DB-AC66-4A7F-9045-59672FF62E64"),
                WareHouseId = Guid.Parse("0C43BA79-F67A-46FA-A7AD-BEF8DD1CFF30"),
                FileImageId = null
            },
            new Product
            {
                IdProduct = Guid.Parse("691D0935-F356-49A8-A857-01CDA670FBF0"),
                ProductName = "Наушники беспроводные Apple Airpods Pro",
                ProductPrice = 20999.99,
                CategoryId = Guid.Parse("4B94A8DB-AC66-4A7F-9045-59672FF62E64"),
                WareHouseId = Guid.Parse("0C43BA79-F67A-46FA-A7AD-BEF8DD1CFF30"),
                FileImageId = null
            },
            new Product
            {
                IdProduct = Guid.Parse("A70046B5-3427-4FEE-B0DE-5EE1445DDAC4"),
                ProductName = "Часы Apple Watch 7",
                ProductPrice = 25999.99,
                CategoryId = Guid.Parse("DA7D35D3-735C-40F7-B16C-CB65C1AC0D3F"),
                WareHouseId = Guid.Parse("0C43BA79-F67A-46FA-A7AD-BEF8DD1CFF30"),
                FileImageId = null
            },
            new Product
            {
                IdProduct = Guid.Parse("483359BA-0F10-4224-94B1-20F6DC8457DE"),
                ProductName = "Смарт браслет Xiaomi mi smart band 6",
                ProductPrice = 2999.99,
                CategoryId = Guid.Parse("DA7D35D3-735C-40F7-B16C-CB65C1AC0D3F"),
                WareHouseId = Guid.Parse("0C43BA79-F67A-46FA-A7AD-BEF8DD1CFF30"),
                FileImageId = null
            },
            new Product
            {
                IdProduct = Guid.Parse("DCDD60D8-385F-400C-9597-726F67BBF18B"),
                ProductName = "Планшет Apple Ipad Air (2021)",
                ProductPrice = 69999.99,
                CategoryId = Guid.Parse("497D6809-488D-4252-9CCA-D04956BAA87B"),
                WareHouseId = Guid.Parse("0C43BA79-F67A-46FA-A7AD-BEF8DD1CFF30"),
                FileImageId = null
            },
            new Product
            {
                IdProduct = Guid.Parse("E21D96DB-DA7C-4FF9-8025-DAB8B66961E6"),
                ProductName = "Планшет Samsung Tab 7 pro",
                ProductPrice = 59999.99,
                CategoryId = Guid.Parse("497D6809-488D-4252-9CCA-D04956BAA87B"),
                WareHouseId = Guid.Parse("0C43BA79-F67A-46FA-A7AD-BEF8DD1CFF30"),
                FileImageId = null
            },
            new Product
            {
                IdProduct = Guid.Parse("76ACD5F8-B37E-4954-82CE-FDCB41DEEBCD"),
                ProductName = "Ноутбук Apple MacBook Pro 16' M2 (2022)",
                ProductPrice = 169999.99,
                CategoryId = Guid.Parse("E571E876-CCE6-4CDE-ADE1-D36732F3761D"),
                WareHouseId = Guid.Parse("0C43BA79-F67A-46FA-A7AD-BEF8DD1CFF30"),
                FileImageId = null
            },
            new Product
            {
                IdProduct = Guid.Parse("FAEB4309-E0A0-44A6-B8EC-5F2E73C3FC2F"),
                ProductName = "Ноутбук Apple MacBook Air 14' M1 (2021)",
                ProductPrice = 109999.99,
                CategoryId = Guid.Parse("E571E876-CCE6-4CDE-ADE1-D36732F3761D"),
                WareHouseId = Guid.Parse("0C43BA79-F67A-46FA-A7AD-BEF8DD1CFF30"),
                FileImageId = null
            }) ;

        modelBuilder.Entity<Order>()
            .HasData(
            new Order
            {
                IdOrder = Guid.Parse("C99A7126-6BB2-4B9C-AA21-FCB4A7817F54"),
                OrderNumber = 1,
                OrderDate = DateTime.Parse("7.11.2022"),
                TotalOrderAmount = 9999.99,
                EmployeeId = Guid.Parse("CAF7B5FF-56CF-40D7-93D2-685191C8774A"),
                orderStatus = OrderStatus.Done,
                payementStatus = PayementStatus.Payed,
                CustomerName = "Фролов Дмитрий Анатольевич",
                CustomerEmail = "Leox555@yandex.ru",
                CustomerNumber = "+79671525380",
                CustomerAddress = "Москва, ул. Кутузовская, д. 32",
                ClientId = Guid.Parse("2F036EBB-A7AE-43A2-9838-3DE45598F2F7")
            }, 
            new Order
            {
                IdOrder = Guid.Parse("67F321B4-D3A4-4E26-9D1D-5ABD4C5AD328"),
                OrderNumber = 2,
                OrderDate = DateTime.Parse("7.11.2022"),
                TotalOrderAmount = 9999.99,
                EmployeeId = Guid.Parse("CAF7B5FF-56CF-40D7-93D2-685191C8774A"),
                orderStatus = OrderStatus.Done,
                payementStatus = PayementStatus.Payed,
                CustomerName = "Фролов Дмитрий Анатольевич",
                CustomerEmail = "Leox555@yandex.ru",
                CustomerNumber = "+79671525380",
                CustomerAddress = "Москва, ул. Кутузовская, д. 32",
                ClientId = Guid.Parse("2F036EBB-A7AE-43A2-9838-3DE45598F2F7")
            }, 
            
            new Order
            {
                IdOrder = Guid.Parse("E98676B9-4C4C-47F6-86CD-06CC5D8D0FFB"),
                OrderNumber = 3,
                OrderDate = DateTime.Parse("8.11.2022"),
                TotalOrderAmount = 19999.99,
                EmployeeId = Guid.Parse("CAF7B5FF-56CF-40D7-93D2-685191C8774A"),
                orderStatus = OrderStatus.Done,
                payementStatus = PayementStatus.Payed,
                CustomerName = "Фролов Дмитрий Анатольевич",
                CustomerEmail = "Leox555@yandex.ru",
                CustomerNumber = "+79671525380",
                CustomerAddress = "Москва, ул. Кутузовская, д. 32",
                ClientId = Guid.Parse("2F036EBB-A7AE-43A2-9838-3DE45598F2F7")
            },
            new Order
            {
                IdOrder = Guid.Parse("2835BDB2-E0B7-4EFF-9760-54D8D508AB0D"),
                OrderNumber = 4,
                OrderDate = DateTime.Parse("8.11.2022"),
                TotalOrderAmount = 19999.99,
                EmployeeId = Guid.Parse("CAF7B5FF-56CF-40D7-93D2-685191C8774A"),
                orderStatus = OrderStatus.Done,
                payementStatus = PayementStatus.Payed,
                CustomerName = "Фролов Дмитрий Анатольевич",
                CustomerEmail = "Leox555@yandex.ru",
                CustomerNumber = "+79671525380",
                CustomerAddress = "Москва, ул. Кутузовская, д. 32",
                ClientId = Guid.Parse("2F036EBB-A7AE-43A2-9838-3DE45598F2F7")
            },
            new Order
            {
                IdOrder = Guid.Parse("4B2EF55C-BBB0-48E3-8DD0-646D1C08F5F8"),
                OrderNumber = 5,
                OrderDate = DateTime.Parse("8.11.2022"),
                TotalOrderAmount = 9999.99,
                EmployeeId = Guid.Parse("CAF7B5FF-56CF-40D7-93D2-685191C8774A"),
                orderStatus = OrderStatus.Done,
                payementStatus = PayementStatus.Payed,
                CustomerName = "Фролов Дмитрий Анатольевич",
                CustomerEmail = "Leox555@yandex.ru",
                CustomerNumber = "+79671525380",
                CustomerAddress = "Москва, ул. Кутузовская, д. 32",
                ClientId = Guid.Parse("2F036EBB-A7AE-43A2-9838-3DE45598F2F7")
            },
            new Order
            {
                IdOrder = Guid.Parse("09100597-57F0-4FB2-B4C8-7C19DFF08311"),
                OrderNumber = 6,
                OrderDate = DateTime.Parse("14.11.2022"),
                TotalOrderAmount = 9999.99,
                EmployeeId = Guid.Parse("CAF7B5FF-56CF-40D7-93D2-685191C8774A"),
                orderStatus = OrderStatus.Done,
                payementStatus = PayementStatus.Payed,
                CustomerName = "Иванов Иван Иванович",
                CustomerEmail = "Tomlev418@yandex.ru",
                CustomerNumber = "+79671524411",
                CustomerAddress = "Москва, ул. Пролетарская, д. 65А, к. 2",
                ClientId = Guid.Parse("2F036EBB-A7AE-43A2-9838-3DE45598F2F7")
            },
            new Order
            {
                IdOrder = Guid.Parse("CA8C9DB3-40A7-4A50-B148-6678441CFE54"),
                OrderNumber = 7,
                OrderDate = DateTime.Parse("14.11.2022"),
                TotalOrderAmount = 9999.99,
                EmployeeId = Guid.Parse("CAF7B5FF-56CF-40D7-93D2-685191C8774A"),
                orderStatus = OrderStatus.Done,
                payementStatus = PayementStatus.Payed,
                CustomerName = "Иванов Иван Иванович",
                CustomerEmail = "Tomlev418@yandex.ru",
                CustomerNumber = "+79671524411",
                CustomerAddress = "Москва, ул. Пролетарская, д. 65А, к. 2",
                ClientId = Guid.Parse("2F036EBB-A7AE-43A2-9838-3DE45598F2F7")
            },
            new Order
            {
                IdOrder = Guid.Parse("733DF3FB-5830-40E8-B206-1FFEE7600ADD"),
                OrderNumber = 8,
                OrderDate = DateTime.Parse("14.11.2022"),
                TotalOrderAmount = 9999.99,
                EmployeeId = Guid.Parse("CAF7B5FF-56CF-40D7-93D2-685191C8774A"),
                orderStatus = OrderStatus.Done,
                payementStatus = PayementStatus.Payed,
                CustomerName = "Иванов Иван Иванович",
                CustomerEmail = "Tomlev418@yandex.ru",
                CustomerNumber = "+79671524411",
                CustomerAddress = "Москва, ул. Пролетарская, д. 65А, к. 2",
                ClientId = Guid.Parse("2F036EBB-A7AE-43A2-9838-3DE45598F2F7")
            },
            new Order
            {
                IdOrder = Guid.Parse("7E255600-DEB8-4697-B425-0516938FDCB1"),
                OrderNumber = 9,
                OrderDate = DateTime.Parse("16.11.2022"),
                TotalOrderAmount = 9999_99,
                EmployeeId = Guid.Parse("CAF7B5FF-56CF-40D7-93D2-685191C8774A"),
                orderStatus = OrderStatus.Done,
                payementStatus = PayementStatus.Payed,
                CustomerName = "Иванов Иван Иванович",
                CustomerEmail = "Tomlev418@yandex.ru",
                CustomerNumber = "+79671524411",
                CustomerAddress = "Москва, ул. Пролетарская, д. 65А, к. 2",
                ClientId = Guid.Parse("2F036EBB-A7AE-43A2-9838-3DE45598F2F7")
            }, 
            new Order
            {
                IdOrder = Guid.Parse("{8380BC2E-B6D2-4DE6-B2F4-F5D296997659}"),
                OrderNumber = 10,
                OrderDate = DateTime.Parse("15.11.2022"),
                TotalOrderAmount = 100_000,
                EmployeeId = Guid.Parse("CAF7B5FF-56CF-40D7-93D2-685191C8774A"),
                orderStatus = OrderStatus.Done,
                payementStatus = PayementStatus.Payed,
                CustomerName = "Иванов Иван Иванович",
                CustomerEmail = "Tomlev418@yandex.ru",
                CustomerNumber = "+79671524411",
                CustomerAddress = "Москва, ул. Пролетарская, д. 65А, к. 2",
                ClientId = Guid.Parse("2F036EBB-A7AE-43A2-9838-3DE45598F2F7")
            });
        #endregion
    }
    public DbSet<Client> Clients { get; set; }
    public DbSet<WishlistElements> WishlistElements { get; set; }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Post> Posts { get; set; }
    //public DbSet<PostEmployee> PostEmployees { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<Supplier> Supplier { get; set; }
    public DbSet<WareHouse> WareHouses { get; set; }
    public DbSet<EmployeeHistory> EmployeeHistories { get; set; }
    public DbSet<ProductHistory> ProductHistories { get; set; }
    public DbSet<OrderElement> OrderElements { get; set; }

    public DbSet<FileIMG> Files { get; set; }
}
