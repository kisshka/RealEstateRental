using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Real_Estate_Rental_Practic.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estate_agency",
                columns: table => new
                {
                    ID_Estate_agency = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Agency_name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Phone_number = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    Director_surname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Director_name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Director_patronymic = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Town = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Home = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Flat = table.Column<int>(type: "int", nullable: false),
                    Comission = table.Column<decimal>(type: "decimal(2,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estate_Agency", x => x.ID_Estate_agency);
                });

            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    ID_Owner = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Phone_number = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    Town = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Home = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Flat = table.Column<int>(type: "int", nullable: false),
                    Email_address = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.ID_Owner);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID_User = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Phone_number = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    Email_address = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID_User);
                });

            migrationBuilder.CreateTable(
                name: "Realtor",
                columns: table => new
                {
                    ID_Realtor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Phone_number = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    Email_address = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    ID_Estate_agency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Realtor", x => x.ID_Realtor);
                    table.ForeignKey(
                        name: "FK_Realtor_Estate_Agency",
                        column: x => x.ID_Estate_agency,
                        principalTable: "Estate_agency",
                        principalColumn: "ID_Estate_agency",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estate_object",
                columns: table => new
                {
                    ID_Estate_object = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Object_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Town = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Home = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Flat = table.Column<int>(type: "int", nullable: true),
                    Citty_area = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Number_of_rooms = table.Column<int>(type: "int", nullable: false),
                    Square = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Rental_period = table.Column<int>(type: "int", nullable: false),
                    ID_Owner = table.Column<int>(type: "int", nullable: false),
                    Children_allowed = table.Column<bool>(type: "bit", nullable: false),
                    Animals_allowed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estate_object", x => x.ID_Estate_object);
                    table.ForeignKey(
                        name: "FK_Estate_object_Owner",
                        column: x => x.ID_Owner,
                        principalTable: "Owner",
                        principalColumn: "ID_Owner",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Owner_Estate_agency",
                columns: table => new
                {
                    ID_Owner_Estate_agency = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Owner = table.Column<int>(type: "int", nullable: false),
                    ID_Estate_agency = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner_Estate_agency", x => x.ID_Owner_Estate_agency);
                    table.ForeignKey(
                        name: "FK_Owner_Estate_agency_Estate_Agency",
                        column: x => x.ID_Estate_agency,
                        principalTable: "Estate_agency",
                        principalColumn: "ID_Estate_agency",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Owner_Estate_agency_Owner",
                        column: x => x.ID_Owner,
                        principalTable: "Owner",
                        principalColumn: "ID_Owner",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estate_rental",
                columns: table => new
                {
                    ID_Estate_rental = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rent_beginning = table.Column<DateOnly>(type: "date", nullable: false),
                    Rent_ending = table.Column<DateOnly>(type: "date", nullable: true),
                    Cost_per_month = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Deposit = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ID_Estate_object = table.Column<int>(type: "int", nullable: false),
                    ID_Realtor = table.Column<int>(type: "int", nullable: true),
                    ID_User = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estate_rental", x => x.ID_Estate_rental);
                    table.ForeignKey(
                        name: "FK_Estate_rental_Estate_object",
                        column: x => x.ID_Estate_object,
                        principalTable: "Estate_object",
                        principalColumn: "ID_Estate_object");
                    table.ForeignKey(
                        name: "FK_Estate_rental_Realtor",
                        column: x => x.ID_Realtor,
                        principalTable: "Realtor",
                        principalColumn: "ID_Realtor");
                    table.ForeignKey(
                        name: "FK_Estate_rental_User",
                        column: x => x.ID_User,
                        principalTable: "User",
                        principalColumn: "ID_User");
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_area",
                table: "Estate_object",
                column: "Citty_area");

            migrationBuilder.CreateIndex(
                name: "IX_Estate_object_ID_Owner",
                table: "Estate_object",
                column: "ID_Owner");

            migrationBuilder.CreateIndex(
                name: "IX_Number_of_rooms",
                table: "Estate_object",
                column: "Number_of_rooms");

            migrationBuilder.CreateIndex(
                name: "IX_Object_type",
                table: "Estate_object",
                column: "Object_type");

            migrationBuilder.CreateIndex(
                name: "IX_Estate_rental_ID_Estate_object",
                table: "Estate_rental",
                column: "ID_Estate_object");

            migrationBuilder.CreateIndex(
                name: "IX_Estate_rental_ID_Realtor",
                table: "Estate_rental",
                column: "ID_Realtor");

            migrationBuilder.CreateIndex(
                name: "IX_Estate_rental_ID_User",
                table: "Estate_rental",
                column: "ID_User");

            migrationBuilder.CreateIndex(
                name: "IX_Owner_Estate_agency_ID_Estate_agency",
                table: "Owner_Estate_agency",
                column: "ID_Estate_agency");

            migrationBuilder.CreateIndex(
                name: "IX_Owner_Estate_agency_ID_Owner",
                table: "Owner_Estate_agency",
                column: "ID_Owner");

            migrationBuilder.CreateIndex(
                name: "IX_Realtor_ID_Estate_agency",
                table: "Realtor",
                column: "ID_Estate_agency");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estate_rental");

            migrationBuilder.DropTable(
                name: "Owner_Estate_agency");

            migrationBuilder.DropTable(
                name: "Estate_object");

            migrationBuilder.DropTable(
                name: "Realtor");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Owner");

            migrationBuilder.DropTable(
                name: "Estate_agency");
        }
    }
}
