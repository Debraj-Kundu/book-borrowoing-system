using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Layer.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tokens_Available = table.Column<int>(type: "int", nullable: false),
                    CreatedOnDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Is_Book_Available = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lent_By_User_id = table.Column<int>(type: "int", nullable: true),
                    Currently_Borrowed_By_User_Id = table.Column<int>(type: "int", nullable: true),
                    BookImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOnDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Users_Currently_Borrowed_By_User_Id",
                        column: x => x.Currently_Borrowed_By_User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_Users_Lent_By_User_id",
                        column: x => x.Lent_By_User_id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOnDate", "IsDeleted", "ModifiedOnDate", "Name", "Password", "Tokens_Available", "Username" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2023, 11, 24, 5, 9, 44, 472, DateTimeKind.Unspecified).AddTicks(7487), new TimeSpan(0, 0, 0, 0, 0)), false, new DateTimeOffset(new DateTime(2023, 11, 24, 5, 9, 44, 472, DateTimeKind.Unspecified).AddTicks(7487), new TimeSpan(0, 0, 0, 0, 0)), "Userone", "VXNlckAxMjMjc2VjcmV0QHBhc3N3b3JkITFoYXNoaW5nX2tleSQ=", 1, "User1" },
                    { 2, new DateTimeOffset(new DateTime(2023, 11, 24, 5, 9, 44, 472, DateTimeKind.Unspecified).AddTicks(7492), new TimeSpan(0, 0, 0, 0, 0)), false, new DateTimeOffset(new DateTime(2023, 11, 24, 5, 9, 44, 472, DateTimeKind.Unspecified).AddTicks(7493), new TimeSpan(0, 0, 0, 0, 0)), "Usertwo", "VXNlckAxMjMjc2VjcmV0QHBhc3N3b3JkITFoYXNoaW5nX2tleSQ=", 1, "User2" },
                    { 3, new DateTimeOffset(new DateTime(2023, 11, 24, 5, 9, 44, 472, DateTimeKind.Unspecified).AddTicks(7496), new TimeSpan(0, 0, 0, 0, 0)), false, new DateTimeOffset(new DateTime(2023, 11, 24, 5, 9, 44, 472, DateTimeKind.Unspecified).AddTicks(7497), new TimeSpan(0, 0, 0, 0, 0)), "Userthree", "VXNlckAxMjMjc2VjcmV0QHBhc3N3b3JkITFoYXNoaW5nX2tleSQ=", 1, "User3" },
                    { 4, new DateTimeOffset(new DateTime(2023, 11, 24, 5, 9, 44, 472, DateTimeKind.Unspecified).AddTicks(7500), new TimeSpan(0, 0, 0, 0, 0)), false, new DateTimeOffset(new DateTime(2023, 11, 24, 5, 9, 44, 472, DateTimeKind.Unspecified).AddTicks(7500), new TimeSpan(0, 0, 0, 0, 0)), "Userfour", "VXNlckAxMjMjc2VjcmV0QHBhc3N3b3JkITFoYXNoaW5nX2tleSQ=", 1, "User4" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_Currently_Borrowed_By_User_Id",
                table: "Books",
                column: "Currently_Borrowed_By_User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Lent_By_User_id",
                table: "Books",
                column: "Lent_By_User_id");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Name",
                table: "Books",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
