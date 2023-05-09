using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gardener.Api.Core.Migrations.GardenerAuditDb
{
    /// <inheritdoc />
    public partial class v001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_AuditEntity_AuditOperationId",
                table: "AuditEntity",
                column: "AuditOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditProperty_AuditEntityid",
                table: "AuditProperty",
                column: "AuditEntityid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditProperty");

            migrationBuilder.DropTable(
                name: "AuditEntity");

            migrationBuilder.DropTable(
                name: "AuditOperation");
        }
    }
}
