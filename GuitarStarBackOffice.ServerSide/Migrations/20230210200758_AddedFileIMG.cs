using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuitarStarBackOffice.ServerSide.Migrations
{
    public partial class AddedFileIMG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderElements_Employees_EmployeeId",
                table: "OrderElements");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderElements_OrderElements_OrderId",
                table: "OrderElements");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderElements_Products_ProductId",
                table: "OrderElements");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_WareHouses_WareHouseId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "PostEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderElements",
                table: "OrderElements");

            migrationBuilder.DropIndex(
                name: "IX_OrderElements_EmployeeId",
                table: "OrderElements");

            migrationBuilder.DropColumn(
                name: "IdOrder",
                table: "OrderElements");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "OrderElements");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "OrderElements");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "OrderElements");

            migrationBuilder.DropColumn(
                name: "TotalOrderAmount",
                table: "OrderElements");

            migrationBuilder.DropColumn(
                name: "orderStatus",
                table: "OrderElements");

            migrationBuilder.DropColumn(
                name: "paymentStatus",
                table: "OrderElements");

            migrationBuilder.AddColumn<Guid>(
                name: "FileImageId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PasportSeries",
                table: "Employees",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PasportNumber",
                table: "Employees",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "PostId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderElements",
                table: "OrderElements",
                column: "IdOrderElement");

            migrationBuilder.CreateTable(
                name: "EmployeeHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    IdOrder = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalOrderAmount = table.Column<double>(type: "float", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    orderStatus = table.Column<int>(type: "int", nullable: false),
                    payementStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.IdOrder);
                    table.ForeignKey(
                        name: "FK_Orders_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "IdEmployee",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHistories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "IdCategory", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("11a0d7ce-007c-44ac-9da6-fe333ba042be"), "Акустическая гитара" },
                    { new Guid("497d6809-488d-4252-9cca-d04956baa87b"), "Укулеле" },
                    { new Guid("4b94a8db-ac66-4a7f-9045-59672ff62e64"), "Электро гитара" },
                    { new Guid("da7d35d3-735c-40f7-b16c-cb65c1ac0d3f"), "Бас гитара" },
                    { new Guid("e571e876-cce6-4cde-ade1-d36732f3761d"), "Балалайка" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "IdPost", "PostName", "Salary" },
                values: new object[,]
                {
                    { new Guid("07ceffea-914d-4509-b21d-e3a8042b6bf9"), "Администратор", 1.0 },
                    { new Guid("2f8c686a-bf13-42c7-aad5-1f3ef26e7b55"), "Сотрудник отдела продаж", 45000.0 },
                    { new Guid("834a256a-6a3a-4035-ae0f-a121bff88244"), "Сотрудник склада", 34000.0 },
                    { new Guid("8cb375a6-9a1d-49f1-8069-e832b085530a"), "Сотрудник отдела кадров", 150000.0 },
                    { new Guid("f8393c7d-3dad-4055-a68a-2b56bae6f21c"), "Бухгалтер", 65000.0 }
                });

            migrationBuilder.InsertData(
                table: "WareHouses",
                columns: new[] { "IdEWareHouse", "Address" },
                values: new object[] { new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30"), "Ольховская ул., 14, стр. 1, Москва" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "IdEmployee", "AccountCreateDate", "DateOfBirth", "DateOfEmployment", "Email", "EmployeeRole", "Name", "PasportNumber", "PasportSeries", "Password", "Patronymic", "PostId", "Surname" },
                values: new object[] { new Guid("caf7b5ff-56cf-40d7-93d2-685191c8774a"), new DateTime(2023, 2, 10, 23, 7, 57, 976, DateTimeKind.Local).AddTicks(667), new DateTime(2002, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.com", 0, "Иван", "111111", "1111", "D033E22AE348AEB5660FC2140AEC35850C4DA997", "Иванович", new Guid("07ceffea-914d-4509-b21d-e3a8042b6bf9"), "Иванов" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "IdProduct", "CategoryId", "FileImageId", "ProductName", "ProductPrice", "WareHouseId" },
                values: new object[,]
                {
                    { new Guid("483359ba-0f10-4224-94b1-20f6dc8457de"), new Guid("da7d35d3-735c-40f7-b16c-cb65c1ac0d3f"), new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30"), "Бас-гитара Prodipe JMFJB80MAASH4C JB80MA MA", 9999.9899999999998, new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30") },
                    { new Guid("5e19d537-d1eb-4a96-97ec-008b1874ec80"), new Guid("11a0d7ce-007c-44ac-9da6-fe333ba042be"), new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30"), "Классическая гитара Doff RGC", 9999.9899999999998, new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30") },
                    { new Guid("691d0935-f356-49a8-a857-01cda670fbf0"), new Guid("4b94a8db-ac66-4a7f-9045-59672ff62e64"), new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30"), "Электрогитара Caraya E235BK", 9999.9899999999998, new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30") },
                    { new Guid("76acd5f8-b37e-4954-82ce-fdcb41deebcd"), new Guid("e571e876-cce6-4cde-ade1-d36732f3761d"), new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30"), "Балалайка 3-струнная DOFF BBM Bass Master", 9999.9899999999998, new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30") },
                    { new Guid("7e3a380b-4eb3-4081-8872-b9fa93b2910f"), new Guid("4b94a8db-ac66-4a7f-9045-59672ff62e64"), new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30"), "Электрогитара Fabio ST100 N", 9999.9899999999998, new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30") },
                    { new Guid("a53b5791-5769-4723-8996-5839add00bd3"), new Guid("11a0d7ce-007c-44ac-9da6-fe333ba042be"), new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30"), "Акустическая гитара Doff D016A", 9999.9899999999998, new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30") },
                    { new Guid("a70046b5-3427-4fee-b0de-5ee1445ddac4"), new Guid("da7d35d3-735c-40f7-b16c-cb65c1ac0d3f"), new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30"), "Бас-гитара Cort Action-HH4-TLB Action Series", 9999.9899999999998, new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30") },
                    { new Guid("dcdd60d8-385f-400c-9597-726f67bbf18b"), new Guid("497d6809-488d-4252-9cca-d04956baa87b"), new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30"), "RFU10S Friends Series Укулеле сопрано, Ortega", 9999.9899999999998, new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30") },
                    { new Guid("e21d96db-da7c-4ff9-8025-dab8b66961e6"), new Guid("497d6809-488d-4252-9cca-d04956baa87b"), new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30"), "Укулеле концерт LAG TKU-130C", 9999.9899999999998, new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30") },
                    { new Guid("faeb4309-e0a0-44a6-b8ec-5f2e73c3fc2f"), new Guid("e571e876-cce6-4cde-ade1-d36732f3761d"), new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30"), "SBF-RRE Русский рок Балалайка электроакустическая, трехструнная, Балалайкеръ", 9999.9899999999998, new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30") }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "IdOrder", "EmployeeId", "OrderDate", "OrderNumber", "TotalOrderAmount", "orderStatus", "payementStatus" },
                values: new object[,]
                {
                    { new Guid("09100597-57f0-4fb2-b4c8-7c19dff08311"), new Guid("caf7b5ff-56cf-40d7-93d2-685191c8774a"), new DateTime(2022, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 9999.9899999999998, 2, 1 },
                    { new Guid("2835bdb2-e0b7-4eff-9760-54d8d508ab0d"), new Guid("caf7b5ff-56cf-40d7-93d2-685191c8774a"), new DateTime(2022, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 19999.990000000002, 2, 1 },
                    { new Guid("4b2ef55c-bbb0-48e3-8dd0-646d1c08f5f8"), new Guid("caf7b5ff-56cf-40d7-93d2-685191c8774a"), new DateTime(2022, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 9999.9899999999998, 2, 1 },
                    { new Guid("67f321b4-d3a4-4e26-9d1d-5abd4c5ad328"), new Guid("caf7b5ff-56cf-40d7-93d2-685191c8774a"), new DateTime(2022, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 9999.9899999999998, 2, 1 },
                    { new Guid("733df3fb-5830-40e8-b206-1ffee7600add"), new Guid("caf7b5ff-56cf-40d7-93d2-685191c8774a"), new DateTime(2022, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 9999.9899999999998, 2, 1 },
                    { new Guid("7e255600-deb8-4697-b425-0516938fdcb1"), new Guid("caf7b5ff-56cf-40d7-93d2-685191c8774a"), new DateTime(2022, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 999999.0, 2, 1 },
                    { new Guid("8380bc2e-b6d2-4de6-b2f4-f5d296997659"), new Guid("caf7b5ff-56cf-40d7-93d2-685191c8774a"), new DateTime(2022, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 100000.0, 2, 1 },
                    { new Guid("c99a7126-6bb2-4b9c-aa21-fcb4a7817f54"), new Guid("caf7b5ff-56cf-40d7-93d2-685191c8774a"), new DateTime(2022, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 9999.9899999999998, 2, 1 },
                    { new Guid("ca8c9db3-40a7-4a50-b148-6678441cfe54"), new Guid("caf7b5ff-56cf-40d7-93d2-685191c8774a"), new DateTime(2022, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 9999.9899999999998, 2, 1 },
                    { new Guid("e98676b9-4c4c-47f6-86cd-06cc5d8d0ffb"), new Guid("caf7b5ff-56cf-40d7-93d2-685191c8774a"), new DateTime(2022, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 19999.990000000002, 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_FileImageId",
                table: "Products",
                column: "FileImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PostId",
                table: "Employees",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmployeeId",
                table: "Orders",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Posts_PostId",
                table: "Employees",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "IdPost",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderElements_Orders_OrderId",
                table: "OrderElements",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "IdOrder",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderElements_Products_ProductId",
                table: "OrderElements",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "IdProduct",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "IdCategory",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Files_FileImageId",
                table: "Products",
                column: "FileImageId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_WareHouses_WareHouseId",
                table: "Products",
                column: "WareHouseId",
                principalTable: "WareHouses",
                principalColumn: "IdEWareHouse",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Posts_PostId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderElements_Orders_OrderId",
                table: "OrderElements");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderElements_Products_ProductId",
                table: "OrderElements");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Files_FileImageId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_WareHouses_WareHouseId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "EmployeeHistories");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductHistories");

            migrationBuilder.DropIndex(
                name: "IX_Products_FileImageId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderElements",
                table: "OrderElements");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PostId",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "IdPost",
                keyValue: new Guid("2f8c686a-bf13-42c7-aad5-1f3ef26e7b55"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "IdPost",
                keyValue: new Guid("834a256a-6a3a-4035-ae0f-a121bff88244"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "IdPost",
                keyValue: new Guid("8cb375a6-9a1d-49f1-8069-e832b085530a"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "IdPost",
                keyValue: new Guid("f8393c7d-3dad-4055-a68a-2b56bae6f21c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "IdProduct",
                keyValue: new Guid("483359ba-0f10-4224-94b1-20f6dc8457de"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "IdProduct",
                keyValue: new Guid("5e19d537-d1eb-4a96-97ec-008b1874ec80"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "IdProduct",
                keyValue: new Guid("691d0935-f356-49a8-a857-01cda670fbf0"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "IdProduct",
                keyValue: new Guid("76acd5f8-b37e-4954-82ce-fdcb41deebcd"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "IdProduct",
                keyValue: new Guid("7e3a380b-4eb3-4081-8872-b9fa93b2910f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "IdProduct",
                keyValue: new Guid("a53b5791-5769-4723-8996-5839add00bd3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "IdProduct",
                keyValue: new Guid("a70046b5-3427-4fee-b0de-5ee1445ddac4"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "IdProduct",
                keyValue: new Guid("dcdd60d8-385f-400c-9597-726f67bbf18b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "IdProduct",
                keyValue: new Guid("e21d96db-da7c-4ff9-8025-dab8b66961e6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "IdProduct",
                keyValue: new Guid("faeb4309-e0a0-44a6-b8ec-5f2e73c3fc2f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "IdCategory",
                keyValue: new Guid("11a0d7ce-007c-44ac-9da6-fe333ba042be"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "IdCategory",
                keyValue: new Guid("497d6809-488d-4252-9cca-d04956baa87b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "IdCategory",
                keyValue: new Guid("4b94a8db-ac66-4a7f-9045-59672ff62e64"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "IdCategory",
                keyValue: new Guid("da7d35d3-735c-40f7-b16c-cb65c1ac0d3f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "IdCategory",
                keyValue: new Guid("e571e876-cce6-4cde-ade1-d36732f3761d"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "IdEmployee",
                keyValue: new Guid("caf7b5ff-56cf-40d7-93d2-685191c8774a"));

            migrationBuilder.DeleteData(
                table: "WareHouses",
                keyColumn: "IdEWareHouse",
                keyValue: new Guid("0c43ba79-f67a-46fa-a7ad-bef8dd1cff30"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "IdPost",
                keyValue: new Guid("07ceffea-914d-4509-b21d-e3a8042b6bf9"));

            migrationBuilder.DropColumn(
                name: "FileImageId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Employees");

            migrationBuilder.AddColumn<Guid>(
                name: "IdOrder",
                table: "OrderElements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "OrderElements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "OrderElements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "OrderNumber",
                table: "OrderElements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TotalOrderAmount",
                table: "OrderElements",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "orderStatus",
                table: "OrderElements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "paymentStatus",
                table: "OrderElements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "PasportSeries",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4);

            migrationBuilder.AlterColumn<string>(
                name: "PasportNumber",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderElements",
                table: "OrderElements",
                column: "IdOrder");

            migrationBuilder.CreateTable(
                name: "PostEmployees",
                columns: table => new
                {
                    IdPostEmployee = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostEmployees", x => x.IdPostEmployee);
                    table.ForeignKey(
                        name: "FK_PostEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "IdEmployee",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostEmployees_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "IdPost",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderElements_EmployeeId",
                table: "OrderElements",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PostEmployees_EmployeeId",
                table: "PostEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PostEmployees_PostId",
                table: "PostEmployees",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderElements_Employees_EmployeeId",
                table: "OrderElements",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "IdEmployee",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderElements_OrderElements_OrderId",
                table: "OrderElements",
                column: "OrderId",
                principalTable: "OrderElements",
                principalColumn: "IdOrder",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderElements_Products_ProductId",
                table: "OrderElements",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "IdProduct",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "IdCategory",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_WareHouses_WareHouseId",
                table: "Products",
                column: "WareHouseId",
                principalTable: "WareHouses",
                principalColumn: "IdEWareHouse",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
