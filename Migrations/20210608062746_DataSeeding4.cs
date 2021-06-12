using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeXiecheng.API.Migrations
{
    public partial class DataSeeding4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("2430bf64-fd56-460c-8b75-da0a1d1cd74c"),
                column: "Discount",
                value: 0.10000000000000001);

            migrationBuilder.UpdateData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("3ecbcd92-a9e0-45f7-9b29-e03272cb0862"),
                column: "Discount",
                value: 0.10000000000000001);

            migrationBuilder.UpdateData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("88cf89b9-e4b5-4b42-a5bf-622bd3039601"),
                column: "Discount",
                value: 0.5);

            migrationBuilder.UpdateData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("a1fd0bee-0afc-4586-96c8-f46b7c99d2a0"),
                column: "Discount",
                value: 0.10000000000000001);

            migrationBuilder.UpdateData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e1"),
                column: "Discount",
                value: 0.10000000000000001);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("2430bf64-fd56-460c-8b75-da0a1d1cd74c"),
                column: "Discount",
                value: null);

            migrationBuilder.UpdateData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("3ecbcd92-a9e0-45f7-9b29-e03272cb0862"),
                column: "Discount",
                value: null);

            migrationBuilder.UpdateData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("88cf89b9-e4b5-4b42-a5bf-622bd3039601"),
                column: "Discount",
                value: null);

            migrationBuilder.UpdateData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("a1fd0bee-0afc-4586-96c8-f46b7c99d2a0"),
                column: "Discount",
                value: null);

            migrationBuilder.UpdateData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e1"),
                column: "Discount",
                value: null);
        }
    }
}
