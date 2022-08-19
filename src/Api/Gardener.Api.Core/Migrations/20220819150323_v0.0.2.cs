using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gardener.Api.Core.Migrations
{
    public partial class v002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "CreatorId", "CreatorIdentityType", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("547d94df-965f-4833-88fb-c3f66244926c"), 1306552940953600480L, "1", 1, "导出数据", true, "系统基础服务", false, false, "B0929232FD86E71F313EBB348D60B23A", 1, "/api/audit-entity/export", "审计数据服务", "导出", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "CreatorId", "CreatorIdentityType", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("5df6ccb0-8985-4767-9ff2-b306d791194e"), 1306552940953600480L, "1", 1, "导出数据", true, "系统基础服务", false, false, "0F54F5EA3FD76F2067A9C26F6A1D139B", 1, "/api/audit-operation/export", "审计操作服务", "导出", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "CreatorId", "CreatorIdentityType", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("b0b9900d-5ff7-4164-957a-9b8d55d5d5bf"), 1306552940666880480L, "1", 1, "导出数据", true, "用户中心服务", false, false, "BF0393C2E61B44E125040F8947AEFB23", 1, "/api/user/export", "用户服务", "导出", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "CreatorId", "CreatorIdentityType", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("cd1b93ed-2fae-47f2-83b2-e9b0a949f476"), 1306552940953600480L, "1", 1, "导出数据", true, "系统基础服务", false, false, "91D755377D8744A976836037290BB199", 1, "/api/login-token/export", "用户登录TOKEN服务", "导出", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "CreatorId", "CreatorIdentityType", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("dca2b115-3363-4f7f-8bba-b051b8d8603a"), 1306552940953600480L, "1", 1, "导出数据", true, "系统基础服务", false, false, "C159134CE2D63BC05680B2AD2BB86E7C", 1, "/api/function/export", "功能服务", "导出", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "CreatorId", "CreatorIdentityType", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("ee8b1345-e0c0-47a7-9e85-cb0bd7ede472"), 1306552940953600480L, "1", 1, "导出数据", true, "系统基础服务", false, false, "A1BCA09FC8FF82506B93BFD5FCD6FCF5", 1, "/api/sys-timer/export", "任务调度服务", "导出", null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "CreatorId", "CreatorIdentityType", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("2ac78309-1719-4ea5-ac0f-6974a86f168c"), 1306552959733760480L, "1", 1, null, false, false, "system_manager_audit_operation_export", "导出操作审计", 0, new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"), null, "导出操作审计", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "CreatorId", "CreatorIdentityType", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("32809cde-b1bb-4076-a37b-6c9375e84aac"), 1306552964710400480L, "1", 1, null, false, false, "system_manager_timer_export", "导出任务调度", 0, new Guid("3d93eb77-2a72-4b4f-aa79-5da1fc794300"), null, "导出任务调度", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "CreatorId", "CreatorIdentityType", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("4171f5aa-2ce1-40ad-b69e-59de1cd20416"), 1306552948224000480L, "1", 1, null, false, false, "system_manager_function_export", "导出接口", 0, new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), null, "导出接口", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "CreatorId", "CreatorIdentityType", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("4f259695-23ea-4453-a4f1-2b055d135c37"), 1306552960327680480L, "1", 1, null, false, false, "system_manager_audit_entity_export", "导出数据审计", 0, new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"), null, "导出数据审计", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "CreatorId", "CreatorIdentityType", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("a25da8f5-23d4-4118-b399-0a36f912a370"), 1306552952463360480L, "1", 1, null, false, false, "user_center_user_export", "导出用户", 0, new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), null, "导出用户", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "CreatorId", "CreatorIdentityType", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("bddc6ccc-3f93-4be7-8756-15613cdf76b6"), 1306552963031040480L, "1", 1, null, false, false, "system_manager_login_token_export", "导出登录数据", 0, new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"), null, "导出登录数据", 2000, null });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("5df6ccb0-8985-4767-9ff2-b306d791194e"), new Guid("2ac78309-1719-4ea5-ac0f-6974a86f168c"), 1306552961720320480L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("ee8b1345-e0c0-47a7-9e85-cb0bd7ede472"), new Guid("32809cde-b1bb-4076-a37b-6c9375e84aac"), 1306552965263360480L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("dca2b115-3363-4f7f-8bba-b051b8d8603a"), new Guid("4171f5aa-2ce1-40ad-b69e-59de1cd20416"), 1306552948920320480L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("547d94df-965f-4833-88fb-c3f66244926c"), new Guid("4f259695-23ea-4453-a4f1-2b055d135c37"), 1306552962355200480L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("b0b9900d-5ff7-4164-957a-9b8d55d5d5bf"), new Guid("a25da8f5-23d4-4118-b399-0a36f912a370"), 1306552953200640480L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("cd1b93ed-2fae-47f2-83b2-e9b0a949f476"), new Guid("bddc6ccc-3f93-4be7-8756-15613cdf76b6"), 1306552963686400480L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ResourceFunction",
                keyColumns: new[] { "FunctionId", "ResourceId" },
                keyValues: new object[] { new Guid("5df6ccb0-8985-4767-9ff2-b306d791194e"), new Guid("2ac78309-1719-4ea5-ac0f-6974a86f168c") });

            migrationBuilder.DeleteData(
                table: "ResourceFunction",
                keyColumns: new[] { "FunctionId", "ResourceId" },
                keyValues: new object[] { new Guid("ee8b1345-e0c0-47a7-9e85-cb0bd7ede472"), new Guid("32809cde-b1bb-4076-a37b-6c9375e84aac") });

            migrationBuilder.DeleteData(
                table: "ResourceFunction",
                keyColumns: new[] { "FunctionId", "ResourceId" },
                keyValues: new object[] { new Guid("dca2b115-3363-4f7f-8bba-b051b8d8603a"), new Guid("4171f5aa-2ce1-40ad-b69e-59de1cd20416") });

            migrationBuilder.DeleteData(
                table: "ResourceFunction",
                keyColumns: new[] { "FunctionId", "ResourceId" },
                keyValues: new object[] { new Guid("547d94df-965f-4833-88fb-c3f66244926c"), new Guid("4f259695-23ea-4453-a4f1-2b055d135c37") });

            migrationBuilder.DeleteData(
                table: "ResourceFunction",
                keyColumns: new[] { "FunctionId", "ResourceId" },
                keyValues: new object[] { new Guid("b0b9900d-5ff7-4164-957a-9b8d55d5d5bf"), new Guid("a25da8f5-23d4-4118-b399-0a36f912a370") });

            migrationBuilder.DeleteData(
                table: "ResourceFunction",
                keyColumns: new[] { "FunctionId", "ResourceId" },
                keyValues: new object[] { new Guid("cd1b93ed-2fae-47f2-83b2-e9b0a949f476"), new Guid("bddc6ccc-3f93-4be7-8756-15613cdf76b6") });

            migrationBuilder.DeleteData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("547d94df-965f-4833-88fb-c3f66244926c"));

            migrationBuilder.DeleteData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("5df6ccb0-8985-4767-9ff2-b306d791194e"));

            migrationBuilder.DeleteData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("b0b9900d-5ff7-4164-957a-9b8d55d5d5bf"));

            migrationBuilder.DeleteData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("cd1b93ed-2fae-47f2-83b2-e9b0a949f476"));

            migrationBuilder.DeleteData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("dca2b115-3363-4f7f-8bba-b051b8d8603a"));

            migrationBuilder.DeleteData(
                table: "Function",
                keyColumn: "Id",
                keyValue: new Guid("ee8b1345-e0c0-47a7-9e85-cb0bd7ede472"));

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: new Guid("2ac78309-1719-4ea5-ac0f-6974a86f168c"));

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: new Guid("32809cde-b1bb-4076-a37b-6c9375e84aac"));

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: new Guid("4171f5aa-2ce1-40ad-b69e-59de1cd20416"));

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: new Guid("4f259695-23ea-4453-a4f1-2b055d135c37"));

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: new Guid("a25da8f5-23d4-4118-b399-0a36f912a370"));

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: new Guid("bddc6ccc-3f93-4be7-8756-15613cdf76b6"));
        }
    }
}
