using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeXiecheng.API.Migrations
{
    public partial class DataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TouristRoutes",
                columns: new[] { "Id", "CreateTime", "DepartureTime", "Description", "Discount", "Features", "Fees", "Notes", "OriginalPrice", "Title", "UpdateTime" },
                values: new object[] { new Guid("7de26989-724d-46ec-9079-bbc1f342b49d"), new DateTime(2021, 6, 7, 10, 29, 18, 170, DateTimeKind.Local).AddTicks(8736), null, "说明", null, null, null, null, 0m, "标题 ", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("7de26989-724d-46ec-9079-bbc1f342b49d"));
        }
    }
}
