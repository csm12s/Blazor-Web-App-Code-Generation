using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardener.Api.Core.Migrations
{
    public partial class v101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    BusinessId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    BusinessType = table.Column<int>(type: "INTEGER", nullable: false),
                    FileType = table.Column<int>(type: "INTEGER", nullable: false),
                    ContentType = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Size = table.Column<long>(type: "INTEGER", nullable: false),
                    Path = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    OriginalName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Url = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Suffix = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditOperation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ResourceName = table.Column<string>(type: "TEXT", nullable: true),
                    ResourceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OperaterId = table.Column<string>(type: "TEXT", nullable: true),
                    OperaterName = table.Column<string>(type: "TEXT", nullable: true),
                    Ip = table.Column<string>(type: "TEXT", nullable: true),
                    UserAgent = table.Column<string>(type: "TEXT", nullable: true),
                    Path = table.Column<string>(type: "TEXT", nullable: true),
                    Method = table.Column<int>(type: "INTEGER", nullable: false),
                    Parameters = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditOperation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Remark = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Contacts = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Tel = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    EncryptKey = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dept",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Contacts = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Tel = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dept", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dept_Dept_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Dept",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntityCodeGenerationSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EntityFullName = table.Column<string>(type: "TEXT", nullable: false),
                    ControllerRoute = table.Column<string>(type: "TEXT", nullable: true),
                    ControllerGroup = table.Column<string>(type: "TEXT", nullable: true),
                    ModuleName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityCodeGenerationSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Function",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Group = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Service = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Summary = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Key = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Path = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Method = table.Column<int>(type: "INTEGER", nullable: false),
                    EnableAudit = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Function", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdentityId = table.Column<string>(type: "TEXT", nullable: true),
                    IdentityName = table.Column<string>(type: "TEXT", nullable: true),
                    IdentityGivenName = table.Column<string>(type: "TEXT", nullable: true),
                    IdentityType = table.Column<int>(type: "INTEGER", nullable: false),
                    ClientId = table.Column<string>(type: "TEXT", nullable: true),
                    LoginClientType = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    EndTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Target = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Duty = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Right = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Grade = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Salary = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Qualifications = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Key = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Remark = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Path = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Icon = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    ParentId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resource_Resource_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Remark = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    IsSuperAdministrator = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VerifyCode",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Key = table.Column<string>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    EndTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerifyCode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataId = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    TypeName = table.Column<string>(type: "TEXT", nullable: true),
                    OperationType = table.Column<int>(type: "INTEGER", nullable: false),
                    OperaterId = table.Column<string>(type: "TEXT", nullable: true),
                    OperaterName = table.Column<string>(type: "TEXT", nullable: true),
                    OperationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AuditOperationId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditEntity_AuditOperation_AuditOperationId",
                        column: x => x.AuditOperationId,
                        principalTable: "AuditOperation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientFunction",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FunctionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientFunction", x => new { x.ClientId, x.FunctionId });
                    table.ForeignKey(
                        name: "FK_ClientFunction_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientFunction_Function_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "Function",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    NickName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    PasswordEncryptKey = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Avatar = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    DeptId = table.Column<int>(type: "INTEGER", nullable: true),
                    PositionId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Dept_DeptId",
                        column: x => x.DeptId,
                        principalTable: "Dept",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Position_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResourceFunction",
                columns: table => new
                {
                    ResourceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FunctionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceFunction", x => new { x.ResourceId, x.FunctionId });
                    table.ForeignKey(
                        name: "FK_ResourceFunction_Function_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "Function",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceFunction_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleResource",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    ResourceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleResource", x => new { x.RoleId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_RoleResource_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleResource_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditProperty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    FieldName = table.Column<string>(type: "TEXT", nullable: true),
                    OriginalValue = table.Column<string>(type: "TEXT", nullable: true),
                    NewValue = table.Column<string>(type: "TEXT", nullable: true),
                    DataType = table.Column<string>(type: "TEXT", nullable: true),
                    AuditEntityid = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditProperty_AuditEntity_AuditEntityid",
                        column: x => x.AuditEntityid,
                        principalTable: "AuditEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserExtension",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    QQ = table.Column<string>(type: "TEXT", nullable: true),
                    WeChat = table.Column<string>(type: "TEXT", nullable: true),
                    CityId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserExtension_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "Id", "Contacts", "CreatedTime", "Email", "EncryptKey", "IsDeleted", "IsLocked", "Name", "Remark", "Tel", "UpdatedTime" },
                values: new object[] { new Guid("96c0eec0-861f-4ed2-a183-5604b20bdff9"), "园丁", 1305892579553280000L, "qq@qq.com", "9f700cec-b787-4e23-a2da-9e45b3bd6cbb", false, false, "测试client1", "用于测试", "13838888888", null });

            migrationBuilder.InsertData(
                table: "Dept",
                columns: new[] { "Id", "Contacts", "CreatedTime", "IsDeleted", "IsLocked", "Name", "Order", "ParentId", "Remark", "Tel", "UpdatedTime" },
                values: new object[] { 1, "老A", 1305892579553280000L, false, false, "北京分部", 1, null, "北京分部", "400-8888888", null });

            migrationBuilder.InsertData(
                table: "Dept",
                columns: new[] { "Id", "Contacts", "CreatedTime", "IsDeleted", "IsLocked", "Name", "Order", "ParentId", "Remark", "Tel", "UpdatedTime" },
                values: new object[] { 4, "老D", 1305892579553280000L, false, false, "河北分部", 1, null, "河北分部", "400-8888888", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("08d002b9-d320-4410-b9f3-7986ed87ece4"), 1305892581478400000L, "根据主键查找一条数据", false, "系统管理", false, false, "DA5651C09F319A1358B9948735712DCF", 0, "/api/audit-operation/{id}", "审计操作服务", "根据主键获取", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("ffbd98b8-8945-4068-b70c-ea58b487bd25"), 1305892581478400000L, "根据主键删除一条数据", true, "系统管理", false, false, "AD48018AF04E0A4573815675E555E98D", 3, "/api/audit-operation/{id}", "审计操作服务", "删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("2f820c7f-4f1c-4737-aae6-329585c75d92"), 1305892581478400000L, "根据主键查找一条数据", false, "系统管理", false, false, "271DFDC5E142CFE1AF0C4200C6DC060A", 0, "/api/attachment/{id}", "附件服务", "根据主键获取", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("7cb8921d-0a0c-4e80-8895-604c05480c43"), 1305892581478400000L, "根据分页参数，分页获取数据", false, "系统管理", false, false, "0BBAF9866F200FEDE526AB75E03319CC", 0, "/api/dept/page/{pageindex}/{pagesize}", "部门服务", "分页查询", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("0d2df690-6aa7-466b-b1e4-73fa4fda1b5d"), 1305892581478400000L, "根据主键锁定或解锁数据", true, "系统管理", false, false, "8AED5C0B53588415D98E97119880AC6A", 2, "/api/dept/{id}/lock/{islocked}", "部门服务", "锁定", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("0ac7b7a5-1ca7-4345-b0cd-a328aaa76723"), 1305892581478400000L, "查找到所有数据", false, "新闻服务", false, false, "DA5FD7BA5C124E41186D976D87084C19", 0, "/api/news/all", "新闻管理", "查询所有", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("1562071d-e18c-4d29-a854-12a562961140"), 1305892581478400000L, "查找到所有数据", false, "系统管理", false, false, "9B0AD48E75A6C37EDC7101236F93CF77", 0, "/api/user/all", "用户服务", "查询所有", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("10190ac3-1092-49a9-8ad2-313454b40447"), 1305892581478400000L, "根据多个主键批量逻辑删除", true, "系统管理", false, false, "17B55B877E0FB6704577EA356573BBC3", 1, "/api/attachment/fake-deletes", "附件服务", "批量逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("a3ea9c9f-da6f-48e1-8255-d250bb3e52d5"), 1305892581478400000L, "查找到所有数据", false, "系统管理", false, false, "3CBCB4608120758739D941BFCCC09C18", 0, "/api/attachment/all", "附件服务", "查询所有", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("aedc9e9c-f011-4d46-966e-3b14fd5298c2"), 1305892581478400000L, "根据分页参数，分页获取数据", false, "系统管理", false, false, "9BE552AEF35878A71ABE8179B80AA036", 0, "/api/attachment/page/{pageindex}/{pagesize}", "附件服务", "分页查询", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("5604fcc2-595f-4cc5-b0b8-c0d75a4c9351"), 1305892581478400000L, "根据主键锁定或解锁数据", true, "系统管理", false, false, "ADF53A6D1C062BF2CC40EBDE20D8E841", 2, "/api/attachment/{id}/lock/{islocked}", "附件服务", "锁定", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("8f1c2eeb-248f-41bb-a083-511664f2fd8e"), 1305892581478400000L, "根据多个主键批量删除", true, "系统管理", false, false, "717D6057E652BA28D3BF0CE337180E9E", 1, "/api/function/deletes", "功能服务", "批量删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("03c9956e-b832-4202-9c47-55ba3793f606"), 1305892581478400000L, "根据分页参数，分页获取数据", false, "系统管理", false, false, "778BD549C3ACEF321ECEDF39C80241D0", 0, "/api/audit-operation/page/{pageindex}/{pagesize}", "审计操作服务", "分页查询", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("46aef5bc-9d0f-4a05-b21d-747753b98569"), 1305892581478400000L, "根据主键逻辑删除", true, "系统管理", false, false, "30759F98C0CF4A34813C280451C2E4CF", 3, "/api/audit-entity/fake-delete/{id}", "审计数据服务", "逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("73cfe63f-3338-4bd0-a0b9-1b9cc39951ea"), 1305892581478400000L, "根据多个主键批量逻辑删除", true, "系统管理", false, false, "C53E746377386D224D0941DB8F4CB539", 1, "/api/audit-operation/fake-deletes", "审计操作服务", "批量逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("9513e5e1-37ab-4937-94f1-1f6b99a385f7"), 1305892581478400000L, "添加一条数据", true, "系统管理", false, false, "07BC6868FFAD4A5B26193E2372B9821C", 1, "/api/audit-operation", "审计操作服务", "添加", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("a8211f75-bf19-459a-bf66-9c31c6f334aa"), 1305892581478400000L, "根据多个主键批量删除", true, "系统管理", false, false, "1C8C95EA831A3D031460A1390DF26E83", 1, "/api/audit-operation/deletes", "审计操作服务", "批量删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("fe3c8d2c-02ce-4073-a2b5-0b05168e7fc9"), 1305892581478400000L, "登录接口", true, "系统管理", false, false, "BBDF1AED32167D0970DA83229C7A8A03", 1, "/api/authorize/login", "权限认证服务", "登录", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("9d26c715-9b8b-40c6-bbf4-9c51df1193da"), 1305892581478400000L, "更新一条数据", true, "系统管理", false, false, "DE9B14A3BC0E0653399F870F27F24CEF", 2, "/api/audit-entity", "审计数据服务", "更新", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("cd7db809-50f5-4bf3-a464-89218e24077f"), 1305892581478400000L, "获取角色所有资源", false, "系统管理", false, false, "011A2E3F574F9C151E044EFA80A05F29", 0, "/api/role/{roleid}/resource", "角色服务", "获取角色所有资源", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("aeb8b23d-4da3-4ec0-867f-70d2e2ba9550"), 1305892581478400000L, "更新一条数据", true, "系统管理", false, false, "E2248234B183BA3EEA82273CB03F500C", 2, "/api/function", "功能服务", "更新", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("2a670df1-f01c-4cdb-b084-a46fdb339ced"), 1305892581478400000L, "根据主键删除一条数据", true, "系统管理", false, false, "097D7A323BFFCA32788EAA8C6BDB5157", 3, "/api/function/{id}", "功能服务", "删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("7e5577d4-32b2-4f43-a83f-05410b59b195"), 1305892581478400000L, "根据主键删除一条数据", true, "系统管理", false, false, "CCD570BA5C66619052354D738927A007", 3, "/api/audit-entity/{id}", "审计数据服务", "删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("99264e5a-76d3-4f92-a56a-9c8711067218"), 1305892581478400000L, "搜索角色数据", false, "系统管理", false, false, "0E961600B0A0BFF9928C779B1C49389D", 0, "/api/role/search/{pageindex}/{pagesize}", "角色服务", "搜索", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("1d994e50-d40a-465b-8445-646041a8131a"), 1305892581478400000L, "根据操作审计ID获取数据审计", false, "系统管理", false, false, "26806445F59D861F9FDB9F91B164A1CD", 0, "/api/audit-operation/{operationid}/audit-entity", "审计操作服务", "根据操作审计ID获取数据审计", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("a75bd9a7-e3f0-4736-9c27-8763a3d3768b"), 1305892581478400000L, "更新一条数据", true, "系统管理", false, false, "7037CCD6F97FA35692ED560CE1756F86", 2, "/api/audit-operation", "审计操作服务", "更新", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("a15ce231-80ae-46c6-ada8-49666e81e328"), 1305892581478400000L, "根据 HttpMethod 和 path 判断是否存在", false, "系统管理", false, false, "27693C4354A64289D9A1D3EB50E68E7E", 0, "/api/function/exists/{method}/{path}", "功能服务", "判断是否存在", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("416fe54b-6c50-4b1b-bf77-6744cf19fa72"), 1305892581478400000L, "搜索功能数据", false, "系统管理", false, false, "9A316E9E6A41D1F57870A5F0CDDC93EF", 1, "/api/function/search", "功能服务", "搜索", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("056ff2f6-009b-40ff-a1b9-a6983e471967"), 1305892581478400000L, "启用或禁用审计", true, "系统管理", false, false, "1DC1B5ECD34759A80CE8C468366A378F", 2, "/api/function/{id}/enable-audit/{enableaudit}", "功能服务", "启用或禁用审计", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("eefdb20f-b508-415a-b798-1aa9420a5b62"), 1305892581478400000L, "根据主键锁定或解锁数据", true, "系统管理", false, false, "6999F8BBB5F9BA97658BB99113A381F5", 2, "/api/audit-operation/{id}/lock/{islocked}", "审计操作服务", "锁定", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("7bb514a5-d62d-4ba1-a9b9-9e7756eaae2d"), 1305892581478400000L, "查找到所有数据", false, "系统管理", false, false, "D403D5F25D7ACA97A10BEF07B2A816F4", 0, "/api/audit-operation/all", "审计操作服务", "查询所有", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("0367ad11-0be0-48dd-a5a9-1d473b78c0bf"), 1305892581478400000L, "上传单个附件", true, "系统管理", false, false, "3BF647BFC6987B8CEA91C97FEE17CC6D", 1, "/api/attachment/upload", "附件服务", "上传附件", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("c39030b8-d207-4c22-a3ba-74b0eccaa2fa"), 1305892581478400000L, "根据多个主键批量逻辑删除", true, "系统管理", false, false, "4603BCE62CA130E67C2450C127DD7728", 1, "/api/function/fake-deletes", "功能服务", "批量逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("590cd04c-025c-4cc1-bdd1-e9cea201bb46"), 1305892581478400000L, "根据分页参数，分页获取数据", false, "系统管理", false, false, "67C405B4CBCC02144945800F26CC1F4F", 0, "/api/function/page/{pageindex}/{pagesize}", "功能服务", "分页查询", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("8ae9c253-584e-46e4-b805-6ec90281d6dd"), 1305892581478400000L, "根据主键锁定或解锁数据", true, "系统管理", false, false, "E7F5596D4D8517C85871566D8EFA0855", 2, "/api/function/{id}/lock/{islocked}", "功能服务", "锁定", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("475207d6-4c0b-4054-a051-7315295694a1"), 1305892581478400000L, "添加一条数据", true, "系统管理", false, false, "44405A33B9DEC6F934920AF5AC6F7111", 1, "/api/audit-entity", "审计数据服务", "添加", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("622c1a11-7dff-4318-9d21-b57fbd1da9ba"), 1305892581478400000L, "根据主键锁定或解锁数据", true, "系统管理", false, false, "718DFD76BA4C2997D3DDA216BDB98369", 2, "/api/user/{id}/lock/{islocked}", "用户服务", "锁定", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("4b57474a-88b4-4393-bb49-4b59e8c3c41d"), 1305892581478400000L, "根据主键逻辑删除", true, "系统管理", false, false, "A401312992835BA902C0CFDC5FEEE1F3", 3, "/api/function/fake-delete/{id}", "功能服务", "逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("a96bb19e-794e-4fe0-ad39-f423df44f633"), 1305892581478400000L, "", false, "系统管理", false, false, "6A85EF9D6FBD3B330E1827AB0949D7E4", 0, "/api/dept/tree", "部门服务", "获取所有部门数据，以树形结构返回", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("973edc2c-42e1-473e-9656-a43890663d8a"), 1305892581478400000L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统管理", false, false, "3D033D8178E68247D2C34E53F00D468F", 0, "/api/dept/all-usable", "部门服务", "查询所有可以用的", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("84256e5b-2cef-4b16-8fd3-79ff8d47c731"), 1305892581478400000L, "添加一条数据", true, "系统管理", false, false, "3F9869D1A16CD359E268F2C2DBEFD0E2", 1, "/api/function", "功能服务", "添加", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("7229563b-7311-41b8-947b-f07d58fa6c87"), 1305892581478400000L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统管理", false, false, "6C03A3540C36BB4BD1BB9F1606F0F550", 0, "/api/resource/all-usable", "资源服务", "查询所有可以用的", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("1295aed2-ae71-411f-9542-d50f75432840"), 1305892581478400000L, "搜索数据", false, "系统管理", false, false, "24C5B533C5DDC7D494830FF5E28F6EC2", 1, "/api/resource/search", "资源服务", "搜索", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("5efd6ab4-a9d3-4742-9a48-fb54a1b1e463"), 1305892581478400000L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统管理", false, false, "D4F99E0AE4263D647F3440B66DB7AC7B", 0, "/api/role/all-usable", "角色服务", "查询所有可以用的", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("498638f7-dc92-4d0e-ac5e-26e48cf87a8d"), 1305892581478400000L, "搜索数据", false, "系统管理", false, false, "4B11C588FC856C862E41859F189370C0", 1, "/api/role/search", "角色服务", "搜索", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("cbc8aff4-6dc0-41f2-b684-caba8e0657ac"), 1305892581478400000L, "搜索用户数据", false, "系统管理", false, false, "04608E487B494D4597BBAD83DF59D2FF", 1, "/api/user/search", "用户服务", "搜索", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("f512069b-3c5c-47c6-bb97-5f7e7b71039d"), 1305892581478400000L, "根据多个主键批量逻辑删除", true, "新闻服务", false, false, "CBD3FFEFF6C8E0CF0D8789610DCDB5B5", 1, "/api/news/fake-deletes", "新闻管理", "批量逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("42b3486a-8ea0-4296-a526-7cd3ef9ea73a"), 1305892581478400000L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统管理", false, false, "6B7B11626AE0ABB28C5331DB67DACAA0", 0, "/api/attachment/all-usable", "附件服务", "查询所有可以用的", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("bdab8953-956d-4b1a-945b-b1806e9ac749"), 1305892581478400000L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统管理", false, false, "073E6E78B3A88E41DBDC46DCA32C4837", 0, "/api/user/all-usable", "用户服务", "查询所有可以用的", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("4a127124-6348-4db1-aa38-5f3af2c8efdf"), 1305892581478400000L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统管理", false, false, "A99DB9777E1C5C11D2FA6A8957F696E8", 0, "/api/audit-operation/all-usable", "审计操作服务", "查询所有可以用的", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("b79d2f63-487c-44c8-b7d3-1e882994789b"), 1305892581478400000L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统管理", false, false, "1CCC2478B5AC5FDDB537DCD33166ABF7", 0, "/api/function/all-usable", "功能服务", "查询所有可以用的", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("0a63566b-0a91-4d79-b417-60b1d8f92aeb"), 1305892581478400000L, "根据主键查找一条数据", false, "新闻服务", false, false, "2A6B6013C84E500CF451D706AF4FF7B8", 0, "/api/news/{id}", "新闻管理", "根据主键获取", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("9a4766ee-e624-41ce-98fc-e8abb6ef580a"), 1305892581478400000L, "根据主键删除一条数据", true, "新闻服务", false, false, "BB2AEA0FA60BE9D7016A8271D23591E3", 3, "/api/news/{id}", "新闻管理", "删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("81ee6d06-adc6-42c4-a8cd-5d1496581a6c"), 1305892581478400000L, "更新一条数据", true, "新闻服务", false, false, "0A1ABF7F373AA969110F03D1A1977088", 2, "/api/news", "新闻管理", "更新", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("c0b7ba65-dd12-4733-b2f5-e7347aa9f301"), 1305892581478400000L, "添加一条数据", true, "新闻服务", false, false, "61FD0B99A537BC225735AA062D6A9AEB", 1, "/api/news", "新闻管理", "添加", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("a02b5d10-7dc1-474a-9802-781da1172c3f"), 1305892581478400000L, "根据主键锁定或解锁数据", true, "新闻服务", false, false, "59F9B25ACB6F71D8E351B43A6443FB6A", 2, "/api/news/{id}/lock/{islocked}", "新闻管理", "锁定", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("edcd4871-7520-4437-93b1-3150c63b3486"), 1305892581478400000L, "根据分页参数，分页获取数据", false, "新闻服务", false, false, "2BE2F4CAE116170A84E87A542B7B2E03", 0, "/api/news/page/{pageindex}/{pagesize}", "新闻管理", "分页查询", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("1fe857c9-c027-4ca3-b8f8-21ec2c1f5cde"), 1305892581478400000L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统管理", false, false, "280713DC4618277C7BF307117835ED7B", 0, "/api/audit-entity/all-usable", "审计数据服务", "查询所有可以用的", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("c715a6d5-cd99-4c94-8760-936817c1e09c"), 1305892581478400000L, "搜索数据", false, "系统管理", false, false, "9A501F3D2F0A3A2D47A17D6F42042CD5", 1, "/api/position/search", "岗位管理服务", "搜索", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("85f94b4c-e897-4f3c-b80a-c7ddb8ebf1b5"), 1305892581478400000L, "搜索数据", false, "系统管理", false, false, "E6BAA5C7F35ED0CBD3902A30349A992B", 1, "/api/dept/search", "部门服务", "搜索", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("4e1a2966-bdfd-485a-b0cf-52004e40f6a7"), 1305892581478400000L, "查找到所有数据", false, "系统管理", false, false, "0730ED2F37C050E4994609C45BE0C4A4", 0, "/api/dept/all", "部门服务", "查询所有", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("bd24a2bb-42cf-4a84-b114-7d727464ebd1"), 1305892581478400000L, "根据主键逻辑删除", true, "新闻服务", false, false, "964B61937A0A0C000BC8D8D4251188EC", 3, "/api/news/fake-delete/{id}", "新闻管理", "逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("b3577dc2-dfea-41be-ba8f-bb8efa389f36"), 1305892581478400000L, "根据主键查找一条数据", false, "系统管理", false, false, "A46663EE883A6E5BE0A0C8FE0B3D7A4C", 0, "/api/audit-entity/{id}", "审计数据服务", "根据主键获取", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("5c8381ec-7e8a-4060-9c04-83032d18872c"), 1305892581478400000L, "根据主键逻辑删除", true, "系统管理", false, false, "A5DA0BB6BEA388B99626E5A34BDE68F4", 3, "/api/dept/fake-delete/{id}", "部门服务", "逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("2502e6ae-879b-4674-a557-cd7b4de891a7"), 1305892581478400000L, "根据主键查找一条数据", false, "系统管理", false, false, "213D1BBDB567A74636ACE841D780F663", 0, "/api/dept/{id}", "部门服务", "根据主键获取", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("333edf31-c542-4fa1-baca-b770d558a4d7"), 1305892581478400000L, "根据主键删除一条数据", true, "系统管理", false, false, "EA96F9C3B67BB0EB8E3D5337D3482162", 3, "/api/dept/{id}", "部门服务", "删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("e23b555c-600a-4839-9439-2ee0ad0ae4f8"), 1305892581478400000L, "更新一条数据", true, "系统管理", false, false, "248BF161E6BEB662D259298A8E564433", 2, "/api/dept", "部门服务", "更新", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("16517409-c055-447b-8e91-7155537c6d15"), 1305892581478400000L, "添加一条数据", true, "系统管理", false, false, "F57997ED31483BE396EB71C98D07B6F5", 1, "/api/role", "角色服务", "添加", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("f5c318f6-9230-475a-830e-a404e17506b5"), 1305892581478400000L, "添加一条数据", true, "系统管理", false, false, "3AB1D0424907EC010DC69F029B4FBD06", 1, "/api/dept", "部门服务", "添加", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("ff8621c9-1b88-4e6d-be00-34615c48c69f"), 1305892581478400000L, "", false, "系统管理", false, false, "EFFE9D726D7792B023DF91E15AA48C89", 0, "/api/dept/seed-data", "部门服务", "获取种子数据", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("9caf800a-de55-4d59-a138-675a16924c3c"), 1305892581478400000L, "查找到所有数据", false, "系统管理", false, false, "488BDECFA97ADDE5E940446C32C42693", 0, "/api/function/all", "功能服务", "查询所有", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("fcebd316-c2f3-4f8e-97fc-498dd3a33d4e"), 1305892581478400000L, "根据多个主键批量删除", true, "系统管理", false, false, "951D030BDA5FAE619E5A7BB9EFB43F33", 1, "/api/dept/deletes", "部门服务", "批量删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("080dd200-8e8a-489c-86ca-8eb74c417c0b"), 1305892581478400000L, "根据主键逻辑删除", true, "系统管理", false, false, "A20264B6A44D74DBF0C7990CF3FE6DC1", 3, "/api/audit-operation/fake-delete/{id}", "审计操作服务", "逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("db76ae46-851b-47bc-94be-b2e869043636"), 1305892581478400000L, "搜索操作审计", false, "系统管理", false, false, "704D356B44E6DEA692BA099781A321DD", 1, "/api/audit-operation/search", "审计操作服务", "搜索", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("12dbe1a6-7d23-48a4-bacb-164f0403d0f4"), 1305892581478400000L, "根据多个主键批量逻辑删除", true, "系统管理", false, false, "87F7F066F0A0605D1DB5CE8B7286E0CB", 1, "/api/audit-entity/fake-deletes", "审计数据服务", "批量逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("10fc92a8-30ed-4536-a995-c7af8e5548a1"), 1305892581478400000L, "根据主键查找一条数据", false, "系统管理", false, false, "6E9E5AA61727C2BD1E4142F0ED0F9DC5", 0, "/api/resource/{id}", "资源服务", "根据主键获取", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("f4ba1bf6-c07e-4df2-b7de-93b35fb79bf0"), 1305892581478400000L, "根据主键删除一条数据", true, "系统管理", false, false, "8650A7797FF354BBB742C87D0F560844", 3, "/api/resource/{id}", "资源服务", "删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("fff9f1e7-7fd3-42f5-afe7-d40cca07f0ca"), 1305892581478400000L, "", false, "系统管理", false, false, "B2A11324BCA0A9070B6160AE6B0EE6F2", 0, "/api/resource/{id}/functions", "资源服务", "根据资源id获取功能信息", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("51d98131-fb32-4f4a-b9ed-a89ec4c0718a"), 1305892581478400000L, "根据多个主键批量删除", true, "新闻服务", false, false, "900D7247E48D5866026A9DFCDD9758C0", 1, "/api/news/deletes", "新闻管理", "批量删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("337bae83-a083-4e0e-8ceb-2bb21ae22145"), 1305892581478400000L, "根据多个主键批量逻辑删除", true, "系统管理", false, false, "32ABBEA6610DE2420AC7B5E7FDAA315E", 1, "/api/dept/fake-deletes", "部门服务", "批量逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("7fa014c4-08db-4f96-8132-2bf3db32b256"), 1305892581478400000L, "搜索数据审计", false, "系统管理", false, false, "5A7181978F26890284CE44ED28A2F7AA", 1, "/api/audit-entity/search", "审计数据服务", "搜索", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("0b605fe1-c77c-4735-8320-b8f400163ac9"), 1305892581478400000L, "根据主键逻辑删除", true, "系统管理", false, false, "02836036DDDF7900E5F5E9762F5E4229", 3, "/api/user/fake-delete/{id}", "用户服务", "逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("383c5aaf-a3e1-44d1-a1c8-3074abe55f95"), 1305892581478400000L, "根据多个主键批量删除", true, "系统管理", false, false, "91B03FFD3080A9684592C45A15C826A5", 1, "/api/role/deletes", "角色服务", "批量删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("ffef6a8e-3f80-4a39-97c6-5b2b81582830"), 1305892581478400000L, "", true, "系统管理", false, false, "FE150D4F1EE3DDDE5BD78C718100A247", 3, "/api/resource-function/{resourceid}/{functionid}", "资源与接口关系服务", "删除资源与接口关系", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("c1e7fa06-b759-4bb0-9545-7265e3798d28"), 1305892581478400000L, "", true, "系统管理", false, false, "43844F96A173330CECD6470FD62A8A76", 1, "/api/resource-function", "资源与接口关系服务", "添加资源与接口关系", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("45dd0581-3394-4c0a-bb8e-c9e0074d5611"), 1305892581478400000L, "更新一条数据", true, "系统管理", false, false, "CE39C474540DD96EAF373115B164EDC7", 2, "/api/resource", "资源服务", "更新", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("3402d3b2-cf24-4634-a65c-534f96e2991a"), 1305892581478400000L, "根据多个主键批量删除", true, "系统管理", false, false, "5C9E8B48C5C77A0CEB8E6A853D56A808", 1, "/api/user/deletes", "用户服务", "批量删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("0c6f2138-e984-4fba-ad2a-2890716a7259"), 1305892581478400000L, "更新用户的头像", true, "系统管理", false, false, "FEBD6097BE29268FDFDC295C98A9AD9F", 2, "/api/user/avatar", "用户服务", "更新头像", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("0d2e0194-2238-457b-aab0-9b3259cc4ed9"), 1305892581478400000L, "给用户设置角色", true, "系统管理", false, false, "A843DEF0CDD97A394996DCF7C5E80F5B", 1, "/api/user/{userid}/role", "用户服务", "设置角色", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("5eb48cf2-6c45-47c2-a68b-84284a389c69"), 1305892581478400000L, "根据多个主键批量删除", true, "系统管理", false, false, "F74128AC93B49FC04CB29781E17E5302", 1, "/api/resource/deletes", "资源服务", "批量删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("e7e8c401-2ff1-45ee-adfd-cebe90117575"), 1305892581478400000L, "根据主键逻辑删除", true, "系统管理", false, false, "FEB756C21615385FC3C747ACB240DC2D", 3, "/api/resource/fake-delete/{id}", "资源服务", "逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("7f0d7abb-06a4-4a35-b4e3-7798b21e37fa"), 1305892581478400000L, "根据多个主键批量逻辑删除", true, "系统管理", false, false, "9C888321143AC3E991B72D3B32193A35", 1, "/api/resource/fake-deletes", "资源服务", "批量逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("3204c8c0-2c00-47ea-b2b3-711c0e7a2c70"), 1305892581478400000L, "获取当前用户的所有菜单", false, "系统管理", false, false, "B181FD356A433DD14FE2F54E4115AAC0", 0, "/api/authorize/current-user-menus", "权限认证服务", "获取当前用户的所有菜单", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("bdecc6f3-86f4-4818-af34-5e61001bdeeb"), 1305892581478400000L, "移除当前用户token", true, "系统管理", false, false, "CFCB833E52DBB2A3C4B4F5EAD96701DC", 3, "/api/authorize/current-user-refresh-token", "权限认证服务", "移除当前用户token", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("7602df12-8a81-4ab1-8314-df9ce948a876"), 1305892581478400000L, "通过刷新token获取新的token", true, "系统管理", false, false, "943DE09097CAE8DED8AAAF2C489E30D5", 1, "/api/authorize/refresh-token", "权限认证服务", "刷新Token", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("04ad3c68-6e35-4175-a8ff-564d4bf51e91"), 1305892581478400000L, "添加资源", true, "系统管理", false, false, "56C72854CD92865B84133D0D791DEC22", 1, "/api/resource", "资源服务", "添加资源", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("6aea8a77-edd2-444b-b8be-901d78321a49"), 1305892581478400000L, "根据多个主键批量逻辑删除", true, "系统管理", false, false, "3E010DCA7BAD6C3FCCCA32FB77F050F0", 1, "/api/user/fake-deletes", "用户服务", "批量逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("c56d6a82-abc8-4b17-bc28-27b1904116c9"), 1305892581478400000L, "", false, "系统管理", false, false, "DDE05A70BD80F948C9AEAFB9708090F3", 0, "/api/resource-function/seed-data", "资源与接口关系服务", "获取种子数据", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("c9e572ab-6363-49a4-9c74-d6e21553e45d"), 1305892581478400000L, "获取种子数据", false, "系统管理", false, false, "49CCD72DC5A379DF8AC6925CF391FC54", 0, "/api/function/seed-data", "功能服务", "获取种子数据", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("2c3ec3c9-76c7-4d29-953f-e7430f22577b"), 1305892581478400000L, "根据主键逻辑删除", true, "系统管理", false, false, "BBF7B9CA0FE646DBAE2923B70DA8A7A4", 3, "/api/role/fake-delete/{id}", "角色服务", "逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("814304bb-22fe-4a33-82e1-8ad7c64bab4a"), 1305892581478400000L, "获取所有子资源", false, "系统管理", false, false, "C5668FD7C42E9FB532AB9CB2E1480E1F", 0, "/api/resource/{id}/children", "资源服务", "获取所有子资源", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("a8c06d41-806a-4bf5-8ceb-15995dac08cb"), 1305892581478400000L, "获取种子数据", false, "系统管理", false, false, "3A4F1575935BB1B9D3B3F6F407EB43C6", 0, "/api/resource/seed-data", "资源服务", "获取种子数据", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("9ebd4172-5191-4931-9b22-4c339be4a816"), 1305892581478400000L, "更新用户", true, "系统管理", false, false, "8C82B0DF3A0F5EB8DFED7794B16DA9A5", 2, "/api/user", "用户服务", "更新", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("6a9763c9-c40f-44f3-a248-a3b1e3d1f586"), 1305892581478400000L, "搜索附件数据", false, "系统管理", false, false, "0C58B2617EA08ED81F14B53C00C678D7", 1, "/api/attachment/search", "附件服务", "搜索", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("3e2f4464-6b69-4a00-acfb-d39184729cdd"), 1305892581478400000L, "根据分页参数，分页获取数据", false, "系统管理", false, false, "5FFF46E52DE5943FA225B0F6E29A338D", 0, "/api/user/page/{pageindex}/{pagesize}", "用户服务", "分页查询", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("94f22c97-ae4a-40e0-95cd-d0a6347eacd7"), 1305892581478400000L, "根据角色编号删除所有资源", true, "系统管理", false, false, "DECA4ECA67D27FC9932271EE3B0AC5DD", 3, "/api/role/{roleid}/resource", "角色服务", "根据角色编号删除所有资源", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("01944b79-bfe5-4304-ade0-9c66e038d5d4"), 1305892581478400000L, "更新一条数据", true, "系统管理", false, false, "3005F52703299DD4885D51C80CA3B370", 2, "/api/role", "角色服务", "更新", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("39812ff6-2c40-4017-9d11-fd7b13fe2a6b"), 1305892581478400000L, "查看当前用户角色", false, "系统管理", false, false, "9F7F841076617DA5F308E11132F7E666", 0, "/api/authorize/current-user-roles", "权限认证服务", "查看用户角色", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("03f2bbda-d0d4-429f-9c95-03345d00c2cd"), 1305892581478400000L, "获取当前用户信息", false, "系统管理", false, false, "8B52F19B65946082EBFB93CE354018DE", 0, "/api/authorize/current-user", "权限认证服务", "获取当前用户信息", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("2eb943e9-5b65-4572-b76e-4ef2de07bcd3"), 1305892581478400000L, "", false, "系统管理", false, false, "58B28787804A078C4A724AF9A151DEDC", 0, "/api/authorize/current-user-resources", "权限认证服务", "获取用户资源", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("ef62671e-4d35-4993-83c4-4dcdf7cbf0d0"), 1305892581478400000L, "根据多个主键批量删除", true, "系统管理", false, false, "33524956F6EC6C08F348500B3E2D9E9C", 1, "/api/attachment/deletes", "附件服务", "批量删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("070ae0e4-0193-4ce0-8ba6-b8c344086ced"), 1305892581478400000L, "根据主键逻辑删除", true, "系统管理", false, false, "4A29177C50844829451B9ABBFA5DAFAC", 3, "/api/attachment/fake-delete/{id}", "附件服务", "逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("a53a9c89-7968-4598-9c46-dad4e9188bd0"), 1305892581478400000L, "获取api分组设置", false, "系统管理", false, false, "945B6A21E0C00F9BB0F7EEE37C671E3E", 0, "/api/swagger/api-group", "Swagger服务", "获取哦 swagger 配置", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("d22007c6-fada-4ef1-bafa-08455b767883"), 1305892581478400000L, "根据分页参数，分页获取数据", false, "系统管理", false, false, "F543F08AB768F7D444481F5D7EB52373", 0, "/api/position/page/{pageindex}/{pagesize}", "岗位管理服务", "分页查询", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("9d9233d8-df0a-43b7-929a-65b9bd532c8c"), 1305892581478400000L, "根据分页参数，分页获取数据", false, "系统管理", false, false, "5CF48BAB60B771300975D93C49925CA0", 0, "/api/role/page/{pageindex}/{pagesize}", "角色服务", "分页查询", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("7120bd2f-4491-41ac-bef3-7cd86615da14"), 1305892581478400000L, "搜索用户数据", false, "系统管理", false, false, "E0587A70B59848C9664BE0BF58E13A17", 0, "/api/user/search/{pageindex}/{pagesize}", "用户服务", "搜索", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("a4a2536b-1cc6-438c-ba00-054e16fc2c7c"), 1305892581478400000L, "根据主键删除一条数据", true, "系统管理", false, false, "1A6C9AC4F4D71B0FC154AD8CE6FE6D29", 3, "/api/role/{id}", "角色服务", "删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("38c69230-1ed0-413e-9ae6-05bc1ef989e0"), 1305892581478400000L, "分配权限（重置）", true, "系统管理", false, false, "2BBD7196A51542F56FAC25FF3D760D21", 1, "/api/role/{roleid}/resource", "角色服务", "分配权限", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("cba739f0-9f8a-40c2-afff-d66c3382e096"), 1305892581478400000L, "根据主键查找一条数据", false, "系统管理", false, false, "CC8DA87E574A106E9B14287FEC850037", 0, "/api/role/{id}", "角色服务", "根据主键获取", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("3790cc0d-dc3a-4669-acba-3a90812c6386"), 1305892581478400000L, "查看用户角色", false, "系统管理", false, false, "652940681CC97C52299C95242AB1E858", 0, "/api/user/{userid}/roles", "用户服务", "查看用户角色", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("9c6cefe2-d57d-490c-8b0f-70749bc5cdfa"), 1305892581478400000L, "根据主键删除", true, "系统管理", false, false, "085CB1560C82B28FE4C8C5F28EA31A59", 3, "/api/attachment/{id}", "附件服务", "删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("0f372dde-1e65-441a-b002-eee8b2e1a1f9"), 1305892581478400000L, "根据多个主键批量删除", true, "系统管理", false, false, "13457B9CA71646A02E6F004CE877A0E6", 1, "/api/audit-entity/deletes", "审计数据服务", "批量删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("cdd3c605-ed1d-4d94-a482-16430b729541"), 1305892581478400000L, "根据主键锁定或解锁数据", true, "系统管理", false, false, "BC8D1127FE54019A5476079400388CF3", 2, "/api/resource/{id}/lock/{islocked}", "资源服务", "锁定", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("c591c0ca-3305-4684-89bb-278218d13c47"), 1305892581478400000L, "", false, "系统管理", false, false, "187E0857A128187E01EFBBD569C3DE92", 0, "/api/swagger/functions-from-json/{url}", "Swagger服务", "从json中获取function", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("f8ddd5e5-7c20-43c2-a2cf-31ebc3f9971a"), 1305892581478400000L, "根据分页参数，分页获取数据", false, "系统管理", false, false, "5848947AEE0064BC746DE38E1AC0E3D2", 0, "/api/resource/page/{pageindex}/{pagesize}", "资源服务", "分页查询", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("b952b41e-b3e9-4c53-9a7d-6b561acf4bc4"), 1305892581478400000L, "根据主键查找一条数据", false, "系统管理", false, false, "FDD3EAB18820A6CD5C6DA3B17D40EEB9", 0, "/api/function/{id}", "功能服务", "根据主键获取", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("aed3a535-b700-48a5-a8f5-3657e500e400"), 1305892581478400000L, "根据主键锁定或解锁数据", true, "系统管理", false, false, "882FEEBFEAF1F50D83E0189AA69B9ED0", 2, "/api/audit-entity/{id}/lock/{islocked}", "审计数据服务", "锁定", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("4b7e7f68-8925-4b5c-b8d2-8a51df917b0c"), 1305892581478400000L, "根据分页参数，分页获取数据", false, "系统管理", false, false, "72DE329278C26111EB3F431ACB89B0A4", 0, "/api/audit-entity/page/{pageindex}/{pagesize}", "审计数据服务", "分页查询", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("045945e7-94c4-4727-8392-31fc9d99cd9f"), 1305892581478400000L, "查找到所有数据", false, "系统管理", false, false, "E46343E17F6F09D2DD0BB1B6C78C81F6", 0, "/api/audit-entity/all", "审计数据服务", "查询所有", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("f6843cdf-133d-4eb8-92b2-c36fe63ea9d7"), 1305892581478400000L, "根据主键锁定或解锁数据（必须有IsLock才能生效）", true, "系统管理", false, false, "9501F9B0B5D4867FF65611B203B43D69", 2, "/api/position/{id}/lock/{islocked}", "岗位管理服务", "锁定", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("63d7208e-45d3-406e-a4a1-c87e3afda04d"), 1305892581478400000L, "获取种子数据", false, "系统管理", false, false, "72B515FB99A1EFE42DEFCFC12954F93D", 0, "/api/role/role-resource-seed-data", "角色服务", "获取种子数据", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("39ccceae-2cba-4cd2-a44b-fc8fe8a3f2e4"), 1305892581478400000L, "查看用户权限", false, "系统管理", false, false, "FAA3B104E6EBF3B5F16DB92C56836A63", 0, "/api/user/{userid}/resources", "用户服务", "查看用户权限", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("868fc0df-7cdf-4b56-873e-16dd3e0aa528"), 1305892581478400000L, "根据主键锁定或解锁数据", true, "系统管理", false, false, "439ED218846E25C27A388B09904AABC8", 2, "/api/role/{id}/lock/{islocked}", "角色服务", "锁定", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("af79d7de-0141-4338-8c52-05216d1b07ff"), 1305892581478400000L, "新增用户", true, "系统管理", false, false, "7CBF6D43C3F9935BF83629FCEED2FFFB", 1, "/api/user", "用户服务", "新增", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("69f70da1-fb4e-443f-9efe-e3d12cc95eed"), 1305892581478400000L, "查找到所有数据", false, "系统管理", false, false, "88BAC4E29D23BD095207644BB397E5EE", 0, "/api/position/all", "岗位管理服务", "查询所有", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("8d94c826-ddba-47fe-94c9-333880fee187"), 1305892581478400000L, "swagger json 文件解析功能", false, "系统管理", false, false, "7E9057E559FB68353DCA5D208B7B2A71", 0, "/api/swagger/analysis/{url}", "Swagger服务", "解析api json", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("89954833-64a5-4c87-a717-9c863ca3b263"), 1305892581478400000L, "根据多个主键批量逻辑删除", true, "系统管理", false, false, "710C2B0A026A9C3FF0D6235FCD8E0F26", 1, "/api/position/fake-deletes", "岗位管理服务", "批量逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("ed340c0c-9b63-45f4-942a-c8a14c4491d3"), 1305892581478400000L, "根据多个主键批量删除", true, "系统管理", false, false, "C9E5F9B494BBF428A85ECEA53B095285", 1, "/api/position/deletes", "岗位管理服务", "批量删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("5e8adf52-8db2-4d56-9ff3-003cae13e0aa"), 1305892581478400000L, "根据多个主键批量逻辑删除", true, "系统管理", false, false, "F19E71A217BEEADDD5EF20B65D93439E", 1, "/api/role/fake-deletes", "角色服务", "批量逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("61cc62e4-34da-4a0a-9899-488d3ab399fa"), 1305892581478400000L, "根据主键删除一条数据", true, "系统管理", false, false, "34B7575A20F0D8D6B1B2522F9DD7A7B8", 3, "/api/position/{id}", "岗位管理服务", "删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("05153ee4-dc99-4834-b398-5999f7dc8d01"), 1305892581478400000L, "更新一条数据", true, "系统管理", false, false, "47FEFB8B545A5A813AB9ABA70F02BD49", 2, "/api/position", "岗位管理服务", "更新", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("b56c4126-411c-445e-86aa-a91a5ce816d4"), 1305892581478400000L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统管理", false, false, "C47AACD68B1EF833AAC0EC90CD878FDD", 0, "/api/position/all-usable", "岗位管理服务", "查询所有可以用的", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("715a2905-da23-405d-98a0-1a1222f7d101"), 1305892581478400000L, "根据主键查找一条数据", false, "系统管理", false, false, "DF0B66D0FC43BB25047A470707E01EF8", 0, "/api/position/{id}", "岗位管理服务", "根据主键获取", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("65a3c1ee-f5cf-48eb-9bf0-3d4db44257e4"), 1305892581478400000L, "根据主键逻辑删除", true, "系统管理", false, false, "BB0B0620A9F5665B13ADC8D8C8B8F98A", 3, "/api/position/fake-delete/{id}", "岗位管理服务", "逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("424fd96a-a889-4ff9-910a-25a59204d2ec"), 1305892581478400000L, "返回根节点资源", false, "系统管理", false, false, "34CFCB2759472E91321739C5D43B00D0", 0, "/api/resource/root", "资源服务", "返回根节点", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("cb9f6387-5817-4fd6-b9eb-6553dcaf5e87"), 1305892581478400000L, "查找到所有数据", false, "系统管理", false, false, "8D8980AD32B8E49FB140F9DCE14B897C", 0, "/api/role/all", "角色服务", "查询所有", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("2428c3c3-740e-45fc-9047-5a2be3c9cd70"), 1305892581478400000L, "根据主键删除一条数据", true, "系统管理", false, false, "FBAC1FD6280B05C7EAFD6BD24F0DE077", 3, "/api/user/{id}", "用户服务", "删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("b38fb0cc-4275-4d1f-8bb7-6f5a962bcc35"), 1305892581478400000L, "根据主键获取用户", false, "系统管理", false, false, "011AC4559477AB1F24A281BDC1033AAB", 0, "/api/user/{id}", "用户服务", "根据主键获取用户", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("7a3399b3-6003-4aae-8e24-2e478992630e"), 1305892581478400000L, "添加一条数据", true, "系统管理", false, false, "1EB184263BA127C79364162F4E75E660", 1, "/api/position", "岗位管理服务", "添加", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("23f69ca2-fcef-4cc7-93ac-484a1e38ba22"), 1305892581478400000L, "搜索数据", false, "系统管理", false, false, "6707D74A420C1B835267E8F89B2A733C", 1, "/api/user-token/search", "用户登录TOKEN服务", "搜索", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("5eb3ac07-56a4-401f-86c5-686a512663ce"), 1305892581478400000L, "添加一条数据", true, "系统管理", false, false, "B8C5050A9AF405D498549684AFBE6BA5", 1, "/api/user-token", "用户登录TOKEN服务", "添加", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("36843f91-b2dd-4be2-81bb-98ae3ca02905"), 1305892581478400000L, "更新一条数据", true, "系统管理", false, false, "20A3A8A905329855BDB1538D1FC4952E", 2, "/api/user-token", "用户登录TOKEN服务", "更新", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("994bcc67-2758-4b1d-894c-1ff8aa234aa9"), 1305892581478400000L, "根据主键删除一条数据", true, "系统管理", false, false, "F246AC8F372EBBB4DBA7FD9E387C78CF", 3, "/api/user-token/{id}", "用户登录TOKEN服务", "删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("29d09bb6-202c-43d4-b223-1bab9a8110c7"), 1305892581478400000L, "根据主键查找一条数据", false, "系统管理", false, false, "F644EA520FFD15E462A5C058F5B034B3", 0, "/api/user-token/{id}", "用户登录TOKEN服务", "根据主键获取", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("ca7391b0-2691-4bb9-87c5-1230c5f1e00e"), 1305892581478400000L, "根据多个主键批量删除", true, "系统管理", false, false, "B63C74AC690A1F5B2E8C23CD2F4C4A0B", 1, "/api/user-token/deletes", "用户登录TOKEN服务", "批量删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("98fdbccd-fde2-414d-9cfe-0d6cf3339d58"), 1305892581478400000L, "根据主键逻辑删除", true, "系统管理", false, false, "FF4F009CAC7CE2915AA92B3864ADD4CF", 3, "/api/user-token/fake-delete/{id}", "用户登录TOKEN服务", "逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("453f751d-70d5-4725-ac7c-ad083bd5253d"), 1305892581478400000L, "根据多个主键批量逻辑删除", true, "系统管理", false, false, "3176F9202C7FE094C0BCDBA6E428E1F4", 1, "/api/user-token/fake-deletes", "用户登录TOKEN服务", "批量逻辑删除", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("9b54a63a-b157-4bd3-adcc-daa0e248edc6"), 1305892581478400000L, "查找到所有数据", false, "系统管理", false, false, "75D578DDEE9C4085A751D6DD1C66F861", 0, "/api/user-token/all", "用户登录TOKEN服务", "查询所有", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("025150b5-37a7-4f19-b8a2-187cb1717928"), 1305892581478400000L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统管理", false, false, "64B675513AC8714610A3F1E35127EFCE", 0, "/api/user-token/all-usable", "用户登录TOKEN服务", "查询所有可以用的", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("4d23c991-627c-4a7a-8fa5-267c6682115d"), 1305892581478400000L, "根据分页参数，分页获取数据", false, "系统管理", false, false, "12097C2120D203155FD96BDD7EB37F23", 0, "/api/user-token/page/{pageindex}/{pagesize}", "用户登录TOKEN服务", "分页查询", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("a0e6ea28-fcd8-4c9b-b937-35a2afa10b86"), 1305892581478400000L, "根据主键锁定或解锁数据（必须有IsLock才能生效）", true, "系统管理", false, false, "DC9409CFC9E16F58BB9584E1445317AD", 2, "/api/user-token/{id}/lock/{islocked}", "用户登录TOKEN服务", "锁定", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("4d51608e-5988-4d3d-8f5e-00e0c0c07b02"), 1305892581478400000L, "查找到所有数据", false, "系统管理", false, false, "7971E7E4FDCB5CBA6EE06E7DFE3F199E", 0, "/api/resource/all", "资源服务", "查询所有", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("e8060504-9fce-43a4-a7f0-7818c2de567e"), 1305892581478400000L, "搜索岗位数据", false, "系统管理", false, false, "8B38909A67FDFE704F49AFB6AF35995A", 0, "/api/position/search/{pageindex}/{pagesize}", "岗位管理服务", "搜索", null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdatedTime" },
                values: new object[] { new Guid("e38c1619-0f84-4e55-81c2-0f47992ee33d"), 1305892581478400000L, "查询所有资源 按树形结构返回", false, "系统管理", false, false, "6AFF14D9D209CDEEFFC0E4872E060F42", 0, "/api/resource/tree", "资源服务", "查询所有资源", null });

            migrationBuilder.InsertData(
                table: "Position",
                columns: new[] { "Id", "CreatedTime", "Duty", "Grade", "IsDeleted", "IsLocked", "Name", "Qualifications", "Right", "Salary", "Target", "UpdatedTime" },
                values: new object[] { 2, 1305892579553280000L, null, null, false, false, "总经理", null, null, null, null, null });

            migrationBuilder.InsertData(
                table: "Position",
                columns: new[] { "Id", "CreatedTime", "Duty", "Grade", "IsDeleted", "IsLocked", "Name", "Qualifications", "Right", "Salary", "Target", "UpdatedTime" },
                values: new object[] { 1, 1305892579553280000L, null, null, false, false, "董事长", null, null, null, null, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"), 1305892580638720000L, "apartment", false, false, "root", "根节点", 0, null, "", "根根节点不能删除，不能改变类型！！。", 0, null });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedTime", "IsDefault", "IsDeleted", "IsLocked", "IsSuperAdministrator", "Name", "Remark", "UpdatedTime" },
                values: new object[] { 1, 1305892579553280000L, false, false, false, true, "超级管理员", "拥有所有权限", null });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedTime", "IsDefault", "IsDeleted", "IsLocked", "IsSuperAdministrator", "Name", "Remark", "UpdatedTime" },
                values: new object[] { 2, 1305892579553280000L, false, false, false, false, "浏览者", "只能浏览", null });

            migrationBuilder.InsertData(
                table: "Dept",
                columns: new[] { "Id", "Contacts", "CreatedTime", "IsDeleted", "IsLocked", "Name", "Order", "ParentId", "Remark", "Tel", "UpdatedTime" },
                values: new object[] { 2, "老B", 1305892579553280000L, false, false, "昌平办事处", 1, 1, "昌平办事处", "400-8888888", null });

            migrationBuilder.InsertData(
                table: "Dept",
                columns: new[] { "Id", "Contacts", "CreatedTime", "IsDeleted", "IsLocked", "Name", "Order", "ParentId", "Remark", "Tel", "UpdatedTime" },
                values: new object[] { 3, "老C", 1305892579553280000L, false, false, "海淀办事处", 1, 1, "海淀办事处", "400-8888888", null });

            migrationBuilder.InsertData(
                table: "Dept",
                columns: new[] { "Id", "Contacts", "CreatedTime", "IsDeleted", "IsLocked", "Name", "Order", "ParentId", "Remark", "Tel", "UpdatedTime" },
                values: new object[] { 5, "老E", 1305892579553280000L, false, false, "石家庄办事处", 1, 4, "石家庄办事处", "400-8888888", null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("371b335b-29e5-4846-b6de-78c9cc691717"), 1305892580638720000L, "home", false, false, "admin_home", "首页", 10, new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"), "/", "", 1000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), 1305892580638720000L, "setting", false, false, "system_manager", "系统管理", 20, new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"), "", "系统管理", 1000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1305892580638720000L, "", false, false, "system_login", "登录", 0, new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"), "", "登录系统", 2000, null });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("2dd1a78c-f725-461b-8bc6-66112a7e156c"), 1305892580638720000L, "audit", false, false, "system_manager_audit", "审计管理", 60, new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), "", "审计管理", 1000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), 1305892580638720000L, "crown", false, false, "system_manager_position", "岗位管理", 5, new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), "/system_manager/position", "", 1000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("925c3162-155c-4644-8ca2-075f9fc76235"), 1305892580638720000L, "file", false, false, "system_manager_attachment", "附件管理", 50, new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), "/system_manager/attachment", "附件管理", 1000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), 1305892580638720000L, "menu", false, false, "system_manager_resource", "资源管理", 30, new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), "/system_manager/resource", "", 1000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), 1305892580638720000L, "team", false, false, "system_manager_dept", "部门管理", 0, new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), "/system_manager/dept", "", 1000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), 1305892580638720000L, "api", false, false, "system_manager_function", "接口管理", 40, new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), "/system_manager/function", "", 1000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), 1305892580638720000L, "user", false, false, "system_manager_user", "用户管理", 10, new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), "/system_manager/user", "用户管理", 1000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), 1305892580638720000L, "user-switch", false, false, "system_manager_role", "角色管理", 20, new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), "/system_manager/role", "", 1000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"), 1305892580638720000L, "idcard", false, false, "system_manager_user_token", "登录管理", 70, new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), "system_manager/user-token", "", 1000, null });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("3204c8c0-2c00-47ea-b2b3-711c0e7a2c70"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("fe3c8d2c-02ce-4073-a2b5-0b05168e7fc9"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("2eb943e9-5b65-4572-b76e-4ef2de07bcd3"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("03f2bbda-d0d4-429f-9c95-03345d00c2cd"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("bdecc6f3-86f4-4818-af34-5e61001bdeeb"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("39812ff6-2c40-4017-9d11-fd7b13fe2a6b"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("7602df12-8a81-4ab1-8314-df9ce948a876"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("371b335b-29e5-4846-b6de-78c9cc691717"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Avatar", "CreatedTime", "DeptId", "Email", "EmailConfirmed", "Gender", "IsDeleted", "IsLocked", "NickName", "Password", "PasswordEncryptKey", "PhoneNumber", "PhoneNumberConfirmed", "PositionId", "UpdatedTime", "UserName" },
                values: new object[] { 2, "https://www.baidu.com/img/PCtm_d9c8750bed0b3c7d089fa7d55720d6cf.png", 1305892579553280000L, 3, null, false, 0, false, false, "测试员", "60759dd06243d0837b88ab9b7183e6df", "032854df-332d-4c60-905a-fb9487b711e4", null, false, 2, null, "testuser" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Avatar", "CreatedTime", "DeptId", "Email", "EmailConfirmed", "Gender", "IsDeleted", "IsLocked", "NickName", "Password", "PasswordEncryptKey", "PhoneNumber", "PhoneNumberConfirmed", "PositionId", "UpdatedTime", "UserName" },
                values: new object[] { 1, "https://portrait.gitee.com/uploads/avatars/user/100/302533_hgflydream_1578919799.png", 1305892579553280000L, 2, null, false, 0, false, false, "管理员", "6b8ecfe60e9d1945869fdfc7e65c1315", "032854df-332d-4c60-905a-fb9487b711e4", null, false, 1, null, "admin" });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("6ac07813-4d10-4b50-9f0c-ecd444041282"), 1305892580638720000L, "", false, false, "system_manager_function_refresh", "刷新接口", 0, new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("04c237bb-7670-4d66-bbaa-dcd9624d2d90"), 1305892580638720000L, "", false, false, "system_manager_dept_add_children", "添加子级部门", 0, new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"), 1305892580638720000L, "", false, false, "system_manager_role_set_resource", "角色分配资源", 5, new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("b100a7eb-ef44-4669-bac5-3c5ce52871bb"), 1305892580638720000L, "", false, false, "system_manager_function_delete", "删除接口", 0, new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("bd7d1a4c-960a-48b2-9c9e-083aa5c5924f"), 1305892580638720000L, "", false, false, "system_manager_function_delete_selected", "删除选中接口", 0, new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("749c3a63-6bd8-4755-87ed-c1d455e5b717"), 1305892580638720000L, "", false, false, "system_manager_function_import", "导入接口", 0, new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("50062351-8235-4da1-9f90-4917d0e8abe0"), 1305892580638720000L, "", false, false, "system_manager_function_edit", "编辑接口", 0, new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("6e487179-5bb2-4ab5-80e3-58c514c9595f"), 1305892580638720000L, "", false, false, "system_manager_function_enable_audit", "锁定接口", 0, new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"), 1305892580638720000L, "", false, false, "system_manager_audit_entity", "数据审计", 2, new Guid("2dd1a78c-f725-461b-8bc6-66112a7e156c"), "/system_manager/audit-entity", "数据审计", 1000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("ea0fb035-1f06-4f61-9946-8df027a7462d"), 1305892580638720000L, "", false, false, "system_manager_user_list_edit_avatar", "编辑用户头像-列表中", 8, new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), "", "编辑用户头像-列表中", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("46cad808-0d0b-42bb-a134-3ad6db8ebf54"), 1305892580638720000L, "", false, false, "system_manager_user_role_edit", "用户分配角色", 5, new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("e44bb45d-514c-4217-bfba-452c0bd38f28"), 1305892580638720000L, "", false, false, "system_manager_user_refresh", "刷新用户", 3, new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("1d2fb341-3b69-4d0b-934d-c4c2cd250401"), 1305892580638720000L, "", false, false, "system_manager_dept_delete_selected", "删除选中部门", 0, new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("87377abe-785d-426c-b052-f706a2c7173d"), 1305892580638720000L, "", false, false, "system_manager_user_lock", "锁定用户", 7, new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("0aa9b237-dab8-472e-b2e6-af9c0af9f916"), 1305892580638720000L, "", false, false, "system_manager_user_edit", "编辑用户", 4, new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("d5756ad0-6a8b-4462-907f-1c52a1e11369"), 1305892580638720000L, "", false, false, "system_manager_user_delete", "删除用户", 1, new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("476cf96a-0e18-4c30-a760-e8b9c615bb99"), 1305892580638720000L, "", false, false, "system_manager_user_delete_selected", "删除选中用户", 0, new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), "", "删除选中", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"), 1305892580638720000L, "", false, false, "system_manager_audit_operation", "操作审计", 1, new Guid("2dd1a78c-f725-461b-8bc6-66112a7e156c"), "/system_manager/audit-operation", "操作审计", 1000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("a468499c-7115-44f1-ad38-2c5f696891d4"), 1305892580638720000L, "", false, false, "system_manager_role_delete_selected", "删除选中角色", 0, new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("67501fd4-4fbf-48c2-b383-f3a2085268ed"), 1305892580638720000L, "", false, false, "system_manager_role_add", "添加角色", 2, new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("d982a072-4681-45d9-8489-7a14218adb04"), 1305892580638720000L, "", false, false, "system_manager_role_delete", "删除角色", 1, new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("1efd01cf-42f2-45c7-95f2-84be55e65646"), 1305892580638720000L, "", false, false, "system_manager_role_refresh", "刷新角色", 3, new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("b71bbc5f-83a3-4065-b561-cb4b69b4a507"), 1305892580638720000L, "", false, false, "system_manager_role_lock", "锁定角色", 7, new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("bf05ffe8-c3ff-402d-bef1-3e95d202fd03"), 1305892580638720000L, "", false, false, "system_manager_role_resource_download_seed_data", "获取种子数据", 0, new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("13e7d01e-93ca-429c-b412-ff6fa5b6a026"), 1305892580638720000L, "", false, false, "system_manager_role_edit", "编辑角色", 4, new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"), 1305892580638720000L, "", false, false, "system_manager_user_add", "添加用户", 2, new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("316ecba5-5d89-44ae-908f-a54268723bd1"), 1305892580638720000L, "", false, false, "system_manager_dept_edit", "编辑部门", 0, new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("186bca5f-cc2c-427e-a58a-dbb81641a296"), 1305892580638720000L, "", false, false, "system_manager_dept_refresh", "刷新部门", 0, new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("defa9a78-229f-43a9-b6b8-95dd6fd8a3c3"), 1305892580638720000L, "", false, false, "system_manager_dept_add", "添加部门", 0, new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("c98160ef-ce87-4a1b-bfb3-09fc79d2a34a"), 1305892580638720000L, "", false, false, "system_manager_user_token_refresh", "刷新用户Token", 0, new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("f4fa035f-27ae-4eee-b006-3cbfac3d2172"), 1305892580638720000L, "", false, false, "system_manager_position_refresh", "刷新岗位", 0, new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("3d6e9553-2baf-4d9d-8a82-65de1c7d7ece"), 1305892580638720000L, "", false, false, "system_manager_position_delete_selected", "删除选中岗位", 0, new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("4e845d07-33a4-4dc4-ba7f-8568f88b9d68"), 1305892580638720000L, "", false, false, "system_manager_position_delete", "删除岗位", 0, new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("0fd84267-ee22-47c4-b41c-ce654eba29d9"), 1305892580638720000L, "", false, false, "system_manager_position_add", "添加岗位", 0, new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("25535592-81a1-42dd-8a55-509f2c852ff9"), 1305892580638720000L, "", false, false, "system_manager_position_edit", "编辑岗位", 0, new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("f077211f-0e79-44a3-935c-0f704f6a5962"), 1305892580638720000L, "", false, false, "system_manager_user_token_delete_selected", "删除选中用户Token", 0, new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("f02f906a-7579-478a-9406-3c8fd2c54886"), 1305892580638720000L, "", false, false, "system_manager_attachment_delete", "删除附件", 0, new Guid("925c3162-155c-4644-8ca2-075f9fc76235"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("f1649263-ef9a-4f42-85ac-16009283efff"), 1305892580638720000L, "", false, false, "system_manager_attachment_refresh", "刷新附件", 0, new Guid("925c3162-155c-4644-8ca2-075f9fc76235"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("d998802f-776e-4137-bc63-d8d818464f98"), 1305892580638720000L, "null", false, false, "system_manager_attachment_delete_selected", "删除选中附件", 0, new Guid("925c3162-155c-4644-8ca2-075f9fc76235"), "null", "删除选中附件", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("0cbb3d40-de41-483e-a76c-3d85682176af"), 1305892580638720000L, "", false, false, "system_manager_user_token_lock", "锁定用户Token", 0, new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("de62a886-64b2-4a40-b70a-47eb08f23202"), 1305892580638720000L, "", false, false, "system_manager_dept_delete", "删除部门", 0, new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("e252c0c6-0f19-4768-954c-c0d83fb96d74"), 1305892580638720000L, "", false, false, "system_manager_resource_show_function", "关联资源接口", 0, new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("859aa714-67c7-4414-bc96-9de5b7aec2c4"), 1305892580638720000L, "", false, false, "system_manager_resource_download_seed_data", "导出种子数据", 0, new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("365fc5c4-404e-408a-88dc-7614dffad91b"), 1305892580638720000L, "", false, false, "system_manager_resource_refresh", "刷新资源", 0, new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("3d007d84-d209-49e2-94ca-11ad2a3dd91d"), 1305892580638720000L, "", false, false, "system_manager_user_token_delete", "删除用户Token", 0, new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("a1958e51-06d4-4b29-9533-eae9d86c41d1"), 1305892580638720000L, "", false, false, "system_manager_resource_lock", "锁定资源", 0, new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("dec04485-3dab-4251-b7b8-1044e749a51e"), 1305892580638720000L, "", false, false, "system_manager_resource_edit", "编辑资源", 0, new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("97a7d440-b7fe-4af6-a8a1-18846c48828b"), 1305892580638720000L, "", false, false, "system_manager_resource_delete", "删除资源", 0, new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), "", "删除资源", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("8ab307de-ad4b-462f-b61d-7f1d53b82f3d"), 1305892580638720000L, "", false, false, "system_manager_resource_add", "添加资源", 0, new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), "", "添加资源", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("c18d4928-35d2-4085-aec9-379d00bcfd8f"), 1305892580638720000L, "", false, false, "system_manager_resource_add_children", "添加子资源", 0, new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("08ae2764-e551-45d2-9da7-49648481a8e0"), 1305892580638720000L, "", false, false, "system_manager_resource_delete_selected", "删除选中", 0, new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), "", "删除选中", 2000, null });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("e38c1619-0f84-4e55-81c2-0f47992ee33d"), new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("a96bb19e-794e-4fe0-ad39-f423df44f633"), new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("2502e6ae-879b-4674-a557-cd7b4de891a7"), new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("23f69ca2-fcef-4cc7-93ac-484a1e38ba22"), new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("c715a6d5-cd99-4c94-8760-936817c1e09c"), new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("cbc8aff4-6dc0-41f2-b684-caba8e0657ac"), new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("6a9763c9-c40f-44f3-a248-a3b1e3d1f586"), new Guid("925c3162-155c-4644-8ca2-075f9fc76235"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("416fe54b-6c50-4b1b-bf77-6744cf19fa72"), new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("498638f7-dc92-4d0e-ac5e-26e48cf87a8d"), new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("2dd1a78c-f725-461b-8bc6-66112a7e156c"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("925c3162-155c-4644-8ca2-075f9fc76235"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId", "CreatedTime" },
                values: new object[] { 2, 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId", "CreatedTime" },
                values: new object[] { 1, 1, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("cc23917b-930a-4e34-9717-be71b9fd2dd5"), 1305892580638720000L, "", false, false, "system_manager_audit_operation_refresh", "刷新操作审计", 1, new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"), "", "刷新操作审计", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("4f943ed1-997a-485f-9b54-9824b4ac285c"), 1305892580638720000L, "", false, false, "system_manager_resource_function_delete_selected", "删除选中资源接口关系", 0, new Guid("e252c0c6-0f19-4768-954c-c0d83fb96d74"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("c4991844-d3b4-4f9a-9c90-c13114515796"), 1305892580638720000L, "", false, false, "system_manager_resource_function_add_page_show", "显示可选资源接口关系", 0, new Guid("e252c0c6-0f19-4768-954c-c0d83fb96d74"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("46a9084a-0ae2-496e-bda5-e7e02a419a53"), 1305892580638720000L, "", false, false, "system_manager_resource_show_function_1", "展示已选资源接口关系", 0, new Guid("e252c0c6-0f19-4768-954c-c0d83fb96d74"), "", "", 3000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("286dc779-f58d-439a-bb9b-1333ff2b111b"), 1305892580638720000L, "", false, false, "system_manager_audit_entity_delete", "删除数据审计", 3, new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"), "", "删除数据审计", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("8158e1a6-335d-4a29-9177-0f30e86fa8ec"), 1305892580638720000L, "", false, false, "system_manager_audit_entity_delete_selected", "删除选中数据审计", 2, new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"), "", "删除选中数据审计", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"), 1305892580638720000L, "", false, false, "system_manager_audit_entity_refresh", "刷新数据审计", 1, new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"), "", "刷新数据审计", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("3f8d700a-bc26-4d5c-9622-d98bf9359159"), 1305892580638720000L, "", false, false, "system_manager_audit_entity_detail", "查询数据审计详情", 4, new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"), "", "查询数据审计详情", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("ca1d4b3a-336b-40a5-b683-0fe0bcbabaf8"), 1305892580638720000L, "", false, false, "system_manager_resource_function_add", "添加资源接口关系", 0, new Guid("e252c0c6-0f19-4768-954c-c0d83fb96d74"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("24ace337-41fe-429d-b32e-d9f88bd97aaa"), 1305892580638720000L, "", false, false, "system_manager_audit_operation_detail", "操作审计数据变更详情", 0, new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"), "", "操作审计数据变更详情", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("1c377037-13b4-4ef2-8010-d914a40fdbb3"), 1305892580638720000L, "", false, false, "system_manager_audit_operation_delete", "删除操作审计", 3, new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"), "", "删除操作审计", 2000, null });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreatedTime", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "Type", "UpdatedTime" },
                values: new object[] { new Guid("5591c5b9-9ee0-44ae-a4fa-39234b95afa4"), 1305892580638720000L, "", false, false, "system_manager_audit_operation_delete_selected", "删除选中操作审计", 2, new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"), "", "", 2000, null });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("3790cc0d-dc3a-4669-acba-3a90812c6386"), new Guid("46cad808-0d0b-42bb-a134-3ad6db8ebf54"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("7120bd2f-4491-41ac-bef3-7cd86615da14"), new Guid("e44bb45d-514c-4217-bfba-452c0bd38f28"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("622c1a11-7dff-4318-9d21-b57fbd1da9ba"), new Guid("87377abe-785d-426c-b052-f706a2c7173d"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("af79d7de-0141-4338-8c52-05216d1b07ff"), new Guid("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("9ebd4172-5191-4931-9b22-4c339be4a816"), new Guid("0aa9b237-dab8-472e-b2e6-af9c0af9f916"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("3e2f4464-6b69-4a00-acfb-d39184729cdd"), new Guid("e44bb45d-514c-4217-bfba-452c0bd38f28"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("0d2e0194-2238-457b-aab0-9b3259cc4ed9"), new Guid("46cad808-0d0b-42bb-a134-3ad6db8ebf54"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("c715a6d5-cd99-4c94-8760-936817c1e09c"), new Guid("f4fa035f-27ae-4eee-b006-3cbfac3d2172"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("0b605fe1-c77c-4735-8320-b8f400163ac9"), new Guid("d5756ad0-6a8b-4462-907f-1c52a1e11369"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("0367ad11-0be0-48dd-a5a9-1d473b78c0bf"), new Guid("ea0fb035-1f06-4f61-9946-8df027a7462d"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("8ae9c253-584e-46e4-b805-6ec90281d6dd"), new Guid("6e487179-5bb2-4ab5-80e3-58c514c9595f"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("aeb8b23d-4da3-4ec0-867f-70d2e2ba9550"), new Guid("50062351-8235-4da1-9f90-4917d0e8abe0"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("b952b41e-b3e9-4c53-9a7d-6b561acf4bc4"), new Guid("50062351-8235-4da1-9f90-4917d0e8abe0"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("c591c0ca-3305-4684-89bb-278218d13c47"), new Guid("749c3a63-6bd8-4755-87ed-c1d455e5b717"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("0c6f2138-e984-4fba-ad2a-2890716a7259"), new Guid("ea0fb035-1f06-4f61-9946-8df027a7462d"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("6aea8a77-edd2-444b-b8be-901d78321a49"), new Guid("476cf96a-0e18-4c30-a760-e8b9c615bb99"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("63d7208e-45d3-406e-a4a1-c87e3afda04d"), new Guid("bf05ffe8-c3ff-402d-bef1-3e95d202fd03"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("16517409-c055-447b-8e91-7155537c6d15"), new Guid("67501fd4-4fbf-48c2-b383-f3a2085268ed"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("2c3ec3c9-76c7-4d29-953f-e7430f22577b"), new Guid("d982a072-4681-45d9-8489-7a14218adb04"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("cbc8aff4-6dc0-41f2-b684-caba8e0657ac"), new Guid("1efd01cf-42f2-45c7-95f2-84be55e65646"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("868fc0df-7cdf-4b56-873e-16dd3e0aa528"), new Guid("b71bbc5f-83a3-4065-b561-cb4b69b4a507"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("8d94c826-ddba-47fe-94c9-333880fee187"), new Guid("749c3a63-6bd8-4755-87ed-c1d455e5b717"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("01944b79-bfe5-4304-ade0-9c66e038d5d4"), new Guid("13e7d01e-93ca-429c-b412-ff6fa5b6a026"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("b38fb0cc-4275-4d1f-8bb7-6f5a962bcc35"), new Guid("13e7d01e-93ca-429c-b412-ff6fa5b6a026"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("38c69230-1ed0-413e-9ae6-05bc1ef989e0"), new Guid("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("e38c1619-0f84-4e55-81c2-0f47992ee33d"), new Guid("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("db76ae46-851b-47bc-94be-b2e869043636"), new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("7fa014c4-08db-4f96-8132-2bf3db32b256"), new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("98fdbccd-fde2-414d-9cfe-0d6cf3339d58"), new Guid("3d007d84-d209-49e2-94ca-11ad2a3dd91d"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("a0e6ea28-fcd8-4c9b-b937-35a2afa10b86"), new Guid("0cbb3d40-de41-483e-a76c-3d85682176af"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("453f751d-70d5-4725-ac7c-ad083bd5253d"), new Guid("f077211f-0e79-44a3-935c-0f704f6a5962"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("383c5aaf-a3e1-44d1-a1c8-3074abe55f95"), new Guid("a468499c-7115-44f1-ad38-2c5f696891d4"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("a53a9c89-7968-4598-9c46-dad4e9188bd0"), new Guid("749c3a63-6bd8-4755-87ed-c1d455e5b717"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("a15ce231-80ae-46c6-ada8-49666e81e328"), new Guid("749c3a63-6bd8-4755-87ed-c1d455e5b717"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("84256e5b-2cef-4b16-8fd3-79ff8d47c731"), new Guid("749c3a63-6bd8-4755-87ed-c1d455e5b717"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("10fc92a8-30ed-4536-a995-c7af8e5548a1"), new Guid("dec04485-3dab-4251-b7b8-1044e749a51e"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("cdd3c605-ed1d-4d94-a482-16430b729541"), new Guid("a1958e51-06d4-4b29-9533-eae9d86c41d1"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("23f69ca2-fcef-4cc7-93ac-484a1e38ba22"), new Guid("c98160ef-ce87-4a1b-bfb3-09fc79d2a34a"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("e38c1619-0f84-4e55-81c2-0f47992ee33d"), new Guid("365fc5c4-404e-408a-88dc-7614dffad91b"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("04ad3c68-6e35-4175-a8ff-564d4bf51e91"), new Guid("c18d4928-35d2-4085-aec9-379d00bcfd8f"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("10fc92a8-30ed-4536-a995-c7af8e5548a1"), new Guid("c18d4928-35d2-4085-aec9-379d00bcfd8f"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("e38c1619-0f84-4e55-81c2-0f47992ee33d"), new Guid("dec04485-3dab-4251-b7b8-1044e749a51e"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("e38c1619-0f84-4e55-81c2-0f47992ee33d"), new Guid("c18d4928-35d2-4085-aec9-379d00bcfd8f"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("c56d6a82-abc8-4b17-bc28-27b1904116c9"), new Guid("859aa714-67c7-4414-bc96-9de5b7aec2c4"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("a8c06d41-806a-4bf5-8ceb-15995dac08cb"), new Guid("859aa714-67c7-4414-bc96-9de5b7aec2c4"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("10190ac3-1092-49a9-8ad2-313454b40447"), new Guid("d998802f-776e-4137-bc63-d8d818464f98"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("715a2905-da23-405d-98a0-1a1222f7d101"), new Guid("0fd84267-ee22-47c4-b41c-ce654eba29d9"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("6a9763c9-c40f-44f3-a248-a3b1e3d1f586"), new Guid("f1649263-ef9a-4f42-85ac-16009283efff"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("715a2905-da23-405d-98a0-1a1222f7d101"), new Guid("25535592-81a1-42dd-8a55-509f2c852ff9"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("7a3399b3-6003-4aae-8e24-2e478992630e"), new Guid("0fd84267-ee22-47c4-b41c-ce654eba29d9"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("070ae0e4-0193-4ce0-8ba6-b8c344086ced"), new Guid("f02f906a-7579-478a-9406-3c8fd2c54886"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("45dd0581-3394-4c0a-bb8e-c9e0074d5611"), new Guid("dec04485-3dab-4251-b7b8-1044e749a51e"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("e38c1619-0f84-4e55-81c2-0f47992ee33d"), new Guid("8ab307de-ad4b-462f-b61d-7f1d53b82f3d"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("c39030b8-d207-4c22-a3ba-74b0eccaa2fa"), new Guid("bd7d1a4c-960a-48b2-9c9e-083aa5c5924f"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("416fe54b-6c50-4b1b-bf77-6744cf19fa72"), new Guid("6ac07813-4d10-4b50-9f0c-ecd444041282"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("4b57474a-88b4-4393-bb49-4b59e8c3c41d"), new Guid("b100a7eb-ef44-4669-bac5-3c5ce52871bb"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("89954833-64a5-4c87-a717-9c863ca3b263"), new Guid("3d6e9553-2baf-4d9d-8a82-65de1c7d7ece"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("a96bb19e-794e-4fe0-ad39-f423df44f633"), new Guid("186bca5f-cc2c-427e-a58a-dbb81641a296"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("f5c318f6-9230-475a-830e-a404e17506b5"), new Guid("04c237bb-7670-4d66-bbaa-dcd9624d2d90"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("5eb48cf2-6c45-47c2-a68b-84284a389c69"), new Guid("97a7d440-b7fe-4af6-a8a1-18846c48828b"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("337bae83-a083-4e0e-8ceb-2bb21ae22145"), new Guid("1d2fb341-3b69-4d0b-934d-c4c2cd250401"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("337bae83-a083-4e0e-8ceb-2bb21ae22145"), new Guid("de62a886-64b2-4a40-b70a-47eb08f23202"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("f5c318f6-9230-475a-830e-a404e17506b5"), new Guid("defa9a78-229f-43a9-b6b8-95dd6fd8a3c3"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("65a3c1ee-f5cf-48eb-9bf0-3d4db44257e4"), new Guid("4e845d07-33a4-4dc4-ba7f-8568f88b9d68"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("7f0d7abb-06a4-4a35-b4e3-7798b21e37fa"), new Guid("08ae2764-e551-45d2-9da7-49648481a8e0"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("10fc92a8-30ed-4536-a995-c7af8e5548a1"), new Guid("8ab307de-ad4b-462f-b61d-7f1d53b82f3d"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("04ad3c68-6e35-4175-a8ff-564d4bf51e91"), new Guid("8ab307de-ad4b-462f-b61d-7f1d53b82f3d"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("e23b555c-600a-4839-9439-2ee0ad0ae4f8"), new Guid("316ecba5-5d89-44ae-908f-a54268723bd1"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("05153ee4-dc99-4834-b398-5999f7dc8d01"), new Guid("25535592-81a1-42dd-8a55-509f2c852ff9"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("f4fa035f-27ae-4eee-b006-3cbfac3d2172"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("1efd01cf-42f2-45c7-95f2-84be55e65646"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("f1649263-ef9a-4f42-85ac-16009283efff"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("e252c0c6-0f19-4768-954c-c0d83fb96d74"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("365fc5c4-404e-408a-88dc-7614dffad91b"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("e44bb45d-514c-4217-bfba-452c0bd38f28"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("186bca5f-cc2c-427e-a58a-dbb81641a296"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("6ac07813-4d10-4b50-9f0c-ecd444041282"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("c98160ef-ce87-4a1b-bfb3-09fc79d2a34a"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("fff9f1e7-7fd3-42f5-afe7-d40cca07f0ca"), new Guid("46a9084a-0ae2-496e-bda5-e7e02a419a53"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("7fa014c4-08db-4f96-8132-2bf3db32b256"), new Guid("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("1d994e50-d40a-465b-8445-646041a8131a"), new Guid("24ace337-41fe-429d-b32e-d9f88bd97aaa"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("080dd200-8e8a-489c-86ca-8eb74c417c0b"), new Guid("1c377037-13b4-4ef2-8010-d914a40fdbb3"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("12dbe1a6-7d23-48a4-bacb-164f0403d0f4"), new Guid("8158e1a6-335d-4a29-9177-0f30e86fa8ec"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("db76ae46-851b-47bc-94be-b2e869043636"), new Guid("cc23917b-930a-4e34-9717-be71b9fd2dd5"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("73cfe63f-3338-4bd0-a0b9-1b9cc39951ea"), new Guid("5591c5b9-9ee0-44ae-a4fa-39234b95afa4"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("ffef6a8e-3f80-4a39-97c6-5b2b81582830"), new Guid("4f943ed1-997a-485f-9b54-9824b4ac285c"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("b79d2f63-487c-44c8-b7d3-1e882994789b"), new Guid("c4991844-d3b4-4f9a-9c90-c13114515796"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("fff9f1e7-7fd3-42f5-afe7-d40cca07f0ca"), new Guid("c4991844-d3b4-4f9a-9c90-c13114515796"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("c1e7fa06-b759-4bb0-9545-7265e3798d28"), new Guid("ca1d4b3a-336b-40a5-b683-0fe0bcbabaf8"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[] { new Guid("7e5577d4-32b2-4f43-a83f-05410b59b195"), new Guid("286dc779-f58d-439a-bb9b-1333ff2b111b"), 1305892580638720000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("cc23917b-930a-4e34-9717-be71b9fd2dd5"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("24ace337-41fe-429d-b32e-d9f88bd97aaa"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("3f8d700a-bc26-4d5c-9622-d98bf9359159"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("46a9084a-0ae2-496e-bda5-e7e02a419a53"), 2, 1305892579553280000L });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreatedTime" },
                values: new object[] { new Guid("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"), 2, 1305892579553280000L });

            migrationBuilder.CreateIndex(
                name: "IX_AuditEntity_AuditOperationId",
                table: "AuditEntity",
                column: "AuditOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditProperty_AuditEntityid",
                table: "AuditProperty",
                column: "AuditEntityid");

            migrationBuilder.CreateIndex(
                name: "IX_ClientFunction_FunctionId",
                table: "ClientFunction",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Dept_ParentId",
                table: "Dept",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_ParentId",
                table: "Resource",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceFunction_FunctionId",
                table: "ResourceFunction",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleResource_ResourceId",
                table: "RoleResource",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_User_DeptId",
                table: "User",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PositionId",
                table: "User",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "AuditProperty");

            migrationBuilder.DropTable(
                name: "ClientFunction");

            migrationBuilder.DropTable(
                name: "EntityCodeGenerationSetting");

            migrationBuilder.DropTable(
                name: "LoginToken");

            migrationBuilder.DropTable(
                name: "ResourceFunction");

            migrationBuilder.DropTable(
                name: "RoleResource");

            migrationBuilder.DropTable(
                name: "UserExtension");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "VerifyCode");

            migrationBuilder.DropTable(
                name: "AuditEntity");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Function");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "AuditOperation");

            migrationBuilder.DropTable(
                name: "Dept");

            migrationBuilder.DropTable(
                name: "Position");
        }
    }
}
