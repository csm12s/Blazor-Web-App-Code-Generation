using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gardener.Api.Core.Migrations
{
    public partial class v003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ResourceFunction",
                keyColumns: new[] { "FunctionId", "ResourceId" },
                keyValues: new object[] { new Guid("a96bb19e-794e-4fe0-ad39-f423df44f633"), new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf") });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "CreatorId", "CreatorIdentityType", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("6e2ff941-1e79-4e2a-bbe9-ec68318e5d3a"), 1306582968770560480L, "1", 1, null, false, "用户中心服务", false, false, "98B6A43EC42A6B9F1D8257EE1D05E9BB", 0, "/api/dept/tree/{includelocked}", "部门服务", "获取所有部门数据，以树形结构返回", null });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("03ee6f4b-dfea-4803-9515-3a9b2f907c90"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1306582939402240480L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("1c6dfb26-4149-4fa3-a7de-083ad7ff7d6c"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1306582939402240480L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("38545a67-61ff-4e5c-90bb-a555a93fcbea"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1306582939402240480L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("68ce42ff-acc7-485f-bc91-df471b520be7"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1306582939402240480L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("713341f2-47e1-42af-b717-bfa75904d32e"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1306582939402240480L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("89a06a4e-1a8e-41aa-a443-fd11bcc8497d"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1306582939402240480L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("c96dd7f7-f935-4499-8ef5-6d39fe26141a"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1306582939402240480L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("e2bb65e0-5d9e-485e-9059-8148fc236246"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1306582939402240480L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("6e2ff941-1e79-4e2a-bbe9-ec68318e5d3a"), new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), 1306582975447040480L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ResourceFunction",
                keyColumns: new[] { "FunctionId", "ResourceId" },
                keyValues: new object[] { new Guid("6e2ff941-1e79-4e2a-bbe9-ec68318e5d3a"), new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf") });

            migrationBuilder.DeleteData(
                table: "ResourceFunction",
                keyColumns: new[] { "FunctionId", "ResourceId" },
                keyValues: new object[] { new Guid("03ee6f4b-dfea-4803-9515-3a9b2f907c90"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74") });

            migrationBuilder.DeleteData(
                table: "ResourceFunction",
                keyColumns: new[] { "FunctionId", "ResourceId" },
                keyValues: new object[] { new Guid("1c6dfb26-4149-4fa3-a7de-083ad7ff7d6c"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74") });

            migrationBuilder.DeleteData(
                table: "ResourceFunction",
                keyColumns: new[] { "FunctionId", "ResourceId" },
                keyValues: new object[] { new Guid("38545a67-61ff-4e5c-90bb-a555a93fcbea"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74") });

            migrationBuilder.DeleteData(
                table: "ResourceFunction",
                keyColumns: new[] { "FunctionId", "ResourceId" },
                keyValues: new object[] { new Guid("68ce42ff-acc7-485f-bc91-df471b520be7"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74") });

            migrationBuilder.DeleteData(
                table: "ResourceFunction",
                keyColumns: new[] { "FunctionId", "ResourceId" },
                keyValues: new object[] { new Guid("713341f2-47e1-42af-b717-bfa75904d32e"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74") });

            migrationBuilder.DeleteData(
                table: "ResourceFunction",
                keyColumns: new[] { "FunctionId", "ResourceId" },
                keyValues: new object[] { new Guid("89a06a4e-1a8e-41aa-a443-fd11bcc8497d"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74") });

            migrationBuilder.DeleteData(
                table: "ResourceFunction",
                keyColumns: new[] { "FunctionId", "ResourceId" },
                keyValues: new object[] { new Guid("c96dd7f7-f935-4499-8ef5-6d39fe26141a"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74") });

            migrationBuilder.DeleteData(
                table: "ResourceFunction",
                keyColumns: new[] { "FunctionId", "ResourceId" },
                keyValues: new object[] { new Guid("e2bb65e0-5d9e-485e-9059-8148fc236246"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74") });

            migrationBuilder.DeleteData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("6e2ff941-1e79-4e2a-bbe9-ec68318e5d3a"));

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("a96bb19e-794e-4fe0-ad39-f423df44f633"), new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), 1306069130997760480L });
        }
    }
}
