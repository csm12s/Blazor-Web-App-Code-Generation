using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardener.Database.Migrations.Migrations
{
    public partial class v001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Box",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Week = table.Column<int>(type: "INTEGER", nullable: false),
                    Section = table.Column<int>(type: "INTEGER", nullable: false),
                    No = table.Column<string>(type: "TEXT", nullable: true),
                    Frequency = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Box", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    GradeId = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassHourSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassHourSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Curriculum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    SubjectId = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Remark = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolYear",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolYear", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UniqueName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Security", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semester",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semester", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    Sex = table.Column<int>(type: "INTEGER", nullable: false),
                    CardID = table.Column<string>(type: "TEXT", nullable: true),
                    Avatar = table.Column<string>(type: "TEXT", nullable: true),
                    DateOfDeath = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateEntrance = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsFinish = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    Sex = table.Column<int>(type: "INTEGER", nullable: false),
                    Avatar = table.Column<string>(type: "TEXT", nullable: true),
                    CardID = table.Column<string>(type: "TEXT", nullable: true),
                    DateOfDeath = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    DateEntrance = table.Column<DateTime>(type: "TEXT", nullable: false),
                    WorkYears = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TutorialScheduleRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ForObject = table.Column<string>(type: "TEXT", nullable: true),
                    No = table.Column<string>(type: "TEXT", nullable: true),
                    RuleSort = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorialScheduleRule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Account = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true),
                    Password = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeacherId = table.Column<string>(type: "TEXT", nullable: true),
                    TeacherName = table.Column<string>(type: "TEXT", nullable: true),
                    ClassesId = table.Column<string>(type: "TEXT", nullable: true),
                    ClassName = table.Column<string>(type: "TEXT", nullable: true),
                    CurriculumId = table.Column<string>(type: "TEXT", nullable: true),
                    CurriculumName = table.Column<string>(type: "TEXT", nullable: true),
                    BoxId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fill_Box_BoxId",
                        column: x => x.BoxId,
                        principalTable: "Box",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleSecurity",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    SecurityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleSecurity", x => new { x.RoleId, x.SecurityId });
                    table.ForeignKey(
                        name: "FK_RoleSecurity_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleSecurity_Security_SecurityId",
                        column: x => x.SecurityId,
                        principalTable: "Security",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
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
                table: "Role",
                columns: new[] { "Id", "Name", "Remark" },
                values: new object[] { 1, "超级管理员", "拥有所有权限" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name", "Remark" },
                values: new object[] { 2, "普通人", "没有关联权限" });

            migrationBuilder.InsertData(
                table: "Security",
                columns: new[] { "Id", "UniqueName" },
                values: new object[] { 1, "ViewRoles" });

            migrationBuilder.InsertData(
                table: "Security",
                columns: new[] { "Id", "UniqueName" },
                values: new object[] { 2, "ViewSecuries" });

            migrationBuilder.InsertData(
                table: "Security",
                columns: new[] { "Id", "UniqueName" },
                values: new object[] { 3, "GetRoles" });

            migrationBuilder.InsertData(
                table: "Security",
                columns: new[] { "Id", "UniqueName" },
                values: new object[] { 4, "InsertRole" });

            migrationBuilder.InsertData(
                table: "Security",
                columns: new[] { "Id", "UniqueName" },
                values: new object[] { 5, "GiveUserRole" });

            migrationBuilder.InsertData(
                table: "Security",
                columns: new[] { "Id", "UniqueName" },
                values: new object[] { 6, "GiveRoleSecurity" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Account", "Password" },
                values: new object[] { 1, "admin", "admin" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Account", "Password" },
                values: new object[] { 2, "Fur", "dotnetchina" });

            migrationBuilder.InsertData(
                table: "RoleSecurity",
                columns: new[] { "RoleId", "SecurityId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "RoleSecurity",
                columns: new[] { "RoleId", "SecurityId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "RoleSecurity",
                columns: new[] { "RoleId", "SecurityId" },
                values: new object[] { 1, 3 });

            migrationBuilder.InsertData(
                table: "RoleSecurity",
                columns: new[] { "RoleId", "SecurityId" },
                values: new object[] { 1, 4 });

            migrationBuilder.InsertData(
                table: "RoleSecurity",
                columns: new[] { "RoleId", "SecurityId" },
                values: new object[] { 1, 5 });

            migrationBuilder.InsertData(
                table: "RoleSecurity",
                columns: new[] { "RoleId", "SecurityId" },
                values: new object[] { 1, 6 });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Fill_BoxId",
                table: "Fill",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleSecurity_SecurityId",
                table: "RoleSecurity",
                column: "SecurityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "ClassHourSetting");

            migrationBuilder.DropTable(
                name: "Curriculum");

            migrationBuilder.DropTable(
                name: "Fill");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "RoleSecurity");

            migrationBuilder.DropTable(
                name: "SchoolYear");

            migrationBuilder.DropTable(
                name: "Semester");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "TutorialScheduleRule");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Box");

            migrationBuilder.DropTable(
                name: "Security");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
