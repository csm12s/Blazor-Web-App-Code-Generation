using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gardener.Api.Core.Migrations
{
    public partial class v004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("1295aed2-ae71-411f-9542-d50f75432840"),
                column: "EnableAudit",
                value: false);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("25bad725-529b-4a67-814a-1a6171a4b6d1"),
                column: "EnableAudit",
                value: false);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("2bf3ff67-c1a3-4426-8320-11839daa0a81"),
                column: "EnableAudit",
                value: false);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("416fe54b-6c50-4b1b-bf77-6744cf19fa72"),
                column: "EnableAudit",
                value: false);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("498638f7-dc92-4d0e-ac5e-26e48cf87a8d"),
                column: "EnableAudit",
                value: false);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("6a9763c9-c40f-44f3-a248-a3b1e3d1f586"),
                column: "EnableAudit",
                value: false);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("7fa014c4-08db-4f96-8132-2bf3db32b256"),
                column: "EnableAudit",
                value: false);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("83cc7cb7-dac6-49f2-85fa-e903039f3d0a"),
                column: "EnableAudit",
                value: false);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("85f94b4c-e897-4f3c-b80a-c7ddb8ebf1b5"),
                column: "EnableAudit",
                value: false);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("9fe5cc45-a851-4d3f-8b44-32dd96130946"),
                column: "EnableAudit",
                value: false);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("c715a6d5-cd99-4c94-8760-936817c1e09c"),
                column: "EnableAudit",
                value: false);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("db76ae46-851b-47bc-94be-b2e869043636"),
                column: "EnableAudit",
                value: false);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("e651d9a4-9d6d-44c7-a833-08da6ed19892"),
                column: "EnableAudit",
                value: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("1295aed2-ae71-411f-9542-d50f75432840"),
                column: "EnableAudit",
                value: true);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("25bad725-529b-4a67-814a-1a6171a4b6d1"),
                column: "EnableAudit",
                value: true);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("2bf3ff67-c1a3-4426-8320-11839daa0a81"),
                column: "EnableAudit",
                value: true);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("416fe54b-6c50-4b1b-bf77-6744cf19fa72"),
                column: "EnableAudit",
                value: true);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("498638f7-dc92-4d0e-ac5e-26e48cf87a8d"),
                column: "EnableAudit",
                value: true);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("6a9763c9-c40f-44f3-a248-a3b1e3d1f586"),
                column: "EnableAudit",
                value: true);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("7fa014c4-08db-4f96-8132-2bf3db32b256"),
                column: "EnableAudit",
                value: true);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("83cc7cb7-dac6-49f2-85fa-e903039f3d0a"),
                column: "EnableAudit",
                value: true);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("85f94b4c-e897-4f3c-b80a-c7ddb8ebf1b5"),
                column: "EnableAudit",
                value: true);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("9fe5cc45-a851-4d3f-8b44-32dd96130946"),
                column: "EnableAudit",
                value: true);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("c715a6d5-cd99-4c94-8760-936817c1e09c"),
                column: "EnableAudit",
                value: true);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("db76ae46-851b-47bc-94be-b2e869043636"),
                column: "EnableAudit",
                value: true);

            migrationBuilder.UpdateData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("e651d9a4-9d6d-44c7-a833-08da6ed19892"),
                column: "EnableAudit",
                value: true);
        }
    }
}
