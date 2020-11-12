﻿// <auto-generated />
using System;
using Gardener.EntityFramwork.Core.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gardener.Database.Migrations.Migrations
{
    [DbContext(typeof(GardenerDbContext))]
    partial class GardenerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Gardener.Core.Entites.Box", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("No")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Section")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Week")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Box");
                });

            modelBuilder.Entity("Gardener.Core.Entites.ClassHourSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("ClassHourSetting");
                });

            modelBuilder.Entity("Gardener.Core.Entites.Classes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("GradeId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("Gardener.Core.Entites.Curriculum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4");

                    b.Property<string>("SubjectId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Curriculum");
                });

            modelBuilder.Entity("Gardener.Core.Entites.Fill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("BoxId")
                        .HasColumnType("int");

                    b.Property<string>("ClassName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClassesId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CurriculumId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CurriculumName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("TeacherId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("TeacherName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("BoxId");

                    b.ToTable("Fill");
                });

            modelBuilder.Entity("Gardener.Core.Entites.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Grade");
                });

            modelBuilder.Entity("Gardener.Core.Entites.SchoolYear", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SchoolYear");
                });

            modelBuilder.Entity("Gardener.Core.Entites.Semester", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Semester");
                });

            modelBuilder.Entity("Gardener.Core.Entites.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CardID")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateEntrance")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateOfDeath")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsFinish")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Gardener.Core.Entites.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CardID")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateEntrance")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateOfDeath")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("WorkYears")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("Gardener.Core.Entites.TutorialScheduleRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ForObject")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("No")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("RuleSort")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("TutorialScheduleRule");
                });

            modelBuilder.Entity("Gardener.Core.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Remark")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            Name = "超级管理员",
                            Remark = "拥有所有权限"
                        },
                        new
                        {
                            Id = 2,
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            Name = "普通人",
                            Remark = "没有关联权限"
                        });
                });

            modelBuilder.Entity("Gardener.Core.RoleSecurity", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("SecurityId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("RoleId", "SecurityId");

                    b.HasIndex("SecurityId");

                    b.ToTable("RoleSecurity");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            SecurityId = 1,
                            CreatedTime = new DateTimeOffset(new DateTime(2020, 11, 11, 17, 50, 31, 436, DateTimeKind.Unspecified).AddTicks(5427), new TimeSpan(0, 8, 0, 0, 0))
                        },
                        new
                        {
                            RoleId = 1,
                            SecurityId = 2,
                            CreatedTime = new DateTimeOffset(new DateTime(2020, 11, 11, 17, 50, 31, 440, DateTimeKind.Unspecified).AddTicks(7874), new TimeSpan(0, 8, 0, 0, 0))
                        },
                        new
                        {
                            RoleId = 1,
                            SecurityId = 3,
                            CreatedTime = new DateTimeOffset(new DateTime(2020, 11, 11, 17, 50, 31, 440, DateTimeKind.Unspecified).AddTicks(7915), new TimeSpan(0, 8, 0, 0, 0))
                        },
                        new
                        {
                            RoleId = 1,
                            SecurityId = 4,
                            CreatedTime = new DateTimeOffset(new DateTime(2020, 11, 11, 17, 50, 31, 440, DateTimeKind.Unspecified).AddTicks(7919), new TimeSpan(0, 8, 0, 0, 0))
                        },
                        new
                        {
                            RoleId = 1,
                            SecurityId = 5,
                            CreatedTime = new DateTimeOffset(new DateTime(2020, 11, 11, 17, 50, 31, 440, DateTimeKind.Unspecified).AddTicks(7921), new TimeSpan(0, 8, 0, 0, 0))
                        },
                        new
                        {
                            RoleId = 1,
                            SecurityId = 6,
                            CreatedTime = new DateTimeOffset(new DateTime(2020, 11, 11, 17, 50, 31, 440, DateTimeKind.Unspecified).AddTicks(7922), new TimeSpan(0, 8, 0, 0, 0))
                        });
                });

            modelBuilder.Entity("Gardener.Core.Security", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UniqueName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Security");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            UniqueName = "ViewRoles"
                        },
                        new
                        {
                            Id = 2,
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            UniqueName = "ViewSecuries"
                        },
                        new
                        {
                            Id = 3,
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            UniqueName = "GetRoles"
                        },
                        new
                        {
                            Id = 4,
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            UniqueName = "InsertRole"
                        },
                        new
                        {
                            Id = 5,
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            UniqueName = "GiveUserRole"
                        },
                        new
                        {
                            Id = 6,
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            UniqueName = "GiveRoleSecurity"
                        });
                });

            modelBuilder.Entity("Gardener.Core.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Account")
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Account = "admin",
                            CreatedTime = new DateTimeOffset(new DateTime(2020, 11, 11, 17, 50, 31, 519, DateTimeKind.Unspecified).AddTicks(3345), new TimeSpan(0, 8, 0, 0, 0)),
                            IsDeleted = false,
                            Password = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Account = "Fur",
                            CreatedTime = new DateTimeOffset(new DateTime(2020, 11, 11, 17, 50, 31, 519, DateTimeKind.Unspecified).AddTicks(4297), new TimeSpan(0, 8, 0, 0, 0)),
                            IsDeleted = false,
                            Password = "dotnetchina"
                        });
                });

            modelBuilder.Entity("Gardener.Core.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1,
                            CreatedTime = new DateTimeOffset(new DateTime(2020, 11, 11, 17, 50, 31, 519, DateTimeKind.Unspecified).AddTicks(7), new TimeSpan(0, 8, 0, 0, 0))
                        });
                });

            modelBuilder.Entity("Gardener.Core.Entites.Fill", b =>
                {
                    b.HasOne("Gardener.Core.Entites.Box", null)
                        .WithMany("Fills")
                        .HasForeignKey("BoxId");
                });

            modelBuilder.Entity("Gardener.Core.RoleSecurity", b =>
                {
                    b.HasOne("Gardener.Core.Role", "Role")
                        .WithMany("RoleSecurities")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gardener.Core.Security", "Security")
                        .WithMany("RoleSecurities")
                        .HasForeignKey("SecurityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Security");
                });

            modelBuilder.Entity("Gardener.Core.UserRole", b =>
                {
                    b.HasOne("Gardener.Core.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gardener.Core.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Gardener.Core.Entites.Box", b =>
                {
                    b.Navigation("Fills");
                });

            modelBuilder.Entity("Gardener.Core.Role", b =>
                {
                    b.Navigation("RoleSecurities");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Gardener.Core.Security", b =>
                {
                    b.Navigation("RoleSecurities");
                });

            modelBuilder.Entity("Gardener.Core.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
