using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Layer.Migrations
{
    public partial class book_schama_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BookImage",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOnDate", "ModifiedOnDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 24, 5, 26, 12, 209, DateTimeKind.Unspecified).AddTicks(8792), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 24, 5, 26, 12, 209, DateTimeKind.Unspecified).AddTicks(8793), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOnDate", "ModifiedOnDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 24, 5, 26, 12, 209, DateTimeKind.Unspecified).AddTicks(8799), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 24, 5, 26, 12, 209, DateTimeKind.Unspecified).AddTicks(8799), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOnDate", "ModifiedOnDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 24, 5, 26, 12, 209, DateTimeKind.Unspecified).AddTicks(8804), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 24, 5, 26, 12, 209, DateTimeKind.Unspecified).AddTicks(8804), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOnDate", "ModifiedOnDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 24, 5, 26, 12, 209, DateTimeKind.Unspecified).AddTicks(8809), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 24, 5, 26, 12, 209, DateTimeKind.Unspecified).AddTicks(8809), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BookImage",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOnDate", "ModifiedOnDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 24, 5, 9, 44, 472, DateTimeKind.Unspecified).AddTicks(7487), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 24, 5, 9, 44, 472, DateTimeKind.Unspecified).AddTicks(7487), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOnDate", "ModifiedOnDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 24, 5, 9, 44, 472, DateTimeKind.Unspecified).AddTicks(7492), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 24, 5, 9, 44, 472, DateTimeKind.Unspecified).AddTicks(7493), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOnDate", "ModifiedOnDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 24, 5, 9, 44, 472, DateTimeKind.Unspecified).AddTicks(7496), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 24, 5, 9, 44, 472, DateTimeKind.Unspecified).AddTicks(7497), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOnDate", "ModifiedOnDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 24, 5, 9, 44, 472, DateTimeKind.Unspecified).AddTicks(7500), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 24, 5, 9, 44, 472, DateTimeKind.Unspecified).AddTicks(7500), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
