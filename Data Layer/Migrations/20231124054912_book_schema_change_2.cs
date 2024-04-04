using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Layer.Migrations
{
    public partial class book_schema_change_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Lent_By_User_id",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOnDate", "ModifiedOnDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 24, 5, 49, 12, 638, DateTimeKind.Unspecified).AddTicks(2237), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 24, 5, 49, 12, 638, DateTimeKind.Unspecified).AddTicks(2237), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOnDate", "ModifiedOnDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 24, 5, 49, 12, 638, DateTimeKind.Unspecified).AddTicks(2244), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 24, 5, 49, 12, 638, DateTimeKind.Unspecified).AddTicks(2245), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOnDate", "ModifiedOnDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 24, 5, 49, 12, 638, DateTimeKind.Unspecified).AddTicks(2249), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 24, 5, 49, 12, 638, DateTimeKind.Unspecified).AddTicks(2249), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOnDate", "ModifiedOnDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 24, 5, 49, 12, 638, DateTimeKind.Unspecified).AddTicks(2253), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 24, 5, 49, 12, 638, DateTimeKind.Unspecified).AddTicks(2254), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Lent_By_User_id",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
    }
}
