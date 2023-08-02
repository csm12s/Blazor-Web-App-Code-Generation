using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gardener.Api.Core.Migrations
{
    /// <inheritdoc />
    public partial class v001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Announcement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Summary = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Content = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: true),
                    FixTop = table.Column<bool>(type: "INTEGER", nullable: false),
                    OpposeCount = table.Column<int>(type: "INTEGER", nullable: false),
                    FavourCount = table.Column<int>(type: "INTEGER", nullable: false),
                    ReplyCount = table.Column<int>(type: "INTEGER", nullable: false),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    BusinessId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    BusinessType = table.Column<int>(type: "INTEGER", nullable: false),
                    FileType = table.Column<int>(type: "INTEGER", nullable: false),
                    ContentType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Size = table.Column<long>(type: "INTEGER", nullable: false),
                    Path = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    OriginalName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Url = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Suffix = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true)
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
                    OperaterType = table.Column<int>(type: "INTEGER", nullable: false),
                    Ip = table.Column<string>(type: "TEXT", nullable: true),
                    UserAgent = table.Column<string>(type: "TEXT", nullable: true),
                    Path = table.Column<string>(type: "TEXT", nullable: true),
                    Method = table.Column<int>(type: "INTEGER", nullable: false),
                    Parameters = table.Column<string>(type: "TEXT", nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true)
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
                    SecretKey = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CodeType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CodeTypeName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CodeTypeValue = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Remark = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeType", x => x.Id);
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
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dept", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dept_Dept_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Dept",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmailServerConfig",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Remark = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Host = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Port = table.Column<int>(type: "INTEGER", nullable: false),
                    FromEmail = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    AccountName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    AccountPassword = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Tags = table.Column<string>(type: "TEXT", nullable: true),
                    EnableSsl = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailServerConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Remark = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    FromName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    SubjectTemplate = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ContentTemplate = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: true),
                    Example = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    IsHtml = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.Id);
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
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true)
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
                    Group = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Service = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Summary = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Key = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Path = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Method = table.Column<int>(type: "INTEGER", nullable: false),
                    EnableAudit = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Function", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImSession",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SessionType = table.Column<int>(type: "INTEGER", nullable: false),
                    SessionName = table.Column<string>(type: "TEXT", nullable: true),
                    Announcement = table.Column<string>(type: "TEXT", nullable: true),
                    LastMessageTime = table.Column<long>(type: "INTEGER", nullable: false),
                    UsersSignature = table.Column<string>(type: "TEXT", nullable: false),
                    AllUserIsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DisableSendMessage = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImSession", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImSessionMessage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ImSessionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    MessageType = table.Column<int>(type: "INTEGER", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImSessionMessage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImUserSession",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ImSessionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImUserSession", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdentityId = table.Column<string>(type: "TEXT", nullable: false),
                    IdentityName = table.Column<string>(type: "TEXT", nullable: false),
                    IdentityNickName = table.Column<string>(type: "TEXT", nullable: true),
                    IdentityType = table.Column<int>(type: "INTEGER", nullable: false),
                    LoginId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginClientType = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    EndTime = table.Column<long>(type: "INTEGER", nullable: false),
                    Ip = table.Column<string>(type: "TEXT", nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true)
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
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true)
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
                    SupportMultiTenant = table.Column<bool>(type: "INTEGER", nullable: false),
                    Hide = table.Column<bool>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resource_Resource_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Resource",
                        principalColumn: "Id");
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
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_CodeGen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TableName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    Module = table.Column<string>(type: "TEXT", nullable: true),
                    Remark = table.Column<string>(type: "TEXT", nullable: true),
                    TableDescriptionEN = table.Column<string>(type: "TEXT", nullable: true),
                    TableDescriptionCH = table.Column<string>(type: "TEXT", nullable: true),
                    IconName = table.Column<string>(type: "TEXT", nullable: true),
                    MenuNameEN = table.Column<string>(type: "TEXT", nullable: true),
                    MenuNameCH = table.Column<string>(type: "TEXT", nullable: true),
                    MenuParentId = table.Column<Guid>(type: "TEXT", nullable: true),
                    TablePrefix = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    NameSpace = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    GenerateSelectFields = table.Column<bool>(type: "INTEGER", nullable: true),
                    UseCustomTemplate = table.Column<bool>(type: "INTEGER", nullable: true),
                    GenerateProjectFile = table.Column<bool>(type: "INTEGER", nullable: true),
                    GenerateBaseClass = table.Column<bool>(type: "INTEGER", nullable: true),
                    GenerateService = table.Column<bool>(type: "INTEGER", nullable: true),
                    GenerateLocaleFile = table.Column<bool>(type: "INTEGER", nullable: true),
                    UseChineseKey = table.Column<bool>(type: "INTEGER", nullable: true),
                    UseNetColumnAsKey = table.Column<bool>(type: "INTEGER", nullable: true),
                    PrimaryKeyName = table.Column<string>(type: "TEXT", nullable: false),
                    HasAdd = table.Column<bool>(type: "INTEGER", nullable: true),
                    HasEdit = table.Column<bool>(type: "INTEGER", nullable: true),
                    HasBatchEdit = table.Column<bool>(type: "INTEGER", nullable: true),
                    HasDelete = table.Column<bool>(type: "INTEGER", nullable: true),
                    HasBatchDelete = table.Column<bool>(type: "INTEGER", nullable: true),
                    HasLock = table.Column<bool>(type: "INTEGER", nullable: true),
                    HasImport = table.Column<bool>(type: "INTEGER", nullable: true),
                    HasExport = table.Column<bool>(type: "INTEGER", nullable: true),
                    EntityFromTable = table.Column<bool>(type: "INTEGER", nullable: true),
                    OriginModule = table.Column<string>(type: "TEXT", nullable: true),
                    NewTableName = table.Column<string>(type: "TEXT", nullable: true),
                    AllowNull = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_CodeGen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysJobCluster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClusterId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    Status = table.Column<uint>(type: "INTEGER", nullable: false),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysJobCluster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysJobDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JobId = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    GroupName = table.Column<string>(type: "TEXT", nullable: true),
                    JobType = table.Column<string>(type: "TEXT", nullable: true),
                    AssemblyName = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Concurrent = table.Column<bool>(type: "INTEGER", nullable: false),
                    IncludeAnnotations = table.Column<bool>(type: "INTEGER", nullable: false),
                    Properties = table.Column<string>(type: "TEXT", nullable: true),
                    CreateType = table.Column<int>(type: "INTEGER", nullable: false),
                    ScriptCode = table.Column<string>(type: "TEXT", nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysJobDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysJobTrigger",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TriggerId = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    JobId = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    TriggerType = table.Column<string>(type: "TEXT", nullable: true),
                    AssemblyName = table.Column<string>(type: "TEXT", nullable: true),
                    Args = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<uint>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<long>(type: "INTEGER", nullable: true),
                    EndTime = table.Column<long>(type: "INTEGER", nullable: true),
                    LastRunTime = table.Column<long>(type: "INTEGER", nullable: true),
                    NextRunTime = table.Column<long>(type: "INTEGER", nullable: true),
                    NumberOfRuns = table.Column<long>(type: "INTEGER", nullable: false),
                    MaxNumberOfRuns = table.Column<long>(type: "INTEGER", nullable: false),
                    NumberOfErrors = table.Column<long>(type: "INTEGER", nullable: false),
                    MaxNumberOfErrors = table.Column<long>(type: "INTEGER", nullable: false),
                    NumRetries = table.Column<int>(type: "INTEGER", nullable: false),
                    RetryTimeout = table.Column<int>(type: "INTEGER", nullable: false),
                    StartNow = table.Column<bool>(type: "INTEGER", nullable: false),
                    RunOnStart = table.Column<bool>(type: "INTEGER", nullable: false),
                    ResetOnlyOnce = table.Column<bool>(type: "INTEGER", nullable: false),
                    Result = table.Column<string>(type: "TEXT", nullable: true),
                    ElapsedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysJobTrigger", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemTenant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Tel = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true),
                    Remark = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemTenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VerifyCodeLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    VerifyCodeType = table.Column<int>(type: "INTEGER", nullable: false),
                    Key = table.Column<string>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    EndTime = table.Column<long>(type: "INTEGER", nullable: false),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerifyCodeLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    TypeName = table.Column<string>(type: "TEXT", nullable: false),
                    OperationType = table.Column<int>(type: "INTEGER", nullable: false),
                    OperaterId = table.Column<string>(type: "TEXT", nullable: false),
                    OperaterName = table.Column<string>(type: "TEXT", nullable: false),
                    OperaterType = table.Column<int>(type: "INTEGER", nullable: false),
                    OperationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AuditOperationId = table.Column<Guid>(type: "TEXT", nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditEntity_AuditOperation_AuditOperationId",
                        column: x => x.AuditOperationId,
                        principalTable: "AuditOperation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Code",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CodeTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    CodeValue = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CodeName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    ExtendParams = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Color = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Code", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Code_CodeType_CodeTypeId",
                        column: x => x.CodeTypeId,
                        principalTable: "CodeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientFunction",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FunctionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true)
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
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Dept_DeptId",
                        column: x => x.DeptId,
                        principalTable: "Dept",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Position_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "Id");
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
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true)
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
                name: "Sys_CodeGenConfig",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CodeGenId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ColumnName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    NetColumnName = table.Column<string>(type: "TEXT", nullable: false),
                    ColumnDescription = table.Column<string>(type: "TEXT", nullable: true),
                    ColumnSummary = table.Column<string>(type: "TEXT", nullable: true),
                    NetType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    NetTypeRaw = table.Column<string>(type: "TEXT", nullable: true),
                    DbDataType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ViewComponentType = table.Column<int>(type: "INTEGER", nullable: true),
                    EditComponentType = table.Column<int>(type: "INTEGER", nullable: true),
                    EditComponentLength = table.Column<int>(type: "INTEGER", nullable: true),
                    CustomSearchType = table.Column<int>(type: "INTEGER", nullable: true),
                    CustomSearchLength = table.Column<int>(type: "INTEGER", nullable: true),
                    EffectType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    FkEntityName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    FkColumnName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    FkColumnNetType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    DictTypeCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    IsDictRetract = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsDictRequired = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsSearch = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsCustomSearch = table.Column<bool>(type: "INTEGER", nullable: true),
                    SearchType = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    IsView = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsCreate = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsRequired = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsEdit = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsBatchEdit = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsCommon = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsEntity = table.Column<bool>(type: "INTEGER", nullable: true),
                    ColumnKey = table.Column<string>(type: "TEXT", maxLength: 5, nullable: true),
                    IsPrimaryKey = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsIdentity = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsNullable = table.Column<bool>(type: "INTEGER", nullable: true),
                    DecimalDigits = table.Column<int>(type: "INTEGER", nullable: true),
                    Length = table.Column<int>(type: "INTEGER", nullable: true),
                    IsIgnore = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsSpecialType = table.Column<bool>(type: "INTEGER", nullable: true),
                    DbDataTypeText = table.Column<string>(type: "TEXT", nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_CodeGenConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sys_CodeGenConfig_Sys_CodeGen_CodeGenId",
                        column: x => x.CodeGenId,
                        principalTable: "Sys_CodeGen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemTenantResource",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ResourceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemTenantResource", x => new { x.TenantId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_SystemTenantResource_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemTenantResource_SystemTenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "SystemTenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditProperty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: false),
                    FieldName = table.Column<string>(type: "TEXT", nullable: false),
                    OriginalValue = table.Column<string>(type: "TEXT", nullable: true),
                    NewValue = table.Column<string>(type: "TEXT", nullable: true),
                    DataType = table.Column<string>(type: "TEXT", nullable: true),
                    AuditEntityid = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true)
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
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExtension", x => x.UserId);
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
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    UpdateBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateIdentityType = table.Column<int>(type: "INTEGER", nullable: true),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true)
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
                columns: new[] { "Id", "Contacts", "CreateBy", "CreateIdentityType", "CreatedTime", "Email", "IsDeleted", "IsLocked", "Name", "Remark", "SecretKey", "Tel", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[] { new Guid("96c0eec0-861f-4ed2-a183-5604b20bdff9"), "园丁", null, null, 1305892579553280000L, "qq@qq.com", false, false, "测试client1", "用于测试", "9f700cec-b787-4e23-a2da-9e45b3bd6cbb", "13838888888", null, null, null });

            migrationBuilder.InsertData(
                table: "CodeType",
                columns: new[] { "Id", "CodeTypeName", "CodeTypeValue", "CreateBy", "CreateIdentityType", "CreatedTime", "IsDeleted", "IsLocked", "Remark", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { 1, "心情", "mood", "2", 1, 1306966793932800480L, false, false, "", null, null, null },
                    { 2, "爱好", "interest", "6", 1, 1306991751352320480L, false, false, "", null, null, null },
                    { 3, "岗位等级", "position-level", "6", 1, 1306992877936640480L, false, false, "", "6", 1, 1306992878100480480L }
                });

            migrationBuilder.InsertData(
                table: "Dept",
                columns: new[] { "Id", "Contacts", "CreateBy", "CreateIdentityType", "CreatedTime", "IsDeleted", "IsLocked", "Name", "Order", "ParentId", "Remark", "Tel", "TenantId", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { 1, "老A", null, null, 1305892579553280000L, false, false, "北京分部", 1, null, "北京分部", "400-8888888", null, null, null, null },
                    { 4, "老D", null, null, 1305892579553280000L, false, false, "河北分部", 1, null, "河北分部", "400-8888888", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "EmailServerConfig",
                columns: new[] { "Id", "AccountName", "AccountPassword", "CreateBy", "CreateIdentityType", "CreatedTime", "EnableSsl", "FromEmail", "Host", "IsDeleted", "IsLocked", "Name", "Port", "Remark", "Tags", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[] { new Guid("1812e5c1-7bcc-4d51-9b5e-45d610357e0e"), "888888@qq.com", "123456", null, null, 1306051084984320000L, false, "888888@qq.com", "smtp.qq.com", false, false, "QQ Email", 25, "QQ Email", "Base,QQ", null, null, null });

            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "Id", "ContentTemplate", "CreateBy", "CreateIdentityType", "CreatedTime", "Example", "FromName", "IsDeleted", "IsHtml", "IsLocked", "Name", "Remark", "SubjectTemplate", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[] { new Guid("90587db9-3c8d-4ec1-80cc-ff001166fd25"), "<p>您的验证码是：<b> @Model.Code </b></p>\r\n                                  <P>时间：@(System.DateTime.Now.ToString(\"yyyy-MM-dd HH:mm:ss\"))</p>", null, null, 1306051084984320000L, "{\"Code\":123}", "园丁", false, true, false, "验证码", "发送验证码", "你好，请查收验证码", null, null, null });

            migrationBuilder.InsertData(
                table: "Function",
                columns: new[] { "Id", "CreateBy", "CreateIdentityType", "CreatedTime", "Description", "EnableAudit", "Group", "IsDeleted", "IsLocked", "Key", "Method", "Path", "Service", "Summary", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("01944b79-bfe5-4304-ade0-9c66e038d5d4"), null, null, 1306532718346240480L, "更新一条数据", true, "用户中心服务", false, false, "3005F52703299DD4885D51C80CA3B370", 2, "/api/role", "角色服务", "更新", null, null, 1306532718346240480L },
                    { new Guid("023f5b95-d07b-4118-a601-dcbd0cc191af"), "1", 1, 1306782482534400480L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统基础服务", false, false, "56F09861128F3AE3FC2496CAB12749D6", 0, "/api/code-gen-config/all-usable", "代码生成配置 - DB First", "查询所有可以用的", null, null, null },
                    { new Guid("0367ad11-0be0-48dd-a5a9-1d473b78c0bf"), null, null, 1306532718346240480L, "上传单个附件", true, "系统基础服务", false, false, "3BF647BFC6987B8CEA91C97FEE17CC6D", 1, "/api/attachment/upload", "附件服务", "上传附件", null, null, 1306532718346240480L },
                    { new Guid("037d2517-d1fa-4b5f-adba-a8f4aae6c205"), null, null, 1306532718346240480L, "根据主键逻辑删除", true, "用户中心服务", false, false, "EFC227D985161F0ED01B189C5CCF532F", 3, "/api/client/fake-delete/{id}", "客户端服务", "逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("03c9956e-b832-4202-9c47-55ba3793f606"), null, null, 1306532718346240480L, "根据分页参数，分页获取数据", false, "系统基础服务", false, false, "778BD549C3ACEF321ECEDF39C80241D0", 0, "/api/audit-operation/page/{pageindex}/{pagesize}", "审计操作服务", "分页查询", null, null, 1306532718346240480L },
                    { new Guid("03ee6f4b-dfea-4803-9515-3a9b2f907c90"), null, null, 1306532718346240480L, "通过刷新token获取新的token", true, "用户中心服务", false, false, "1549F5F1C34E25281CBC00CA283BC404", 1, "/api/account/refresh-token", "用户账户认证授权服务", "刷新Token", null, null, 1306532718346240480L },
                    { new Guid("040878a9-1b78-494e-9ee1-b4a7eab118fb"), null, null, 1306532718346240480L, "根据多个主键批量删除", true, "系统基础服务", false, false, "D522DFCA12CBF851EA48D676E7432DF8", 1, "/api/login-token/deletes", "用户登录TOKEN服务", "批量删除", null, null, 1306532718346240480L },
                    { new Guid("045945e7-94c4-4727-8392-31fc9d99cd9f"), null, null, 1306532718346240480L, "查找到所有数据", false, "系统基础服务", false, false, "E46343E17F6F09D2DD0BB1B6C78C81F6", 0, "/api/audit-entity/all", "审计数据服务", "查询所有", null, null, 1306532718346240480L },
                    { new Guid("04ad3c68-6e35-4175-a8ff-564d4bf51e91"), null, null, 1306532718346240480L, "添加资源", true, "系统基础服务", false, false, "56C72854CD92865B84133D0D791DEC22", 1, "/api/resource", "资源服务", "添加资源", null, null, 1306532718346240480L },
                    { new Guid("05153ee4-dc99-4834-b398-5999f7dc8d01"), null, null, 1306532718346240480L, "更新一条数据", true, "用户中心服务", false, false, "47FEFB8B545A5A813AB9ABA70F02BD49", 2, "/api/position", "岗位管理服务", "更新", null, null, 1306532718346240480L },
                    { new Guid("056ff2f6-009b-40ff-a1b9-a6983e471967"), null, null, 1306532718346240480L, "启用或禁用功能", true, "系统基础服务", false, false, "1DC1B5ECD34759A80CE8C468366A378F", 2, "/api/function/{id}/enable-audit/{enableaudit}", "功能服务", "启用或禁用", null, null, 1306532718346240480L },
                    { new Guid("06b69fae-5d2a-48c4-883a-aff7f1015661"), "1", 1, 1306961911889920480L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统基础服务", false, false, "CD838C782BF38C679F6017D29A5075B1", 0, "/api/code/all-usable", "字典管理", "查询所有可以用的", null, null, null },
                    { new Guid("06eff7a6-4291-43b0-9c34-58a97aa0fc01"), "1", 1, 1306961911828480480L, "添加一条数据", true, "系统基础服务", false, false, "F53D637DD7CC01612737CC2F878A95AA", 1, "/api/code", "字典管理", "添加", null, null, null },
                    { new Guid("070ae0e4-0193-4ce0-8ba6-b8c344086ced"), null, null, 1306532718346240480L, "根据主键逻辑删除", true, "系统基础服务", false, false, "4A29177C50844829451B9ABBFA5DAFAC", 3, "/api/attachment/fake-delete/{id}", "附件服务", "逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("080dd200-8e8a-489c-86ca-8eb74c417c0b"), null, null, 1306532718346240480L, "根据主键逻辑删除", true, "系统基础服务", false, false, "A20264B6A44D74DBF0C7990CF3FE6DC1", 3, "/api/audit-operation/fake-delete/{id}", "审计操作服务", "逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("0837b06f-e1d7-4ed4-9204-0c237aac6978"), "6", 1, 1307019897774080480L, "查找到所有数据", false, "用户中心服务", false, false, "98F7C609E0FDCAC25D70E29A5DCFF83B", 0, "/api/tenant/all", "租户服务", "查询所有", null, null, null },
                    { new Guid("08d002b9-d320-4410-b9f3-7986ed87ece4"), null, null, 1306532718346240480L, "根据主键查找一条数据", false, "系统基础服务", false, false, "DA5651C09F319A1358B9948735712DCF", 0, "/api/audit-operation/{id}", "审计操作服务", "根据主键获取", null, null, 1306532718346240480L },
                    { new Guid("09d15841-71c7-4afc-99f8-dd906a0248d9"), "6", 1, 1306994996674560480L, "根据多个字典类型编号获取所有字典的结果", false, "系统基础服务", false, false, "4B5955C39899A574748AEC9472EAD3DF", 0, "/api/code-type/code-dic-by-values", "字典类型管理", "根据多个字典类型编号获取所有字典的结果", null, null, null },
                    { new Guid("0a0cfc96-79d4-473e-aaef-91ae86c640d3"), "1", 1, 1306782482677760480L, "查找到所有数据", false, "系统基础服务", false, false, "F73EC4D6186B170DD30D0084F7CB1D04", 0, "/api/code-gen/all", "代码生成 - DB First", "查询所有", null, null, null },
                    { new Guid("0b605fe1-c77c-4735-8320-b8f400163ac9"), null, null, 1306532718346240480L, "根据主键逻辑删除", true, "用户中心服务", false, false, "02836036DDDF7900E5F5E9762F5E4229", 3, "/api/user/fake-delete/{id}", "用户服务", "逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("0b7a9ed1-86cc-42a6-a260-f7ba33054054"), null, null, 1306532718346240480L, "根据搜索条叫生成种子数据", true, "用户中心服务", false, false, "FD2CAFBFF34B435DF026315EF4D89CC5", 1, "/api/role/generate-seed-data", "角色服务", "生成种子数据", null, null, 1306532718346240480L },
                    { new Guid("0bc4b901-d9ee-4fac-91c7-e6ea6d6fb852"), "1", 1, 1306961911930880480L, "导出数据", true, "系统基础服务", false, false, "B802BC3519436726D4D39F9D6C8E0F72", 1, "/api/code/export", "字典管理", "导出", null, null, null },
                    { new Guid("0c1058f6-a425-40fe-bc5e-47fbf91ca7bd"), "6", 1, 1307019897794560480L, "根据搜索条叫生成种子数据", true, "用户中心服务", false, false, "746BEA6EC665547A8A7166B62497998B", 1, "/api/tenant/generate-seed-data", "租户服务", "生成种子数据", null, null, null },
                    { new Guid("0c6f2138-e984-4fba-ad2a-2890716a7259"), null, null, 1306532718346240480L, "更新用户的头像", true, "用户中心服务", false, false, "FEBD6097BE29268FDFDC295C98A9AD9F", 2, "/api/user/avatar", "用户服务", "更新头像", null, null, 1306532718346240480L },
                    { new Guid("0d2df690-6aa7-466b-b1e4-73fa4fda1b5d"), null, null, 1306532718346240480L, "根据主键锁定或解锁数据（必须有IsLock才能生效）", true, "用户中心服务", false, false, "8AED5C0B53588415D98E97119880AC6A", 2, "/api/dept/{id}/lock/{islocked}", "部门服务", "锁定", null, null, 1306532718346240480L },
                    { new Guid("0d2e0194-2238-457b-aab0-9b3259cc4ed9"), null, null, 1306532718346240480L, "给用户设置角色", true, "用户中心服务", false, false, "A843DEF0CDD97A394996DCF7C5E80F5B", 1, "/api/user/{userid}/role", "用户服务", "设置角色", null, null, 1306532718346240480L },
                    { new Guid("0d899b61-e2ba-4d0d-b2fd-83dad377ed78"), null, null, 1306532718346240480L, "根据主键查找一条数据", false, "通知系统服务", false, false, "336B54D6E3393F56F6C35FCA416A3EE5", 0, "/api/announcement/{id}", "公告服务", "根据主键获取", null, null, null },
                    { new Guid("0f372dde-1e65-441a-b002-eee8b2e1a1f9"), null, null, 1306532718346240480L, "根据多个主键批量删除", true, "系统基础服务", false, false, "13457B9CA71646A02E6F004CE877A0E6", 1, "/api/audit-entity/deletes", "审计数据服务", "批量删除", null, null, 1306532718346240480L },
                    { new Guid("0f43ba88-09f0-4b8d-83ef-e7747a3c9ed2"), "1", 1, 1306782482452480480L, "{codegenid}", false, "系统基础服务", false, false, "DD83F39265552523A9C35427C3E90DCF", 0, "/api/code-gen-config/code-gen-configs-by-code-gen-id/{codegenid}", "代码生成配置 - DB First", "获取代码生成配置", null, null, null },
                    { new Guid("0ffffde0-ddba-4632-b083-e191e3f71b36"), "1", 1, 1306961911889920480L, "根据分页参数，分页获取数据", false, "系统基础服务", false, false, "FE1D0FCB38385E82E9DBE4111184F797", 0, "/api/code/page/{pageindex}/{pagesize}", "字典管理", "分页查询", null, null, null },
                    { new Guid("10190ac3-1092-49a9-8ad2-313454b40447"), null, null, 1306532718346240480L, "根据多个主键批量逻辑删除", true, "系统基础服务", false, false, "17B55B877E0FB6704577EA356573BBC3", 1, "/api/attachment/fake-deletes", "附件服务", "批量逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("10fc92a8-30ed-4536-a995-c7af8e5548a1"), null, null, 1306532718346240480L, "根据主键查找一条数据", false, "系统基础服务", false, false, "6E9E5AA61727C2BD1E4142F0ED0F9DC5", 0, "/api/resource/{id}", "资源服务", "根据主键获取", null, null, 1306532718346240480L },
                    { new Guid("1295aed2-ae71-411f-9542-d50f75432840"), null, null, 1306532718346240480L, "搜索数据", false, "系统基础服务", false, false, "24C5B533C5DDC7D494830FF5E28F6EC2", 1, "/api/resource/search", "资源服务", "搜索", null, null, 1306532718346240480L },
                    { new Guid("12dbe1a6-7d23-48a4-bacb-164f0403d0f4"), null, null, 1306532718346240480L, "根据多个主键批量逻辑删除", true, "系统基础服务", false, false, "87F7F066F0A0605D1DB5CE8B7286E0CB", 1, "/api/audit-entity/fake-deletes", "审计数据服务", "批量逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("138283bd-f2ee-4b3b-b268-a12185264103"), null, null, 1306532718346240480L, null, true, "系统基础服务", false, false, "715A826DCD331B3155650A79BE0015D8", 3, "/api/image-verify-code/{key}", "图片验证码服务", "移除验证码", null, null, 1306532718346240480L },
                    { new Guid("1562071d-e18c-4d29-a854-12a562961140"), null, null, 1306532718346240480L, "查找到所有数据", false, "用户中心服务", false, false, "9B0AD48E75A6C37EDC7101236F93CF77", 0, "/api/user/all", "用户服务", "查询所有", null, null, 1306532718346240480L },
                    { new Guid("16517409-c055-447b-8e91-7155537c6d15"), null, null, 1306532718346240480L, "添加一条数据", true, "用户中心服务", false, false, "F57997ED31483BE396EB71C98D07B6F5", 1, "/api/role", "角色服务", "添加", null, null, 1306532718346240480L },
                    { new Guid("17388acc-2ef7-487d-bcef-fe4c6a708ecd"), "6", 1, 1307019897794560480L, "导出数据", true, "用户中心服务", false, false, "9880EDB9BD13EEB5B77C89EFBEE1CDAB", 1, "/api/tenant/export", "租户服务", "导出", null, null, null },
                    { new Guid("17e7dc0d-b36a-48e6-9e5f-645baf674299"), "1", 1, 1306782482493440480L, "根据主键逻辑删除", true, "系统基础服务", false, false, "60BB1B471DADBD43F1A6E69E7233BE6A", 3, "/api/code-gen-config/fake-delete/{id}", "代码生成配置 - DB First", "逻辑删除", null, null, null },
                    { new Guid("189016af-1473-4107-9d91-67d25f5c4dfd"), "6", 1, 1307019897774080480L, "根据多个主键批量删除", true, "用户中心服务", false, false, "B1885DC2864312A61C6427637A0E7FB4", 1, "/api/tenant/deletes", "租户服务", "批量删除", null, null, null },
                    { new Guid("18f4fc9a-8670-4fcb-a13b-03aec85c673a"), "1", 1, 1306782482452480480L, "save-all", true, "系统基础服务", false, false, "E557654AE20986A0E2E7297D32D66170", 1, "/api/code-gen-config/save-all", "代码生成配置 - DB First", "保存", null, null, null },
                    { new Guid("1adcacf3-33ae-4b36-b5c9-dcd95151ef3a"), "4", 1, 1306984506163200480L, "my-session-messages", false, "通知系统服务", false, false, "4CC387634C5025C3FEE188125B33C9D2", 0, "/api/wo-chat-im/my-session-messages", "Im聊天服务", "获取会话消息列表", null, null, null },
                    { new Guid("1c6dfb26-4149-4fa3-a7de-083ad7ff7d6c"), null, null, 1306532718346240480L, "移除当前用户token", true, "用户中心服务", false, false, "34925D025D1D97104B7A51EF41C393F3", 3, "/api/account/current-user-refresh-token", "用户账户认证授权服务", "移除当前用户token", null, null, 1306532718346240480L },
                    { new Guid("1d325e63-3e9e-4cbc-b275-00a057c71e63"), null, null, 1306532718346240480L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统基础服务", false, false, "4548CF61B82D6B6ED737DE1D568D5E7B", 0, "/api/email-server-config/all-usable", "邮件服务器配置服务", "查询所有可以用的", null, null, 1306532718346240480L },
                    { new Guid("1d994e50-d40a-465b-8445-646041a8131a"), null, null, 1306532718346240480L, "根据操作审计ID获取数据审计", false, "系统基础服务", false, false, "26806445F59D861F9FDB9F91B164A1CD", 0, "/api/audit-operation/{operationid}/audit-entity", "审计操作服务", "根据操作审计ID获取数据审计", null, null, 1306532718346240480L },
                    { new Guid("1df3723c-f435-4fba-a751-7a63e5b98cfe"), "1", 1, 1306782482575360480L, "导出数据", true, "系统基础服务", false, false, "9428EC584D39E7558884C1BE60B6325C", 1, "/api/code-gen-config/export", "代码生成配置 - DB First", "导出", null, null, null },
                    { new Guid("1ef3b8a8-6e46-49d7-9a7e-f63137beaade"), null, null, 1306532718346240480L, "添加一条数据", true, "系统基础服务", false, false, "DD71B200E8B3E6E24BD6F9C05E3D666C", 1, "/api/email-server-config", "邮件服务器配置服务", "添加", null, null, 1306532718346240480L },
                    { new Guid("1f164c01-cd48-4b41-8c87-0cd7e5dc1973"), "1", 1, 1306961911930880480L, "添加一条数据", true, "系统基础服务", false, false, "34F97902F9A851C8D126DB33A4C0C0BD", 1, "/api/code-type", "字典类型管理", "添加", "6", 1, 1306994996695040480L },
                    { new Guid("1f60193e-30ed-412b-8afd-f8f1f8d45703"), "1", 1, 1306782482554880480L, "根据主键锁定或解锁数据（必须有IsLock才能生效）", false, "系统基础服务", false, false, "E6AA2D3457A2946923C9B1BBF4092E42", 2, "/api/code-gen-config/{id}/lock/{islocked}", "代码生成配置 - DB First", "锁定", null, null, null },
                    { new Guid("1fe857c9-c027-4ca3-b8f8-21ec2c1f5cde"), null, null, 1306532718346240480L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统基础服务", false, false, "280713DC4618277C7BF307117835ED7B", 0, "/api/audit-entity/all-usable", "审计数据服务", "查询所有可以用的", null, null, 1306532718346240480L },
                    { new Guid("20b44c15-481f-4bba-8905-3e5f983927b0"), null, null, 1306532718346240480L, "登录接口", true, "用户中心服务", false, false, "6050B0AE0242E8D1D8A6B5B0EAFFA1E0", 1, "/api/client/login", "客户端服务", "登录", null, null, 1306532718346240480L },
                    { new Guid("20b7e3c2-1ab5-4a5e-993e-e5599a583fdd"), null, null, 1306532718346240480L, "添加一条数据", true, "系统基础服务", false, false, "7E91BA4770C4FDF6B865C2D4C7984132", 1, "/api/login-token", "用户登录TOKEN服务", "添加", null, null, 1306532718346240480L },
                    { new Guid("213eb6ca-e125-4181-b5bd-3e5666d3e8c9"), "1", 1, 1306961911910400480L, "搜索数据", false, "系统基础服务", false, false, "874CCECCA57B5B0C884223B9B77FB6CA", 1, "/api/code/search", "字典管理", "搜索", null, null, null },
                    { new Guid("22f82d98-9176-4391-a950-1d1d01e105fd"), "6", 1, 1307019897753600480L, "根据主键查找一条数据", false, "用户中心服务", false, false, "723A65C5AE34149B05DCF8A128CBC74A", 0, "/api/tenant/{id}", "租户服务", "根据主键获取", null, null, null },
                    { new Guid("23cd301c-ebbb-4715-a96d-72232d9a6927"), "5", 1, 1307030674636800480L, "resources", false, "用户中心服务", false, false, "93FF4A02546A74DA1406F440D9C43006", 0, "/api/tenant/{tenantid}/resources", "租户服务", "获取租户资源列表", null, null, null },
                    { new Guid("2428c3c3-740e-45fc-9047-5a2be3c9cd70"), null, null, 1306532718346240480L, "根据主键删除一条数据", true, "用户中心服务", false, false, "FBAC1FD6280B05C7EAFD6BD24F0DE077", 3, "/api/user/{id}", "用户服务", "删除", null, null, 1306532718346240480L },
                    { new Guid("2502e6ae-879b-4674-a557-cd7b4de891a7"), null, null, 1306532718346240480L, "根据主键查找一条数据", false, "用户中心服务", false, false, "213D1BBDB567A74636ACE841D780F663", 0, "/api/dept/{id}", "部门服务", "根据主键获取", null, null, 1306532718346240480L },
                    { new Guid("25bad725-529b-4a67-814a-1a6171a4b6d1"), null, null, 1306532718346240480L, "搜索数据", false, "通知系统服务", false, false, "9AD6BA02957A5D79C763F37FC7350C1F", 1, "/api/announcement/search", "公告服务", "搜索", null, null, null },
                    { new Guid("269fe59d-473c-44c5-aadf-54553cb5e059"), "1", 1, 1306961911992320480L, "根据主键锁定或解锁数据（必须有IsLock才能生效）", true, "系统基础服务", false, false, "67BAAA6E6062B824EFCBADF73BCB1587", 2, "/api/code-type/{id}/lock/{islocked}", "字典类型管理", "锁定", "6", 1, 1306994996736000480L },
                    { new Guid("26d95428-ebbd-4bf2-9bcc-2eeec4263bd5"), null, null, 1306532718346240480L, "根据多个主键批量逻辑删除", true, "系统基础服务", false, false, "006AC2DA9C0126A631FE4092AAB706C0", 1, "/api/email-server-config/fake-deletes", "邮件服务器配置服务", "批量逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("27d997d7-e691-4dbe-b5e0-74acbca53d98"), "4", 1, 1306984506204160480L, "disable-session-send-message", true, "通知系统服务", false, false, "DE3E8E8CE4BC746D5DF73DA399E0D30D", 1, "/api/wo-chat-im/disable-session-send-message", "Im聊天服务", "关闭会话消息发送权限", null, null, null },
                    { new Guid("28f6b60c-e528-4fd7-9ce9-b1c87452514b"), "1", 1, 1306961911869440480L, "根据多个主键批量删除", true, "系统基础服务", false, false, "7B25BA78214DA9C788A6A0A32E41AEEE", 1, "/api/code/deletes", "字典管理", "批量删除", null, null, null },
                    { new Guid("2a670df1-f01c-4cdb-b084-a46fdb339ced"), null, null, 1306532718346240480L, "根据主键删除一条数据", true, "系统基础服务", false, false, "097D7A323BFFCA32788EAA8C6BDB5157", 3, "/api/function/{id}", "功能服务", "删除", null, null, 1306532718346240480L },
                    { new Guid("2bf3ff67-c1a3-4426-8320-11839daa0a81"), null, null, 1306532718346240480L, "搜索数据", false, "系统基础服务", false, false, "E4979CA111E299FA747D5A547C6E4A99", 1, "/api/email-template/search", "邮件模板服务", "搜索", null, null, 1306532718346240480L },
                    { new Guid("2bf807cd-7d48-40bd-839b-fdd71f419711"), null, null, 1306532718346240480L, "根据主键删除一条数据", true, "系统基础服务", false, false, "38B9A961DB74BD743A3B5D434B2EB66A", 3, "/api/email-template/{id}", "邮件模板服务", "删除", null, null, 1306532718346240480L },
                    { new Guid("2c3ec3c9-76c7-4d29-953f-e7430f22577b"), null, null, 1306532718346240480L, "根据主键逻辑删除", true, "用户中心服务", false, false, "BBF7B9CA0FE646DBAE2923B70DA8A7A4", 3, "/api/role/fake-delete/{id}", "角色服务", "逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("2ea4faea-ec29-4383-833b-b5dedaa1b735"), null, null, 1306532718346240480L, "根据分页参数，分页获取数据", false, "系统基础服务", false, false, "1B1FD29D8E0A4A89B600CAA46C82B02F", 0, "/api/email-template/page/{pageindex}/{pagesize}", "邮件模板服务", "分页查询", null, null, 1306532718346240480L },
                    { new Guid("2f820c7f-4f1c-4737-aae6-329585c75d92"), null, null, 1306532718346240480L, "根据主键查找一条数据", false, "系统基础服务", false, false, "271DFDC5E142CFE1AF0C4200C6DC060A", 0, "/api/attachment/{id}", "附件服务", "根据主键获取", null, null, 1306532718346240480L },
                    { new Guid("2fe5043f-fd43-45f7-a60b-e76865778c2a"), "1", 1, 1306961912012800480L, "根据搜索条叫生成种子数据", true, "系统基础服务", false, false, "304CB45DB11EEBFDDE5E493F9BADE678", 1, "/api/code-type/generate-seed-data", "字典类型管理", "生成种子数据", "6", 1, 1306994996736000480L },
                    { new Guid("31896c5d-2ed7-4e43-a952-4edc076d29d0"), null, null, 1306532718346240480L, "根据多个主键批量逻辑删除", true, "系统基础服务", false, false, "CBB8DE4E5D6DD206685DA33E90EF1EE1", 1, "/api/email-template/fake-deletes", "邮件模板服务", "批量逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("332d731a-e209-48da-84e9-508288efbf22"), "6", 1, 1306994995814400480L, "refresh-code-util-cache", true, "系统基础服务", false, false, "84729CE5CCEDE2B91418572851547D7A", 1, "/api/code-type/refresh-code-util-cache", "字典类型管理", "刷新字典工具缓存", "6", 1, 1306994996695040480L },
                    { new Guid("333a3802-f8d2-4625-9476-dee8bf43fd0d"), "4", 1, 1306984506183680480L, "send-message", true, "通知系统服务", false, false, "4AB01742CF4A20C9238B7BA892732792", 1, "/api/wo-chat-im/send-message", "Im聊天服务", "发送消息到会话", null, null, null },
                    { new Guid("333edf31-c542-4fa1-baca-b770d558a4d7"), null, null, 1306532718346240480L, "根据主键删除一条数据", true, "用户中心服务", false, false, "EA96F9C3B67BB0EB8E3D5337D3482162", 3, "/api/dept/{id}", "部门服务", "删除", null, null, 1306532718346240480L },
                    { new Guid("336b98be-e9f1-4f42-824b-a9a3b91350c5"), null, null, 1306532718346240480L, "根据搜索条叫生成种子数据", true, "系统基础服务", false, false, "7A0F189854BBC9084AE004012A7870E9", 1, "/api/login-token/generate-seed-data", "用户登录TOKEN服务", "生成种子数据", null, null, 1306532718346240480L },
                    { new Guid("337bae83-a083-4e0e-8ceb-2bb21ae22145"), null, null, 1306532718346240480L, "根据多个主键批量逻辑删除", true, "用户中心服务", false, false, "32ABBEA6610DE2420AC7B5E7FDAA315E", 1, "/api/dept/fake-deletes", "部门服务", "批量逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("33c2157a-884d-4030-abea-a9aeea51fdf8"), null, null, 1306532718346240480L, "更新一条数据", true, "系统基础服务", false, false, "CDA3DE9664774C06E0D86F62F2FCDDE2", 2, "/api/email-template", "邮件模板服务", "更新", null, null, 1306532718346240480L },
                    { new Guid("3402d3b2-cf24-4634-a65c-534f96e2991a"), null, null, 1306532718346240480L, "根据多个主键批量删除", true, "用户中心服务", false, false, "5C9E8B48C5C77A0CEB8E6A853D56A808", 1, "/api/user/deletes", "用户服务", "批量删除", null, null, 1306532718346240480L },
                    { new Guid("3790cc0d-dc3a-4669-acba-3a90812c6386"), null, null, 1306532718346240480L, "查看用户角色", false, "用户中心服务", false, false, "652940681CC97C52299C95242AB1E858", 0, "/api/user/{userid}/roles", "用户服务", "查看用户角色", null, null, 1306532718346240480L },
                    { new Guid("383c5aaf-a3e1-44d1-a1c8-3074abe55f95"), null, null, 1306532718346240480L, "根据多个主键批量删除", true, "用户中心服务", false, false, "91B03FFD3080A9684592C45A15C826A5", 1, "/api/role/deletes", "角色服务", "批量删除", null, null, 1306532718346240480L },
                    { new Guid("383f8a77-378d-415a-a11d-1e10cc09bd6b"), "1", 1, 1306782482657280480L, "根据多个主键批量删除", true, "系统基础服务", false, false, "A657348B3DA465884F1DB1275C763DA7", 1, "/api/code-gen/deletes", "代码生成 - DB First", "批量删除", null, null, null },
                    { new Guid("38545a67-61ff-4e5c-90bb-a555a93fcbea"), null, null, 1306532718346240480L, "获取当前用户信息", false, "用户中心服务", false, false, "2FAAF199BA16D914E7796C0B65B7CD13", 0, "/api/account/current-user", "用户账户认证授权服务", "获取当前用户信息", null, null, 1306532718346240480L },
                    { new Guid("3896ea42-a5ed-4bc5-8dc5-21e0e5adb2fa"), null, null, 1306532718346240480L, null, true, "系统基础服务", false, false, "88BF07EAB2CA231DE36CF2C1A2D2546D", 3, "/api/email-verify-code/{key}", "邮件验证码服务", "移除验证码", null, null, 1306532718346240480L },
                    { new Guid("38c69230-1ed0-413e-9ae6-05bc1ef989e0"), null, null, 1306532718346240480L, "分配权限（重置）", true, "用户中心服务", false, false, "2BBD7196A51542F56FAC25FF3D760D21", 1, "/api/role/{roleid}/resource", "角色服务", "分配权限", null, null, 1306532718346240480L },
                    { new Guid("39421a19-9cbf-477b-baea-34f40341357f"), null, null, 1306532718346240480L, "根据主键逻辑删除", true, "系统基础服务", false, false, "FB316294679817930CABB93BE346C453", 3, "/api/email-server-config/fake-delete/{id}", "邮件服务器配置服务", "逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("39ccceae-2cba-4cd2-a44b-fc8fe8a3f2e4"), null, null, 1306532718346240480L, "查看用户权限", false, "用户中心服务", false, false, "FAA3B104E6EBF3B5F16DB92C56836A63", 0, "/api/user/{userid}/resources", "用户服务", "查看用户权限", null, null, 1306532718346240480L },
                    { new Guid("3a679983-7a4e-4c1c-b3e0-8b374f6ebc91"), "1", 1, 1306782482595840480L, "code-gen", true, "系统基础服务", false, false, "FDD5BB4E7994DBDDAC2764230F31BC75", 2, "/api/code-gen", "代码生成 - DB First", "更新", null, null, null },
                    { new Guid("3a6f74c2-0165-46b0-8cd5-1846846d97bc"), null, null, 1306532718346240480L, null, false, "用户中心服务", false, false, "BB9E3C06F2507147FADEA21712CB70CA", 0, "/api/client/{id}/functions", "客户端服务", "根据客户端编号获取绑定的接口列表", null, null, null },
                    { new Guid("3ac59980-d2df-4363-b8db-a4d043e362e7"), null, null, 1306532718346240480L, "根据主键查找一条数据", false, "系统基础服务", false, false, "2E69B891C48D1F7E7974825E470447DC", 0, "/api/email-template/{id}", "邮件模板服务", "根据主键获取", null, null, 1306532718346240480L },
                    { new Guid("3c68f73b-5a83-4429-9046-4fe33473739f"), null, null, 1306532718346240480L, "根据主键逻辑删除", true, "通知系统服务", false, false, "9D50105E0F127FF7F9C12C9EC2643787", 3, "/api/announcement/fake-delete/{id}", "公告服务", "逻辑删除", null, null, null },
                    { new Guid("3e2f4464-6b69-4a00-acfb-d39184729cdd"), null, null, 1306532718346240480L, "根据分页参数，分页获取数据", false, "用户中心服务", false, false, "5FFF46E52DE5943FA225B0F6E29A338D", 0, "/api/user/page/{pageindex}/{pagesize}", "用户服务", "分页查询", null, null, 1306532718346240480L },
                    { new Guid("3ed89bcc-7eb1-4b51-86a5-dbe449370e1b"), null, null, 1306532718346240480L, "根据多个主键批量删除", true, "系统基础服务", false, false, "4FBBC2EF99F020CC28878731394CF303", 1, "/api/email-server-config/deletes", "邮件服务器配置服务", "批量删除", null, null, 1306532718346240480L },
                    { new Guid("416fe54b-6c50-4b1b-bf77-6744cf19fa72"), null, null, 1306532718346240480L, "搜索数据", false, "系统基础服务", false, false, "9A316E9E6A41D1F57870A5F0CDDC93EF", 1, "/api/function/search", "功能服务", "搜索", null, null, 1306532718346240480L },
                    { new Guid("424fd96a-a889-4ff9-910a-25a59204d2ec"), null, null, 1306532718346240480L, "返回根节点资源", false, "系统基础服务", false, false, "34CFCB2759472E91321739C5D43B00D0", 0, "/api/resource/root", "资源服务", "返回根节点", null, null, 1306532718346240480L },
                    { new Guid("42636f5d-d5f6-4f64-bd2b-e77d80e51ff2"), "4", 1, 1306984506183680480L, "私聊隐藏，群聊自己创建的话直接解散，不是就退出", true, "通知系统服务", false, false, "9614EDD6363C8F365B3728C29B22CF50", 1, "/api/wo-chat-im/quit-my-im-session", "Im聊天服务", "退出会话", null, null, null },
                    { new Guid("428421b4-bb3e-49c9-91ce-74da66d3729b"), "1", 1, 1306961911971840480L, "查找到所有数据", false, "系统基础服务", false, false, "E326F4D7B7330B9E249A6BEBEF7C6E59", 0, "/api/code-type/all", "字典类型管理", "查询所有", "6", 1, 1306994996715520480L },
                    { new Guid("42b3486a-8ea0-4296-a526-7cd3ef9ea73a"), null, null, 1306532718346240480L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统基础服务", false, false, "6B7B11626AE0ABB28C5331DB67DACAA0", 0, "/api/attachment/all-usable", "附件服务", "查询所有可以用的", null, null, 1306532718346240480L },
                    { new Guid("438d1c2f-f0fa-4975-bac5-13c5f3a2645b"), "1", 1, 1306782482636800480L, "open-code-gen-folder", true, "系统基础服务", false, false, "B3D81BD31F37628B71C6B6FFDF3D719E", 1, "/api/code-gen/open-code-gen-folder", "代码生成 - DB First", "打开代码目录", null, null, null },
                    { new Guid("45dd0581-3394-4c0a-bb8e-c9e0074d5611"), null, null, 1306532718346240480L, "更新一条数据", true, "系统基础服务", false, false, "CE39C474540DD96EAF373115B164EDC7", 2, "/api/resource", "资源服务", "更新", null, null, 1306532718346240480L },
                    { new Guid("46aef5bc-9d0f-4a05-b21d-747753b98569"), null, null, 1306532718346240480L, "根据主键逻辑删除", true, "系统基础服务", false, false, "30759F98C0CF4A34813C280451C2E4CF", 3, "/api/audit-entity/fake-delete/{id}", "审计数据服务", "逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("475207d6-4c0b-4054-a051-7315295694a1"), null, null, 1306532718346240480L, "添加一条数据", true, "系统基础服务", false, false, "44405A33B9DEC6F934920AF5AC6F7111", 1, "/api/audit-entity", "审计数据服务", "添加", null, null, 1306532718346240480L },
                    { new Guid("4795ae43-0d52-42f1-8aaf-fc6e6412ac1b"), null, null, 1306532718346240480L, null, true, "系统基础服务", false, false, "A3F388958310A592E004DDD848AB0CB7", 1, "/api/image-verify-code/verify", "图片验证码服务", "验证验证码", null, null, 1306532718346240480L },
                    { new Guid("483956b8-4651-4dea-8c34-501108220cc1"), "1", 1, 1306961911848960480L, "根据主键查找一条数据", false, "系统基础服务", false, false, "B732D3822802617A15E93D2BC1872E8D", 0, "/api/code/{id}", "字典管理", "根据主键获取", null, null, null },
                    { new Guid("4963631e-6343-469a-a189-10bfce6e3195"), null, null, 1306532718346240480L, null, true, "用户中心服务", false, false, "1ADEBA08C209B9D06D9D6788FB0509E6", 3, "/api/client-function/{clientid}/{functionid}", "客户端与接口关系服务", "删除客户端与接口关系", null, null, 1306532718346240480L },
                    { new Guid("498638f7-dc92-4d0e-ac5e-26e48cf87a8d"), null, null, 1306532718346240480L, "搜索数据", false, "用户中心服务", false, false, "4B11C588FC856C862E41859F189370C0", 1, "/api/role/search", "角色服务", "搜索", null, null, 1306532718346240480L },
                    { new Guid("4a127124-6348-4db1-aa38-5f3af2c8efdf"), null, null, 1306532718346240480L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统基础服务", false, false, "A99DB9777E1C5C11D2FA6A8957F696E8", 0, "/api/audit-operation/all-usable", "审计操作服务", "查询所有可以用的", null, null, 1306532718346240480L },
                    { new Guid("4b57474a-88b4-4393-bb49-4b59e8c3c41d"), null, null, 1306532718346240480L, "根据主键逻辑删除", true, "系统基础服务", false, false, "A401312992835BA902C0CFDC5FEEE1F3", 3, "/api/function/fake-delete/{id}", "功能服务", "逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("4b7e7f68-8925-4b5c-b8d2-8a51df917b0c"), null, null, 1306532718346240480L, "根据分页参数，分页获取数据", false, "系统基础服务", false, false, "72DE329278C26111EB3F431ACB89B0A4", 0, "/api/audit-entity/page/{pageindex}/{pagesize}", "审计数据服务", "分页查询", null, null, 1306532718346240480L },
                    { new Guid("4c1b9201-09e6-421f-95d1-d98d009a3417"), null, null, 1306532718346240480L, "根据主键查找一条数据", false, "用户中心服务", false, false, "8B2F2030F705698FEA9D98536F415ADD", 0, "/api/client/{id}", "客户端服务", "根据主键获取", null, null, 1306532718346240480L },
                    { new Guid("4c206f6e-5e3a-4c96-b48c-81d6df44c9af"), "1", 1, 1306961911930880480L, "更新一条数据", true, "系统基础服务", false, false, "4E8341596E72B28095CC42F5E4D4A614", 2, "/api/code-type", "字典类型管理", "更新", "6", 1, 1306994996695040480L },
                    { new Guid("4c4b9d0c-6137-437c-8b04-833c73e8a70d"), "1", 1, 1306782482472960480L, "根据主键查找一条数据", false, "系统基础服务", false, false, "324BB8D9B949D3E0BF9A1A1B4A346CC1", 0, "/api/code-gen-config/{id}", "代码生成配置 - DB First", "根据主键获取", null, null, null },
                    { new Guid("4d51608e-5988-4d3d-8f5e-00e0c0c07b02"), null, null, 1306532718346240480L, "查找到所有数据", false, "系统基础服务", false, false, "7971E7E4FDCB5CBA6EE06E7DFE3F199E", 0, "/api/resource/all", "资源服务", "查询所有", null, null, 1306532718346240480L },
                    { new Guid("4e1a2966-bdfd-485a-b0cf-52004e40f6a7"), null, null, 1306532718346240480L, "查找到所有数据", false, "用户中心服务", false, false, "0730ED2F37C050E4994609C45BE0C4A4", 0, "/api/dept/all", "部门服务", "查询所有", null, null, 1306532718346240480L },
                    { new Guid("4e85ec32-8a3c-46e9-ba60-4abd7bee6745"), "2", 1, 1306966748139520480L, "根据多个字典类型编号获取所有字典的结果", false, "系统基础服务", false, false, "79DE9C21EF2535EFF7ACDCFAD9E9F2E9", 0, "/api/code-type/code-dic", "字典类型管理", "根据多个字典类型编号获取所有字典的结果", "6", 1, 1306994996674560480L },
                    { new Guid("513f19e0-c96f-49e6-b763-734527548dc3"), "1", 1, 1306782482554880480L, "搜索数据", true, "系统基础服务", false, false, "2498E3052239D4287BEA0D8B3F21F50A", 1, "/api/code-gen-config/search", "代码生成配置 - DB First", "搜索", null, null, null },
                    { new Guid("5328608a-6b71-4507-a52a-e1beffa7a4ab"), null, null, 1306532718346240480L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统基础服务", false, false, "33D096038DC823412DC051FA7371FB68", 0, "/api/login-token/all-usable", "用户登录TOKEN服务", "查询所有可以用的", null, null, 1306532718346240480L },
                    { new Guid("542d6de4-1b2c-4820-8f8c-b6fa17c023aa"), null, null, 1306532718346240480L, "根据搜索条叫生成种子数据", true, "系统基础服务", false, false, "6C10A6FBF3AD17499C371C48E0FEF6D6", 1, "/api/attachment/generate-seed-data", "附件服务", "生成种子数据", null, null, 1306532718346240480L },
                    { new Guid("547d94df-965f-4833-88fb-c3f66244926c"), "1", 1, 1306552940953600480L, "导出数据", true, "系统基础服务", false, false, "B0929232FD86E71F313EBB348D60B23A", 1, "/api/audit-entity/export", "审计数据服务", "导出", null, null, null },
                    { new Guid("5604fcc2-595f-4cc5-b0b8-c0d75a4c9351"), null, null, 1306532718346240480L, "根据主键锁定或解锁数据（必须有IsLock才能生效）", true, "系统基础服务", false, false, "ADF53A6D1C062BF2CC40EBDE20D8E841", 2, "/api/attachment/{id}/lock/{islocked}", "附件服务", "锁定", null, null, 1306532718346240480L },
                    { new Guid("56871df3-2984-4d02-8ff7-61101f0abc19"), "1", 1, 1306961911971840480L, "根据多个主键批量逻辑删除", true, "系统基础服务", false, false, "E08C5E40CD258CEC1CBC279A8A56E26F", 1, "/api/code-type/fake-deletes", "字典类型管理", "批量逻辑删除", "6", 1, 1306994996715520480L },
                    { new Guid("571200a8-bde2-430b-84ea-743db7b282cd"), null, null, 1306532718346240480L, "根据主键逻辑删除", true, "系统基础服务", false, false, "A094FB1391CD1E7F7A2C7E8536A491DF", 3, "/api/login-token/fake-delete/{id}", "用户登录TOKEN服务", "逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("590cd04c-025c-4cc1-bdd1-e9cea201bb46"), null, null, 1306532718346240480L, "根据分页参数，分页获取数据", false, "系统基础服务", false, false, "67C405B4CBCC02144945800F26CC1F4F", 0, "/api/function/page/{pageindex}/{pagesize}", "功能服务", "分页查询", null, null, 1306532718346240480L },
                    { new Guid("595922c1-5099-4c6f-946f-ed3a7af7d7d3"), "1", 1, 1306961911869440480L, "根据主键逻辑删除", true, "系统基础服务", false, false, "E4E1DDA22B1458D2372BEE9FB3ED124A", 3, "/api/code/fake-delete/{id}", "字典管理", "逻辑删除", null, null, null },
                    { new Guid("5c8381ec-7e8a-4060-9c04-83032d18872c"), null, null, 1306532718346240480L, "根据主键逻辑删除", true, "用户中心服务", false, false, "A5DA0BB6BEA388B99626E5A34BDE68F4", 3, "/api/dept/fake-delete/{id}", "部门服务", "逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("5d67bd9d-853c-4e16-973d-be0511241fc0"), null, null, 1306532718346240480L, "根据多个主键批量删除", true, "用户中心服务", false, false, "678276BC3559FA79B62455965C7229B8", 1, "/api/client/deletes", "客户端服务", "批量删除", null, null, 1306532718346240480L },
                    { new Guid("5df6ccb0-8985-4767-9ff2-b306d791194e"), "1", 1, 1306552940953600480L, "导出数据", true, "系统基础服务", false, false, "0F54F5EA3FD76F2067A9C26F6A1D139B", 1, "/api/audit-operation/export", "审计操作服务", "导出", null, null, null },
                    { new Guid("5e8adf52-8db2-4d56-9ff3-003cae13e0aa"), null, null, 1306532718346240480L, "根据多个主键批量逻辑删除", true, "用户中心服务", false, false, "F19E71A217BEEADDD5EF20B65D93439E", 1, "/api/role/fake-deletes", "角色服务", "批量逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("5eb48cf2-6c45-47c2-a68b-84284a389c69"), null, null, 1306532718346240480L, "根据多个主键批量删除", true, "系统基础服务", false, false, "F74128AC93B49FC04CB29781E17E5302", 1, "/api/resource/deletes", "资源服务", "批量删除", null, null, 1306532718346240480L },
                    { new Guid("5efd6ab4-a9d3-4742-9a48-fb54a1b1e463"), null, null, 1306532718346240480L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "用户中心服务", false, false, "D4F99E0AE4263D647F3440B66DB7AC7B", 0, "/api/role/all-usable", "角色服务", "查询所有可以用的", null, null, 1306532718346240480L },
                    { new Guid("61cc62e4-34da-4a0a-9899-488d3ab399fa"), null, null, 1306532718346240480L, "根据主键删除一条数据", true, "用户中心服务", false, false, "34B7575A20F0D8D6B1B2522F9DD7A7B8", 3, "/api/position/{id}", "岗位管理服务", "删除", null, null, 1306532718346240480L },
                    { new Guid("622c1a11-7dff-4318-9d21-b57fbd1da9ba"), null, null, 1306532718346240480L, "根据主键锁定或解锁数据（必须有IsLock才能生效）", true, "用户中心服务", false, false, "718DFD76BA4C2997D3DDA216BDB98369", 2, "/api/user/{id}/lock/{islocked}", "用户服务", "锁定", null, null, 1306532718346240480L },
                    { new Guid("63b4ad68-3fc7-46e3-93c3-1a9b87e18a85"), null, null, 1306532718346240480L, "通过刷新token获取新的token", true, "用户中心服务", false, false, "DF709DE63630893E744DA34D950EC7AE", 1, "/api/client/refresh-token", "客户端服务", "刷新Token", null, null, null },
                    { new Guid("63d7208e-45d3-406e-a4a1-c87e3afda04d"), null, null, 1306532718346240480L, "获取种子数据", false, "用户中心服务", false, false, "72B515FB99A1EFE42DEFCFC12954F93D", 0, "/api/role/role-resource-seed-data", "角色服务", "获取种子数据", null, null, 1306532718346240480L },
                    { new Guid("65a3c1ee-f5cf-48eb-9bf0-3d4db44257e4"), null, null, 1306532718346240480L, "根据主键逻辑删除", true, "用户中心服务", false, false, "BB0B0620A9F5665B13ADC8D8C8B8F98A", 3, "/api/position/fake-delete/{id}", "岗位管理服务", "逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("687b278e-aa4e-4d1c-95fd-f148eeb2a658"), "6", 1, 1307019897794560480L, "搜索数据", false, "用户中心服务", false, false, "5F3F0C211E6008478BC1B6AB08374C4C", 1, "/api/tenant/search", "租户服务", "搜索", null, null, null },
                    { new Guid("68ce42ff-acc7-485f-bc91-df471b520be7"), null, null, 1306532718346240480L, "查看当前用户角色", false, "用户中心服务", false, false, "7F3E99BDC443556613552A21A56D9B73", 0, "/api/account/current-user-roles", "用户账户认证授权服务", "查看用户角色", null, null, 1306532718346240480L },
                    { new Guid("69d12a63-87a3-48bf-b246-86acd30697f2"), "1", 1, 1306961911848960480L, "根据主键删除一条数据", true, "系统基础服务", false, false, "E0B6AFB00AB714406B8E6EB6A1C54C28", 3, "/api/code/{id}", "字典管理", "删除", null, null, null },
                    { new Guid("69f70da1-fb4e-443f-9efe-e3d12cc95eed"), null, null, 1306532718346240480L, "查找到所有数据", false, "用户中心服务", false, false, "88BAC4E29D23BD095207644BB397E5EE", 0, "/api/position/all", "岗位管理服务", "查询所有", null, null, 1306532718346240480L },
                    { new Guid("6a9763c9-c40f-44f3-a248-a3b1e3d1f586"), null, null, 1306532718346240480L, "搜索数据", false, "系统基础服务", false, false, "0C58B2617EA08ED81F14B53C00C678D7", 1, "/api/attachment/search", "附件服务", "搜索", null, null, 1306532718346240480L },
                    { new Guid("6aea8a77-edd2-444b-b8be-901d78321a49"), null, null, 1306532718346240480L, "根据多个主键批量逻辑删除", true, "用户中心服务", false, false, "3E010DCA7BAD6C3FCCCA32FB77F050F0", 1, "/api/user/fake-deletes", "用户服务", "批量逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("6b7f0b3c-c2ed-458e-8f26-abe68eb17854"), null, null, 1306532718346240480L, "根据搜索条叫生成种子数据", true, "用户中心服务", false, false, "A074E8CFB7457551C240FE7D510618AC", 1, "/api/position/generate-seed-data", "岗位管理服务", "生成种子数据", null, null, 1306532718346240480L },
                    { new Guid("6c9aa43e-921c-44bc-83fb-64a9c451255f"), null, null, 1306532718346240480L, "根据主键锁定或解锁数据（必须有IsLock才能生效）", true, "系统基础服务", false, false, "C0F3F05AE24A0E8BBA9BAF52852E09D4", 2, "/api/email-server-config/{id}/lock/{islocked}", "邮件服务器配置服务", "锁定", null, null, 1306532718346240480L },
                    { new Guid("6dc1a088-15f6-43b8-8465-3a95cc495bab"), null, null, 1306532718346240480L, "根据多个主键批量逻辑删除", true, "系统基础服务", false, false, "80A4CB99380D0D7A70F7C28604C5B0C7", 1, "/api/login-token/fake-deletes", "用户登录TOKEN服务", "批量逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("6e2ff941-1e79-4e2a-bbe9-ec68318e5d3a"), "1", 1, 1306582968770560480L, null, false, "用户中心服务", false, false, "98B6A43EC42A6B9F1D8257EE1D05E9BB", 0, "/api/dept/tree/{includelocked}", "部门服务", "获取所有部门数据，以树形结构返回", null, null, null },
                    { new Guid("6e8d08f8-ba2a-4697-8b69-ac5a5bb31bff"), null, null, 1306532718346240480L, null, true, "用户中心服务", false, false, "6C3EB756645619B25BF1323C05E781D8", 1, "/api/client-function", "客户端与接口关系服务", "添加客户端与接口关系", null, null, 1306532718346240480L },
                    { new Guid("70dd5b33-585d-4ed4-83b7-4e55f62298d1"), "6", 1, 1306994996695040480L, "根据字典类型值获取字典列表", false, "系统基础服务", false, false, "F1EB36795F9ABEFB53058F8A1B80E294", 0, "/api/code-type/{codetypevalue}/codes-by-value", "字典类型管理", "根据字典类型值获取字典列表", null, null, null },
                    { new Guid("713341f2-47e1-42af-b717-bfa75904d32e"), null, null, 1306532718346240480L, null, false, "用户中心服务", false, false, "3F2A1F37C00070D6D3EB4F27E24BB687", 0, "/api/account/current-user-resources", "用户账户认证授权服务", "获取用户资源", null, null, 1306532718346240480L },
                    { new Guid("715a2905-da23-405d-98a0-1a1222f7d101"), null, null, 1306532718346240480L, "根据主键查找一条数据", false, "用户中心服务", false, false, "DF0B66D0FC43BB25047A470707E01EF8", 0, "/api/position/{id}", "岗位管理服务", "根据主键获取", null, null, 1306532718346240480L },
                    { new Guid("7229563b-7311-41b8-947b-f07d58fa6c87"), null, null, 1306532718346240480L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统基础服务", false, false, "6C03A3540C36BB4BD1BB9F1606F0F550", 0, "/api/resource/all-usable", "资源服务", "查询所有可以用的", null, null, 1306532718346240480L },
                    { new Guid("724e4ba8-59ff-458a-a940-325f973827d0"), null, null, 1306532718346240480L, "根据搜索条叫生成种子数据", true, "系统基础服务", false, false, "25F7A33EC2479E4589E5A540765C3DA0", 1, "/api/audit-entity/generate-seed-data", "审计数据服务", "生成种子数据", null, null, 1306532718346240480L },
                    { new Guid("736fd9b6-b56a-4860-8a1c-9a077be886e3"), null, null, 1306532718346240480L, "根据主键锁定或解锁数据（必须有IsLock才能生效）", true, "系统基础服务", false, false, "18B1E82C7D5150FD3EDC3BB52FB3ACF9", 2, "/api/email-template/{id}/lock/{islocked}", "邮件模板服务", "锁定", null, null, 1306532718346240480L },
                    { new Guid("73cfe63f-3338-4bd0-a0b9-1b9cc39951ea"), null, null, 1306532718346240480L, "根据多个主键批量逻辑删除", true, "系统基础服务", false, false, "C53E746377386D224D0941DB8F4CB539", 1, "/api/audit-operation/fake-deletes", "审计操作服务", "批量逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("74a1a6b4-4559-48fc-8987-3287e5a4e056"), "6", 1, 1307019897753600480L, "更新一条数据", true, "用户中心服务", false, false, "18C3543AD49B3D3CB78C696A636FB1D5", 2, "/api/tenant", "租户服务", "更新", null, null, null },
                    { new Guid("752b1623-8898-431f-ab3f-db9ebffae4e6"), "4", 1, 1306984506142720480L, "im-group-sessions", false, "通知系统服务", false, false, "5DC2499F878F522E4C0DD78321F444DE", 0, "/api/wo-chat-im/im-group-sessions", "Im聊天服务", "获取会话列表", null, null, null },
                    { new Guid("75abfcbe-a00b-444f-baa6-503ae03b3434"), null, null, 1306532718346240480L, "根据搜索条叫生成种子数据", true, "系统基础服务", false, false, "53151648D7858D9061CC0D89B4EA43F5", 1, "/api/email-template/generate-seed-data", "邮件模板服务", "生成种子数据", null, null, 1306532718346240480L },
                    { new Guid("7a3399b3-6003-4aae-8e24-2e478992630e"), null, null, 1306532718346240480L, "添加一条数据", true, "用户中心服务", false, false, "1EB184263BA127C79364162F4E75E660", 1, "/api/position", "岗位管理服务", "添加", null, null, 1306532718346240480L },
                    { new Guid("7bb514a5-d62d-4ba1-a9b9-9e7756eaae2d"), null, null, 1306532718346240480L, "查找到所有数据", false, "系统基础服务", false, false, "D403D5F25D7ACA97A10BEF07B2A816F4", 0, "/api/audit-operation/all", "审计操作服务", "查询所有", null, null, 1306532718346240480L },
                    { new Guid("7cad69bf-2f23-44e8-b0ef-97bdc57fc6a4"), null, null, 1306532718346240480L, "根据分页参数，分页获取数据", false, "通知系统服务", false, false, "3034444DADCC535B882E3D20DA9E1904", 0, "/api/announcement/page/{pageindex}/{pagesize}", "公告服务", "分页查询", null, null, null },
                    { new Guid("7cb8921d-0a0c-4e80-8895-604c05480c43"), null, null, 1306532718346240480L, "根据分页参数，分页获取数据", false, "用户中心服务", false, false, "0BBAF9866F200FEDE526AB75E03319CC", 0, "/api/dept/page/{pageindex}/{pagesize}", "部门服务", "分页查询", null, null, 1306532718346240480L },
                    { new Guid("7da66506-ed83-40ec-97ad-5323e36af404"), null, null, 1306532718346240480L, "根据主键锁定或解锁数据（必须有IsLock才能生效）", true, "通知系统服务", false, false, "F5A7F4A1B3C633F14D21BE37F2D8F7FC", 2, "/api/announcement/{id}/lock/{islocked}", "公告服务", "锁定", null, null, null },
                    { new Guid("7e5577d4-32b2-4f43-a83f-05410b59b195"), null, null, 1306532718346240480L, "根据主键删除一条数据", true, "系统基础服务", false, false, "CCD570BA5C66619052354D738927A007", 3, "/api/audit-entity/{id}", "审计数据服务", "删除", null, null, 1306532718346240480L },
                    { new Guid("7f0d7abb-06a4-4a35-b4e3-7798b21e37fa"), null, null, 1306532718346240480L, "根据多个主键批量逻辑删除", true, "系统基础服务", false, false, "9C888321143AC3E991B72D3B32193A35", 1, "/api/resource/fake-deletes", "资源服务", "批量逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("7f36ba4f-ec97-4fa9-953b-fa2f1686c448"), null, null, 1306532718346240480L, "发送邮件", true, "系统基础服务", false, false, "2C72E2117E4F5092A5C6F2C807389D38", 1, "/api/email/send", "邮件服务", "发送邮件", null, null, 1306532718346240480L },
                    { new Guid("7fa014c4-08db-4f96-8132-2bf3db32b256"), null, null, 1306532718346240480L, "搜索数据", false, "系统基础服务", false, false, "5A7181978F26890284CE44ED28A2F7AA", 1, "/api/audit-entity/search", "审计数据服务", "搜索", null, null, 1306532718346240480L },
                    { new Guid("7faa6bc4-385b-4cdd-ad8b-4e1673fc893b"), "1", 1, 1306961912033280480L, "导出数据", true, "系统基础服务", false, false, "B0112E9E5DE1B2C88091175412D6BF7D", 1, "/api/code-type/export", "字典类型管理", "导出", "6", 1, 1306994996756480480L },
                    { new Guid("810235c8-01f5-4255-a30c-fd77ffeb6eab"), "1", 1, 1306961912012800480L, "搜索数据", true, "系统基础服务", false, false, "79EB05031A70D307DD782CED65E24E0F", 1, "/api/code-type/search", "字典类型管理", "搜索", "6", 1, 1306994996736000480L },
                    { new Guid("814304bb-22fe-4a33-82e1-8ad7c64bab4a"), null, null, 1306532718346240480L, "获取所有子资源", false, "系统基础服务", false, false, "C5668FD7C42E9FB532AB9CB2E1480E1F", 0, "/api/resource/{id}/children", "资源服务", "获取所有子资源", null, null, 1306532718346240480L },
                    { new Guid("8172d258-7a75-4ced-b5e2-b0be7350aa1f"), null, null, 1306532718346240480L, "添加一条数据", true, "用户中心服务", false, false, "8ECC90D5D58B7FD57A1D06C0F5C4CECA", 1, "/api/client", "客户端服务", "添加", null, null, 1306532718346240480L },
                    { new Guid("82635595-f7c8-45af-9aaa-534e53e53ff5"), "6", 1, 1307019897753600480L, "根据主键删除一条数据", true, "用户中心服务", false, false, "72760A5FE6E2516D899EF7C88273FEB1", 3, "/api/tenant/{id}", "租户服务", "删除", null, null, null },
                    { new Guid("8267679d-0482-4b2d-81ca-67a555f28b79"), "1", 1, 1306782482616320480L, "generate-code", true, "系统基础服务", false, false, "58F507AB025E3DD861EFE9AF5FAED7D3", 1, "/api/code-gen/generate-code", "代码生成 - DB First", "生成代码", null, null, null },
                    { new Guid("83cc7cb7-dac6-49f2-85fa-e903039f3d0a"), null, null, 1306532718346240480L, "搜索数据", false, "用户中心服务", false, false, "DA7F00498254B5B31B18D7C877F96FB7", 1, "/api/client/search", "客户端服务", "搜索", null, null, 1306532718346240480L },
                    { new Guid("841a3afa-a128-4751-b3b2-b2849da338e1"), null, null, 1306532718346240480L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "通知系统服务", false, false, "7DAD544022ECAB407CA07965FBDEC6AB", 0, "/api/announcement/all-usable", "公告服务", "查询所有可以用的", null, null, null },
                    { new Guid("841c572c-5098-4e72-a590-2b81706aaa93"), null, null, 1306532718346240480L, "根据主键逻辑删除", true, "系统基础服务", false, false, "C5F41046D8531C1E77B503560A7E220E", 3, "/api/email-template/fake-delete/{id}", "邮件模板服务", "逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("84247930-2035-443d-bde3-69d4d23bec85"), null, null, 1306532718346240480L, "根据主键删除一条数据", true, "系统基础服务", false, false, "BA79EB71501051CA1F082DE15FBE73D3", 3, "/api/email-server-config/{id}", "邮件服务器配置服务", "删除", null, null, 1306532718346240480L },
                    { new Guid("84256e5b-2cef-4b16-8fd3-79ff8d47c731"), null, null, 1306532718346240480L, "添加一条数据", true, "系统基础服务", false, false, "3F9869D1A16CD359E268F2C2DBEFD0E2", 1, "/api/function", "功能服务", "添加", null, null, 1306532718346240480L },
                    { new Guid("84a87eb4-7ba2-4623-ad0d-ea467e7e5434"), "1", 1, 1306961911869440480L, "根据多个主键批量逻辑删除", true, "系统基础服务", false, false, "5D8485FD1F4D6A5A7924C93928BB3223", 1, "/api/code/fake-deletes", "字典管理", "批量逻辑删除", null, null, null },
                    { new Guid("85f94b4c-e897-4f3c-b80a-c7ddb8ebf1b5"), null, null, 1306532718346240480L, "搜索数据", false, "用户中心服务", false, false, "E6BAA5C7F35ED0CBD3902A30349A992B", 1, "/api/dept/search", "部门服务", "搜索", null, null, 1306532718346240480L },
                    { new Guid("8663473b-4a13-46ce-8187-a850215151fe"), "5", 1, 1307030674636800480L, "resources", true, "用户中心服务", false, false, "F17552720E10BFAF2622962F852D4C36", 1, "/api/tenant/{tenantid}/resources", "租户服务", "为租户添加资源", null, null, null },
                    { new Guid("868fc0df-7cdf-4b56-873e-16dd3e0aa528"), null, null, 1306532718346240480L, "根据主键锁定或解锁数据（必须有IsLock才能生效）", true, "用户中心服务", false, false, "439ED218846E25C27A388B09904AABC8", 2, "/api/role/{id}/lock/{islocked}", "角色服务", "锁定", null, null, 1306532718346240480L },
                    { new Guid("875de992-3eb6-478d-82a9-6994e6cabf44"), "1", 1, 1306782482698240480L, "搜索数据", false, "系统基础服务", false, false, "D0BD4286146CB2D2B1A7B2950E509F72", 1, "/api/code-gen/search", "代码生成 - DB First", "搜索", null, null, null },
                    { new Guid("89954833-64a5-4c87-a717-9c863ca3b263"), null, null, 1306532718346240480L, "根据多个主键批量逻辑删除", true, "用户中心服务", false, false, "710C2B0A026A9C3FF0D6235FCD8E0F26", 1, "/api/position/fake-deletes", "岗位管理服务", "批量逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("89a06a4e-1a8e-41aa-a443-fd11bcc8497d"), null, null, 1306532718346240480L, null, false, "用户中心服务", false, false, "0AC55E6880AE8FACEBACB093AF914C65", 0, "/api/account/current-user-resource-keys", "用户账户认证授权服务", "获取用户资源的key", null, null, 1306532718346240480L },
                    { new Guid("8ae9c253-584e-46e4-b805-6ec90281d6dd"), null, null, 1306532718346240480L, "根据主键锁定或解锁数据（必须有IsLock才能生效）", true, "系统基础服务", false, false, "E7F5596D4D8517C85871566D8EFA0855", 2, "/api/function/{id}/lock/{islocked}", "功能服务", "锁定", null, null, 1306532718346240480L },
                    { new Guid("8be6d20e-686c-4259-8eeb-3ec2b18739c3"), null, null, 1306532718346240480L, null, false, "示例服务", false, false, "2A74937190C8E652BF107434EFFD1C17", 0, "/api/chat-demo/history", "聊天示例服务", "获取聊天历史记录", null, null, null },
                    { new Guid("8d94c826-ddba-47fe-94c9-333880fee187"), null, null, 1306532718346240480L, "swagger json 文件解析功能", false, "系统基础服务", false, false, "7E9057E559FB68353DCA5D208B7B2A71", 0, "/api/swagger/analysis/{url}", "Swagger服务", "解析api json", null, null, 1306532718346240480L },
                    { new Guid("8d97bc08-7505-4b87-a99d-dc555a28265e"), "1", 1, 1306782482493440480L, "根据多个主键批量逻辑删除", true, "系统基础服务", false, false, "518FA5CE716696F2F9AEC8D3C85D46AE", 1, "/api/code-gen-config/fake-deletes", "代码生成配置 - DB First", "批量逻辑删除", null, null, null },
                    { new Guid("8db56ca8-b0e3-45c6-9c73-15893dd616f6"), "6", 1, 1307019897753600480L, "添加一条数据", true, "用户中心服务", false, false, "6C437FCA7A65CB18677DEAC79A73AC50", 1, "/api/tenant", "租户服务", "添加", null, null, null },
                    { new Guid("8f0fb7b6-9087-40c3-a894-8be057ac044e"), null, null, 1306532718346240480L, "根据分页参数，分页获取数据", false, "用户中心服务", false, false, "A16E0CAA03E75A172F6A782E8BB86ECC", 0, "/api/client/page/{pageindex}/{pagesize}", "客户端服务", "分页查询", null, null, 1306532718346240480L },
                    { new Guid("8f114b96-dc3d-4dd4-854a-4c793c121e43"), null, null, 1306532718346240480L, "根据主键删除一条数据", true, "系统基础服务", false, false, "873AFCBA915D056ED9D8EDA9D23F9061", 3, "/api/login-token/{id}", "用户登录TOKEN服务", "删除", null, null, 1306532718346240480L },
                    { new Guid("8f1c2eeb-248f-41bb-a083-511664f2fd8e"), null, null, 1306532718346240480L, "根据多个主键批量删除", true, "系统基础服务", false, false, "717D6057E652BA28D3BF0CE337180E9E", 1, "/api/function/deletes", "功能服务", "批量删除", null, null, 1306532718346240480L },
                    { new Guid("910d2a4f-85ae-46ff-bddd-b65ffcc6b9e1"), null, null, 1306532718346240480L, "根据搜索条叫生成种子数据", true, "系统基础服务", false, false, "74B1FF2C0E45DDB2D649404A53E7F7E9", 1, "/api/resource/generate-seed-data", "资源服务", "生成种子数据", null, null, 1306532718346240480L },
                    { new Guid("9191206c-f35e-4eb7-b19a-5949dc560369"), null, null, 1306532718346240480L, "添加一条数据", true, "系统基础服务", false, false, "98410D052E1A609292E627692BFA3375", 1, "/api/email-template", "邮件模板服务", "添加", null, null, 1306532718346240480L },
                    { new Guid("91ebe8d4-2b04-4277-b743-4f3a66bd487d"), "1", 1, 1306782482616320480L, "{id}", true, "系统基础服务", false, false, "1CFFA0F2216B185E282E88F7AE873F43", 3, "/api/code-gen/{id}", "代码生成 - DB First", "删除", null, null, null },
                    { new Guid("94f22c97-ae4a-40e0-95cd-d0a6347eacd7"), null, null, 1306532718346240480L, "根据角色编号删除所有资源", true, "用户中心服务", false, false, "DECA4ECA67D27FC9932271EE3B0AC5DD", 3, "/api/role/{roleid}/resource", "角色服务", "根据角色编号删除所有资源", null, null, 1306532718346240480L },
                    { new Guid("9513e5e1-37ab-4937-94f1-1f6b99a385f7"), null, null, 1306532718346240480L, "添加一条数据", true, "系统基础服务", false, false, "07BC6868FFAD4A5B26193E2372B9821C", 1, "/api/audit-operation", "审计操作服务", "添加", null, null, 1306532718346240480L },
                    { new Guid("973edc2c-42e1-473e-9656-a43890663d8a"), null, null, 1306532718346240480L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "用户中心服务", false, false, "3D033D8178E68247D2C34E53F00D468F", 0, "/api/dept/all-usable", "部门服务", "查询所有可以用的", null, null, 1306532718346240480L },
                    { new Guid("97b24035-cd96-4e28-bc42-103ac7e5fe3f"), "4", 1, 1306984506204160480L, "{imsessionid}", true, "通知系统服务", false, false, "ED0E0A2262D113044CA7B4539AE1B1EA", 1, "/api/wo-chat-im/enable-session-send-message", "Im聊天服务", "开启会话消息发送权限", null, null, null },
                    { new Guid("984eedd6-ff5e-46fc-a51c-4650af8f9171"), "1", 1, 1306782482452480480L, "添加一条数据", true, "系统基础服务", false, false, "3BA59B7F7AB75F87CFC9B46593496FBB", 1, "/api/code-gen-config", "代码生成配置 - DB First", "添加", null, null, null },
                    { new Guid("99546746-70b8-42d6-884d-ea1b79f88c0a"), null, null, 1306532718346240480L, "更新一条数据", true, "系统基础服务", false, false, "D15206E2B2CEFD1CC520AF32A357F56E", 2, "/api/email-server-config", "邮件服务器配置服务", "更新", null, null, 1306532718346240480L },
                    { new Guid("9a7bf3a2-802b-43b8-817f-f021a75d437f"), "2", 1, 1306966748139520480L, "根据字典类型编号获取字典列表", false, "系统基础服务", false, false, "2FEBEEAEEB7FDD3630AD1411F97E27D2", 0, "/api/code-type/{codetypeid}/codes", "字典类型管理", "根据字典类型编号获取字典列表", "6", 1, 1306994996674560480L },
                    { new Guid("9b34d89d-af78-4c4e-9326-d9d909c7ef19"), "1", 1, 1306782482575360480L, "根据搜索条叫生成种子数据", true, "系统基础服务", false, false, "366BAD01449B2FD91948CE215296D4DE", 1, "/api/code-gen-config/generate-seed-data", "代码生成配置 - DB First", "生成种子数据", null, null, null },
                    { new Guid("9ba644e7-fdba-4be3-9ab7-b9c8f344652c"), "1", 1, 1306782482657280480L, "根据多个主键批量逻辑删除", true, "系统基础服务", false, false, "D5F8E4AACAFC9C5D3FDB3287E106CE2D", 1, "/api/code-gen/fake-deletes", "代码生成 - DB First", "批量逻辑删除", null, null, null },
                    { new Guid("9bda79c9-783c-469c-acda-b72be7391a82"), null, null, 1306532718346240480L, "添加一条数据", true, "通知系统服务", false, false, "EDE2920EEFF4D581ED8EFB72359C19F5", 1, "/api/announcement", "公告服务", "添加", null, null, null },
                    { new Guid("9c6702ef-176c-4bff-90d3-5e6883d65399"), "1", 1, 1306782482698240480L, "根据搜索条叫生成种子数据", true, "系统基础服务", false, false, "3AC3E3B3217897587A05BE31D478A39D", 1, "/api/code-gen/generate-seed-data", "代码生成 - DB First", "生成种子数据", null, null, null },
                    { new Guid("9c6cefe2-d57d-490c-8b0f-70749bc5cdfa"), null, null, 1306532718346240480L, "根据主键删除", true, "系统基础服务", false, false, "085CB1560C82B28FE4C8C5F28EA31A59", 3, "/api/attachment/{id}", "附件服务", "删除", null, null, 1306532718346240480L },
                    { new Guid("9caf800a-de55-4d59-a138-675a16924c3c"), null, null, 1306532718346240480L, "查找到所有数据", false, "系统基础服务", false, false, "488BDECFA97ADDE5E940446C32C42693", 0, "/api/function/all", "功能服务", "查询所有", null, null, 1306532718346240480L },
                    { new Guid("9d25bf25-5470-4fed-b58c-c4ef4339d533"), null, null, 1306532718346240480L, "根据主键查找一条数据", false, "系统基础服务", false, false, "4166A00DD3058EA57C09B869E68927D4", 0, "/api/email-server-config/{id}", "邮件服务器配置服务", "根据主键获取", null, null, 1306532718346240480L },
                    { new Guid("9d26c715-9b8b-40c6-bbf4-9c51df1193da"), null, null, 1306532718346240480L, "更新一条数据", true, "系统基础服务", false, false, "DE9B14A3BC0E0653399F870F27F24CEF", 2, "/api/audit-entity", "审计数据服务", "更新", null, null, 1306532718346240480L },
                    { new Guid("9d9233d8-df0a-43b7-929a-65b9bd532c8c"), null, null, 1306532718346240480L, "根据分页参数，分页获取数据", false, "用户中心服务", false, false, "5CF48BAB60B771300975D93C49925CA0", 0, "/api/role/page/{pageindex}/{pagesize}", "角色服务", "分页查询", null, null, 1306532718346240480L },
                    { new Guid("9ebd4172-5191-4931-9b22-4c339be4a816"), null, null, 1306532718346240480L, "更新用户", true, "用户中心服务", false, false, "8C82B0DF3A0F5EB8DFED7794B16DA9A5", 2, "/api/user", "用户服务", "更新", null, null, 1306532718346240480L },
                    { new Guid("9fe5cc45-a851-4d3f-8b44-32dd96130946"), null, null, 1306532718346240480L, "搜索数据", false, "系统基础服务", false, false, "87C5CB00FB6A44D52C1C4CC5E9312B02", 1, "/api/email-server-config/search", "邮件服务器配置服务", "搜索", null, null, 1306532718346240480L },
                    { new Guid("a15ce231-80ae-46c6-ada8-49666e81e328"), null, null, 1306532718346240480L, "根据 HttpMethod 和 path 判断是否存在", false, "系统基础服务", false, false, "27693C4354A64289D9A1D3EB50E68E7E", 0, "/api/function/exists/{method}/{path}", "功能服务", "判断是否存在", null, null, 1306532718346240480L },
                    { new Guid("a1805308-f3d5-4aa2-ba20-26015da551bd"), "1", 1, 1306961911889920480L, "查找到所有数据", false, "系统基础服务", false, false, "64C74ADD81A7CEDA017064A06CDE4792", 0, "/api/code/all", "字典管理", "查询所有", null, null, null },
                    { new Guid("a28ac095-5faa-4a34-bf4e-5c3d3ab7cac9"), "1", 1, 1306961911951360480L, "根据主键查找一条数据", false, "系统基础服务", false, false, "29DF672B679D6B4BACD18812F0527DE4", 0, "/api/code-type/{id}", "字典类型管理", "根据主键获取", "6", 1, 1306994996695040480L },
                    { new Guid("a2e21aa5-c2ff-4893-954f-263822d168c3"), null, null, 1306532718346240480L, "更新一条数据", true, "系统基础服务", false, false, "2909033694587286F3458217843E20D8", 2, "/api/login-token", "用户登录TOKEN服务", "更新", null, null, 1306532718346240480L },
                    { new Guid("a2eab26f-f15c-48be-a976-2411c18f42bf"), null, null, 1306532718346240480L, "根据主键删除一条数据", true, "通知系统服务", false, false, "411B20D0DAA265D807874613A8DCB9F9", 3, "/api/announcement/{id}", "公告服务", "删除", null, null, null },
                    { new Guid("a3ea9c9f-da6f-48e1-8255-d250bb3e52d5"), null, null, 1306532718346240480L, "查找到所有数据", false, "系统基础服务", false, false, "3CBCB4608120758739D941BFCCC09C18", 0, "/api/attachment/all", "附件服务", "查询所有", null, null, 1306532718346240480L },
                    { new Guid("a4a2536b-1cc6-438c-ba00-054e16fc2c7c"), null, null, 1306532718346240480L, "根据主键删除一条数据", true, "用户中心服务", false, false, "1A6C9AC4F4D71B0FC154AD8CE6FE6D29", 3, "/api/role/{id}", "角色服务", "删除", null, null, 1306532718346240480L },
                    { new Guid("a4e467c5-639c-40bf-a71c-7d3c0d0760e7"), null, null, 1306532718346240480L, "查找到所有数据", false, "通知系统服务", false, false, "54C22639E8EF99A5CEEC744853C5DFCD", 0, "/api/announcement/all", "公告服务", "查询所有", null, null, null },
                    { new Guid("a53a9c89-7968-4598-9c46-dad4e9188bd0"), null, null, 1306532718346240480L, "获取api分组设置", false, "系统基础服务", false, false, "945B6A21E0C00F9BB0F7EEE37C671E3E", 0, "/api/swagger/api-group", "Swagger服务", "获取 swagger 配置", null, null, 1306532718346240480L },
                    { new Guid("a6939609-708f-4231-a726-6da1903ef69a"), "6", 1, 1307019897774080480L, "根据主键逻辑删除", true, "用户中心服务", false, false, "03522FF7125C61C2E7C1BD96FEA4E64C", 3, "/api/tenant/fake-delete/{id}", "租户服务", "逻辑删除", null, null, null },
                    { new Guid("a6db8946-339f-423e-8641-902da36d3d39"), null, null, 1306532718346240480L, "根据搜索条叫生成种子数据", true, "用户中心服务", false, false, "071E85AC46B630CFCC89C5EAF1E23F68", 1, "/api/user/generate-seed-data", "用户服务", "生成种子数据", null, null, 1306532718346240480L },
                    { new Guid("a75bd9a7-e3f0-4736-9c27-8763a3d3768b"), null, null, 1306532718346240480L, "更新一条数据", true, "系统基础服务", false, false, "7037CCD6F97FA35692ED560CE1756F86", 2, "/api/audit-operation", "审计操作服务", "更新", null, null, 1306532718346240480L },
                    { new Guid("a7f5ae02-cff0-4281-a153-145e612fb1ca"), "1", 1, 1306782482595840480L, "table-list", false, "系统基础服务", false, false, "DDC76CDC58280006E86B2F5354E71058", 0, "/api/code-gen/table-list", "代码生成 - DB First", "获取表列表", null, null, null },
                    { new Guid("a8211f75-bf19-459a-bf66-9c31c6f334aa"), null, null, 1306532718346240480L, "根据多个主键批量删除", true, "系统基础服务", false, false, "1C8C95EA831A3D031460A1390DF26E83", 1, "/api/audit-operation/deletes", "审计操作服务", "批量删除", null, null, 1306532718346240480L },
                    { new Guid("a96bb19e-794e-4fe0-ad39-f423df44f633"), null, null, 1306532718346240480L, null, false, "用户中心服务", false, false, "6A85EF9D6FBD3B330E1827AB0949D7E4", 0, "/api/dept/tree", "部门服务", "获取所有部门数据，以树形结构返回", null, null, 1306532718346240480L },
                    { new Guid("a9ec32a6-286b-4fe4-b69e-79cca6243e00"), "6", 1, 1307019897774080480L, "根据主键锁定或解锁数据（必须有IsLock才能生效）", true, "用户中心服务", false, false, "6A181466880BA2B3A8B0BC66906ACA90", 2, "/api/tenant/{id}/lock/{islocked}", "租户服务", "锁定", null, null, null },
                    { new Guid("aa7a6284-4351-4853-b066-4ea925fe36cc"), "1", 1, 1306782482595840480L, "code-gen", true, "系统基础服务", false, false, "2D20CFA2D1B0C847C2F9AA33215EA872", 1, "/api/code-gen", "代码生成 - DB First", "添加配置", null, null, null },
                    { new Guid("aad857df-a1e7-43cb-be82-55c60865da86"), null, null, 1306532718346240480L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统基础服务", false, false, "5F7047AD7EC090D04B2AF8C4847678A8", 0, "/api/email-template/all-usable", "邮件模板服务", "查询所有可以用的", null, null, 1306532718346240480L },
                    { new Guid("ab562cb0-7919-4b26-836a-f5865c1ddf63"), "1", 1, 1306782482677760480L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统基础服务", false, false, "0034E714497B05DB3FA37E8D28D730A1", 0, "/api/code-gen/all-usable", "代码生成 - DB First", "查询所有可以用的", null, null, null },
                    { new Guid("ac3ae978-83b7-4fad-9322-d1e223618d7c"), null, null, 1306532718346240480L, "根据多个主键批量删除", true, "通知系统服务", false, false, "FF53BAAC9AC941B38C99A08E032B9443", 1, "/api/announcement/deletes", "公告服务", "批量删除", null, null, null },
                    { new Guid("ae3a97a9-32fb-4402-a6c7-9a0ffd76ce49"), null, null, 1306532718346240480L, "查找到所有数据", false, "系统基础服务", false, false, "B0F7A5E8F1984DD5545B50F04FB3106D", 0, "/api/email-template/all", "邮件模板服务", "查询所有", null, null, 1306532718346240480L },
                    { new Guid("aeb8b23d-4da3-4ec0-867f-70d2e2ba9550"), null, null, 1306532718346240480L, "更新一条数据", true, "系统基础服务", false, false, "E2248234B183BA3EEA82273CB03F500C", 2, "/api/function", "功能服务", "更新", null, null, 1306532718346240480L },
                    { new Guid("aed3a535-b700-48a5-a8f5-3657e500e400"), null, null, 1306532718346240480L, "根据主键锁定或解锁数据（必须有IsLock才能生效）", true, "系统基础服务", false, false, "882FEEBFEAF1F50D83E0189AA69B9ED0", 2, "/api/audit-entity/{id}/lock/{islocked}", "审计数据服务", "锁定", null, null, 1306532718346240480L },
                    { new Guid("aedc9e9c-f011-4d46-966e-3b14fd5298c2"), null, null, 1306532718346240480L, "根据分页参数，分页获取数据", false, "系统基础服务", false, false, "9BE552AEF35878A71ABE8179B80AA036", 0, "/api/attachment/page/{pageindex}/{pagesize}", "附件服务", "分页查询", null, null, 1306532718346240480L },
                    { new Guid("af1f0410-e9cc-4a73-9da7-ea45aadac8b2"), null, null, 1306532718346240480L, "根据主键删除一条数据", true, "用户中心服务", false, false, "E749E69D854D694E0BC4CD4D97142A49", 3, "/api/client/{id}", "客户端服务", "删除", null, null, 1306532718346240480L },
                    { new Guid("af79d7de-0141-4338-8c52-05216d1b07ff"), null, null, 1306532718346240480L, "新增用户", true, "用户中心服务", false, false, "7CBF6D43C3F9935BF83629FCEED2FFFB", 1, "/api/user", "用户服务", "新增", null, null, 1306532718346240480L },
                    { new Guid("b0b9900d-5ff7-4164-957a-9b8d55d5d5bf"), "1", 1, 1306552940666880480L, "导出数据", true, "用户中心服务", false, false, "BF0393C2E61B44E125040F8947AEFB23", 1, "/api/user/export", "用户服务", "导出", null, null, null },
                    { new Guid("b2dfaad3-e44a-4a76-ac91-34a571ba47e8"), null, null, 1306532718346240480L, "根据key获取 功能点", false, "系统基础服务", false, false, "CE34F0B4FCB2222CF693F501B370149D", 0, "/api/function/by-key/{key}", "功能服务", "根据key获取", null, null, 1306532718346240480L },
                    { new Guid("b3577dc2-dfea-41be-ba8f-bb8efa389f36"), null, null, 1306532718346240480L, "根据主键查找一条数据", false, "系统基础服务", false, false, "A46663EE883A6E5BE0A0C8FE0B3D7A4C", 0, "/api/audit-entity/{id}", "审计数据服务", "根据主键获取", null, null, 1306532718346240480L },
                    { new Guid("b38fb0cc-4275-4d1f-8bb7-6f5a962bcc35"), null, null, 1306532718346240480L, "根据主键获取用户", false, "用户中心服务", false, false, "011AC4559477AB1F24A281BDC1033AAB", 0, "/api/user/{id}", "用户服务", "根据主键获取用户", null, null, 1306532718346240480L },
                    { new Guid("b3f529a4-ad25-4d14-a86b-c1f4cd3f33e0"), "1", 1, 1306961911910400480L, "根据主键锁定或解锁数据（必须有IsLock才能生效）", true, "系统基础服务", false, false, "298247EAC149319EC01AD1B2C3280CBC", 2, "/api/code/{id}/lock/{islocked}", "字典管理", "锁定", null, null, null },
                    { new Guid("b56c4126-411c-445e-86aa-a91a5ce816d4"), null, null, 1306532718346240480L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "用户中心服务", false, false, "C47AACD68B1EF833AAC0EC90CD878FDD", 0, "/api/position/all-usable", "岗位管理服务", "查询所有可以用的", null, null, 1306532718346240480L },
                    { new Guid("b69cedcd-49c3-4833-9d80-d67282cd015f"), "1", 1, 1306961911951360480L, "根据多个主键批量删除", true, "系统基础服务", false, false, "D9A20DEED6CAF38F963F938912CA544C", 1, "/api/code-type/deletes", "字典类型管理", "批量删除", "6", 1, 1306994996715520480L },
                    { new Guid("b6c1592b-cb4b-4ead-bea1-3dc4a917e4a8"), null, null, 1306532718346240480L, "根据多个主键批量删除", true, "系统基础服务", false, false, "9C103F0B6C167211465FB472E46EC968", 1, "/api/email-template/deletes", "邮件模板服务", "批量删除", null, null, 1306532718346240480L },
                    { new Guid("b79d2f63-487c-44c8-b7d3-1e882994789b"), null, null, 1306532718346240480L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统基础服务", false, false, "1CCC2478B5AC5FDDB537DCD33166ABF7", 0, "/api/function/all-usable", "功能服务", "查询所有可以用的", null, null, 1306532718346240480L },
                    { new Guid("b85cabd4-f813-4077-833d-152fb9c3c2b7"), "1", 1, 1306961911992320480L, "根据分页参数，分页获取数据", false, "系统基础服务", false, false, "0D74D8B03DB1CD915D18B06365A77258", 0, "/api/code-type/page/{pageindex}/{pagesize}", "字典类型管理", "分页查询", "6", 1, 1306994996736000480L },
                    { new Guid("b952b41e-b3e9-4c53-9a7d-6b561acf4bc4"), null, null, 1306532718346240480L, "根据主键查找一条数据", false, "系统基础服务", false, false, "FDD3EAB18820A6CD5C6DA3B17D40EEB9", 0, "/api/function/{id}", "功能服务", "根据主键获取", null, null, 1306532718346240480L },
                    { new Guid("bacd1963-f89e-4afb-862f-584cd9ba4c10"), null, null, 1306532718346240480L, "根据搜索条叫生成种子数据", true, "系统基础服务", false, false, "7F4170917F4566615005DB297A93C7CE", 1, "/api/email-server-config/generate-seed-data", "邮件服务器配置服务", "生成种子数据", null, null, 1306532718346240480L },
                    { new Guid("bdab8953-956d-4b1a-945b-b1806e9ac749"), null, null, 1306532718346240480L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "用户中心服务", false, false, "073E6E78B3A88E41DBDC46DCA32C4837", 0, "/api/user/all-usable", "用户服务", "查询所有可以用的", null, null, 1306532718346240480L },
                    { new Guid("be639780-fbd0-4335-95e3-91e099d87850"), "4", 1, 1306984504442880480L, "{attachmentbusinesstype}", false, "系统基础服务", false, false, "A6BE53DBFEC19CB43380056A69B1037F", 0, "/api/attachment/my-attachments/{attachmentbusinesstype}", "附件服务", "获取我的某一类型附件数据", null, null, null },
                    { new Guid("c01f4723-8efa-4268-b793-901c75934f6c"), "1", 1, 1306782482636800480L, "{codegenid}", true, "系统基础服务", false, false, "6EB0EC8EE348241D62D68C48E6C85BED", 1, "/api/code-gen/generate-locale/{codegenid}", "代码生成 - DB First", "导入多语言", null, null, null },
                    { new Guid("c1e7fa06-b759-4bb0-9545-7265e3798d28"), null, null, 1306532718346240480L, "", true, "用户中心服务", false, false, "43844F96A173330CECD6470FD62A8A76", 1, "/api/resource-function", "资源与接口关系服务", "添加资源与接口关系", null, null, null },
                    { new Guid("c2784668-075f-4b7e-a563-b6b92b072542"), null, null, 1306532718346240480L, "根据搜索条叫生成种子数据", true, "用户中心服务", false, false, "298FD49C905F1B1B812B226B95307CE0", 1, "/api/dept/generate-seed-data", "部门服务", "生成种子数据", null, null, null },
                    { new Guid("c39030b8-d207-4c22-a3ba-74b0eccaa2fa"), null, null, 1306532718346240480L, "根据多个主键批量逻辑删除", true, "系统基础服务", false, false, "4603BCE62CA130E67C2450C127DD7728", 1, "/api/function/fake-deletes", "功能服务", "批量逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("c4cc2526-8403-4e6c-a88b-94e55279eaa3"), null, null, 1306532718346240480L, "根据搜索条叫生成种子数据", true, "系统基础服务", false, false, "A53340931409D1BB2882CDB88AE6CB5D", 1, "/api/function/generate-seed-data", "功能服务", "生成种子数据", null, null, 1306532718346240480L },
                    { new Guid("c56d6a82-abc8-4b17-bc28-27b1904116c9"), null, null, 1306532718346240480L, "", false, "用户中心服务", false, false, "DDE05A70BD80F948C9AEAFB9708090F3", 0, "/api/resource-function/seed-data", "资源与接口关系服务", "获取种子数据", null, null, null },
                    { new Guid("c591c0ca-3305-4684-89bb-278218d13c47"), null, null, 1306532718346240480L, null, false, "系统基础服务", false, false, "187E0857A128187E01EFBBD569C3DE92", 0, "/api/swagger/functions-from-json/{url}", "Swagger服务", "从json中获取function", null, null, 1306532718346240480L },
                    { new Guid("c715a6d5-cd99-4c94-8760-936817c1e09c"), null, null, 1306532718346240480L, "搜索数据", false, "用户中心服务", false, false, "9A501F3D2F0A3A2D47A17D6F42042CD5", 1, "/api/position/search", "岗位管理服务", "搜索", null, null, 1306532718346240480L },
                    { new Guid("c81ba6ae-ab11-4e2f-b3a7-cf6cf22284f0"), "1", 1, 1306782482677760480L, "根据分页参数，分页获取数据", false, "系统基础服务", false, false, "8160D0CFFF66846429B61257A1508536", 0, "/api/code-gen/page/{pageindex}/{pagesize}", "代码生成 - DB First", "分页查询", null, null, null },
                    { new Guid("c831e1fc-a428-41bc-876a-f158d0159a01"), "1", 1, 1306782482636800480L, "{codegenid}", true, "系统基础服务", false, false, "0436D43457349178B130D1C651AFCD6D", 1, "/api/code-gen/generate-menu/{codegenid}", "代码生成 - DB First", "生成菜单数据", null, null, null },
                    { new Guid("c96dd7f7-f935-4499-8ef5-6d39fe26141a"), null, null, 1306532718346240480L, "登录接口", true, "用户中心服务", false, false, "B6792454A69F875EEC82455D02BB3AAA", 1, "/api/account/login", "用户账户认证授权服务", "登录", null, null, 1306532718346240480L },
                    { new Guid("c9cf322b-5228-4ea9-b40d-64534db5d292"), "1", 1, 1306782482472960480L, "更新一条数据", true, "系统基础服务", false, false, "99C5D9DAADE9FF4039861475A7C4E7D8", 2, "/api/code-gen-config", "代码生成配置 - DB First", "更新", null, null, null },
                    { new Guid("ca62cf90-fcfd-40aa-bd06-30afc7c6dd9f"), null, null, 1306532718346240480L, "根据搜索条叫生成种子数据", true, "系统基础服务", false, false, "2F1D00EDA3F9BA770FC2D6E15892FBB4", 1, "/api/audit-operation/generate-seed-data", "审计操作服务", "生成种子数据", null, null, 1306532718346240480L },
                    { new Guid("cb4fbf67-4747-43d5-a355-7300f007cce3"), "6", 1, 1307019897774080480L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "用户中心服务", false, false, "7759AEBBAB92B29DD030D2D38CC6C2BC", 0, "/api/tenant/all-usable", "租户服务", "查询所有可以用的", null, null, null },
                    { new Guid("cb9f6387-5817-4fd6-b9eb-6553dcaf5e87"), null, null, 1306532718346240480L, "查找到所有数据", false, "用户中心服务", false, false, "8D8980AD32B8E49FB140F9DCE14B897C", 0, "/api/role/all", "角色服务", "查询所有", null, null, 1306532718346240480L },
                    { new Guid("cba739f0-9f8a-40c2-afff-d66c3382e096"), null, null, 1306532718346240480L, "根据主键查找一条数据", false, "用户中心服务", false, false, "CC8DA87E574A106E9B14287FEC850037", 0, "/api/role/{id}", "角色服务", "根据主键获取", null, null, 1306532718346240480L },
                    { new Guid("cbc8aff4-6dc0-41f2-b684-caba8e0657ac"), null, null, 1306532718346240480L, "搜索用户数据", true, "用户中心服务", false, false, "04608E487B494D4597BBAD83DF59D2FF", 1, "/api/user/search", "用户服务", "搜索", null, null, 1306532718346240480L },
                    { new Guid("cbdf4c22-b54f-4943-b28e-2bb563720fa2"), "6", 1, 1307019897774080480L, "根据多个主键批量逻辑删除", true, "用户中心服务", false, false, "CB0773C970DC23647D09D3D9A51DA5F3", 1, "/api/tenant/fake-deletes", "租户服务", "批量逻辑删除", null, null, null },
                    { new Guid("cc73d556-6ded-4a2a-8b5c-62ea9c897351"), null, null, 1306532718346240480L, "根据多个主键批量逻辑删除", true, "用户中心服务", false, false, "1DC4817A750A7C248B15EA766BDD53C8", 1, "/api/client/fake-deletes", "客户端服务", "批量逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("cd1b93ed-2fae-47f2-83b2-e9b0a949f476"), "1", 1, 1306552940953600480L, "导出数据", true, "系统基础服务", false, false, "91D755377D8744A976836037290BB199", 1, "/api/login-token/export", "用户登录TOKEN服务", "导出", null, null, null },
                    { new Guid("cd7db809-50f5-4bf3-a464-89218e24077f"), null, null, 1306532718346240480L, "获取角色所有资源", false, "用户中心服务", false, false, "011A2E3F574F9C151E044EFA80A05F29", 0, "/api/role/{roleid}/resource", "角色服务", "获取角色所有资源", null, null, 1306532718346240480L },
                    { new Guid("cdd3c605-ed1d-4d94-a482-16430b729541"), null, null, 1306532718346240480L, "根据主键锁定或解锁数据（必须有IsLock才能生效）", true, "系统基础服务", false, false, "BC8D1127FE54019A5476079400388CF3", 2, "/api/resource/{id}/lock/{islocked}", "资源服务", "锁定", null, null, 1306532718346240480L },
                    { new Guid("ce5a43ff-7ee1-451b-ae23-8acd04f87f71"), "1", 1, 1306961911848960480L, "更新一条数据", true, "系统基础服务", false, false, "097F8DBD6CD33EA9DB3976E17A448E17", 2, "/api/code", "字典管理", "更新", null, null, null },
                    { new Guid("cecdfb7d-6796-4bd8-a3d7-164c16a7c959"), null, null, 1306532718346240480L, "更新一条数据", true, "用户中心服务", false, false, "4326F39D4D047A58AA7887EEB0A5B5A3", 2, "/api/client", "客户端服务", "更新", null, null, 1306532718346240480L },
                    { new Guid("d0a4b504-f401-4c1b-a43e-08cca27bd170"), "1", 1, 1306782482698240480L, "根据主键锁定或解锁数据（必须有IsLock才能生效）", true, "系统基础服务", false, false, "684524DB0FA81872CF18DBB1C7557421", 2, "/api/code-gen/{id}/lock/{islocked}", "代码生成 - DB First", "锁定", null, null, null },
                    { new Guid("d0f31c64-db5d-4d25-a7fb-f82b0caf6760"), "1", 1, 1306782482554880480L, "根据分页参数，分页获取数据", false, "系统基础服务", false, false, "53F1180444D85BF8C79EFB89DFCF2A11", 0, "/api/code-gen-config/page/{pageindex}/{pagesize}", "代码生成配置 - DB First", "分页查询", null, null, null },
                    { new Guid("d22007c6-fada-4ef1-bafa-08455b767883"), null, null, 1306532718346240480L, "根据分页参数，分页获取数据", false, "用户中心服务", false, false, "F543F08AB768F7D444481F5D7EB52373", 0, "/api/position/page/{pageindex}/{pagesize}", "岗位管理服务", "分页查询", null, null, 1306532718346240480L },
                    { new Guid("d481028f-d67e-4ee0-b237-a4883c618486"), "4", 1, 1306984506163200480L, "my-im-sessions", false, "通知系统服务", false, false, "317257C6321887B9223ECEF8D1BA8100", 0, "/api/wo-chat-im/my-im-sessions", "Im聊天服务", "获取我的会话列表", null, null, null },
                    { new Guid("d5e9621c-ad9f-4bca-aa51-04aa0b55744e"), null, null, 1306532718346240480L, "根据分页参数，分页获取数据", false, "系统基础服务", false, false, "6946DB3F24E403A804F67F2B116C9392", 0, "/api/email-server-config/page/{pageindex}/{pagesize}", "邮件服务器配置服务", "分页查询", null, null, 1306532718346240480L },
                    { new Guid("d7f59d52-a931-4bec-8312-5142d4d37fda"), null, null, 1306532718346240480L, null, true, "系统基础服务", false, false, "BA7BDB4454250C19379AD4FABE7A58B6", 1, "/api/image-verify-code", "图片验证码服务", "获取验证码", null, null, 1306532718346240480L },
                    { new Guid("d7fa048a-0bfd-4997-94e3-dda3402c3b08"), null, null, 1306532718346240480L, "查找到所有数据", false, "系统基础服务", false, false, "5AE5AD783D5821E6D300DBE1BDD6E631", 0, "/api/email-server-config/all", "邮件服务器配置服务", "查询所有", null, null, 1306532718346240480L },
                    { new Guid("db38be6f-754f-493a-ad9a-70e32ec80dca"), "1", 1, 1306782482616320480L, "根据主键查找一条数据", false, "系统基础服务", false, false, "3DDEDB9584240DB31E6D343A2013BC49", 0, "/api/code-gen/{id}", "代码生成 - DB First", "根据主键获取", null, null, null },
                    { new Guid("db76ae46-851b-47bc-94be-b2e869043636"), null, null, 1306532718346240480L, "搜索数据", false, "系统基础服务", false, false, "704D356B44E6DEA692BA099781A321DD", 1, "/api/audit-operation/search", "审计操作服务", "搜索", null, null, 1306532718346240480L },
                    { new Guid("dca2b115-3363-4f7f-8bba-b051b8d8603a"), "1", 1, 1306552940953600480L, "导出数据", true, "系统基础服务", false, false, "C159134CE2D63BC05680B2AD2BB86E7C", 1, "/api/function/export", "功能服务", "导出", null, null, null },
                    { new Guid("ddeeea7e-09e3-42c1-b536-0ff16393db1c"), null, null, 1306532718346240480L, null, true, "系统基础服务", false, false, "E23CF3B8D86A5D0E1F13759117676687", 1, "/api/email-verify-code", "邮件验证码服务", "获取验证码", null, null, 1306532718346240480L },
                    { new Guid("deb0a631-d0fb-4b6f-8b0d-bfeee5c759c8"), "1", 1, 1306782482718720480L, "导出数据", true, "系统基础服务", false, false, "B544D64F87CECA7C98BD6D944E214805", 1, "/api/code-gen/export", "代码生成 - DB First", "导出", null, null, null },
                    { new Guid("e0b19b01-4b37-426a-a5c4-749e5936b64c"), "2", 1, 1306971987783680480L, "不执行任何内容，token无效将响应401", true, "用户中心服务", false, false, "F8F937E7C7DC6514F3C0E1353D4D35FB", 1, "/api/account/test-token", "用户账户认证授权服务", "测试token是否可用", null, null, null },
                    { new Guid("e22446f3-1a8c-4500-ab7b-8922f70a811a"), "1", 1, 1306782482472960480L, "根据多个主键批量删除", true, "系统基础服务", false, false, "90302348A7CC0816FA57893583185966", 1, "/api/code-gen-config/deletes", "代码生成配置 - DB First", "批量删除", null, null, null },
                    { new Guid("e23b555c-600a-4839-9439-2ee0ad0ae4f8"), null, null, 1306532718346240480L, "更新一条数据", true, "用户中心服务", false, false, "248BF161E6BEB662D259298A8E564433", 2, "/api/dept", "部门服务", "更新", null, null, 1306532718346240480L },
                    { new Guid("e242137a-b4ea-4dd9-9692-63885fce93b8"), "1", 1, 1306961911971840480L, "根据主键逻辑删除", true, "系统基础服务", false, false, "730019C5993BDE609E6596851BA232F6", 3, "/api/code-type/fake-delete/{id}", "字典类型管理", "逻辑删除", "6", 1, 1306994996715520480L },
                    { new Guid("e298058c-8ec9-4637-bf8b-4ece0bfa5a5b"), null, null, 1306532718346240480L, "根据分页参数，分页获取数据", false, "系统基础服务", false, false, "4A8F7B6AB1E6A30A9A65FEBE2B31CE4A", 0, "/api/login-token/page/{pageindex}/{pagesize}", "用户登录TOKEN服务", "分页查询", null, null, 1306532718346240480L },
                    { new Guid("e2bb65e0-5d9e-485e-9059-8148fc236246"), null, null, 1306532718346240480L, "获取当前用户的所有菜单", false, "用户中心服务", false, false, "3317F3470BD4CCECEB26F73F6551D9D6", 0, "/api/account/current-user-menus", "用户账户认证授权服务", "获取当前用户的所有菜单", null, null, 1306532718346240480L },
                    { new Guid("e38c1619-0f84-4e55-81c2-0f47992ee33d"), null, null, 1306532718346240480L, "查询所有资源 按树形结构返回\r\n非租户在所有资源中抽取，租户在自己的资源池中抽取", false, "系统基础服务", false, false, "6AFF14D9D209CDEEFFC0E4872E060F42", 0, "/api/resource/tree", "资源服务", "查询所有资源", null, null, 1306532718346240480L },
                    { new Guid("e466c648-4dc5-4ca4-b8f9-826c51b2a462"), null, null, 1306532718346240480L, null, true, "系统基础服务", false, false, "1113744E52468C0ED06582D699F77B87", 1, "/api/email-verify-code/verify", "邮件验证码服务", "验证验证码", null, null, 1306532718346240480L },
                    { new Guid("e5669b2a-a097-46c4-b5ed-aadff087cbae"), "6", 1, 1307019897774080480L, "根据分页参数，分页获取数据", false, "用户中心服务", false, false, "44CA7D257E0C34AD9066BBF6E6619523", 0, "/api/tenant/page/{pageindex}/{pagesize}", "租户服务", "分页查询", null, null, null },
                    { new Guid("e651d9a4-9d6d-44c7-a833-08da6ed19892"), null, null, 1306532718346240480L, "搜索数据", false, "系统基础服务", false, false, "860A62FFC20FAAAE60E760D4305104DF", 1, "/api/login-token/search", "用户登录TOKEN服务", "搜索", null, null, 1306532718346240480L },
                    { new Guid("e7e8c401-2ff1-45ee-adfd-cebe90117575"), null, null, 1306532718346240480L, "根据主键逻辑删除", true, "系统基础服务", false, false, "FEB756C21615385FC3C747ACB240DC2D", 3, "/api/resource/fake-delete/{id}", "资源服务", "逻辑删除", null, null, 1306532718346240480L },
                    { new Guid("e81c2cc3-b2cb-4515-a5bb-b5ef3caa5050"), null, null, 1306532718346240480L, "根据搜索条叫生成种子数据", true, "用户中心服务", false, false, "80E175A5D68598258AE3022F6CD323F0", 1, "/api/client/generate-seed-data", "客户端服务", "生成种子数据", null, null, null },
                    { new Guid("ea2a64ac-bde9-45e5-a879-10b161b3f825"), "4", 1, 1306984506224640480L, "{imsessionid}", false, "通知系统服务", false, false, "B064921D09886F3BD49621B6C2A81ACC", 0, "/api/wo-chat-im/im-session/{imsessionid}", "Im聊天服务", "根据会话编号获取会话", null, null, null },
                    { new Guid("ec1a6751-d862-4808-8032-033463fdb65e"), "1", 1, 1306782482513920480L, "查找到所有数据", false, "系统基础服务", false, false, "C9B949B9979521AB3FAB741C8177CA7C", 0, "/api/code-gen-config/all", "代码生成配置 - DB First", "查询所有", null, null, null },
                    { new Guid("ed340c0c-9b63-45f4-942a-c8a14c4491d3"), null, null, 1306532718346240480L, "根据多个主键批量删除", true, "用户中心服务", false, false, "C9E5F9B494BBF428A85ECEA53B095285", 1, "/api/position/deletes", "岗位管理服务", "批量删除", null, null, 1306532718346240480L },
                    { new Guid("ed8c2fae-c63f-4aec-af4b-e915b6db38a2"), "1", 1, 1306961911992320480L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "系统基础服务", false, false, "C8E56A2E79C8B64D478D2148908166C7", 0, "/api/code-type/all-usable", "字典类型管理", "查询所有可以用的", "6", 1, 1306994996715520480L },
                    { new Guid("eefdb20f-b508-415a-b798-1aa9420a5b62"), null, null, 1306532718346240480L, "根据主键锁定或解锁数据（必须有IsLock才能生效）", true, "系统基础服务", false, false, "6999F8BBB5F9BA97658BB99113A381F5", 2, "/api/audit-operation/{id}/lock/{islocked}", "审计操作服务", "锁定", null, null, 1306532718346240480L },
                    { new Guid("ef62671e-4d35-4993-83c4-4dcdf7cbf0d0"), null, null, 1306532718346240480L, "根据多个主键批量删除", true, "系统基础服务", false, false, "33524956F6EC6C08F348500B3E2D9E9C", 1, "/api/attachment/deletes", "附件服务", "批量删除", null, null, 1306532718346240480L },
                    { new Guid("f1267fbc-903b-4439-a7b6-a7290507d207"), null, null, 1306532718346240480L, "根据主键查找一条数据", false, "系统基础服务", false, false, "C01E9238420548B6CA87C312935DD043", 0, "/api/login-token/{id}", "用户登录TOKEN服务", "根据主键获取", null, null, 1306532718346240480L },
                    { new Guid("f16b30b9-9e03-48d7-83a1-f09ae3e05345"), null, null, 1306532718346240480L, "查找到所有数据", false, "用户中心服务", false, false, "C75F9424DD51498CD9ADBFCBF2EB4D57", 0, "/api/client/all", "客户端服务", "查询所有", null, null, 1306532718346240480L },
                    { new Guid("f3ca4074-503c-49a5-b714-03b121f25e9e"), "1", 1, 1306961911951360480L, "根据主键删除一条数据", true, "系统基础服务", false, false, "9965590775432EAD1C790DB346C5091A", 3, "/api/code-type/{id}", "字典类型管理", "删除", "6", 1, 1306994996695040480L },
                    { new Guid("f41a15c0-62cc-4e17-a126-15d4873fca33"), "1", 1, 1306782482657280480L, "根据主键逻辑删除", true, "系统基础服务", false, false, "F6565C4790EECA32489B71CF74208BDB", 3, "/api/code-gen/fake-delete/{id}", "代码生成 - DB First", "逻辑删除", null, null, null },
                    { new Guid("f4ba1bf6-c07e-4df2-b7de-93b35fb79bf0"), null, null, 1306532718346240480L, "根据主键删除一条数据", true, "系统基础服务", false, false, "8650A7797FF354BBB742C87D0F560844", 3, "/api/resource/{id}", "资源服务", "删除", null, null, 1306532718346240480L },
                    { new Guid("f59833a1-c9af-4bb2-be4b-d6935513fc99"), null, null, 1306532718346240480L, "根据主键锁定或解锁数据（必须有IsLock才能生效）", true, "系统基础服务", false, false, "B37ED1BEEE60098FACB7182C73B5FA3F", 2, "/api/login-token/{id}/lock/{islocked}", "用户登录TOKEN服务", "锁定", null, null, 1306532718346240480L },
                    { new Guid("f5c318f6-9230-475a-830e-a404e17506b5"), null, null, 1306532718346240480L, "添加一条数据", true, "用户中心服务", false, false, "3AB1D0424907EC010DC69F029B4FBD06", 1, "/api/dept", "部门服务", "添加", null, null, 1306532718346240480L },
                    { new Guid("f6843cdf-133d-4eb8-92b2-c36fe63ea9d7"), null, null, 1306532718346240480L, "根据主键锁定或解锁数据（必须有IsLock才能生效）", true, "用户中心服务", false, false, "9501F9B0B5D4867FF65611B203B43D69", 2, "/api/position/{id}/lock/{islocked}", "岗位管理服务", "锁定", null, null, 1306532718346240480L },
                    { new Guid("f6fd9621-f6e4-45ec-b919-6acb73c7b303"), null, null, 1306532718346240480L, "查找到所有数据", false, "系统基础服务", false, false, "8C9ED3E66D288CA942AD438AD9C50DBF", 0, "/api/login-token/all", "用户登录TOKEN服务", "查询所有", null, null, 1306532718346240480L },
                    { new Guid("f7279175-4aa3-448a-ac71-a17004d66788"), null, null, 1306532718346240480L, "根据搜索条叫生成种子数据", true, "通知系统服务", false, false, "DB4E6D22B47A0BBCB5F87643AA5EB527", 1, "/api/announcement/generate-seed-data", "公告服务", "生成种子数据", null, null, null },
                    { new Guid("f72f5e71-46f6-44eb-8a3d-f07082fa33e5"), null, null, 1306532718346240480L, "查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)", false, "用户中心服务", false, false, "6069031816C15D92B60A246C9CAD1287", 0, "/api/client/all-usable", "客户端服务", "查询所有可以用的", null, null, 1306532718346240480L },
                    { new Guid("f78ea06a-4c55-4445-9e16-bdc92c9b9fa6"), "4", 1, 1306984506142720480L, "my-im-session", true, "通知系统服务", false, false, "925B4B0A0CD782790D2A5F3EDA5A3C25", 1, "/api/wo-chat-im/my-im-session", "Im聊天服务", "添加会话", null, null, null },
                    { new Guid("f8ddd5e5-7c20-43c2-a2cf-31ebc3f9971a"), null, null, 1306532718346240480L, "根据分页参数，分页获取数据", false, "系统基础服务", false, false, "5848947AEE0064BC746DE38E1AC0E3D2", 0, "/api/resource/page/{pageindex}/{pagesize}", "资源服务", "分页查询", null, null, 1306532718346240480L },
                    { new Guid("f9480a7b-3408-44f0-aca9-78ba66a75799"), "1", 1, 1306961911910400480L, "根据搜索条叫生成种子数据", true, "系统基础服务", false, false, "95D3D2AECB7BA05B1D523003576DBA80", 1, "/api/code/generate-seed-data", "字典管理", "生成种子数据", null, null, null },
                    { new Guid("faa3ff98-22d5-4254-9297-ee976a5842de"), null, null, 1306532718346240480L, "根据多个主键批量逻辑删除", true, "通知系统服务", false, false, "6A55E8C4030728438432973ECACF7433", 1, "/api/announcement/fake-deletes", "公告服务", "批量逻辑删除", null, null, null },
                    { new Guid("fc78f4d2-2c96-40f3-9178-83c1be5c00e4"), "1", 1, 1306782482472960480L, "根据主键删除一条数据", true, "系统基础服务", false, false, "545EECBD21B11E9C77FAA49044CB4C09", 3, "/api/code-gen-config/{id}", "代码生成配置 - DB First", "删除", null, null, null },
                    { new Guid("fc90ab49-b7c2-437e-bbdc-4f234cb0f79a"), null, null, 1306532718346240480L, "根据主键锁定或解锁数据（必须有IsLock才能生效）", true, "用户中心服务", false, false, "6D9355E642310F188E728A62002A6879", 2, "/api/client/{id}/lock/{islocked}", "客户端服务", "锁定", null, null, 1306532718346240480L },
                    { new Guid("fcebd316-c2f3-4f8e-97fc-498dd3a33d4e"), null, null, 1306532718346240480L, "根据多个主键批量删除", true, "用户中心服务", false, false, "951D030BDA5FAE619E5A7BB9EFB43F33", 1, "/api/dept/deletes", "部门服务", "批量删除", null, null, 1306532718346240480L },
                    { new Guid("ff8621c9-1b88-4e6d-be00-34615c48c69f"), null, null, 1306532718346240480L, null, false, "用户中心服务", false, false, "EFFE9D726D7792B023DF91E15AA48C89", 0, "/api/dept/seed-data", "部门服务", "获取种子数据", null, null, 1306532718346240480L },
                    { new Guid("ff955e68-22f5-47c2-88f2-2c901cd823e3"), null, null, 1306532718346240480L, "更新一条数据", true, "通知系统服务", false, false, "5C0F247E2ABB90F962DCB4E0D1F948E1", 2, "/api/announcement", "公告服务", "更新", null, null, null },
                    { new Guid("ffbd98b8-8945-4068-b70c-ea58b487bd25"), null, null, 1306532718346240480L, "根据主键删除一条数据", true, "系统基础服务", false, false, "AD48018AF04E0A4573815675E555E98D", 3, "/api/audit-operation/{id}", "审计操作服务", "删除", null, null, 1306532718346240480L },
                    { new Guid("ffef6a8e-3f80-4a39-97c6-5b2b81582830"), null, null, 1306532718346240480L, "", true, "用户中心服务", false, false, "FE150D4F1EE3DDDE5BD78C718100A247", 3, "/api/resource-function/{resourceid}/{functionid}", "资源与接口关系服务", "删除资源与接口关系", null, null, null },
                    { new Guid("fff9f1e7-7fd3-42f5-afe7-d40cca07f0ca"), null, null, 1306532718346240480L, null, false, "系统基础服务", false, false, "B2A11324BCA0A9070B6160AE6B0EE6F2", 0, "/api/resource/{id}/functions", "资源服务", "根据资源id获取功能信息", null, null, 1306532718346240480L }
                });

            migrationBuilder.InsertData(
                table: "Position",
                columns: new[] { "Id", "CreateBy", "CreateIdentityType", "CreatedTime", "Duty", "Grade", "IsDeleted", "IsLocked", "Name", "Qualifications", "Right", "Salary", "Target", "TenantId", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { 1, null, null, 1305892579553280000L, null, null, false, false, "董事长", null, null, null, null, null, null, null, null },
                    { 2, null, null, 1305892579553280000L, null, null, false, false, "总经理", null, null, null, null, null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreateBy", "CreateIdentityType", "CreatedTime", "Hide", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "SupportMultiTenant", "Type", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"), null, 0, 1306546807398400480L, false, "apartment", false, false, "admin_root", "后台根节点", 0, null, "", "根根节点不能删除，不能改变类型！！。", true, 0, null, null, null },
                    { new Guid("f4239a53-b5e1-49bd-99c6-967a86f07cdc"), null, 0, 1306546809856000480L, false, "apartment", false, false, "front_root", "前台根节点", 1, null, "", "根根节点不能删除，不能改变类型！！。", true, 0, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreateBy", "CreateIdentityType", "CreatedTime", "IsDefault", "IsDeleted", "IsLocked", "IsSuperAdministrator", "Name", "Remark", "TenantId", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { 1, null, null, 1305892579553280000L, false, false, false, true, "超级管理员", "拥有所有权限", null, null, null, null },
                    { 2, null, null, 1305892579553280000L, false, false, false, false, "浏览者", "只能浏览", null, null, null, null },
                    { 3, null, null, 1305892579553280000L, false, false, false, false, "租户1_管理员", "租户1_管理员", new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { 4, null, null, 1305892579553280000L, false, false, false, false, "租户2_管理员", "租户2_管理员", new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null }
                });

            migrationBuilder.InsertData(
                table: "SystemTenant",
                columns: new[] { "Id", "CreateBy", "CreateIdentityType", "CreatedTime", "Email", "IsDeleted", "IsLocked", "Name", "Remark", "Tel", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, 1305892579553280000L, "gardener@163.com", false, false, "租户1", "预设数据。", "400-888-8888", null, null, null },
                    { new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, 1305892579553280000L, "gardener@163.com", false, false, "租户2", "预设数据。", "400-888-8888", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Code",
                columns: new[] { "Id", "CodeName", "CodeTypeId", "CodeValue", "Color", "CreateBy", "CreateIdentityType", "CreatedTime", "ExtendParams", "IsDeleted", "IsLocked", "Order", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { 1, "难过", 1, "1", null, "2", 1, 1306966795612160480L, null, false, false, 1, null, null, null },
                    { 2, "高兴", 1, "2", null, "2", 1, 1306966795345920480L, null, false, false, 2, null, null, null },
                    { 3, "乒乓球", 2, "pingpang", null, "6", 1, 1306991751884800480L, null, false, false, 10, "6", 1, 1306991752990720480L },
                    { 4, "篮球", 2, "basketball", null, "6", 1, 1306991752765440480L, null, false, false, 20, null, null, null },
                    { 5, "游泳", 2, "swimming", null, "6", 1, 1306991753584640480L, null, false, false, 30, null, null, null },
                    { 6, "足球", 2, "football", null, "6", 1, 1306991754936320480L, null, false, false, 40, null, null, null },
                    { 7, "P1", 3, "p1", null, "6", 1, 1306992882278400480L, null, false, false, 10, null, null, null },
                    { 8, "P2", 3, "p2", null, "6", 1, 1306992882688000480L, null, false, false, 20, null, null, null },
                    { 9, "P3", 3, "p3", null, "6", 1, 1306992882954240480L, null, false, false, 30, null, null, null },
                    { 10, "P4", 3, "p4", null, "6", 1, 1306992883343360480L, null, false, false, 40, null, null, null },
                    { 11, "P5", 3, "p5", null, "6", 1, 1306992883630080480L, null, false, false, 50, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Dept",
                columns: new[] { "Id", "Contacts", "CreateBy", "CreateIdentityType", "CreatedTime", "IsDeleted", "IsLocked", "Name", "Order", "ParentId", "Remark", "Tel", "TenantId", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { 2, "老B", null, null, 1305892579553280000L, false, false, "昌平办事处", 1, 1, "昌平办事处", "400-8888888", null, null, null, null },
                    { 3, "老C", null, null, 1305892579553280000L, false, false, "海淀办事处", 1, 1, "海淀办事处", "400-8888888", null, null, null, null },
                    { 5, "老E", null, null, 1305892579553280000L, false, false, "石家庄办事处", 1, 4, "石家庄办事处", "400-8888888", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreateBy", "CreateIdentityType", "CreatedTime", "Hide", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "SupportMultiTenant", "Type", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"), "4", 1, 1306984533504000480L, false, null, false, false, "global_wo_chat_btn", "WoChat聊天按钮", 0, new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"), null, "WoChat聊天按钮显资源", true, 2000, null, null, null },
                    { new Guid("371b335b-29e5-4846-b6de-78c9cc691717"), null, 0, 1306051389542400480L, false, "home", false, false, "admin_home", "首页", 10, new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"), "/", "", true, 1000, null, null, null },
                    { new Guid("62e874c8-d286-4b28-831b-90d0c49f0908"), "2", 1, 1307020067348480480L, false, null, false, false, "system_tenant_administrator", "后台租户管理员", 0, new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"), null, "控制全局租户信息的显示，此资源不要分配给租户", false, 2000, "2", 1, 1307021551575040480L },
                    { new Guid("874b5529-81d5-4338-9ba9-c084a2e833f1"), "6", 1, 1306994998394880480L, false, null, false, false, "system_code_utils", "字典工具", 0, new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"), null, null, true, 2000, "6", 1, 1306994999357440480L },
                    { new Guid("ba411ee1-f545-4bf6-8b56-18b8ed6f88fe"), "2", 1, 1307060617400320480L, false, "user", false, false, "account_center", "个人中心", 100, new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"), null, null, true, 1000, "2", 1, 1307060617809920480L },
                    { new Guid("bd892fb3-47b4-469e-ba14-7c0eb703e164"), null, 0, 1306546809856000480L, false, "apartment", false, false, "user_center", "用户中心", 15, new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"), "", "用户中心", true, 1000, null, null, null },
                    { new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), null, 0, 1306546809856000480L, false, "setting", false, false, "system_manager", "系统管理", 20, new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"), "", "系统管理", true, 1000, null, null, null },
                    { new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), null, 0, 1306051389542400480L, false, "", false, false, "system_login", "登录", 0, new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"), "", "登录系统", true, 2000, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreateBy", "CreateIdentityType", "CreatedTime", "IsDeleted", "IsLocked", "TenantId", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null }
                });

            migrationBuilder.InsertData(
                table: "SystemTenantResource",
                columns: new[] { "ResourceId", "TenantId", "CreateBy", "CreateIdentityType", "CreatedTime", "IsDeleted", "IsLocked", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreateBy", "CreateIdentityType", "CreatedTime", "Hide", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "SupportMultiTenant", "Type", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), null, 0, 1306051389542400480L, false, "api", false, false, "system_manager_function", "接口管理", 40, new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), "/system_manager/function", "", false, 1000, null, null, null },
                    { new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), null, 0, 1306051389542400480L, false, "menu", false, false, "system_manager_resource", "资源管理", 30, new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), "/system_manager/resource", "", false, 1000, null, null, null },
                    { new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), null, 0, 1306051389542400480L, false, "user-switch", false, false, "user_center_role", "角色管理", 20, new Guid("bd892fb3-47b4-469e-ba14-7c0eb703e164"), "/user_center/role", "", true, 1000, null, null, null },
                    { new Guid("2dd1a78c-f725-461b-8bc6-66112a7e156c"), null, 0, 1306546809856000480L, false, "audit", false, false, "system_manager_audit", "审计管理", 60, new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), "", "审计管理", true, 1000, null, null, null },
                    { new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), null, 0, 1306051389542400480L, false, "crown", false, false, "user_center_position", "岗位管理", 5, new Guid("bd892fb3-47b4-469e-ba14-7c0eb703e164"), "/user_center/position", "", true, 1000, null, null, null },
                    { new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"), "1", 1, 1306683595427840480L, false, "code-sandbox", false, false, "system_tool_code_gen", "代码生成", 41, new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), "/system_tool/code_gen", null, false, 1000, null, null, 1306683596595200480L },
                    { new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), null, 0, 1306051389542400480L, false, "team", false, false, "user_center_dept", "部门管理", 3, new Guid("bd892fb3-47b4-469e-ba14-7c0eb703e164"), "/user_center/dept", "", true, 1000, null, null, null },
                    { new Guid("6dc2b297-7110-462a-b402-9e9736abf292"), null, 0, 1306051389542400480L, false, "mail", false, false, "system_manager_email_tool", "邮件工具", 80, new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), "", "邮件工具", false, 1000, null, null, null },
                    { new Guid("74a75b21-3fcf-4c26-b998-aa4f0b658292"), "2", 1, 1307060619407360480L, false, null, false, false, "account_center_settings", "个人设置", 0, new Guid("ba411ee1-f545-4bf6-8b56-18b8ed6f88fe"), "/account/settings", null, true, 1000, "2", 1, 1307060658892800480L },
                    { new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), null, 0, 1306051389542400480L, false, "user", false, false, "user_center_user", "用户管理", 10, new Guid("bd892fb3-47b4-469e-ba14-7c0eb703e164"), "/user_center/user", "用户管理", true, 1000, null, null, null },
                    { new Guid("925c3162-155c-4644-8ca2-075f9fc76235"), null, 0, 1306051389542400480L, false, "file", false, false, "system_manager_attachment", "附件管理", 50, new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), "/system_manager/attachment", "附件管理", true, 1000, null, null, null },
                    { new Guid("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"), null, 0, 1306051389542400480L, false, "cloud-server", false, false, "system_manager_client", "客户端管理", 45, new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), "/system_manager/client", "客户端管理", false, 1000, null, null, null },
                    { new Guid("b99ad8cf-68db-49aa-838f-17d57429d9c5"), "1", 1, 1306964577628160480L, false, "tags", false, false, "system_manager_code_manager", "字典管理", 90, new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), null, null, false, 1000, "1", 1, 1306964582297600480L },
                    { new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"), null, 0, 1306051389542400480L, false, "idcard", false, false, "system_manager_login_token", "登录管理", 70, new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), "/system_manager/login-token", "", true, 1000, null, null, null },
                    { new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"), "6", 1, 1307019908341760480L, false, "deployment-unit", false, false, "user_center_tenant", "租户管理", 0, new Guid("bd892fb3-47b4-469e-ba14-7c0eb703e164"), "/user_center/tenant", null, false, 1000, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[,]
                {
                    { new Guid("0367ad11-0be0-48dd-a5a9-1d473b78c0bf"), new Guid("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"), 1306984589557760480L },
                    { new Guid("1adcacf3-33ae-4b36-b5c9-dcd95151ef3a"), new Guid("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"), 1306984539402240480L },
                    { new Guid("27d997d7-e691-4dbe-b5e0-74acbca53d98"), new Guid("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"), 1306984539402240480L },
                    { new Guid("333a3802-f8d2-4625-9476-dee8bf43fd0d"), new Guid("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"), 1306984539402240480L },
                    { new Guid("42636f5d-d5f6-4f64-bd2b-e77d80e51ff2"), new Guid("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"), 1306984539402240480L },
                    { new Guid("752b1623-8898-431f-ab3f-db9ebffae4e6"), new Guid("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"), 1306984539402240480L },
                    { new Guid("97b24035-cd96-4e28-bc42-103ac7e5fe3f"), new Guid("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"), 1306984539402240480L },
                    { new Guid("bdab8953-956d-4b1a-945b-b1806e9ac749"), new Guid("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"), 1306984572805120480L },
                    { new Guid("be639780-fbd0-4335-95e3-91e099d87850"), new Guid("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"), 1306984539791360480L },
                    { new Guid("d481028f-d67e-4ee0-b237-a4883c618486"), new Guid("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"), 1306984539402240480L },
                    { new Guid("ea2a64ac-bde9-45e5-a879-10b161b3f825"), new Guid("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"), 1306984539402240480L },
                    { new Guid("f78ea06a-4c55-4445-9e16-bdc92c9b9fa6"), new Guid("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"), 1306984539402240480L },
                    { new Guid("8be6d20e-686c-4259-8eeb-3ec2b18739c3"), new Guid("371b335b-29e5-4846-b6de-78c9cc691717"), 1306540222341120480L },
                    { new Guid("0837b06f-e1d7-4ed4-9204-0c237aac6978"), new Guid("62e874c8-d286-4b28-831b-90d0c49f0908"), 1307021769543680480L },
                    { new Guid("22f82d98-9176-4391-a950-1d1d01e105fd"), new Guid("62e874c8-d286-4b28-831b-90d0c49f0908"), 1307021769543680480L },
                    { new Guid("cb4fbf67-4747-43d5-a355-7300f007cce3"), new Guid("62e874c8-d286-4b28-831b-90d0c49f0908"), 1307021769543680480L },
                    { new Guid("09d15841-71c7-4afc-99f8-dd906a0248d9"), new Guid("874b5529-81d5-4338-9ba9-c084a2e833f1"), 1307020005580800480L },
                    { new Guid("4e85ec32-8a3c-46e9-ba60-4abd7bee6745"), new Guid("874b5529-81d5-4338-9ba9-c084a2e833f1"), 1306995002163200480L },
                    { new Guid("ed8c2fae-c63f-4aec-af4b-e915b6db38a2"), new Guid("874b5529-81d5-4338-9ba9-c084a2e833f1"), 1306995002163200480L },
                    { new Guid("03ee6f4b-dfea-4803-9515-3a9b2f907c90"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1306582939402240480L },
                    { new Guid("1c6dfb26-4149-4fa3-a7de-083ad7ff7d6c"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1306582939402240480L },
                    { new Guid("38545a67-61ff-4e5c-90bb-a555a93fcbea"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1306582939402240480L },
                    { new Guid("68ce42ff-acc7-485f-bc91-df471b520be7"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1306582939402240480L },
                    { new Guid("713341f2-47e1-42af-b717-bfa75904d32e"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1306582939402240480L },
                    { new Guid("89a06a4e-1a8e-41aa-a443-fd11bcc8497d"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1306582939402240480L },
                    { new Guid("c96dd7f7-f935-4499-8ef5-6d39fe26141a"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1306582939402240480L },
                    { new Guid("e0b19b01-4b37-426a-a5c4-749e5936b64c"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1306971988418560480L },
                    { new Guid("e2bb65e0-5d9e-485e-9059-8148fc236246"), new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 1306582939402240480L }
                });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreateBy", "CreateIdentityType", "CreatedTime", "IsDeleted", "IsLocked", "TenantId", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("371b335b-29e5-4846-b6de-78c9cc691717"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("874b5529-81d5-4338-9ba9-c084a2e833f1"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("bd892fb3-47b4-469e-ba14-7c0eb703e164"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("371b335b-29e5-4846-b6de-78c9cc691717"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("874b5529-81d5-4338-9ba9-c084a2e833f1"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("bd892fb3-47b4-469e-ba14-7c0eb703e164"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("371b335b-29e5-4846-b6de-78c9cc691717"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("874b5529-81d5-4338-9ba9-c084a2e833f1"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("bd892fb3-47b4-469e-ba14-7c0eb703e164"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null }
                });

            migrationBuilder.InsertData(
                table: "SystemTenantResource",
                columns: new[] { "ResourceId", "TenantId", "CreateBy", "CreateIdentityType", "CreatedTime", "IsDeleted", "IsLocked", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("371b335b-29e5-4846-b6de-78c9cc691717"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("874b5529-81d5-4338-9ba9-c084a2e833f1"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("bd892fb3-47b4-469e-ba14-7c0eb703e164"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("371b335b-29e5-4846-b6de-78c9cc691717"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("874b5529-81d5-4338-9ba9-c084a2e833f1"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("bd892fb3-47b4-469e-ba14-7c0eb703e164"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("c2090656-8a05-4e67-b7ea-62f178639620"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Avatar", "CreateBy", "CreateIdentityType", "CreatedTime", "DeptId", "Email", "EmailConfirmed", "Gender", "IsDeleted", "IsLocked", "NickName", "Password", "PasswordEncryptKey", "PhoneNumber", "PhoneNumberConfirmed", "PositionId", "TenantId", "UpdateBy", "UpdateIdentityType", "UpdatedTime", "UserName" },
                values: new object[,]
                {
                    { 1, "./assets/logo.png", null, null, 1305892579553280000L, 2, null, false, 0, false, false, "管理员", "6b8ecfe60e9d1945869fdfc7e65c1315", "032854df-332d-4c60-905a-fb9487b711e4", null, false, 1, null, null, null, null, "admin" },
                    { 2, "./avatars/2.jpeg", null, null, 1305892579553280000L, 2, null, false, 0, false, false, "管理员2", "6b8ecfe60e9d1945869fdfc7e65c1315", "032854df-332d-4c60-905a-fb9487b711e4", null, false, 1, null, null, null, null, "admin2" },
                    { 3, "./avatars/3.jpeg", null, null, 1305892579553280000L, 2, null, false, 0, false, false, "管理员3", "6b8ecfe60e9d1945869fdfc7e65c1315", "032854df-332d-4c60-905a-fb9487b711e4", null, false, 1, null, null, null, null, "admin3" },
                    { 4, "./avatars/4.jpeg", null, null, 1305892579553280000L, 2, null, false, 0, false, false, "管理员4", "6b8ecfe60e9d1945869fdfc7e65c1315", "032854df-332d-4c60-905a-fb9487b711e4", null, false, 1, null, null, null, null, "admin4" },
                    { 5, "./avatars/5.jpeg", null, null, 1305892579553280000L, 2, null, false, 0, false, false, "管理员5", "6b8ecfe60e9d1945869fdfc7e65c1315", "032854df-332d-4c60-905a-fb9487b711e4", null, false, 1, null, null, null, null, "admin5" },
                    { 6, "./avatars/6.jpg", null, null, 1305892579553280000L, 2, null, false, 0, false, false, "管理员6", "6b8ecfe60e9d1945869fdfc7e65c1315", "032854df-332d-4c60-905a-fb9487b711e4", null, false, 1, null, null, null, null, "admin6" },
                    { 7, "./avatars/1.jpeg", null, null, 1305892579553280000L, 2, null, false, 0, false, false, "管理员1", "6b8ecfe60e9d1945869fdfc7e65c1315", "032854df-332d-4c60-905a-fb9487b711e4", null, false, 1, null, null, null, null, "admin1" },
                    { 8, "./avatars/7.jpeg", null, null, 1305892579553280000L, 3, null, false, 0, false, false, "测试员", "60759dd06243d0837b88ab9b7183e6df", "032854df-332d-4c60-905a-fb9487b711e4", null, false, 2, null, null, null, null, "testuser" },
                    { 9, "./avatars/8.jpeg", null, null, 1305892579553280000L, 2, null, false, 0, false, false, "东方财富播报", "bed99107b3729bb4cc015185ff30aaff", "032854df-332d-4c60-905a-fb9487b711e4", null, false, 1, null, null, null, null, "dongfangcaifu" },
                    { 10, "./avatars/9.jpeg", null, null, 1305892579553280000L, 2, null, false, 0, false, false, "租户1用户1", "40a693e8e3fad5807e0a9f241752dd2c", "032854df-332d-4c60-905a-fb9487b711e4", null, false, 1, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null, "zuhu1_user1" },
                    { 11, "./avatars/10.jpeg", null, null, 1305892579553280000L, 2, null, false, 0, false, false, "租户2用户1", "093195e5bda467b3c6f791e530bbf996", "032854df-332d-4c60-905a-fb9487b711e4", null, false, 1, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null, "zuhu2_user1" },
                    { 12, "./avatars/11.jpeg", null, null, 1305892579553280000L, 2, null, false, 0, false, false, "租户2用户2", "55ac694faa3b8ca826beb0cea3f2b4c4", "032854df-332d-4c60-905a-fb9487b711e4", null, false, 1, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null, "zuhu2_user2" }
                });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreateBy", "CreateIdentityType", "CreatedTime", "Hide", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "SupportMultiTenant", "Type", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("04c237bb-7670-4d66-bbaa-dcd9624d2d90"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_dept_add_children", "添加子级部门", 0, new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), "", "", true, 2000, null, null, null },
                    { new Guid("07af05b1-6f3e-49fa-9959-463e246346df"), "6", 1, 1307019922186240480L, false, null, false, false, "user_center_tenant_detail", "查看租户", 0, new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"), null, null, false, 2000, "6", 1, 1307019929804800480L },
                    { new Guid("08ae2764-e551-45d2-9da7-49648481a8e0"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_resource_delete_selected", "删除选中", 0, new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), "", "删除选中", false, 2000, null, null, null },
                    { new Guid("0aa9b237-dab8-472e-b2e6-af9c0af9f916"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_user_edit", "编辑用户", 4, new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), "", "", true, 2000, null, null, null },
                    { new Guid("0cbb3d40-de41-483e-a76c-3d85682176af"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_login_token_lock", "锁定登录Token", 0, new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"), "", "", true, 2000, null, null, null },
                    { new Guid("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_user_detail", "查看用户", 0, new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), "", "查看用户", true, 2000, null, null, null },
                    { new Guid("0fd84267-ee22-47c4-b41c-ce654eba29d9"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_position_add", "添加岗位", 0, new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), "", "", true, 2000, null, null, null },
                    { new Guid("13e7d01e-93ca-429c-b412-ff6fa5b6a026"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_role_edit", "编辑角色", 4, new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), "", "", true, 2000, null, null, null },
                    { new Guid("186bca5f-cc2c-427e-a58a-dbb81641a296"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_dept_refresh", "刷新部门", 0, new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), "", "", true, 2000, null, null, null },
                    { new Guid("1d2fb341-3b69-4d0b-934d-c4c2cd250401"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_dept_delete_selected", "删除选中部门", 0, new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), "", "", true, 2000, null, null, null },
                    { new Guid("1efd01cf-42f2-45c7-95f2-84be55e65646"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_role_refresh", "刷新角色", 3, new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), "", "", true, 2000, null, null, null },
                    { new Guid("25535592-81a1-42dd-8a55-509f2c852ff9"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_position_edit", "编辑岗位", 0, new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), "", "", true, 2000, null, null, null },
                    { new Guid("2c1c895c-6434-4f14-91f2-144e48457101"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_role_detail", "查看角色详情", 0, new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), "", "查看角色详情", true, 2000, null, null, null },
                    { new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"), "1", 1, 1306964580823040480L, false, null, false, false, "system_manager_code_type", "字典类型", 10, new Guid("b99ad8cf-68db-49aa-838f-17d57429d9c5"), "/system_manager/code_type", null, false, 1000, "2", 1, 1306966617313280480L },
                    { new Guid("316ecba5-5d89-44ae-908f-a54268723bd1"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_dept_edit", "编辑部门", 0, new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), "", "", true, 2000, null, null, null },
                    { new Guid("365fc5c4-404e-408a-88dc-7614dffad91b"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_resource_refresh", "刷新资源", 0, new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), "", "", false, 2000, null, null, null },
                    { new Guid("374f7bfd-3c16-40dd-b4dc-a5992a0915cf"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_client_refresh", "刷新客户端", 0, new Guid("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"), "", "刷新客户端", false, 2000, null, null, null },
                    { new Guid("3a2c9195-9a5c-42c7-b5dc-7300bbc66e8c"), "5", 1, 1307030672834560480L, false, null, false, false, "user_center_tenant_set_resource", "绑定资源", 0, new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"), null, null, false, 2000, null, null, null },
                    { new Guid("3d007d84-d209-49e2-94ca-11ad2a3dd91d"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_login_token_delete", "删除登录Token", 0, new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"), "", "", true, 2000, null, null, null },
                    { new Guid("3d6e9553-2baf-4d9d-8a82-65de1c7d7ece"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_position_delete_selected", "删除选中岗位", 0, new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), "", "", true, 2000, null, null, null },
                    { new Guid("4171f5aa-2ce1-40ad-b69e-59de1cd20416"), "1", 1, 1306552948224000480L, false, null, false, false, "system_manager_function_export", "导出接口", 0, new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), null, "导出接口", false, 2000, null, null, null },
                    { new Guid("46cad808-0d0b-42bb-a134-3ad6db8ebf54"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_user_role_edit", "用户分配角色", 5, new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), "", "", true, 2000, null, null, null },
                    { new Guid("476cf96a-0e18-4c30-a760-e8b9c615bb99"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_user_delete_selected", "删除选中用户", 0, new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), "", "删除选中", true, 2000, null, null, null },
                    { new Guid("490bc05f-499e-4f4c-811d-fde4c10be2ed"), "6", 1, 1307020016025600480L, false, null, false, false, "user_center_user_role_edit_save", "保存用户角色", 0, new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), null, null, true, 2000, "6", 1, 1307020019138560480L },
                    { new Guid("4db9a237-1343-4c4a-91f6-9a40fb9f0e2a"), "6", 1, 1307019916656640480L, false, null, false, false, "user_center_tenant_lock", "锁定租户", 0, new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"), null, null, false, 2000, null, null, null },
                    { new Guid("4e845d07-33a4-4dc4-ba7f-8568f88b9d68"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_position_delete", "删除岗位", 0, new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), "", "", true, 2000, null, null, null },
                    { new Guid("50062351-8235-4da1-9f90-4917d0e8abe0"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_function_edit", "编辑接口", 0, new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), "", "", false, 2000, null, null, null },
                    { new Guid("67501fd4-4fbf-48c2-b383-f3a2085268ed"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_role_add", "添加角色", 2, new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), "", "", true, 2000, null, null, null },
                    { new Guid("67ad5c3a-8611-4183-ad9e-63cb4c9760fa"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_attachment_detail", "查看附件", 0, new Guid("925c3162-155c-4644-8ca2-075f9fc76235"), "", "查看附件", true, 2000, null, null, null },
                    { new Guid("6ac07813-4d10-4b50-9f0c-ecd444041282"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_function_refresh", "刷新接口", 0, new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), "", "", false, 2000, null, null, null },
                    { new Guid("6e487179-5bb2-4ab5-80e3-58c514c9595f"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_function_enable_audit", "锁定接口", 0, new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), "", "", false, 2000, null, null, null },
                    { new Guid("749c3a63-6bd8-4755-87ed-c1d455e5b717"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_function_import", "导入接口", 0, new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), "", "", false, 2000, null, null, null },
                    { new Guid("757fdf0b-0cb9-4f24-92f6-24e18f3defcc"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_client_add", "添加客户端", 0, new Guid("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"), "", "添加客户端", false, 2000, null, null, null },
                    { new Guid("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_role_set_resource", "角色分配资源", 5, new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), "", "", true, 2000, null, null, null },
                    { new Guid("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_client_show_function", "关联客户端接口关系", 0, new Guid("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"), "", "关联客户端接口关系", false, 2000, null, null, null },
                    { new Guid("859aa714-67c7-4414-bc96-9de5b7aec2c4"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_resource_download_seed_data", "导出种子数据", 0, new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), "", "", false, 2000, null, null, null },
                    { new Guid("87377abe-785d-426c-b052-f706a2c7173d"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_user_lock", "锁定用户", 7, new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), "", "", true, 2000, null, null, null },
                    { new Guid("8a4e9aee-b116-4822-bd59-b3a98e84b9f3"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_client_detail", "查看客户端", 0, new Guid("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"), "", "查看客户端", false, 2000, null, null, null },
                    { new Guid("8ab307de-ad4b-462f-b61d-7f1d53b82f3d"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_resource_add", "添加资源", 0, new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), "", "添加资源", false, 2000, null, null, null },
                    { new Guid("8b2007b4-821b-49fc-aa5d-35ebc4dbe3c9"), "6", 1, 1307019914690560480L, false, null, false, false, "user_center_tenant_refresh", "刷新租户", 0, new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"), null, null, false, 2000, "6", 1, 1307019926507520480L },
                    { new Guid("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"), null, 0, 1306051389542400480L, false, "copy", false, false, "system_manager_email_temaplate", "邮件模板", 20, new Guid("6dc2b297-7110-462a-b402-9e9736abf292"), "/system_manager/email_temaplate", "邮件模板", false, 1000, null, null, null },
                    { new Guid("92da96d7-c59c-4d4b-8c97-80a9f59e8fa2"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_function_detail", "查看接口详情", 0, new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), "", "查看接口详情", false, 2000, null, null, null },
                    { new Guid("92ed8299-ff26-4fae-b852-fe33f0c01a09"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_client_edit", "编辑客户端", 0, new Guid("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"), "", "编辑客户端", false, 2000, null, null, null },
                    { new Guid("94d2c383-03b6-475c-a744-637dd87a5fdc"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_position_lock", "锁定岗位", 0, new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), "", "锁定岗位", true, 2000, null, null, null },
                    { new Guid("97a7d440-b7fe-4af6-a8a1-18846c48828b"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_resource_delete", "删除资源", 0, new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), "", "删除资源", false, 2000, null, null, null },
                    { new Guid("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_user_add", "添加用户", 2, new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), "", "", true, 2000, null, null, null },
                    { new Guid("99c74c8b-e343-43bc-86e3-bca825b6a270"), null, 0, 1306051389542400480L, false, "setting", false, false, "system_manager_email_server_config", "邮件服务器", 10, new Guid("6dc2b297-7110-462a-b402-9e9736abf292"), "/system_manager/email_server_config", "邮件服务器配置", false, 1000, null, null, null },
                    { new Guid("a1260e4c-e67c-4d72-a758-560a13e9c496"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_client_delete", "删除客户端", 0, new Guid("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"), "", "删除客户端", false, 2000, null, null, null },
                    { new Guid("a1958e51-06d4-4b29-9533-eae9d86c41d1"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_resource_lock", "锁定资源", 0, new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), "", "", false, 2000, null, null, null },
                    { new Guid("a25da8f5-23d4-4118-b399-0a36f912a370"), "1", 1, 1306552952463360480L, false, null, false, false, "user_center_user_export", "导出用户", 0, new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), null, "导出用户", true, 2000, null, null, null },
                    { new Guid("a468499c-7115-44f1-ad38-2c5f696891d4"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_role_delete_selected", "删除选中角色", 0, new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), "", "", true, 2000, null, null, null },
                    { new Guid("a7a949b0-ca8e-47a1-a5be-ce0fa3c501e6"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_client_delete_selected", "删除选中客户端", 0, new Guid("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"), "", "删除选中客户端", false, 2000, null, null, null },
                    { new Guid("b100a7eb-ef44-4669-bac5-3c5ce52871bb"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_function_delete", "删除接口", 0, new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), "", "", false, 2000, null, null, null },
                    { new Guid("b4072d45-f643-4bdb-a63e-7286cfa9c62b"), "6", 1, 1307019920977920480L, false, null, false, false, "user_center_tenant_edit", "编辑租户", 0, new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"), null, null, false, 2000, null, null, null },
                    { new Guid("b63d694e-205f-44c0-8353-0c9507f44696"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_dept_detail", "查看部门详情", 0, new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), "", "查看部门详情", true, 2000, null, null, null },
                    { new Guid("b71bbc5f-83a3-4065-b561-cb4b69b4a507"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_role_lock", "锁定角色", 7, new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), "", "", true, 2000, null, null, null },
                    { new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_audit_operation", "操作审计", 1, new Guid("2dd1a78c-f725-461b-8bc6-66112a7e156c"), "/system_manager/audit-operation", "操作审计", true, 1000, null, null, null },
                    { new Guid("ba89c7b7-552c-415c-b4be-085262dc76b0"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_position_detail", "查看岗位", 0, new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), "", "查看岗位", true, 2000, null, null, null },
                    { new Guid("bd7d1a4c-960a-48b2-9c9e-083aa5c5924f"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_function_delete_selected", "删除选中接口", 0, new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), "", "", false, 2000, null, null, null },
                    { new Guid("bddc6ccc-3f93-4be7-8756-15613cdf76b6"), "1", 1, 1306552963031040480L, false, null, false, false, "system_manager_login_token_export", "导出登录数据", 0, new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"), null, "导出登录数据", true, 2000, null, null, null },
                    { new Guid("bf05ffe8-c3ff-402d-bef1-3e95d202fd03"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_role_resource_download_seed_data", "获取种子数据", 0, new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), "", "", true, 2000, null, null, null },
                    { new Guid("c18d4928-35d2-4085-aec9-379d00bcfd8f"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_resource_add_children", "添加子资源", 0, new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), "", "", false, 2000, null, null, null },
                    { new Guid("c98160ef-ce87-4a1b-bfb3-09fc79d2a34a"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_login_token_refresh", "刷新登录Token", 0, new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"), "", "", true, 2000, null, null, null },
                    { new Guid("cc8a9836-3c4d-4d0b-ae64-a31a6bb36b6f"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_function_download_seed_data", "查看接口种子数据", 0, new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), "", "查看接口种子数据", false, 2000, null, null, null },
                    { new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_audit_entity", "数据审计", 2, new Guid("2dd1a78c-f725-461b-8bc6-66112a7e156c"), "/system_manager/audit-entity", "数据审计", true, 1000, null, null, null },
                    { new Guid("d45effb9-67a8-4459-83ac-c3852c8b4f1f"), "6", 1, 1307019913543680480L, false, null, false, false, "user_center_tenant_add", "添加租户", 0, new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"), null, null, false, 2000, null, null, null },
                    { new Guid("d5756ad0-6a8b-4462-907f-1c52a1e11369"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_user_delete", "删除用户", 1, new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), "", "", true, 2000, null, null, null },
                    { new Guid("d5e3497b-c624-4fde-96bd-108a33cacc6d"), "1", 1, 1306961904844800480L, false, null, false, false, "system_manager_code", "字典", 20, new Guid("b99ad8cf-68db-49aa-838f-17d57429d9c5"), "/system_manager/code_list", "字典管理", false, 1000, "2", 1, 1306966617108480480L },
                    { new Guid("d83c05a0-4d23-4b2b-ba87-284793bf3eba"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_resource_detail", "查看资源", 0, new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), "", "查看资源", false, 2000, null, null, null },
                    { new Guid("d92268ec-6b51-4514-9487-52cb3fb0d850"), "6", 1, 1307019930869760480L, false, null, false, false, "user_center_tenant_delete", "删除租户", 0, new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"), null, null, false, 2000, null, null, null },
                    { new Guid("d982a072-4681-45d9-8489-7a14218adb04"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_role_delete", "删除角色", 1, new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), "", "", true, 2000, null, null, null },
                    { new Guid("d998802f-776e-4137-bc63-d8d818464f98"), null, 0, 1306051389542400480L, false, "null", false, false, "system_manager_attachment_delete_selected", "删除选中附件", 0, new Guid("925c3162-155c-4644-8ca2-075f9fc76235"), "null", "删除选中附件", true, 2000, null, null, null },
                    { new Guid("de62a886-64b2-4a40-b70a-47eb08f23202"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_dept_delete", "删除部门", 0, new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), "", "", true, 2000, null, null, null },
                    { new Guid("dec04485-3dab-4251-b7b8-1044e749a51e"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_resource_edit", "编辑资源", 0, new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), "", "", false, 2000, null, null, null },
                    { new Guid("defa9a78-229f-43a9-b6b8-95dd6fd8a3c3"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_dept_add", "添加部门", 0, new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), "", "", true, 2000, null, null, null },
                    { new Guid("e252c0c6-0f19-4768-954c-c0d83fb96d74"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_resource_show_function", "关联资源接口", 0, new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), "", "", false, 2000, null, null, null },
                    { new Guid("e44bb45d-514c-4217-bfba-452c0bd38f28"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_user_refresh", "刷新用户", 3, new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), "", "", true, 2000, null, null, null },
                    { new Guid("e600186f-dfbe-40dc-bf5d-16a2a01ffc6a"), "5", 1, 1307030707814400480L, false, null, false, false, "user_center_role_set_is_super_administrator", "设置角色为超级管理员", 0, new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), null, "分配该资源后，才能设置角色为超级管理员，请谨慎分配该资源。", false, 3000, null, null, null },
                    { new Guid("ea0fb035-1f06-4f61-9946-8df027a7462d"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_user_list_edit_avatar", "编辑用户头像-列表中", 8, new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), "", "编辑用户头像-列表中", true, 2000, null, null, null },
                    { new Guid("efbcc18b-c193-42cc-b315-cde07f51b496"), "6", 1, 1307019911249920480L, false, null, false, false, "user_center_tenant_delete_selected", "删除选中租户", 0, new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"), null, null, false, 2000, null, null, null },
                    { new Guid("f02f906a-7579-478a-9406-3c8fd2c54886"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_attachment_delete", "删除附件", 0, new Guid("925c3162-155c-4644-8ca2-075f9fc76235"), "", "", true, 2000, null, null, null },
                    { new Guid("f077211f-0e79-44a3-935c-0f704f6a5962"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_login_token_delete_selected", "删除选中登录Token", 0, new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"), "", "", true, 2000, null, null, null },
                    { new Guid("f1649263-ef9a-4f42-85ac-16009283efff"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_attachment_refresh", "刷新附件", 0, new Guid("925c3162-155c-4644-8ca2-075f9fc76235"), "", "", true, 2000, null, null, null },
                    { new Guid("f4fa035f-27ae-4eee-b006-3cbfac3d2172"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_position_refresh", "刷新岗位", 0, new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), "", "", true, 2000, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[,]
                {
                    { new Guid("416fe54b-6c50-4b1b-bf77-6744cf19fa72"), new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), 1306069130997760480L },
                    { new Guid("e38c1619-0f84-4e55-81c2-0f47992ee33d"), new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), 1306069130997760480L },
                    { new Guid("498638f7-dc92-4d0e-ac5e-26e48cf87a8d"), new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), 1306069130997760480L },
                    { new Guid("c715a6d5-cd99-4c94-8760-936817c1e09c"), new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), 1306069130997760480L },
                    { new Guid("6e2ff941-1e79-4e2a-bbe9-ec68318e5d3a"), new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), 1306582975447040480L },
                    { new Guid("cbc8aff4-6dc0-41f2-b684-caba8e0657ac"), new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), 1306069130997760480L },
                    { new Guid("6a9763c9-c40f-44f3-a248-a3b1e3d1f586"), new Guid("925c3162-155c-4644-8ca2-075f9fc76235"), 1306069130997760480L },
                    { new Guid("83cc7cb7-dac6-49f2-85fa-e903039f3d0a"), new Guid("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"), 1306069130997760480L },
                    { new Guid("e651d9a4-9d6d-44c7-a833-08da6ed19892"), new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"), 1306550924513280480L }
                });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreateBy", "CreateIdentityType", "CreatedTime", "IsDeleted", "IsLocked", "TenantId", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("2dd1a78c-f725-461b-8bc6-66112a7e156c"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("6dc2b297-7110-462a-b402-9e9736abf292"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("925c3162-155c-4644-8ca2-075f9fc76235"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("b99ad8cf-68db-49aa-838f-17d57429d9c5"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("2dd1a78c-f725-461b-8bc6-66112a7e156c"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("925c3162-155c-4644-8ca2-075f9fc76235"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("2dd1a78c-f725-461b-8bc6-66112a7e156c"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("925c3162-155c-4644-8ca2-075f9fc76235"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null }
                });

            migrationBuilder.InsertData(
                table: "SystemTenantResource",
                columns: new[] { "ResourceId", "TenantId", "CreateBy", "CreateIdentityType", "CreatedTime", "IsDeleted", "IsLocked", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("2dd1a78c-f725-461b-8bc6-66112a7e156c"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("925c3162-155c-4644-8ca2-075f9fc76235"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("2dd1a78c-f725-461b-8bc6-66112a7e156c"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("925c3162-155c-4644-8ca2-075f9fc76235"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "UserExtension",
                columns: new[] { "UserId", "CityId", "CreateBy", "CreateIdentityType", "CreatedTime", "IsDeleted", "IsLocked", "QQ", "TenantId", "UpdateBy", "UpdateIdentityType", "UpdatedTime", "WeChat" },
                values: new object[] { 8, null, null, null, 0L, false, false, "123456", null, null, null, null, null });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId", "CreateBy", "CreateIdentityType", "CreatedTime", "IsDeleted", "IsLocked", "TenantId", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { 1, 1, "1", 1, 1305892579553280000L, false, false, null, null, null, null },
                    { 1, 2, "1", 1, 1305892579553280000L, false, false, null, null, null, null },
                    { 1, 3, "1", 1, 1305892579553280000L, false, false, null, null, null, null },
                    { 1, 4, "1", 1, 1305892579553280000L, false, false, null, null, null, null },
                    { 1, 5, "1", 1, 1305892579553280000L, false, false, null, null, null, null },
                    { 1, 6, "1", 1, 1305892579553280000L, false, false, null, null, null, null },
                    { 1, 7, "1", 1, 1305892579553280000L, false, false, null, null, null, null },
                    { 2, 8, "1", 1, 1305892579553280000L, false, false, null, null, null, null },
                    { 3, 10, "1", 1, 1305892579553280000L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { 4, 11, "1", 1, 1305892579553280000L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { 4, 12, "1", 1, 1305892579553280000L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "CreateBy", "CreateIdentityType", "CreatedTime", "Hide", "Icon", "IsDeleted", "IsLocked", "Key", "Name", "Order", "ParentId", "Path", "Remark", "SupportMultiTenant", "Type", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("02337e03-c44f-4029-bbb2-0cc5adf84c29"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_email_server_config_lock", "锁定邮件服务器配置", 0, new Guid("99c74c8b-e343-43bc-86e3-bca825b6a270"), "", "锁定邮件服务器配置", false, 2000, null, null, null },
                    { new Guid("083fffc4-2600-49bb-87e6-1a92133499ec"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_email_template_add", "添加邮件模板", 0, new Guid("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"), "", "添加邮件模板", false, 2000, null, null, null },
                    { new Guid("0898c23e-3c3c-4d7f-82ef-9255e11d9af8"), "5", 1, 1307030676643840480L, false, null, false, false, "user_center_tenant_set_resource_select", "查看已有资源", 0, new Guid("3a2c9195-9a5c-42c7-b5dc-7300bbc66e8c"), null, null, false, 3000, "5", 1, 1307030680412160480L },
                    { new Guid("08baa5af-4718-4158-9276-1ad1068b9159"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_email_template_edit", "编辑邮件模板", 0, new Guid("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"), "", "编辑邮件模板", false, 2000, null, null, null },
                    { new Guid("106a3a28-3143-4369-9215-cb223d1b0e45"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_email_server_config_edit", "编辑邮件服务器配置", 0, new Guid("99c74c8b-e343-43bc-86e3-bca825b6a270"), "", "编辑邮件服务器配置", false, 2000, null, null, null },
                    { new Guid("145ec764-6a72-4c4f-85d3-7ad889193970"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_email_template_delete_selected", "删除选中邮件模板", 0, new Guid("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"), "", "删除选中邮件模板", false, 2000, null, null, null },
                    { new Guid("1c377037-13b4-4ef2-8010-d914a40fdbb3"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_audit_operation_delete", "删除操作审计", 3, new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"), "", "删除操作审计", true, 2000, null, null, null },
                    { new Guid("1f289163-7fb0-49d2-9165-cbb111b6f3ab"), "1", 1, 1306961906954240480L, false, null, false, false, "system_manager_code_export", "导出字典", 0, new Guid("d5e3497b-c624-4fde-96bd-108a33cacc6d"), null, "导出字典", false, 2000, null, null, null },
                    { new Guid("1f8605fb-70b3-4929-89eb-4cda69cc305b"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_email_server_config_delete_selected", "删除选中邮件服务器配置", 0, new Guid("99c74c8b-e343-43bc-86e3-bca825b6a270"), "", "删除选中邮件服务器配置", false, 2000, null, null, null },
                    { new Guid("24ace337-41fe-429d-b32e-d9f88bd97aaa"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_audit_operation_detail", "操作审计数据变更详情", 0, new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"), "", "操作审计数据变更详情", true, 2000, null, null, null },
                    { new Guid("286dc779-f58d-439a-bb9b-1333ff2b111b"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_audit_entity_delete", "删除数据审计", 3, new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"), "", "删除数据审计", true, 2000, null, null, null },
                    { new Guid("2a3f7c64-3ee9-473e-837d-5f443089c886"), "6", 1, 1306994982113280480L, false, null, false, false, "system_manager_code_type_refresh_code_util_cache", "刷新字典工具缓存", 0, new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"), null, null, false, 2000, null, null, null },
                    { new Guid("2ac78309-1719-4ea5-ac0f-6974a86f168c"), "1", 1, 1306552959733760480L, false, null, false, false, "system_manager_audit_operation_export", "导出操作审计", 0, new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"), null, "导出操作审计", true, 2000, null, null, null },
                    { new Guid("36a4434a-f702-42be-a211-862d0b3b5288"), "1", 1, 1306961909370880480L, false, null, false, false, "system_manager_code_detail", "查看字典", 0, new Guid("d5e3497b-c624-4fde-96bd-108a33cacc6d"), null, "查看字典", false, 2000, "1", 1, 1306961910128640480L },
                    { new Guid("3d93eb77-2a72-4b4f-aa79-4da1fc7943c9"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_email_server_config_send", "发送测试邮件", 0, new Guid("99c74c8b-e343-43bc-86e3-bca825b6a270"), "", "发送测试邮件", false, 2000, null, null, null },
                    { new Guid("3f8d700a-bc26-4d5c-9622-d98bf9359159"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_audit_entity_detail", "查询数据审计详情", 4, new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"), "", "查询数据审计详情", true, 2000, null, null, null },
                    { new Guid("46b8f9b5-fe41-4b55-b39f-4cb398186d2c"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_email_server_config_delete", "删除邮件服务器配置", 0, new Guid("99c74c8b-e343-43bc-86e3-bca825b6a270"), "", "删除邮件服务器配置", false, 2000, null, null, null },
                    { new Guid("4af87acd-64b4-4d53-8043-cd7ab6b03c77"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_resource_show_function_1", "显示已关联接口", 0, new Guid("e252c0c6-0f19-4768-954c-c0d83fb96d74"), "", "显示已关联接口", false, 2000, null, null, null },
                    { new Guid("4b4f7b73-df18-4201-876e-b27e172f3b55"), "1", 1, 1306961907343360480L, false, null, false, false, "system_manager_code_add", "添加字典", 0, new Guid("d5e3497b-c624-4fde-96bd-108a33cacc6d"), null, "添加字典", false, 2000, null, null, null },
                    { new Guid("4c96cdb4-efc1-4ccc-8ec6-9ca1bc458d8a"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_client_function_delete_selected", "删除选中客户端接口关系", 0, new Guid("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"), "", "删除选中客户端接口关系", false, 2000, null, null, null },
                    { new Guid("4e582063-f524-4ce2-9417-ac2cd957332d"), "1", 1, 1306964896153600480L, false, null, false, false, "system_manager_code_type_refresh", "刷新字典类型列表", 0, new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"), null, null, false, 2000, null, null, null },
                    { new Guid("4f259695-23ea-4453-a4f1-2b055d135c37"), "1", 1, 1306552960327680480L, false, null, false, false, "system_manager_audit_entity_export", "导出数据审计", 0, new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"), null, "导出数据审计", true, 2000, null, null, null },
                    { new Guid("4f943ed1-997a-485f-9b54-9824b4ac285c"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_resource_function_delete_selected", "删除选中资源接口关系", 0, new Guid("e252c0c6-0f19-4768-954c-c0d83fb96d74"), "", "", false, 2000, null, null, null },
                    { new Guid("520f5cec-5f33-447c-a18b-59d8db31c5e9"), "1", 1, 1306961908981760480L, false, null, false, false, "system_manager_code_edit", "编辑字典", 0, new Guid("d5e3497b-c624-4fde-96bd-108a33cacc6d"), null, "编辑字典", false, 2000, "1", 1, 1306961910292480480L },
                    { new Guid("535e5f96-a036-4a40-96af-6c03cecadcd1"), "1", 1, 1306961909780480480L, false, null, false, false, "system_manager_code_delete", "删除字典", 0, new Guid("d5e3497b-c624-4fde-96bd-108a33cacc6d"), null, "删除字典", false, 2000, "1", 1, 1306961909944320480L },
                    { new Guid("5591c5b9-9ee0-44ae-a4fa-39234b95afa4"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_audit_operation_delete_selected", "删除选中操作审计", 2, new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"), "", "", true, 2000, null, null, null },
                    { new Guid("5676c9b3-2d06-4817-9614-4a34230bb05e"), "1", 1, 1306964897853440480L, false, null, false, false, "system_manager_code_type_download_seed_data", "生成字典类型种子数据", 0, new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"), null, null, false, 2000, null, null, null },
                    { new Guid("64346edf-1390-4a90-bc63-93f322ed6c8f"), "1", 1, 1306547599319040480L, false, null, false, false, "system_manager_resource_function_download_seed_data", "获取种子数据", 0, new Guid("e252c0c6-0f19-4768-954c-c0d83fb96d74"), null, null, false, 2000, null, null, null },
                    { new Guid("68ebd579-e2c7-4f1c-8f9f-7a06df30bd5f"), "1", 1, 1306964896993280480L, false, null, false, false, "system_manager_code_type_export", "导出字典类型", 0, new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"), null, null, false, 2000, null, null, null },
                    { new Guid("7819fe8f-8d81-4d00-af2b-c53ec010c65b"), "1", 1, 1306964899082240480L, false, null, false, false, "system_manager_code_type_codes_manager", "管理字典类型下字典", 0, new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"), null, "功能与 字典管理->字典 相同", false, 2000, "1", 1, 1306964905226240480L },
                    { new Guid("7aad6dba-3f13-4982-adfa-525fa94485dd"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_email_template_detail", "查看邮件模板", 0, new Guid("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"), "", "查看邮件模板", false, 2000, null, null, null },
                    { new Guid("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_audit_entity_refresh", "刷新数据审计", 1, new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"), "", "刷新数据审计", true, 2000, null, null, null },
                    { new Guid("807029ec-be10-4faa-a332-bcb1021ff966"), "5", 1, 1307030677667840480L, false, null, false, false, "user_center_tenant_set_resource_save", "绑定资源-保存", 0, new Guid("3a2c9195-9a5c-42c7-b5dc-7300bbc66e8c"), null, null, false, 2000, null, null, null },
                    { new Guid("8158e1a6-335d-4a29-9177-0f30e86fa8ec"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_audit_entity_delete_selected", "删除选中数据审计", 2, new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"), "", "删除选中数据审计", true, 2000, null, null, null },
                    { new Guid("86a086a1-0770-4df4-ade3-433ff7226399"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_client_show_function_1", "显示已关联接口", 0, new Guid("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"), "", "显示已关联接口", false, 2000, null, null, null },
                    { new Guid("8c447e9b-1d39-48e5-b9b9-41ee2058b0c7"), "1", 1, 1306961908142080480L, false, null, false, false, "system_manager_code_download_seed_data", "生成字典种子数据", 0, new Guid("d5e3497b-c624-4fde-96bd-108a33cacc6d"), null, "生成字典种子数据", false, 2000, null, null, null },
                    { new Guid("8e913a11-dbbe-4aa4-ad58-f12737039d83"), "1", 1, 1306964896645120480L, false, null, false, false, "system_manager_code_type_delete_selected", "删除选中字典类型", 0, new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"), null, null, false, 2000, null, null, null },
                    { new Guid("9800d45c-7ba8-4728-a6a6-a62dbc7b6f59"), "1", 1, 1306964897280000480L, false, null, false, false, "system_manager_code_type_add", "添加字典类型", 0, new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"), null, null, false, 2000, null, null, null },
                    { new Guid("a02edffb-0a63-4106-bac2-ea66f1f65060"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_client_function_add_page_show", "显示可选接口", 0, new Guid("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"), "", "显示可选接口", false, 2000, null, null, null },
                    { new Guid("a2b68c70-173f-46fa-8442-e19219a9905b"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_role_resource_select", "查看角色资源", 0, new Guid("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"), "", "查看角色资源", true, 3000, null, null, null },
                    { new Guid("a37b1cd8-98c4-4a93-a73e-436c138639eb"), "1", 1, 1306961908613120480L, false, null, false, false, "system_manager_code_lock", "锁定字典", 0, new Guid("d5e3497b-c624-4fde-96bd-108a33cacc6d"), null, null, false, 2000, null, null, null },
                    { new Guid("a7555120-c3e4-4f8d-bdf8-371ac22daa50"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_client_function_binding", "绑定客户端接口关系", 0, new Guid("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"), "", "绑定资源接口关系", false, 2000, null, null, null },
                    { new Guid("a807706b-ffb3-4f8d-b18d-9a7ee6b88028"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_email_server_config_add", "添加邮件服务器配置", 0, new Guid("99c74c8b-e343-43bc-86e3-bca825b6a270"), "", "添加邮件服务器配置", false, 2000, null, null, null },
                    { new Guid("addfa7a9-7a8c-46ce-90f1-11424f385954"), "1", 1, 1306964899471360480L, false, null, false, false, "system_manager_code_type_delete", "删除字典类型", 0, new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"), null, null, false, 2000, null, null, null },
                    { new Guid("af9b9a49-0094-4e1c-97dc-d0580525244f"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_email_template_send", "发送测试邮件", 0, new Guid("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"), "", "发送测试邮件", false, 2000, null, null, null },
                    { new Guid("b5320a70-11fe-4b7a-9c7e-5bb132e72639"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_email_template_delete", "删除邮件模板", 0, new Guid("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"), "", "删除邮件模板", false, 2000, null, null, null },
                    { new Guid("b7cdae2b-4f9b-493a-b43b-a3c7ffef3b86"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_email_template_refresh", "刷新邮件模板列表", 0, new Guid("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"), "", "刷新邮件模板列表", false, 2000, null, null, null },
                    { new Guid("bedacea2-80f1-4d4a-b401-c82940f80d4c"), "1", 1, 1306961906503680480L, false, null, false, false, "system_manager_code_delete_selected", "删除选中字典", 0, new Guid("d5e3497b-c624-4fde-96bd-108a33cacc6d"), null, "删除选中字典", false, 2000, null, null, null },
                    { new Guid("c4991844-d3b4-4f9a-9c90-c13114515796"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_resource_function_add_page_show", "显示可选接口", 0, new Guid("e252c0c6-0f19-4768-954c-c0d83fb96d74"), "", "显示可选接口", false, 2000, null, null, null },
                    { new Guid("ca1d4b3a-336b-40a5-b683-0fe0bcbabaf8"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_resource_function_binding", "绑定资源接口关系", 0, new Guid("e252c0c6-0f19-4768-954c-c0d83fb96d74"), "", "", false, 2000, null, null, null },
                    { new Guid("cc23917b-930a-4e34-9717-be71b9fd2dd5"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_audit_operation_refresh", "刷新操作审计", 1, new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"), "", "刷新操作审计", true, 2000, null, null, null },
                    { new Guid("cd23a5d8-6eab-4e46-a730-56b2808551c6"), "1", 1, 1306964898672640480L, false, null, false, false, "system_manager_code_type_detail", "查看字典类型", 0, new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"), null, null, false, 2000, null, null, null },
                    { new Guid("d697fda5-28fa-46c3-ba88-a98dd510e09d"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_email_server_config_refresh", "刷新邮件服务器配置", 0, new Guid("99c74c8b-e343-43bc-86e3-bca825b6a270"), "", "刷新邮件服务器配置", false, 2000, null, null, null },
                    { new Guid("d9fc6b89-25bb-458e-936f-d76eea2c680f"), "1", 1, 1306961905971200480L, false, null, false, false, "system_manager_code_refresh", "刷新字典列表", 0, new Guid("d5e3497b-c624-4fde-96bd-108a33cacc6d"), null, "刷新字典列表", false, 2000, null, null, null },
                    { new Guid("e1ab080c-b598-4c1c-9afa-45681f90f1e3"), "1", 1, 1306964898344960480L, false, null, false, false, "system_manager_code_type_edit", "编辑字典类型", 0, new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"), null, null, false, 2000, null, null, null },
                    { new Guid("ef15af79-1be1-4055-82b0-83a6aa8fdd35"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_email_template_lock", "锁定邮件模板", 0, new Guid("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"), "", "锁定邮件模板", false, 2000, null, null, null },
                    { new Guid("f2ca3ab7-40da-4828-ad63-06bc9af9b153"), null, 0, 1306051389542400480L, false, "", false, false, "user_center_role_set_resource_save", "保存角色资源", 0, new Guid("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"), "", "保存角色资源", true, 2000, null, null, null },
                    { new Guid("f63a570e-a762-4410-b4b1-764ee5ceb7ae"), null, 0, 1306051389542400480L, false, "", false, false, "system_manager_email_server_config_detail", "查看邮件服务器配置", 0, new Guid("99c74c8b-e343-43bc-86e3-bca825b6a270"), "", "查看邮件服务器配置", false, 2000, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[,]
                {
                    { new Guid("f5c318f6-9230-475a-830e-a404e17506b5"), new Guid("04c237bb-7670-4d66-bbaa-dcd9624d2d90"), 1306069130997760480L },
                    { new Guid("22f82d98-9176-4391-a950-1d1d01e105fd"), new Guid("07af05b1-6f3e-49fa-9959-463e246346df"), 1307019923640320480L },
                    { new Guid("7f0d7abb-06a4-4a35-b4e3-7798b21e37fa"), new Guid("08ae2764-e551-45d2-9da7-49648481a8e0"), 1306069130997760480L },
                    { new Guid("9ebd4172-5191-4931-9b22-4c339be4a816"), new Guid("0aa9b237-dab8-472e-b2e6-af9c0af9f916"), 1306069130997760480L },
                    { new Guid("f59833a1-c9af-4bb2-be4b-d6935513fc99"), new Guid("0cbb3d40-de41-483e-a76c-3d85682176af"), 1306550925844480480L },
                    { new Guid("a96bb19e-794e-4fe0-ad39-f423df44f633"), new Guid("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"), 1306069130997760480L },
                    { new Guid("b38fb0cc-4275-4d1f-8bb7-6f5a962bcc35"), new Guid("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"), 1306069130997760480L },
                    { new Guid("b56c4126-411c-445e-86aa-a91a5ce816d4"), new Guid("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"), 1306069130997760480L },
                    { new Guid("715a2905-da23-405d-98a0-1a1222f7d101"), new Guid("0fd84267-ee22-47c4-b41c-ce654eba29d9"), 1306069130997760480L },
                    { new Guid("7a3399b3-6003-4aae-8e24-2e478992630e"), new Guid("0fd84267-ee22-47c4-b41c-ce654eba29d9"), 1306069130997760480L },
                    { new Guid("01944b79-bfe5-4304-ade0-9c66e038d5d4"), new Guid("13e7d01e-93ca-429c-b412-ff6fa5b6a026"), 1306069130997760480L },
                    { new Guid("b38fb0cc-4275-4d1f-8bb7-6f5a962bcc35"), new Guid("13e7d01e-93ca-429c-b412-ff6fa5b6a026"), 1306069130997760480L },
                    { new Guid("a96bb19e-794e-4fe0-ad39-f423df44f633"), new Guid("186bca5f-cc2c-427e-a58a-dbb81641a296"), 1306069130997760480L },
                    { new Guid("337bae83-a083-4e0e-8ceb-2bb21ae22145"), new Guid("1d2fb341-3b69-4d0b-934d-c4c2cd250401"), 1306069130997760480L },
                    { new Guid("cbc8aff4-6dc0-41f2-b684-caba8e0657ac"), new Guid("1efd01cf-42f2-45c7-95f2-84be55e65646"), 1306069130997760480L },
                    { new Guid("05153ee4-dc99-4834-b398-5999f7dc8d01"), new Guid("25535592-81a1-42dd-8a55-509f2c852ff9"), 1306069130997760480L },
                    { new Guid("715a2905-da23-405d-98a0-1a1222f7d101"), new Guid("25535592-81a1-42dd-8a55-509f2c852ff9"), 1306069130997760480L },
                    { new Guid("cba739f0-9f8a-40c2-afff-d66c3382e096"), new Guid("2c1c895c-6434-4f14-91f2-144e48457101"), 1306069130997760480L },
                    { new Guid("e23b555c-600a-4839-9439-2ee0ad0ae4f8"), new Guid("316ecba5-5d89-44ae-908f-a54268723bd1"), 1306069130997760480L },
                    { new Guid("e38c1619-0f84-4e55-81c2-0f47992ee33d"), new Guid("365fc5c4-404e-408a-88dc-7614dffad91b"), 1306069130997760480L },
                    { new Guid("83cc7cb7-dac6-49f2-85fa-e903039f3d0a"), new Guid("374f7bfd-3c16-40dd-b4dc-a5992a0915cf"), 1306069130997760480L },
                    { new Guid("23cd301c-ebbb-4715-a96d-72232d9a6927"), new Guid("3a2c9195-9a5c-42c7-b5dc-7300bbc66e8c"), 1307030680944640480L },
                    { new Guid("e38c1619-0f84-4e55-81c2-0f47992ee33d"), new Guid("3a2c9195-9a5c-42c7-b5dc-7300bbc66e8c"), 1307030681333760480L },
                    { new Guid("571200a8-bde2-430b-84ea-743db7b282cd"), new Guid("3d007d84-d209-49e2-94ca-11ad2a3dd91d"), 1306550926888960480L },
                    { new Guid("8f114b96-dc3d-4dd4-854a-4c793c121e43"), new Guid("3d007d84-d209-49e2-94ca-11ad2a3dd91d"), 1306550926888960480L },
                    { new Guid("89954833-64a5-4c87-a717-9c863ca3b263"), new Guid("3d6e9553-2baf-4d9d-8a82-65de1c7d7ece"), 1306069130997760480L },
                    { new Guid("dca2b115-3363-4f7f-8bba-b051b8d8603a"), new Guid("4171f5aa-2ce1-40ad-b69e-59de1cd20416"), 1306552948920320480L },
                    { new Guid("3790cc0d-dc3a-4669-acba-3a90812c6386"), new Guid("46cad808-0d0b-42bb-a134-3ad6db8ebf54"), 1306069130997760480L },
                    { new Guid("5efd6ab4-a9d3-4742-9a48-fb54a1b1e463"), new Guid("46cad808-0d0b-42bb-a134-3ad6db8ebf54"), 1307020021166080480L },
                    { new Guid("6aea8a77-edd2-444b-b8be-901d78321a49"), new Guid("476cf96a-0e18-4c30-a760-e8b9c615bb99"), 1306069130997760480L },
                    { new Guid("0d2e0194-2238-457b-aab0-9b3259cc4ed9"), new Guid("490bc05f-499e-4f4c-811d-fde4c10be2ed"), 1307020017090560480L },
                    { new Guid("a9ec32a6-286b-4fe4-b69e-79cca6243e00"), new Guid("4db9a237-1343-4c4a-91f6-9a40fb9f0e2a"), 1307019918479360480L },
                    { new Guid("65a3c1ee-f5cf-48eb-9bf0-3d4db44257e4"), new Guid("4e845d07-33a4-4dc4-ba7f-8568f88b9d68"), 1306069130997760480L },
                    { new Guid("aeb8b23d-4da3-4ec0-867f-70d2e2ba9550"), new Guid("50062351-8235-4da1-9f90-4917d0e8abe0"), 1306069130997760480L },
                    { new Guid("b952b41e-b3e9-4c53-9a7d-6b561acf4bc4"), new Guid("50062351-8235-4da1-9f90-4917d0e8abe0"), 1306069130997760480L },
                    { new Guid("16517409-c055-447b-8e91-7155537c6d15"), new Guid("67501fd4-4fbf-48c2-b383-f3a2085268ed"), 1306069130997760480L },
                    { new Guid("2f820c7f-4f1c-4737-aae6-329585c75d92"), new Guid("67ad5c3a-8611-4183-ad9e-63cb4c9760fa"), 1306069130997760480L },
                    { new Guid("416fe54b-6c50-4b1b-bf77-6744cf19fa72"), new Guid("6ac07813-4d10-4b50-9f0c-ecd444041282"), 1306069130997760480L },
                    { new Guid("8ae9c253-584e-46e4-b805-6ec90281d6dd"), new Guid("6e487179-5bb2-4ab5-80e3-58c514c9595f"), 1306069130997760480L },
                    { new Guid("84256e5b-2cef-4b16-8fd3-79ff8d47c731"), new Guid("749c3a63-6bd8-4755-87ed-c1d455e5b717"), 1306069130997760480L },
                    { new Guid("8d94c826-ddba-47fe-94c9-333880fee187"), new Guid("749c3a63-6bd8-4755-87ed-c1d455e5b717"), 1306069130997760480L },
                    { new Guid("a15ce231-80ae-46c6-ada8-49666e81e328"), new Guid("749c3a63-6bd8-4755-87ed-c1d455e5b717"), 1306069130997760480L },
                    { new Guid("a53a9c89-7968-4598-9c46-dad4e9188bd0"), new Guid("749c3a63-6bd8-4755-87ed-c1d455e5b717"), 1306069130997760480L },
                    { new Guid("c591c0ca-3305-4684-89bb-278218d13c47"), new Guid("749c3a63-6bd8-4755-87ed-c1d455e5b717"), 1306069130997760480L },
                    { new Guid("8172d258-7a75-4ced-b5e2-b0be7350aa1f"), new Guid("757fdf0b-0cb9-4f24-92f6-24e18f3defcc"), 1306069130997760480L },
                    { new Guid("cd7db809-50f5-4bf3-a464-89218e24077f"), new Guid("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"), 1306069130997760480L },
                    { new Guid("e38c1619-0f84-4e55-81c2-0f47992ee33d"), new Guid("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"), 1306069130997760480L },
                    { new Guid("910d2a4f-85ae-46ff-bddd-b65ffcc6b9e1"), new Guid("859aa714-67c7-4414-bc96-9de5b7aec2c4"), 1306069130997760480L },
                    { new Guid("622c1a11-7dff-4318-9d21-b57fbd1da9ba"), new Guid("87377abe-785d-426c-b052-f706a2c7173d"), 1306069130997760480L },
                    { new Guid("4c1b9201-09e6-421f-95d1-d98d009a3417"), new Guid("8a4e9aee-b116-4822-bd59-b3a98e84b9f3"), 1306069130997760480L },
                    { new Guid("04ad3c68-6e35-4175-a8ff-564d4bf51e91"), new Guid("8ab307de-ad4b-462f-b61d-7f1d53b82f3d"), 1306069130997760480L },
                    { new Guid("10fc92a8-30ed-4536-a995-c7af8e5548a1"), new Guid("8ab307de-ad4b-462f-b61d-7f1d53b82f3d"), 1306069130997760480L },
                    { new Guid("e38c1619-0f84-4e55-81c2-0f47992ee33d"), new Guid("8ab307de-ad4b-462f-b61d-7f1d53b82f3d"), 1306069130997760480L },
                    { new Guid("687b278e-aa4e-4d1c-95fd-f148eeb2a658"), new Guid("8b2007b4-821b-49fc-aa5d-35ebc4dbe3c9"), 1307019915325440480L },
                    { new Guid("b952b41e-b3e9-4c53-9a7d-6b561acf4bc4"), new Guid("92da96d7-c59c-4d4b-8c97-80a9f59e8fa2"), 1306069130997760480L },
                    { new Guid("cecdfb7d-6796-4bd8-a3d7-164c16a7c959"), new Guid("92ed8299-ff26-4fae-b852-fe33f0c01a09"), 1306069130997760480L },
                    { new Guid("5eb48cf2-6c45-47c2-a68b-84284a389c69"), new Guid("97a7d440-b7fe-4af6-a8a1-18846c48828b"), 1306069130997760480L },
                    { new Guid("af79d7de-0141-4338-8c52-05216d1b07ff"), new Guid("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"), 1306069130997760480L },
                    { new Guid("af1f0410-e9cc-4a73-9da7-ea45aadac8b2"), new Guid("a1260e4c-e67c-4d72-a758-560a13e9c496"), 1306069130997760480L },
                    { new Guid("cdd3c605-ed1d-4d94-a482-16430b729541"), new Guid("a1958e51-06d4-4b29-9533-eae9d86c41d1"), 1306069130997760480L },
                    { new Guid("b0b9900d-5ff7-4164-957a-9b8d55d5d5bf"), new Guid("a25da8f5-23d4-4118-b399-0a36f912a370"), 1306552953200640480L },
                    { new Guid("383c5aaf-a3e1-44d1-a1c8-3074abe55f95"), new Guid("a468499c-7115-44f1-ad38-2c5f696891d4"), 1306069130997760480L },
                    { new Guid("5d67bd9d-853c-4e16-973d-be0511241fc0"), new Guid("a7a949b0-ca8e-47a1-a5be-ce0fa3c501e6"), 1306069130997760480L },
                    { new Guid("4b57474a-88b4-4393-bb49-4b59e8c3c41d"), new Guid("b100a7eb-ef44-4669-bac5-3c5ce52871bb"), 1306069130997760480L },
                    { new Guid("22f82d98-9176-4391-a950-1d1d01e105fd"), new Guid("b4072d45-f643-4bdb-a63e-7286cfa9c62b"), 1307019923087360480L },
                    { new Guid("74a1a6b4-4559-48fc-8987-3287e5a4e056"), new Guid("b4072d45-f643-4bdb-a63e-7286cfa9c62b"), 1307019921326080480L },
                    { new Guid("2502e6ae-879b-4674-a557-cd7b4de891a7"), new Guid("b63d694e-205f-44c0-8353-0c9507f44696"), 1306069130997760480L },
                    { new Guid("868fc0df-7cdf-4b56-873e-16dd3e0aa528"), new Guid("b71bbc5f-83a3-4065-b561-cb4b69b4a507"), 1306069130997760480L },
                    { new Guid("db76ae46-851b-47bc-94be-b2e869043636"), new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"), 1306069130997760480L },
                    { new Guid("715a2905-da23-405d-98a0-1a1222f7d101"), new Guid("ba89c7b7-552c-415c-b4be-085262dc76b0"), 1306069130997760480L },
                    { new Guid("c39030b8-d207-4c22-a3ba-74b0eccaa2fa"), new Guid("bd7d1a4c-960a-48b2-9c9e-083aa5c5924f"), 1306069130997760480L },
                    { new Guid("cd1b93ed-2fae-47f2-83b2-e9b0a949f476"), new Guid("bddc6ccc-3f93-4be7-8756-15613cdf76b6"), 1306552963686400480L },
                    { new Guid("63d7208e-45d3-406e-a4a1-c87e3afda04d"), new Guid("bf05ffe8-c3ff-402d-bef1-3e95d202fd03"), 1306069130997760480L },
                    { new Guid("04ad3c68-6e35-4175-a8ff-564d4bf51e91"), new Guid("c18d4928-35d2-4085-aec9-379d00bcfd8f"), 1306069130997760480L },
                    { new Guid("10fc92a8-30ed-4536-a995-c7af8e5548a1"), new Guid("c18d4928-35d2-4085-aec9-379d00bcfd8f"), 1306069130997760480L },
                    { new Guid("e38c1619-0f84-4e55-81c2-0f47992ee33d"), new Guid("c18d4928-35d2-4085-aec9-379d00bcfd8f"), 1306069130997760480L },
                    { new Guid("e651d9a4-9d6d-44c7-a833-08da6ed19892"), new Guid("c98160ef-ce87-4a1b-bfb3-09fc79d2a34a"), 1306550927503360480L },
                    { new Guid("c4cc2526-8403-4e6c-a88b-94e55279eaa3"), new Guid("cc8a9836-3c4d-4d0b-ae64-a31a6bb36b6f"), 1306547596206080480L },
                    { new Guid("7fa014c4-08db-4f96-8132-2bf3db32b256"), new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"), 1306069130997760480L },
                    { new Guid("8db56ca8-b0e3-45c6-9c73-15893dd616f6"), new Guid("d45effb9-67a8-4459-83ac-c3852c8b4f1f"), 1307019913953280480L },
                    { new Guid("0b605fe1-c77c-4735-8320-b8f400163ac9"), new Guid("d5756ad0-6a8b-4462-907f-1c52a1e11369"), 1306069130997760480L },
                    { new Guid("10fc92a8-30ed-4536-a995-c7af8e5548a1"), new Guid("d83c05a0-4d23-4b2b-ba87-284793bf3eba"), 1306069130997760480L },
                    { new Guid("e38c1619-0f84-4e55-81c2-0f47992ee33d"), new Guid("d83c05a0-4d23-4b2b-ba87-284793bf3eba"), 1306069130997760480L },
                    { new Guid("a6939609-708f-4231-a726-6da1903ef69a"), new Guid("d92268ec-6b51-4514-9487-52cb3fb0d850"), 1307019931443200480L },
                    { new Guid("2c3ec3c9-76c7-4d29-953f-e7430f22577b"), new Guid("d982a072-4681-45d9-8489-7a14218adb04"), 1306069130997760480L },
                    { new Guid("10190ac3-1092-49a9-8ad2-313454b40447"), new Guid("d998802f-776e-4137-bc63-d8d818464f98"), 1306069130997760480L },
                    { new Guid("337bae83-a083-4e0e-8ceb-2bb21ae22145"), new Guid("de62a886-64b2-4a40-b70a-47eb08f23202"), 1306069130997760480L },
                    { new Guid("10fc92a8-30ed-4536-a995-c7af8e5548a1"), new Guid("dec04485-3dab-4251-b7b8-1044e749a51e"), 1306069130997760480L },
                    { new Guid("45dd0581-3394-4c0a-bb8e-c9e0074d5611"), new Guid("dec04485-3dab-4251-b7b8-1044e749a51e"), 1306069130997760480L },
                    { new Guid("e38c1619-0f84-4e55-81c2-0f47992ee33d"), new Guid("dec04485-3dab-4251-b7b8-1044e749a51e"), 1306069130997760480L },
                    { new Guid("f5c318f6-9230-475a-830e-a404e17506b5"), new Guid("defa9a78-229f-43a9-b6b8-95dd6fd8a3c3"), 1306069130997760480L },
                    { new Guid("3e2f4464-6b69-4a00-acfb-d39184729cdd"), new Guid("e44bb45d-514c-4217-bfba-452c0bd38f28"), 1306069130997760480L },
                    { new Guid("0367ad11-0be0-48dd-a5a9-1d473b78c0bf"), new Guid("ea0fb035-1f06-4f61-9946-8df027a7462d"), 1306069130997760480L },
                    { new Guid("0c6f2138-e984-4fba-ad2a-2890716a7259"), new Guid("ea0fb035-1f06-4f61-9946-8df027a7462d"), 1306069130997760480L },
                    { new Guid("cbdf4c22-b54f-4943-b28e-2bb563720fa2"), new Guid("efbcc18b-c193-42cc-b315-cde07f51b496"), 1307019912355840480L },
                    { new Guid("070ae0e4-0193-4ce0-8ba6-b8c344086ced"), new Guid("f02f906a-7579-478a-9406-3c8fd2c54886"), 1306069130997760480L },
                    { new Guid("040878a9-1b78-494e-9ee1-b4a7eab118fb"), new Guid("f077211f-0e79-44a3-935c-0f704f6a5962"), 1306550928138240480L },
                    { new Guid("6dc1a088-15f6-43b8-8465-3a95cc495bab"), new Guid("f077211f-0e79-44a3-935c-0f704f6a5962"), 1306550928138240480L },
                    { new Guid("6a9763c9-c40f-44f3-a248-a3b1e3d1f586"), new Guid("f1649263-ef9a-4f42-85ac-16009283efff"), 1306069130997760480L },
                    { new Guid("c715a6d5-cd99-4c94-8760-936817c1e09c"), new Guid("f4fa035f-27ae-4eee-b006-3cbfac3d2172"), 1306069130997760480L }
                });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreateBy", "CreateIdentityType", "CreatedTime", "IsDeleted", "IsLocked", "TenantId", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("07af05b1-6f3e-49fa-9959-463e246346df"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("186bca5f-cc2c-427e-a58a-dbb81641a296"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("1efd01cf-42f2-45c7-95f2-84be55e65646"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("2c1c895c-6434-4f14-91f2-144e48457101"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("365fc5c4-404e-408a-88dc-7614dffad91b"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("374f7bfd-3c16-40dd-b4dc-a5992a0915cf"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("67ad5c3a-8611-4183-ad9e-63cb4c9760fa"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("6ac07813-4d10-4b50-9f0c-ecd444041282"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("8a4e9aee-b116-4822-bd59-b3a98e84b9f3"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("8b2007b4-821b-49fc-aa5d-35ebc4dbe3c9"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("92da96d7-c59c-4d4b-8c97-80a9f59e8fa2"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("99c74c8b-e343-43bc-86e3-bca825b6a270"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("b63d694e-205f-44c0-8353-0c9507f44696"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("ba89c7b7-552c-415c-b4be-085262dc76b0"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("c98160ef-ce87-4a1b-bfb3-09fc79d2a34a"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("d5e3497b-c624-4fde-96bd-108a33cacc6d"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("d83c05a0-4d23-4b2b-ba87-284793bf3eba"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("e252c0c6-0f19-4768-954c-c0d83fb96d74"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("e44bb45d-514c-4217-bfba-452c0bd38f28"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("f1649263-ef9a-4f42-85ac-16009283efff"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("f4fa035f-27ae-4eee-b006-3cbfac3d2172"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("04c237bb-7670-4d66-bbaa-dcd9624d2d90"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("0aa9b237-dab8-472e-b2e6-af9c0af9f916"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("0cbb3d40-de41-483e-a76c-3d85682176af"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("0fd84267-ee22-47c4-b41c-ce654eba29d9"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("13e7d01e-93ca-429c-b412-ff6fa5b6a026"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("186bca5f-cc2c-427e-a58a-dbb81641a296"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("1d2fb341-3b69-4d0b-934d-c4c2cd250401"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("1efd01cf-42f2-45c7-95f2-84be55e65646"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("25535592-81a1-42dd-8a55-509f2c852ff9"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("2c1c895c-6434-4f14-91f2-144e48457101"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("316ecba5-5d89-44ae-908f-a54268723bd1"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("3d007d84-d209-49e2-94ca-11ad2a3dd91d"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("3d6e9553-2baf-4d9d-8a82-65de1c7d7ece"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("46cad808-0d0b-42bb-a134-3ad6db8ebf54"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("476cf96a-0e18-4c30-a760-e8b9c615bb99"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("490bc05f-499e-4f4c-811d-fde4c10be2ed"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("4e845d07-33a4-4dc4-ba7f-8568f88b9d68"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("67501fd4-4fbf-48c2-b383-f3a2085268ed"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("67ad5c3a-8611-4183-ad9e-63cb4c9760fa"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("87377abe-785d-426c-b052-f706a2c7173d"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("94d2c383-03b6-475c-a744-637dd87a5fdc"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("a25da8f5-23d4-4118-b399-0a36f912a370"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("a468499c-7115-44f1-ad38-2c5f696891d4"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("b63d694e-205f-44c0-8353-0c9507f44696"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("b71bbc5f-83a3-4065-b561-cb4b69b4a507"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("ba89c7b7-552c-415c-b4be-085262dc76b0"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("bddc6ccc-3f93-4be7-8756-15613cdf76b6"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("bf05ffe8-c3ff-402d-bef1-3e95d202fd03"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("c98160ef-ce87-4a1b-bfb3-09fc79d2a34a"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("d5756ad0-6a8b-4462-907f-1c52a1e11369"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("d982a072-4681-45d9-8489-7a14218adb04"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("d998802f-776e-4137-bc63-d8d818464f98"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("de62a886-64b2-4a40-b70a-47eb08f23202"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("defa9a78-229f-43a9-b6b8-95dd6fd8a3c3"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("e44bb45d-514c-4217-bfba-452c0bd38f28"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("ea0fb035-1f06-4f61-9946-8df027a7462d"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("f02f906a-7579-478a-9406-3c8fd2c54886"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("f077211f-0e79-44a3-935c-0f704f6a5962"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("f1649263-ef9a-4f42-85ac-16009283efff"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("f4fa035f-27ae-4eee-b006-3cbfac3d2172"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("04c237bb-7670-4d66-bbaa-dcd9624d2d90"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("0aa9b237-dab8-472e-b2e6-af9c0af9f916"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("0cbb3d40-de41-483e-a76c-3d85682176af"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("0fd84267-ee22-47c4-b41c-ce654eba29d9"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("13e7d01e-93ca-429c-b412-ff6fa5b6a026"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("186bca5f-cc2c-427e-a58a-dbb81641a296"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("1d2fb341-3b69-4d0b-934d-c4c2cd250401"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("1efd01cf-42f2-45c7-95f2-84be55e65646"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("25535592-81a1-42dd-8a55-509f2c852ff9"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("2c1c895c-6434-4f14-91f2-144e48457101"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("316ecba5-5d89-44ae-908f-a54268723bd1"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("3d007d84-d209-49e2-94ca-11ad2a3dd91d"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("3d6e9553-2baf-4d9d-8a82-65de1c7d7ece"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("46cad808-0d0b-42bb-a134-3ad6db8ebf54"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("476cf96a-0e18-4c30-a760-e8b9c615bb99"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("490bc05f-499e-4f4c-811d-fde4c10be2ed"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("4e845d07-33a4-4dc4-ba7f-8568f88b9d68"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("67501fd4-4fbf-48c2-b383-f3a2085268ed"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("67ad5c3a-8611-4183-ad9e-63cb4c9760fa"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("87377abe-785d-426c-b052-f706a2c7173d"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("94d2c383-03b6-475c-a744-637dd87a5fdc"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("a25da8f5-23d4-4118-b399-0a36f912a370"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("a468499c-7115-44f1-ad38-2c5f696891d4"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("b63d694e-205f-44c0-8353-0c9507f44696"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("b71bbc5f-83a3-4065-b561-cb4b69b4a507"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("ba89c7b7-552c-415c-b4be-085262dc76b0"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("bddc6ccc-3f93-4be7-8756-15613cdf76b6"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("bf05ffe8-c3ff-402d-bef1-3e95d202fd03"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("c98160ef-ce87-4a1b-bfb3-09fc79d2a34a"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("d5756ad0-6a8b-4462-907f-1c52a1e11369"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("d982a072-4681-45d9-8489-7a14218adb04"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("d998802f-776e-4137-bc63-d8d818464f98"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("de62a886-64b2-4a40-b70a-47eb08f23202"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("defa9a78-229f-43a9-b6b8-95dd6fd8a3c3"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("e44bb45d-514c-4217-bfba-452c0bd38f28"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("ea0fb035-1f06-4f61-9946-8df027a7462d"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("f02f906a-7579-478a-9406-3c8fd2c54886"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("f077211f-0e79-44a3-935c-0f704f6a5962"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("f1649263-ef9a-4f42-85ac-16009283efff"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("f4fa035f-27ae-4eee-b006-3cbfac3d2172"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null }
                });

            migrationBuilder.InsertData(
                table: "SystemTenantResource",
                columns: new[] { "ResourceId", "TenantId", "CreateBy", "CreateIdentityType", "CreatedTime", "IsDeleted", "IsLocked", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("04c237bb-7670-4d66-bbaa-dcd9624d2d90"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("0aa9b237-dab8-472e-b2e6-af9c0af9f916"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("0cbb3d40-de41-483e-a76c-3d85682176af"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("0fd84267-ee22-47c4-b41c-ce654eba29d9"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("13e7d01e-93ca-429c-b412-ff6fa5b6a026"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("186bca5f-cc2c-427e-a58a-dbb81641a296"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("1d2fb341-3b69-4d0b-934d-c4c2cd250401"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("1efd01cf-42f2-45c7-95f2-84be55e65646"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("25535592-81a1-42dd-8a55-509f2c852ff9"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("2c1c895c-6434-4f14-91f2-144e48457101"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("316ecba5-5d89-44ae-908f-a54268723bd1"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("3d007d84-d209-49e2-94ca-11ad2a3dd91d"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("3d6e9553-2baf-4d9d-8a82-65de1c7d7ece"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("46cad808-0d0b-42bb-a134-3ad6db8ebf54"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("476cf96a-0e18-4c30-a760-e8b9c615bb99"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("490bc05f-499e-4f4c-811d-fde4c10be2ed"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("4e845d07-33a4-4dc4-ba7f-8568f88b9d68"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("67501fd4-4fbf-48c2-b383-f3a2085268ed"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("67ad5c3a-8611-4183-ad9e-63cb4c9760fa"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("87377abe-785d-426c-b052-f706a2c7173d"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("94d2c383-03b6-475c-a744-637dd87a5fdc"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("a25da8f5-23d4-4118-b399-0a36f912a370"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("a468499c-7115-44f1-ad38-2c5f696891d4"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("b63d694e-205f-44c0-8353-0c9507f44696"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("b71bbc5f-83a3-4065-b561-cb4b69b4a507"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("ba89c7b7-552c-415c-b4be-085262dc76b0"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("bddc6ccc-3f93-4be7-8756-15613cdf76b6"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("bf05ffe8-c3ff-402d-bef1-3e95d202fd03"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("c98160ef-ce87-4a1b-bfb3-09fc79d2a34a"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("d5756ad0-6a8b-4462-907f-1c52a1e11369"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("d982a072-4681-45d9-8489-7a14218adb04"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("d998802f-776e-4137-bc63-d8d818464f98"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("de62a886-64b2-4a40-b70a-47eb08f23202"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("defa9a78-229f-43a9-b6b8-95dd6fd8a3c3"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("e44bb45d-514c-4217-bfba-452c0bd38f28"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("ea0fb035-1f06-4f61-9946-8df027a7462d"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("f02f906a-7579-478a-9406-3c8fd2c54886"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("f077211f-0e79-44a3-935c-0f704f6a5962"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("f1649263-ef9a-4f42-85ac-16009283efff"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("f4fa035f-27ae-4eee-b006-3cbfac3d2172"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("04c237bb-7670-4d66-bbaa-dcd9624d2d90"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("0aa9b237-dab8-472e-b2e6-af9c0af9f916"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("0cbb3d40-de41-483e-a76c-3d85682176af"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("0fd84267-ee22-47c4-b41c-ce654eba29d9"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("13e7d01e-93ca-429c-b412-ff6fa5b6a026"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("186bca5f-cc2c-427e-a58a-dbb81641a296"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("1d2fb341-3b69-4d0b-934d-c4c2cd250401"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("1efd01cf-42f2-45c7-95f2-84be55e65646"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("25535592-81a1-42dd-8a55-509f2c852ff9"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("2c1c895c-6434-4f14-91f2-144e48457101"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("316ecba5-5d89-44ae-908f-a54268723bd1"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("3d007d84-d209-49e2-94ca-11ad2a3dd91d"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("3d6e9553-2baf-4d9d-8a82-65de1c7d7ece"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("46cad808-0d0b-42bb-a134-3ad6db8ebf54"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("476cf96a-0e18-4c30-a760-e8b9c615bb99"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("490bc05f-499e-4f4c-811d-fde4c10be2ed"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("4e845d07-33a4-4dc4-ba7f-8568f88b9d68"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("67501fd4-4fbf-48c2-b383-f3a2085268ed"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("67ad5c3a-8611-4183-ad9e-63cb4c9760fa"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("87377abe-785d-426c-b052-f706a2c7173d"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("94d2c383-03b6-475c-a744-637dd87a5fdc"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("a25da8f5-23d4-4118-b399-0a36f912a370"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("a468499c-7115-44f1-ad38-2c5f696891d4"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("b63d694e-205f-44c0-8353-0c9507f44696"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("b71bbc5f-83a3-4065-b561-cb4b69b4a507"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("ba89c7b7-552c-415c-b4be-085262dc76b0"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("bddc6ccc-3f93-4be7-8756-15613cdf76b6"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("bf05ffe8-c3ff-402d-bef1-3e95d202fd03"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("c98160ef-ce87-4a1b-bfb3-09fc79d2a34a"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("d5756ad0-6a8b-4462-907f-1c52a1e11369"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("d982a072-4681-45d9-8489-7a14218adb04"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("d998802f-776e-4137-bc63-d8d818464f98"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("de62a886-64b2-4a40-b70a-47eb08f23202"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("defa9a78-229f-43a9-b6b8-95dd6fd8a3c3"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("e44bb45d-514c-4217-bfba-452c0bd38f28"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("ea0fb035-1f06-4f61-9946-8df027a7462d"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("f02f906a-7579-478a-9406-3c8fd2c54886"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("f077211f-0e79-44a3-935c-0f704f6a5962"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("f1649263-ef9a-4f42-85ac-16009283efff"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("f4fa035f-27ae-4eee-b006-3cbfac3d2172"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "ResourceFunction",
                columns: new[] { "FunctionId", "ResourceId", "CreatedTime" },
                values: new object[,]
                {
                    { new Guid("9191206c-f35e-4eb7-b19a-5949dc560369"), new Guid("083fffc4-2600-49bb-87e6-1a92133499ec"), 1306069130997760480L },
                    { new Guid("33c2157a-884d-4030-abea-a9aeea51fdf8"), new Guid("08baa5af-4718-4158-9276-1ad1068b9159"), 1306069130997760480L },
                    { new Guid("99546746-70b8-42d6-884d-ea1b79f88c0a"), new Guid("106a3a28-3143-4369-9215-cb223d1b0e45"), 1306069130997760480L },
                    { new Guid("31896c5d-2ed7-4e43-a952-4edc076d29d0"), new Guid("145ec764-6a72-4c4f-85d3-7ad889193970"), 1306069130997760480L },
                    { new Guid("080dd200-8e8a-489c-86ca-8eb74c417c0b"), new Guid("1c377037-13b4-4ef2-8010-d914a40fdbb3"), 1306069130997760480L },
                    { new Guid("0bc4b901-d9ee-4fac-91c7-e6ea6d6fb852"), new Guid("1f289163-7fb0-49d2-9165-cbb111b6f3ab"), 1306961917214720480L },
                    { new Guid("26d95428-ebbd-4bf2-9bcc-2eeec4263bd5"), new Guid("1f8605fb-70b3-4929-89eb-4cda69cc305b"), 1306069130997760480L },
                    { new Guid("1d994e50-d40a-465b-8445-646041a8131a"), new Guid("24ace337-41fe-429d-b32e-d9f88bd97aaa"), 1306069130997760480L },
                    { new Guid("7e5577d4-32b2-4f43-a83f-05410b59b195"), new Guid("286dc779-f58d-439a-bb9b-1333ff2b111b"), 1306069130997760480L },
                    { new Guid("4e85ec32-8a3c-46e9-ba60-4abd7bee6745"), new Guid("2a3f7c64-3ee9-473e-837d-5f443089c886"), 1306995003985920480L },
                    { new Guid("ed8c2fae-c63f-4aec-af4b-e915b6db38a2"), new Guid("2a3f7c64-3ee9-473e-837d-5f443089c886"), 1306995003985920480L },
                    { new Guid("5df6ccb0-8985-4767-9ff2-b306d791194e"), new Guid("2ac78309-1719-4ea5-ac0f-6974a86f168c"), 1306552961720320480L },
                    { new Guid("483956b8-4651-4dea-8c34-501108220cc1"), new Guid("36a4434a-f702-42be-a211-862d0b3b5288"), 1306961922150400480L },
                    { new Guid("ed8c2fae-c63f-4aec-af4b-e915b6db38a2"), new Guid("36a4434a-f702-42be-a211-862d0b3b5288"), 1306966786068480480L },
                    { new Guid("7f36ba4f-ec97-4fa9-953b-fa2f1686c448"), new Guid("3d93eb77-2a72-4b4f-aa79-4da1fc7943c9"), 1306069130997760480L },
                    { new Guid("39421a19-9cbf-477b-baea-34f40341357f"), new Guid("46b8f9b5-fe41-4b55-b39f-4cb398186d2c"), 1306069130997760480L },
                    { new Guid("fff9f1e7-7fd3-42f5-afe7-d40cca07f0ca"), new Guid("4af87acd-64b4-4d53-8043-cd7ab6b03c77"), 1306069130997760480L },
                    { new Guid("06eff7a6-4291-43b0-9c34-58a97aa0fc01"), new Guid("4b4f7b73-df18-4201-876e-b27e172f3b55"), 1306961917972480480L },
                    { new Guid("ed8c2fae-c63f-4aec-af4b-e915b6db38a2"), new Guid("4b4f7b73-df18-4201-876e-b27e172f3b55"), 1306966786867200480L },
                    { new Guid("4963631e-6343-469a-a189-10bfce6e3195"), new Guid("4c96cdb4-efc1-4ccc-8ec6-9ca1bc458d8a"), 1306069130997760480L },
                    { new Guid("810235c8-01f5-4255-a30c-fd77ffeb6eab"), new Guid("4e582063-f524-4ce2-9417-ac2cd957332d"), 1306964900782080480L },
                    { new Guid("547d94df-965f-4833-88fb-c3f66244926c"), new Guid("4f259695-23ea-4453-a4f1-2b055d135c37"), 1306552962355200480L },
                    { new Guid("ffef6a8e-3f80-4a39-97c6-5b2b81582830"), new Guid("4f943ed1-997a-485f-9b54-9824b4ac285c"), 1306069130997760480L },
                    { new Guid("483956b8-4651-4dea-8c34-501108220cc1"), new Guid("520f5cec-5f33-447c-a18b-59d8db31c5e9"), 1306961921249280480L },
                    { new Guid("ce5a43ff-7ee1-451b-ae23-8acd04f87f71"), new Guid("520f5cec-5f33-447c-a18b-59d8db31c5e9"), 1306961921249280480L },
                    { new Guid("ed8c2fae-c63f-4aec-af4b-e915b6db38a2"), new Guid("520f5cec-5f33-447c-a18b-59d8db31c5e9"), 1306966788812800480L },
                    { new Guid("595922c1-5099-4c6f-946f-ed3a7af7d7d3"), new Guid("535e5f96-a036-4a40-96af-6c03cecadcd1"), 1306961923010560480L },
                    { new Guid("73cfe63f-3338-4bd0-a0b9-1b9cc39951ea"), new Guid("5591c5b9-9ee0-44ae-a4fa-39234b95afa4"), 1306069130997760480L },
                    { new Guid("2fe5043f-fd43-45f7-a60b-e76865778c2a"), new Guid("5676c9b3-2d06-4817-9614-4a34230bb05e"), 1306964902236160480L },
                    { new Guid("c56d6a82-abc8-4b17-bc28-27b1904116c9"), new Guid("64346edf-1390-4a90-bc63-93f322ed6c8f"), 1306547601367040480L },
                    { new Guid("7faa6bc4-385b-4cdd-ad8b-4e1673fc893b"), new Guid("68ebd579-e2c7-4f1c-8f9f-7a06df30bd5f"), 1306964901601280480L },
                    { new Guid("3ac59980-d2df-4363-b8db-a4d043e362e7"), new Guid("7aad6dba-3f13-4982-adfa-525fa94485dd"), 1306069130997760480L },
                    { new Guid("7fa014c4-08db-4f96-8132-2bf3db32b256"), new Guid("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"), 1306069130997760480L },
                    { new Guid("8663473b-4a13-46ce-8187-a850215151fe"), new Guid("807029ec-be10-4faa-a332-bcb1021ff966"), 1307030681927680480L },
                    { new Guid("12dbe1a6-7d23-48a4-bacb-164f0403d0f4"), new Guid("8158e1a6-335d-4a29-9177-0f30e86fa8ec"), 1306069130997760480L },
                    { new Guid("3a6f74c2-0165-46b0-8cd5-1846846d97bc"), new Guid("86a086a1-0770-4df4-ade3-433ff7226399"), 1306069130997760480L },
                    { new Guid("f9480a7b-3408-44f0-aca9-78ba66a75799"), new Guid("8c447e9b-1d39-48e5-b9b9-41ee2058b0c7"), 1306961918914560480L },
                    { new Guid("56871df3-2984-4d02-8ff7-61101f0abc19"), new Guid("8e913a11-dbbe-4aa4-ad58-f12737039d83"), 1306964901253120480L },
                    { new Guid("1f164c01-cd48-4b41-8c87-0cd7e5dc1973"), new Guid("9800d45c-7ba8-4728-a6a6-a62dbc7b6f59"), 1306964901928960480L },
                    { new Guid("b79d2f63-487c-44c8-b7d3-1e882994789b"), new Guid("a02edffb-0a63-4106-bac2-ea66f1f65060"), 1306069130997760480L },
                    { new Guid("b3f529a4-ad25-4d14-a86b-c1f4cd3f33e0"), new Guid("a37b1cd8-98c4-4a93-a73e-436c138639eb"), 1306961920430080480L },
                    { new Guid("6e8d08f8-ba2a-4697-8b69-ac5a5bb31bff"), new Guid("a7555120-c3e4-4f8d-bdf8-371ac22daa50"), 1306069130997760480L },
                    { new Guid("1ef3b8a8-6e46-49d7-9a7e-f63137beaade"), new Guid("a807706b-ffb3-4f8d-b18d-9a7ee6b88028"), 1306069130997760480L },
                    { new Guid("e242137a-b4ea-4dd9-9692-63885fce93b8"), new Guid("addfa7a9-7a8c-46ce-90f1-11424f385954"), 1306964904099840480L },
                    { new Guid("7f36ba4f-ec97-4fa9-953b-fa2f1686c448"), new Guid("af9b9a49-0094-4e1c-97dc-d0580525244f"), 1306069130997760480L },
                    { new Guid("841c572c-5098-4e72-a590-2b81706aaa93"), new Guid("b5320a70-11fe-4b7a-9c7e-5bb132e72639"), 1306069130997760480L },
                    { new Guid("2bf3ff67-c1a3-4426-8320-11839daa0a81"), new Guid("b7cdae2b-4f9b-493a-b43b-a3c7ffef3b86"), 1306069130997760480L },
                    { new Guid("595922c1-5099-4c6f-946f-ed3a7af7d7d3"), new Guid("bedacea2-80f1-4d4a-b401-c82940f80d4c"), 1306961916436480480L },
                    { new Guid("b79d2f63-487c-44c8-b7d3-1e882994789b"), new Guid("c4991844-d3b4-4f9a-9c90-c13114515796"), 1306069130997760480L },
                    { new Guid("fff9f1e7-7fd3-42f5-afe7-d40cca07f0ca"), new Guid("c4991844-d3b4-4f9a-9c90-c13114515796"), 1306069130997760480L },
                    { new Guid("c1e7fa06-b759-4bb0-9545-7265e3798d28"), new Guid("ca1d4b3a-336b-40a5-b683-0fe0bcbabaf8"), 1306069130997760480L },
                    { new Guid("db76ae46-851b-47bc-94be-b2e869043636"), new Guid("cc23917b-930a-4e34-9717-be71b9fd2dd5"), 1306069130997760480L },
                    { new Guid("a28ac095-5faa-4a34-bf4e-5c3d3ab7cac9"), new Guid("cd23a5d8-6eab-4e46-a730-56b2808551c6"), 1306964903444480480L },
                    { new Guid("9fe5cc45-a851-4d3f-8b44-32dd96130946"), new Guid("d697fda5-28fa-46c3-ba88-a98dd510e09d"), 1306069130997760480L },
                    { new Guid("213eb6ca-e125-4181-b5bd-3e5666d3e8c9"), new Guid("d9fc6b89-25bb-458e-936f-d76eea2c680f"), 1306961914593280480L },
                    { new Guid("4c206f6e-5e3a-4c96-b48c-81d6df44c9af"), new Guid("e1ab080c-b598-4c1c-9afa-45681f90f1e3"), 1306964902584320480L },
                    { new Guid("a28ac095-5faa-4a34-bf4e-5c3d3ab7cac9"), new Guid("e1ab080c-b598-4c1c-9afa-45681f90f1e3"), 1306964903055360480L },
                    { new Guid("736fd9b6-b56a-4860-8a1c-9a077be886e3"), new Guid("ef15af79-1be1-4055-82b0-83a6aa8fdd35"), 1306069130997760480L },
                    { new Guid("38c69230-1ed0-413e-9ae6-05bc1ef989e0"), new Guid("f2ca3ab7-40da-4828-ad63-06bc9af9b153"), 1306069130997760480L },
                    { new Guid("9d25bf25-5470-4fed-b58c-c4ef4339d533"), new Guid("f63a570e-a762-4410-b4b1-764ee5ceb7ae"), 1306069130997760480L }
                });

            migrationBuilder.InsertData(
                table: "RoleResource",
                columns: new[] { "ResourceId", "RoleId", "CreateBy", "CreateIdentityType", "CreatedTime", "IsDeleted", "IsLocked", "TenantId", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("24ace337-41fe-429d-b32e-d9f88bd97aaa"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("36a4434a-f702-42be-a211-862d0b3b5288"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("3f8d700a-bc26-4d5c-9622-d98bf9359159"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("4af87acd-64b4-4d53-8043-cd7ab6b03c77"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("4e582063-f524-4ce2-9417-ac2cd957332d"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("7819fe8f-8d81-4d00-af2b-c53ec010c65b"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("7aad6dba-3f13-4982-adfa-525fa94485dd"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("86a086a1-0770-4df4-ade3-433ff7226399"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("a2b68c70-173f-46fa-8442-e19219a9905b"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("b7cdae2b-4f9b-493a-b43b-a3c7ffef3b86"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("cc23917b-930a-4e34-9717-be71b9fd2dd5"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("cd23a5d8-6eab-4e46-a730-56b2808551c6"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("d697fda5-28fa-46c3-ba88-a98dd510e09d"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("d9fc6b89-25bb-458e-936f-d76eea2c680f"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("f63a570e-a762-4410-b4b1-764ee5ceb7ae"), 2, null, null, 1307019360624640480L, false, false, null, null, null, null },
                    { new Guid("1c377037-13b4-4ef2-8010-d914a40fdbb3"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("24ace337-41fe-429d-b32e-d9f88bd97aaa"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("286dc779-f58d-439a-bb9b-1333ff2b111b"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("2ac78309-1719-4ea5-ac0f-6974a86f168c"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("3f8d700a-bc26-4d5c-9622-d98bf9359159"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("4f259695-23ea-4453-a4f1-2b055d135c37"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("5591c5b9-9ee0-44ae-a4fa-39234b95afa4"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("8158e1a6-335d-4a29-9177-0f30e86fa8ec"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("a2b68c70-173f-46fa-8442-e19219a9905b"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("cc23917b-930a-4e34-9717-be71b9fd2dd5"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("f2ca3ab7-40da-4828-ad63-06bc9af9b153"), 3, "1", 1, 1307037971660800480L, false, false, new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), null, null, null },
                    { new Guid("1c377037-13b4-4ef2-8010-d914a40fdbb3"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("24ace337-41fe-429d-b32e-d9f88bd97aaa"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("286dc779-f58d-439a-bb9b-1333ff2b111b"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("2ac78309-1719-4ea5-ac0f-6974a86f168c"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("3f8d700a-bc26-4d5c-9622-d98bf9359159"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("4f259695-23ea-4453-a4f1-2b055d135c37"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("5591c5b9-9ee0-44ae-a4fa-39234b95afa4"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("8158e1a6-335d-4a29-9177-0f30e86fa8ec"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("a2b68c70-173f-46fa-8442-e19219a9905b"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("cc23917b-930a-4e34-9717-be71b9fd2dd5"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null },
                    { new Guid("f2ca3ab7-40da-4828-ad63-06bc9af9b153"), 4, "1", 1, 1307037971578880480L, false, false, new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), null, null, null }
                });

            migrationBuilder.InsertData(
                table: "SystemTenantResource",
                columns: new[] { "ResourceId", "TenantId", "CreateBy", "CreateIdentityType", "CreatedTime", "IsDeleted", "IsLocked", "UpdateBy", "UpdateIdentityType", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("1c377037-13b4-4ef2-8010-d914a40fdbb3"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("24ace337-41fe-429d-b32e-d9f88bd97aaa"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("286dc779-f58d-439a-bb9b-1333ff2b111b"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("2ac78309-1719-4ea5-ac0f-6974a86f168c"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("3f8d700a-bc26-4d5c-9622-d98bf9359159"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("4f259695-23ea-4453-a4f1-2b055d135c37"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("5591c5b9-9ee0-44ae-a4fa-39234b95afa4"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("8158e1a6-335d-4a29-9177-0f30e86fa8ec"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("a2b68c70-173f-46fa-8442-e19219a9905b"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("cc23917b-930a-4e34-9717-be71b9fd2dd5"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("f2ca3ab7-40da-4828-ad63-06bc9af9b153"), new Guid("710148b3-0c80-48a2-8f57-4b863be9859f"), "1", 1, 1307037961912320480L, false, false, null, null, null },
                    { new Guid("1c377037-13b4-4ef2-8010-d914a40fdbb3"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("24ace337-41fe-429d-b32e-d9f88bd97aaa"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("286dc779-f58d-439a-bb9b-1333ff2b111b"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("2ac78309-1719-4ea5-ac0f-6974a86f168c"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("3f8d700a-bc26-4d5c-9622-d98bf9359159"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("4f259695-23ea-4453-a4f1-2b055d135c37"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("5591c5b9-9ee0-44ae-a4fa-39234b95afa4"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("8158e1a6-335d-4a29-9177-0f30e86fa8ec"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("a2b68c70-173f-46fa-8442-e19219a9905b"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("cc23917b-930a-4e34-9717-be71b9fd2dd5"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null },
                    { new Guid("f2ca3ab7-40da-4828-ad63-06bc9af9b153"), new Guid("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"), "1", 1, 1307037961809920480L, false, false, null, null, null }
                });

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
                name: "IX_Code_CodeTypeId",
                table: "Code",
                column: "CodeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeType_CodeTypeValue",
                table: "CodeType",
                column: "CodeTypeValue");

            migrationBuilder.CreateIndex(
                name: "IX_Dept_ParentId",
                table: "Dept",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Function_Key",
                table: "Function",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_Function_Path_Method",
                table: "Function",
                columns: new[] { "Path", "Method" });

            migrationBuilder.CreateIndex(
                name: "IX_Resource_Key",
                table: "Resource",
                column: "Key");

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
                name: "IX_Sys_CodeGenConfig_CodeGenId",
                table: "Sys_CodeGenConfig",
                column: "CodeGenId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemTenantResource_ResourceId",
                table: "SystemTenantResource",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcement");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "AuditProperty");

            migrationBuilder.DropTable(
                name: "ClientFunction");

            migrationBuilder.DropTable(
                name: "Code");

            migrationBuilder.DropTable(
                name: "EmailServerConfig");

            migrationBuilder.DropTable(
                name: "EmailTemplate");

            migrationBuilder.DropTable(
                name: "EntityCodeGenerationSetting");

            migrationBuilder.DropTable(
                name: "ImSession");

            migrationBuilder.DropTable(
                name: "ImSessionMessage");

            migrationBuilder.DropTable(
                name: "ImUserSession");

            migrationBuilder.DropTable(
                name: "LoginToken");

            migrationBuilder.DropTable(
                name: "ResourceFunction");

            migrationBuilder.DropTable(
                name: "RoleResource");

            migrationBuilder.DropTable(
                name: "Sys_CodeGenConfig");

            migrationBuilder.DropTable(
                name: "SysJobCluster");

            migrationBuilder.DropTable(
                name: "SysJobDetail");

            migrationBuilder.DropTable(
                name: "SysJobTrigger");

            migrationBuilder.DropTable(
                name: "SystemTenantResource");

            migrationBuilder.DropTable(
                name: "UserExtension");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "VerifyCodeLog");

            migrationBuilder.DropTable(
                name: "AuditEntity");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "CodeType");

            migrationBuilder.DropTable(
                name: "Function");

            migrationBuilder.DropTable(
                name: "Sys_CodeGen");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "SystemTenant");

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
