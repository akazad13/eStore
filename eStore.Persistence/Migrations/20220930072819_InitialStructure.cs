using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eStore.Persistence.Migrations
{
    public partial class InitialStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<long>(type: "bigint", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        UserName = table.Column<string>(
                            type: "nvarchar(50)",
                            maxLength: 50,
                            nullable: false
                        ),
                        FirstName = table.Column<string>(
                            type: "nvarchar(50)",
                            maxLength: 50,
                            nullable: false
                        ),
                        LastName = table.Column<string>(
                            type: "nvarchar(50)",
                            maxLength: 50,
                            nullable: false
                        ),
                        Email = table.Column<string>(
                            type: "nvarchar(50)",
                            maxLength: 50,
                            nullable: false
                        ),
                        EmailConfirmed = table.Column<bool>(
                            type: "bit",
                            maxLength: 50,
                            nullable: false
                        ),
                        PhoneNumber = table.Column<string>(
                            type: "nvarchar(20)",
                            maxLength: 20,
                            nullable: true
                        ),
                        Birthdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                        Gender = table.Column<byte>(type: "tinyint", nullable: false),
                        CreatedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: false
                        ),
                        CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                        ModifiedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: true
                        ),
                        ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                        Status = table.Column<int>(type: "int", nullable: false),
                        ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        IsSubscribeToNewsletter = table.Column<bool>(type: "bit", nullable: false),
                        NormalizedUserName = table.Column<string>(
                            type: "nvarchar(20)",
                            maxLength: 20,
                            nullable: true
                        ),
                        NormalizedEmail = table.Column<string>(
                            type: "nvarchar(50)",
                            maxLength: 50,
                            nullable: true
                        ),
                        PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        ConcurrencyStamp = table.Column<string>(
                            type: "nvarchar(max)",
                            nullable: true
                        ),
                        PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                        TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                        LockoutEnd = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: true
                        ),
                        LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                        AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Name = table.Column<string>(
                            type: "nvarchar(100)",
                            maxLength: 100,
                            nullable: false
                        ),
                        CreatedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: false
                        ),
                        CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                        ModifiedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: true
                        ),
                        ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                        Status = table.Column<int>(type: "int", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Name = table.Column<string>(
                            type: "nvarchar(50)",
                            maxLength: 50,
                            nullable: false
                        ),
                        CreatedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: false
                        ),
                        CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                        ModifiedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: true
                        ),
                        ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                        Status = table.Column<int>(type: "int", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<long>(type: "bigint", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Name = table.Column<string>(
                            type: "nvarchar(20)",
                            maxLength: 20,
                            nullable: true
                        ),
                        CreatedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: false
                        ),
                        CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                        ModifiedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: true
                        ),
                        ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                        NormalizedName = table.Column<string>(
                            type: "nvarchar(20)",
                            maxLength: 20,
                            nullable: true
                        ),
                        ConcurrencyStamp = table.Column<string>(
                            type: "nvarchar(max)",
                            nullable: true
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<long>(type: "bigint", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        Street2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        Zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        UserID = table.Column<long>(type: "bigint", nullable: false),
                        CreatedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: false
                        ),
                        CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                        ModifiedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: true
                        ),
                        ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                        Status = table.Column<int>(type: "int", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_AppUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<long>(type: "bigint", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        UserId = table.Column<long>(type: "bigint", nullable: false),
                        Subtotal = table.Column<decimal>(
                            type: "decimal(18,5)",
                            precision: 18,
                            scale: 5,
                            nullable: false
                        ),
                        Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        CreatedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: false
                        ),
                        CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                        ModifiedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: true
                        ),
                        ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                        Status = table.Column<int>(type: "int", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Baskets_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        UserId = table.Column<long>(type: "bigint", nullable: false),
                        ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table =>
                    new
                    {
                        LoginProvider = table.Column<string>(
                            type: "nvarchar(450)",
                            nullable: false
                        ),
                        ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                        ProviderDisplayName = table.Column<string>(
                            type: "nvarchar(max)",
                            nullable: true
                        ),
                        UserId = table.Column<long>(type: "bigint", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table =>
                    new
                    {
                        UserId = table.Column<long>(type: "bigint", nullable: false),
                        LoginProvider = table.Column<string>(
                            type: "nvarchar(450)",
                            nullable: false
                        ),
                        Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                        Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_UserTokens",
                        x => new { x.UserId, x.LoginProvider, x.Name }
                    );
                    table.ForeignKey(
                        name: "FK_UserTokens_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Name = table.Column<string>(
                            type: "nvarchar(200)",
                            maxLength: 200,
                            nullable: false
                        ),
                        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        Price = table.Column<decimal>(
                            type: "decimal(18,5)",
                            precision: 18,
                            scale: 5,
                            nullable: false
                        ),
                        Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        SKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        Stock = table.Column<int>(type: "int", nullable: false),
                        CategoryId = table.Column<int>(type: "int", nullable: false),
                        MaterialId = table.Column<int>(type: "int", nullable: false),
                        CreatedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: false
                        ),
                        CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                        ModifiedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: true
                        ),
                        ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                        Status = table.Column<int>(type: "int", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Products_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        RoleId = table.Column<long>(type: "bigint", nullable: false),
                        ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table =>
                    new
                    {
                        UserId = table.Column<long>(type: "bigint", nullable: false),
                        RoleId = table.Column<long>(type: "bigint", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<long>(type: "bigint", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        UserId = table.Column<long>(type: "bigint", nullable: false),
                        Amount = table.Column<decimal>(
                            type: "decimal(18,5)",
                            precision: 18,
                            scale: 5,
                            nullable: false
                        ),
                        ShippingAddress = table.Column<string>(
                            type: "nvarchar(max)",
                            nullable: false
                        ),
                        Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        AddressId = table.Column<long>(type: "bigint", nullable: true),
                        CreatedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: false
                        ),
                        CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                        ModifiedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: true
                        ),
                        ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                        Status = table.Column<int>(type: "int", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Orders_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<long>(type: "bigint", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        BasketId = table.Column<long>(type: "bigint", nullable: false),
                        ProductID = table.Column<int>(type: "int", nullable: false),
                        Quantity = table.Column<int>(type: "int", nullable: false),
                        Price = table.Column<decimal>(
                            type: "decimal(18,5)",
                            precision: 18,
                            scale: 5,
                            nullable: false
                        ),
                        UnitPrice = table.Column<decimal>(
                            type: "decimal(18,5)",
                            precision: 18,
                            scale: 5,
                            nullable: false
                        ),
                        CreatedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: false
                        ),
                        CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                        ModifiedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: true
                        ),
                        ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                        Status = table.Column<int>(type: "int", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItems_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_BasketItems_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "ProductComments",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<long>(type: "bigint", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        ProductId = table.Column<int>(type: "int", nullable: false),
                        UserId = table.Column<long>(type: "bigint", nullable: false),
                        Text = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        CreatedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: false
                        ),
                        CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                        ModifiedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: true
                        ),
                        ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                        Status = table.Column<int>(type: "int", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductComments_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_ProductComments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<long>(type: "bigint", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        IsThumbnail = table.Column<bool>(type: "bit", nullable: true),
                        ProductId = table.Column<int>(type: "int", nullable: false),
                        CreatedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: false
                        ),
                        CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                        ModifiedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: true
                        ),
                        ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                        Status = table.Column<int>(type: "int", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "ProductRatings",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<long>(type: "bigint", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        ProductId = table.Column<int>(type: "int", nullable: false),
                        UserId = table.Column<long>(type: "bigint", nullable: false),
                        Rating = table.Column<decimal>(
                            type: "decimal(18,5)",
                            precision: 18,
                            scale: 5,
                            nullable: false
                        ),
                        CreatedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: false
                        ),
                        CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                        ModifiedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: true
                        ),
                        ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                        Status = table.Column<int>(type: "int", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRatings_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_ProductRatings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<long>(type: "bigint", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        OrderId = table.Column<long>(type: "bigint", nullable: false),
                        ProductId = table.Column<int>(type: "int", nullable: false),
                        Price = table.Column<decimal>(
                            type: "decimal(18,5)",
                            precision: 18,
                            scale: 5,
                            nullable: false
                        ),
                        Quantity = table.Column<int>(type: "int", nullable: false),
                        CreatedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: false
                        ),
                        CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                        ModifiedOn = table.Column<DateTimeOffset>(
                            type: "datetimeoffset",
                            nullable: true
                        ),
                        ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                        Status = table.Column<int>(type: "int", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserID",
                table: "Addresses",
                column: "UserID"
            );

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AppUsers",
                column: "NormalizedEmail"
            );

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AppUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL"
            );

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketId",
                table: "BasketItems",
                column: "BasketId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_ProductID",
                table: "BasketItems",
                column: "ProductID"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_UserId",
                table: "Baskets",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressId",
                table: "Orders",
                column: "AddressId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_ProductId",
                table: "ProductComments",
                column: "ProductId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_UserId",
                table: "ProductComments",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatings_ProductId",
                table: "ProductRatings",
                column: "ProductId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatings_UserId",
                table: "ProductRatings",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Products_MaterialId",
                table: "Products",
                column: "MaterialId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId"
            );

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL"
            );

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_ProviderKey_LoginProvider",
                table: "UserLogins",
                columns: new[] { "ProviderKey", "LoginProvider" }
            );

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "BasketItems");

            migrationBuilder.DropTable(name: "OrderDetails");

            migrationBuilder.DropTable(name: "ProductComments");

            migrationBuilder.DropTable(name: "ProductImages");

            migrationBuilder.DropTable(name: "ProductRatings");

            migrationBuilder.DropTable(name: "RoleClaims");

            migrationBuilder.DropTable(name: "UserClaims");

            migrationBuilder.DropTable(name: "UserLogins");

            migrationBuilder.DropTable(name: "UserRoles");

            migrationBuilder.DropTable(name: "UserTokens");

            migrationBuilder.DropTable(name: "Baskets");

            migrationBuilder.DropTable(name: "Orders");

            migrationBuilder.DropTable(name: "Products");

            migrationBuilder.DropTable(name: "Roles");

            migrationBuilder.DropTable(name: "Addresses");

            migrationBuilder.DropTable(name: "Categories");

            migrationBuilder.DropTable(name: "Materials");

            migrationBuilder.DropTable(name: "AppUsers");
        }
    }
}
