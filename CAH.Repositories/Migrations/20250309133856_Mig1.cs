using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CAH.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicYears",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdmissionMethods",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MethodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredDocuments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Majors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelatedSkills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Majors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniversityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accreditation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstablishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RankingNational = table.Column<int>(type: "int", nullable: true),
                    RankingInternational = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.UserId, x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "UserLoginHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoginTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "UniMajors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MajorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UniversityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TuitionFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MajorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniMajors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UniMajors_Majors_MajorId",
                        column: x => x.MajorId,
                        principalTable: "Majors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UniMajors_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectScores",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Score = table.Column<float>(type: "real", nullable: false),
                    ExamType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectScores_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectScores_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "AdmissionInfors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UniMajorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AcademicYearId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quota = table.Column<int>(type: "int", nullable: false),
                    AdmisstionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionInfors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdmissionInfors_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdmissionInfors_UniMajors_UniMajorId",
                        column: x => x.UniMajorId,
                        principalTable: "UniMajors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UniMajorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_UniMajors_UniMajorId",
                        column: x => x.UniMajorId,
                        principalTable: "UniMajors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InforMethods",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdmissionInforId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdmissionMethodId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ScoreType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScoreRequirement = table.Column<int>(type: "int", nullable: false),
                    PercentageOfQuota = table.Column<float>(type: "real", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InforMethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InforMethods_AdmissionInfors_AdmissionInforId",
                        column: x => x.AdmissionInforId,
                        principalTable: "AdmissionInfors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InforMethods_AdmissionMethods_AdmissionMethodId",
                        column: x => x.AdmissionMethodId,
                        principalTable: "AdmissionMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdmissionInforId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wishlists_AdmissionInfors_AdmissionInforId",
                        column: x => x.AdmissionInforId,
                        principalTable: "AdmissionInfors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wishlists_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AcademicYears",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "LastUpdatedBy", "LastUpdatedTime", "Year" },
                values: new object[,]
                {
                    { "7e2609f6-435a-4c54-b566-38b957cea339", "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(6017), new TimeSpan(0, 0, 0, 0, 0)), null, null, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(6017), new TimeSpan(0, 0, 0, 0, 0)), 2024 },
                    { "fcf5e5a2-f67f-4569-9537-ccad56aa111a", "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5983), new TimeSpan(0, 0, 0, 0, 0)), null, null, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5983), new TimeSpan(0, 0, 0, 0, 0)), 2025 }
                });

            migrationBuilder.InsertData(
                table: "AdmissionMethods",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "LastUpdatedBy", "LastUpdatedTime", "MethodName", "RequiredDocuments" },
                values: new object[,]
                {
                    { "25a5d008-5eb9-479b-b6e8-ab52fb46a001", "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5795), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5795), new TimeSpan(0, 0, 0, 0, 0)), "Xet Diem Tot Nghiep", null },
                    { "3f4d331a-800f-4d9e-9a30-0650d49664f5", "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5799), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5800), new TimeSpan(0, 0, 0, 0, 0)), "Xet diem danh gia nang luc", null }
                });

            migrationBuilder.InsertData(
                table: "Majors",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "LastUpdatedBy", "LastUpdatedTime", "Name", "RelatedSkills" },
                values: new object[,]
                {
                    { "3b075d6a-8c32-4f82-8afa-21c2070e6b21", "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5327), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5327), new TimeSpan(0, 0, 0, 0, 0)), "Kinh Te", null },
                    { "8d1cce75-ff32-4827-b478-a9409b5478f1", "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5315), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5316), new TimeSpan(0, 0, 0, 0, 0)), "Cong Nghe Thong Tin", null }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "LastUpdatedBy", "LastUpdatedTime", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("ff818077-4b3f-4b71-901f-808f3dbc6920"), null, "System", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 152, DateTimeKind.Unspecified).AddTicks(5984), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 152, DateTimeKind.Unspecified).AddTicks(5985), new TimeSpan(0, 0, 0, 0, 0)), "Admin", "ADMIN" },
                    { new Guid("ff94595c-3ab1-4db7-8fa6-7f7cedc52466"), null, "System", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 152, DateTimeKind.Unspecified).AddTicks(5987), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 152, DateTimeKind.Unspecified).AddTicks(5987), new TimeSpan(0, 0, 0, 0, 0)), "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "LastUpdatedBy", "LastUpdatedTime", "Name" },
                values: new object[,]
                {
                    { "46729759-a410-4be3-9902-f352be1ad314", "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5740), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5740), new TimeSpan(0, 0, 0, 0, 0)), "Tieng Anh" },
                    { "76674933-d5f0-4dd4-8b0a-45384c2933f4", "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5734), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5734), new TimeSpan(0, 0, 0, 0, 0)), "Toan" }
                });

            migrationBuilder.InsertData(
                table: "Universities",
                columns: new[] { "Id", "Accreditation", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "Email", "EstablishedDate", "Image", "LastUpdatedBy", "LastUpdatedTime", "Location", "Name", "PhoneNumber", "RankingInternational", "RankingNational", "Type", "UniversityCode" },
                values: new object[,]
                {
                    { "0124a175-3558-4578-962c-95c7113e8c86", "So 1 VVN", "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5246), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, "suphamkithuat@gmail.com", new DateTime(2025, 3, 9, 20, 38, 56, 270, DateTimeKind.Local).AddTicks(5244), null, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5247), new TimeSpan(0, 0, 0, 0, 0)), "Vo Van Ngan, Thu Duc", "Su Pham Ki Thuat", "0987654321", 1, 2, "State", "HCMUTE" },
                    { "62a883af-8908-4067-876f-9ce9356798c2", "So 1 Viet Nam", "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5238), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, "fptuniversity@fpt.edu.vn", new DateTime(2025, 3, 9, 20, 38, 56, 270, DateTimeKind.Local).AddTicks(5231), null, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5238), new TimeSpan(0, 0, 0, 0, 0)), "Thu Duc", "FPT University", "0987654321", 1, 2, "Private", "FPT" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Email", "EmailConfirmed", "LastUpdatedBy", "LastUpdatedTime", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserImage", "UserName" },
                values: new object[,]
                {
                    { new Guid("3ec1a024-a587-44fe-a361-900a130e0c8b"), 0, "f969d7f7-33f1-40e2-8cdf-11aca8452f87", "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 266, DateTimeKind.Unspecified).AddTicks(795), new TimeSpan(0, 0, 0, 0, 0)), null, null, "user@example.com", true, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 266, DateTimeKind.Unspecified).AddTicks(796), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Jack 97", "USER@EXAMPLE.COM", "USER@EXAMPLE.COM", "", "AQAAAAIAAYagAAAAEEbznCT3tZ3mzZMckN+Kk4oDVxLL4EfNbMYdbvsAt76BqHzePxWE98kR8nZeQ5uT8g==", null, false, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "SeedData", false, null, "user" },
                    { new Guid("ccf5d3fb-9d73-4076-9809-4b60077147c5"), 0, "556817ac-6a79-4493-8250-6c066572f786", "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 266, DateTimeKind.Unspecified).AddTicks(787), new TimeSpan(0, 0, 0, 0, 0)), null, null, "admin@example.com", true, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 266, DateTimeKind.Unspecified).AddTicks(788), new TimeSpan(0, 0, 0, 0, 0)), false, null, "KICM vippro", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "", "AQAAAAIAAYagAAAAEPIf9p5JZ1WN0WrpavmNP9B3EjBEhI6e1xc+5vRzT5JfgnDh2Ith2BPwtRz52h7yzg==", null, false, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "SeedData", false, null, "admin" }
                });

            migrationBuilder.InsertData(
                table: "UniMajors",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "LastUpdatedBy", "LastUpdatedTime", "MajorCode", "MajorId", "TuitionFee", "UniversityId" },
                values: new object[,]
                {
                    { "d4a35352-131f-4171-9c25-97d19c36c685", "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5662), new TimeSpan(0, 0, 0, 0, 0)), null, null, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5662), new TimeSpan(0, 0, 0, 0, 0)), "SE", "8d1cce75-ff32-4827-b478-a9409b5478f1", "30.000.000", "62a883af-8908-4067-876f-9ce9356798c2" },
                    { "e52f6bf7-ca13-4d02-abdf-b0371d22e142", "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5666), new TimeSpan(0, 0, 0, 0, 0)), null, null, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5667), new TimeSpan(0, 0, 0, 0, 0)), "SS", "3b075d6a-8c32-4f82-8afa-21c2070e6b21", "20.000.000", "62a883af-8908-4067-876f-9ce9356798c2" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "LastUpdatedBy", "LastUpdatedTime" },
                values: new object[,]
                {
                    { new Guid("ff94595c-3ab1-4db7-8fa6-7f7cedc52466"), new Guid("3ec1a024-a587-44fe-a361-900a130e0c8b"), "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(4814), new TimeSpan(0, 0, 0, 0, 0)), null, null, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(4814), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("ff818077-4b3f-4b71-901f-808f3dbc6920"), new Guid("ccf5d3fb-9d73-4076-9809-4b60077147c5"), "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(4810), new TimeSpan(0, 0, 0, 0, 0)), null, null, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(4810), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "AdmissionInfors",
                columns: new[] { "Id", "AcademicYearId", "AdmisstionDate", "CreatedBy", "CreatedTime", "Deadline", "DeletedBy", "DeletedTime", "LastUpdatedBy", "LastUpdatedTime", "Quota", "UniMajorId" },
                values: new object[,]
                {
                    { "5b2d9021-5c6e-42b5-8530-030b96edfa2e", "fcf5e5a2-f67f-4569-9537-ccad56aa111a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5860), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5861), new TimeSpan(0, 0, 0, 0, 0)), 0, "d4a35352-131f-4171-9c25-97d19c36c685" },
                    { "e57252c6-dbc7-44fe-8e31-21c306283dc1", "7e2609f6-435a-4c54-b566-38b957cea339", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5864), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5865), new TimeSpan(0, 0, 0, 0, 0)), 0, "e52f6bf7-ca13-4d02-abdf-b0371d22e142" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "LastUpdatedBy", "LastUpdatedTime", "Rating", "UniMajorId", "UserId" },
                values: new object[,]
                {
                    { "1b91eb4e-75d4-4cbf-9279-5e4906847158", "Moi truong hoc tap nang dong!", "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(6177), new TimeSpan(0, 0, 0, 0, 0)), null, null, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(6178), new TimeSpan(0, 0, 0, 0, 0)), 4, "e52f6bf7-ca13-4d02-abdf-b0371d22e142", new Guid("ccf5d3fb-9d73-4076-9809-4b60077147c5") },
                    { "5e150d55-be67-409d-b093-2f65f8efd213", "Truong rat tot!", "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(6171), new TimeSpan(0, 0, 0, 0, 0)), null, null, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(6172), new TimeSpan(0, 0, 0, 0, 0)), 5, "d4a35352-131f-4171-9c25-97d19c36c685", new Guid("3ec1a024-a587-44fe-a361-900a130e0c8b") }
                });

            migrationBuilder.InsertData(
                table: "InforMethods",
                columns: new[] { "Id", "AdmissionInforId", "AdmissionMethodId", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "LastUpdatedBy", "LastUpdatedTime", "PercentageOfQuota", "ScoreRequirement", "ScoreType" },
                values: new object[,]
                {
                    { "7b21b721-e53e-45f1-ba9c-882020b52dd2", "e57252c6-dbc7-44fe-8e31-21c306283dc1", "3f4d331a-800f-4d9e-9a30-0650d49664f5", "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(6070), new TimeSpan(0, 0, 0, 0, 0)), null, null, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(6070), new TimeSpan(0, 0, 0, 0, 0)), 0f, 0, "Percentage" },
                    { "c5628af6-0165-462f-aede-0b34a487eb82", "5b2d9021-5c6e-42b5-8530-030b96edfa2e", "25a5d008-5eb9-479b-b6e8-ab52fb46a001", "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(6063), new TimeSpan(0, 0, 0, 0, 0)), null, null, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(6064), new TimeSpan(0, 0, 0, 0, 0)), 0f, 0, "GPA" }
                });

            migrationBuilder.InsertData(
                table: "Wishlists",
                columns: new[] { "Id", "AdmissionInforId", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "LastUpdatedBy", "LastUpdatedTime", "UserId" },
                values: new object[] { "5bad6dde-9d3e-4aa8-a960-2086267dc684", "5b2d9021-5c6e-42b5-8530-030b96edfa2e", "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5925), new TimeSpan(0, 0, 0, 0, 0)), null, null, "SeedData", new DateTimeOffset(new DateTime(2025, 3, 9, 13, 38, 56, 270, DateTimeKind.Unspecified).AddTicks(5931), new TimeSpan(0, 0, 0, 0, 0)), new Guid("3ec1a024-a587-44fe-a361-900a130e0c8b") });

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionInfors_AcademicYearId",
                table: "AdmissionInfors",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionInfors_UniMajorId",
                table: "AdmissionInfors",
                column: "UniMajorId");

            migrationBuilder.CreateIndex(
                name: "IX_InforMethods_AdmissionInforId",
                table: "InforMethods",
                column: "AdmissionInforId");

            migrationBuilder.CreateIndex(
                name: "IX_InforMethods_AdmissionMethodId",
                table: "InforMethods",
                column: "AdmissionMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UniMajorId",
                table: "Reviews",
                column: "UniMajorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectScores_SubjectId",
                table: "SubjectScores",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectScores_UserId",
                table: "SubjectScores",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UniMajors_MajorId",
                table: "UniMajors",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_UniMajors_UniversityId",
                table: "UniMajors",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_AdmissionInforId",
                table: "Wishlists",
                column: "AdmissionInforId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InforMethods");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "SubjectScores");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserLoginHistories");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "Wishlists");

            migrationBuilder.DropTable(
                name: "AdmissionMethods");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "AdmissionInfors");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "AcademicYears");

            migrationBuilder.DropTable(
                name: "UniMajors");

            migrationBuilder.DropTable(
                name: "Majors");

            migrationBuilder.DropTable(
                name: "Universities");
        }
    }
}
