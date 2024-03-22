using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GolfCourses",
                columns: table => new
                {
                    GCId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GCName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCZip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCNbrHoles = table.Column<int>(type: "int", nullable: false),
                    GCType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCName1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCName3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCT1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCT2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCT3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCT4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCT5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCC1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCC2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCC3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCC4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCC5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCT1TotIn = table.Column<int>(type: "int", nullable: false),
                    GCT2TotIn = table.Column<int>(type: "int", nullable: false),
                    GCT3TotIn = table.Column<int>(type: "int", nullable: false),
                    GCT4TotIn = table.Column<int>(type: "int", nullable: false),
                    GCT5TotIn = table.Column<int>(type: "int", nullable: false),
                    GCT1TotOut = table.Column<int>(type: "int", nullable: false),
                    GCT2TotOut = table.Column<int>(type: "int", nullable: false),
                    GCT3TotOut = table.Column<int>(type: "int", nullable: false),
                    GCT4TotOut = table.Column<int>(type: "int", nullable: false),
                    GCT5TotOut = table.Column<int>(type: "int", nullable: false),
                    GCT1Tot = table.Column<int>(type: "int", nullable: false),
                    GCT2Tot = table.Column<int>(type: "int", nullable: false),
                    GCT3Tot = table.Column<int>(type: "int", nullable: false),
                    GCT4Tot = table.Column<int>(type: "int", nullable: false),
                    GCT5Tot = table.Column<int>(type: "int", nullable: false),
                    GCParIn = table.Column<int>(type: "int", nullable: false),
                    GCParOut = table.Column<int>(type: "int", nullable: false),
                    GCParTot = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GolfCourses", x => x.GCId);
                });

            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    LId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeagueName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LZip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LContactFName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LContactLName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LContactPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LSeasonStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LSeasonEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.LId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GolfCourseHoles",
                columns: table => new
                {
                    GHId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GCId = table.Column<int>(type: "int", nullable: false),
                    GCName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GHSectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GHHole = table.Column<int>(type: "int", nullable: false),
                    GHHandicap = table.Column<int>(type: "int", nullable: false),
                    GHPar = table.Column<int>(type: "int", nullable: false),
                    GHT1 = table.Column<int>(type: "int", nullable: false),
                    GHT2 = table.Column<int>(type: "int", nullable: false),
                    GHT3 = table.Column<int>(type: "int", nullable: false),
                    GHT4 = table.Column<int>(type: "int", nullable: false),
                    GHT5 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GolfCourseHoles", x => x.GHId);
                    table.ForeignKey(
                        name: "FK_GolfCourseHoles_GolfCourses_GCId",
                        column: x => x.GCId,
                        principalTable: "GolfCourses",
                        principalColumn: "GCId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EvId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EvTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EvEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EvEndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EvRegMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvRegStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EvRegStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EvRegEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EvRegEndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EvLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvContactFName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvContactLName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvContactPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvFeeAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvRegComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EvId);
                    table.ForeignKey(
                        name: "FK_Events_Leagues_LId",
                        column: x => x.LId,
                        principalTable: "Leagues",
                        principalColumn: "LId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GolfRounds",
                columns: table => new
                {
                    GRId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GCId = table.Column<int>(type: "int", nullable: false),
                    GRDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GRType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GolfRounds", x => x.GRId);
                    table.ForeignKey(
                        name: "FK_GolfRounds_GolfCourses_GCId",
                        column: x => x.GCId,
                        principalTable: "GolfCourses",
                        principalColumn: "GCId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GolfRounds_Leagues_LId",
                        column: x => x.LId,
                        principalTable: "Leagues",
                        principalColumn: "LId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Registered = table.Column<bool>(type: "bit", nullable: false),
                    PreferredNotification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberTee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Handicap = table.Column<int>(type: "int", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberId);
                    table.ForeignKey(
                        name: "FK_Members_Leagues_LId",
                        column: x => x.LId,
                        principalTable: "Leagues",
                        principalColumn: "LId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GolfPlayerScores",
                columns: table => new
                {
                    GPSId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GPId = table.Column<int>(type: "int", nullable: false),
                    GHId = table.Column<int>(type: "int", nullable: false),
                    GPSStrokes = table.Column<int>(type: "int", nullable: false),
                    GPSPutts = table.Column<int>(type: "int", nullable: false),
                    GPSGIY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GPSFW = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GolfPlayerScores", x => x.GPSId);
                    table.ForeignKey(
                        name: "FK_GolfPlayerScores_GolfCourseHoles_GHId",
                        column: x => x.GHId,
                        principalTable: "GolfCourseHoles",
                        principalColumn: "GHId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvParticipants",
                columns: table => new
                {
                    EPId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    EPType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EPLName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EPFName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EPEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EPPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvParticipants", x => x.EPId);
                    table.ForeignKey(
                        name: "FK_EvParticipants_Events_EvId",
                        column: x => x.EvId,
                        principalTable: "Events",
                        principalColumn: "EvId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GolfGroups",
                columns: table => new
                {
                    GGId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GGTtime = table.Column<TimeSpan>(type: "time", nullable: false),
                    GGName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GRId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GolfGroups", x => x.GGId);
                    table.ForeignKey(
                        name: "FK_GolfGroups_GolfRounds_GRId",
                        column: x => x.GRId,
                        principalTable: "GolfRounds",
                        principalColumn: "GRId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GolfPlayers",
                columns: table => new
                {
                    GPId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GPType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GPFName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GPLName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GGId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    Tot1 = table.Column<int>(type: "int", nullable: false),
                    Tot2 = table.Column<int>(type: "int", nullable: false),
                    Tot3 = table.Column<int>(type: "int", nullable: false),
                    PTot1 = table.Column<int>(type: "int", nullable: false),
                    PTot2 = table.Column<int>(type: "int", nullable: false),
                    PTot3 = table.Column<int>(type: "int", nullable: false),
                    GTot1 = table.Column<int>(type: "int", nullable: false),
                    GTot2 = table.Column<int>(type: "int", nullable: false),
                    GTot3 = table.Column<int>(type: "int", nullable: false),
                    FTot1 = table.Column<int>(type: "int", nullable: false),
                    FTot2 = table.Column<int>(type: "int", nullable: false),
                    FTot3 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GolfPlayers", x => x.GPId);
                    table.ForeignKey(
                        name: "FK_GolfPlayers_GolfGroups_GGId",
                        column: x => x.GGId,
                        principalTable: "GolfGroups",
                        principalColumn: "GGId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GolfPlayers_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "GolfCourses",
                columns: new[] { "GCId", "GCC1", "GCC2", "GCC3", "GCC4", "GCC5", "GCCity", "GCComments", "GCName", "GCName1", "GCName2", "GCName3", "GCNbrHoles", "GCParIn", "GCParOut", "GCParTot", "GCPhoneNumber", "GCState", "GCStreet", "GCT1", "GCT1Tot", "GCT1TotIn", "GCT1TotOut", "GCT2", "GCT2Tot", "GCT2TotIn", "GCT2TotOut", "GCT3", "GCT3Tot", "GCT3TotIn", "GCT3TotOut", "GCT4", "GCT4Tot", "GCT4TotIn", "GCT4TotOut", "GCT5", "GCT5Tot", "GCT5TotIn", "GCT5TotOut", "GCType", "GCZip" },
                values: new object[,]
                {
                    { 1, "White", "White", "Black", "Black", "White", "Lake Worth", "Comment number 1", "Palm Beach National Golf & Country Club", "", "", "", 18, 36, 36, 72, "(561) 965-3381", "Fl", "7500 St Andrews Rd", "Black", 6734, 3368, 3366, "Blue", 6572, 3287, 3285, "White", 6138, 3072, 3066, "Gold", 5679, 2842, 2837, "Red", 5086, 2638, 2448, "Public", "12345" },
                    { 2, "White", "White", "Black", "Black", "White", "Boyton Beach", "Comment number 2", "The Links at Boyton Beach", "", "", "", 18, 36, 36, 72, "(561) 742-6500", "Fl", "8020 Jog Rd", "Gold", 0, 0, 0, "Blue", 0, 0, 0, "White", 0, 0, 0, "Orange", 0, 0, 0, "Red", 0, 0, 0, "Public", "12345" },
                    { 3, "White", "Black", "Black", "White", "White", "Jamestown", "Comment number 2", "Jamestown Golf Course", "", "", "", 9, 36, 36, 72, "(401) 423-9930", "RI", "245 Conanicus Avenue", "Blue", 0, 0, 0, "White", 0, 0, 0, "Red", 0, 0, 0, "White", 0, 0, 0, "White", 0, 0, 0, "Public", "02835" },
                    { 4, "White", "Black", "Black", "Black", "White", "Portsmouth", "Comment number 2", "Green Valley Country Club", "", "", "", 18, 36, 36, 72, "(401)847-9543", "RI", "371 Union St", "Blue", 0, 0, 0, "White", 0, 0, 0, "Yellow", 0, 0, 0, "Red", 0, 0, 0, "White", 0, 0, 0, "Public", "02871" }
                });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "LId", "ConfirmPassword", "LCity", "LContactEmail", "LContactFName", "LContactLName", "LContactPhoneNumber", "LDescription", "LSeasonEnd", "LSeasonStart", "LState", "LStreet", "LZip", "LeagueName", "Password" },
                values: new object[] { 1, "Kahida01!", "Delray Beach", "willffdunn@gmail.com", "Bill", "Dunn", "484.885.7000", "The Tropic Bay Golf League is a a golf league comprised primarily of golf enthusuiasts of all skill ranges primarily from the TRpoic Bay Condominium community", new DateTime(2024, 4, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), "Fl", "2801 Florida Blvd", "33483", "Tropic Bay Golf", "Kahida01!" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EvId", "EvContactEmail", "EvContactFName", "EvContactLName", "EvContactPhoneNumber", "EvDate", "EvDescription", "EvEndDate", "EvEndTime", "EvFeeAmount", "EvLocation", "EvRegComment", "EvRegEndDate", "EvRegEndTime", "EvRegMethod", "EvRegStartDate", "EvRegStartTime", "EvShortDescription", "EvTime", "EvType", "LId" },
                values: new object[] { 1, "willffdunn@tropicbaygolf.com", "Bill", "Dunn", "(999)999-9999", new DateTime(2023, 12, 3, 8, 0, 0, 0, DateTimeKind.Unspecified), "First Event", new DateTime(2023, 12, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0), "75.00", "TB Club House", "Fee is per person and can be paid by check to TB Golf Club", new DateTime(2023, 12, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0), "Online", new DateTime(2023, 12, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0), "First Event", new TimeSpan(0, 8, 0, 0, 0), "Golf Round", 1 });

            migrationBuilder.InsertData(
                table: "GolfCourseHoles",
                columns: new[] { "GHId", "GCId", "GCName", "GHHandicap", "GHHole", "GHPar", "GHSectName", "GHT1", "GHT2", "GHT3", "GHT4", "GHT5" },
                values: new object[,]
                {
                    { 1, 1, "Palm Beach National Golf & Country Club", 1, 1, 5, "Front", 509, 504, 483, 405, 393 },
                    { 2, 1, "Palm Beach National Golf & Country Club", 3, 2, 4, "Front", 390, 378, 349, 327, 290 },
                    { 3, 1, "Palm Beach Natioanl Golf & Country Club", 17, 3, 3, "Front", 165, 158, 141, 130, 93 },
                    { 4, 1, "Palm Beach Natioanl Golf & Country Club", 5, 4, 4, "Front", 407, 395, 352, 317, 272 },
                    { 5, 1, "Palm Beach Natioanl Golf & Country Club", 7, 5, 4, "Front", 426, 411, 384, 369, 300 },
                    { 6, 1, "Palm Beach Natioanl Golf & Country Club", 11, 6, 4, "Front", 414, 412, 398, 383, 277 },
                    { 7, 1, "Palm Beach Natioanl Golf & Country Club", 15, 7, 5, "Front", 505, 492, 465, 452, 425 },
                    { 8, 1, "Palm Beach Natioanl Golf & Country Club", 13, 8, 3, "Front", 179, 174, 155, 143, 119 },
                    { 9, 1, "Palm Beach Natioanl Golf & Country Club", 9, 9, 4, "Front", 371, 361, 339, 311, 279 },
                    { 10, 1, "Palm Beach Natioanl Golf & Country Club", 10, 10, 4, "Back", 418, 404, 370, 344, 318 },
                    { 11, 1, "Palm Beach Natioanl Golf & Country Club", 16, 11, 3, "Back", 160, 154, 145, 135, 130 },
                    { 12, 1, "Palm Beach Natioanl Golf & Country Club", 12, 12, 4, "Back", 360, 361, 348, 324, 299 },
                    { 13, 1, "Palm Beach Natioanl Golf & Country Club", 2, 13, 4, "Back", 420, 413, 385, 345, 329 },
                    { 14, 1, "Palm Beach Natioanl Golf & Country Club", 14, 14, 5, "Back", 509, 501, 472, 415, 400 },
                    { 15, 1, "Palm Beach Natioanl Golf & Country Club", 8, 15, 4, "Back", 390, 378, 356, 333, 318 },
                    { 16, 1, "Palm Beach Natioanl Golf & Country Club", 18, 16, 4, "Back", 397, 384, 353, 329, 262 },
                    { 17, 1, "Palm Beach Natioanl Golf & Country Club", 4, 17, 4, "Back", 356, 349, 332, 318, 302 },
                    { 18, 1, "Palm Beach Natioanl Golf & Country Club", 6, 18, 4, "Back", 358, 343, 311, 299, 280 },
                    { 37, 3, "Jamestown Golf Course", 1, 1, 4, "Front", 300, 280, 248, 0, 0 },
                    { 38, 3, "Jamestown Golf Course", 3, 2, 5, "Front", 541, 484, 426, 0, 0 },
                    { 39, 3, "Jamestown Golf Course", 17, 3, 4, "Front", 293, 279, 240, 0, 0 },
                    { 40, 3, "Jamestown Golf Course", 5, 4, 4, "Front", 403, 375, 317, 0, 0 },
                    { 41, 3, "Jamestown Golf Course", 7, 5, 3, "Front", 130, 114, 100, 0, 0 },
                    { 42, 3, "Jamestown Golf Course", 11, 6, 5, "Front", 450, 379, 351, 0, 0 },
                    { 43, 3, "Jamestown Golf Course", 15, 7, 3, "Front", 161, 141, 130, 0, 0 },
                    { 44, 3, "Jamestown Golf Course", 13, 8, 4, "Front", 405, 368, 327, 0, 0 },
                    { 45, 3, "Jamestown Golf Course", 9, 9, 4, "Front", 375, 328, 292, 0, 0 },
                    { 46, 4, "Green Valley Country Club", 7, 1, 4, "Front", 370, 361, 345, 331, 0 },
                    { 47, 4, "Green Valley Country Club", 1, 2, 4, "Front", 461, 454, 420, 341, 0 },
                    { 48, 4, "Green Valley Country Club", 13, 3, 4, "Front", 397, 386, 355, 329, 0 },
                    { 49, 4, "Green Valley Country Club", 3, 4, 5, "Front", 550, 541, 514, 474, 0 },
                    { 50, 4, "Green Valley Country Club", 11, 5, 3, "Front", 190, 175, 132, 125, 0 },
                    { 51, 4, "Green Valley Country Club", 9, 6, 4, "Front", 404, 392, 358, 354, 0 },
                    { 52, 4, "Green Valley Country Club", 17, 7, 4, "Front", 360, 354, 335, 314, 0 },
                    { 53, 4, "Green Valley Country Club", 15, 8, 3, "Front", 210, 201, 155, 145, 0 },
                    { 54, 4, "Green Valley Country Club", 5, 9, 4, "Front", 433, 424, 375, 361, 0 },
                    { 55, 4, "Green Valley Country Club", 2, 10, 5, "Back", 613, 605, 576, 466, 0 },
                    { 56, 4, "Green Valley Country Club", 16, 11, 3, "Back", 229, 220, 187, 134, 0 },
                    { 57, 4, "Green Valley Country Club", 18, 12, 3, "Back", 147, 125, 107, 101, 0 },
                    { 58, 4, "Green Valley Country Club", 10, 13, 4, "Back", 342, 327, 304, 252, 0 },
                    { 59, 4, "Green Valley Country Club", 6, 14, 4, "Back", 451, 440, 414, 330, 0 },
                    { 60, 4, "Green Valley Country Club", 9, 15, 4, "Back", 340, 334, 304, 290, 0 },
                    { 61, 4, "Green Valley Country Club", 4, 16, 4, "Back", 407, 394, 360, 336, 0 },
                    { 63, 4, "Green Valley Country Club", 14, 17, 5, "Back", 548, 540, 515, 449, 0 },
                    { 64, 4, "Green Valley Country Club", 8, 18, 4, "Back", 378, 368, 335, 313, 0 },
                    { 65, 2, "The Links at Boyton Beach", 4, 1, 5, "Front", 509, 500, 451, 439, 0 },
                    { 66, 2, "The Links at Boyton Beach", 18, 2, 3, "Front", 156, 140, 139, 123, 0 },
                    { 67, 2, "The Links at Boyton Beach", 2, 3, 4, "Front", 431, 399, 388, 373, 0 },
                    { 68, 2, "The Links at Boyton Beach", 8, 4, 4, "Front", 420, 389, 330, 302, 0 },
                    { 69, 2, "The Links at Boyton Beach", 10, 5, 4, "Front", 388, 338, 255, 250, 0 },
                    { 70, 2, "The Links at Boyton Beach", 12, 6, 4, "Front", 358, 340, 266, 240, 0 },
                    { 71, 2, "The Links at Boyton Beach", 16, 7, 3, "Front", 186, 145, 86, 80, 0 },
                    { 72, 2, "The Links at Boyton Beach", 6, 8, 4, "Front", 307, 298, 258, 250, 0 },
                    { 73, 2, "The Links at Boyton Beach", 14, 9, 4, "Front", 324, 313, 296, 289, 0 },
                    { 74, 2, "The Links at Boyton Beach", 11, 10, 4, "Front", 368, 340, 326, 294, 0 },
                    { 75, 2, "The Links at Boyton Beach", 1, 11, 5, "Front", 515, 478, 455, 391, 0 },
                    { 76, 2, "The Links at Boyton Beach", 3, 12, 4, "Front", 395, 374, 308, 302, 0 },
                    { 77, 2, "The Links at Boyton Beach", 13, 13, 4, "Front", 353, 336, 321, 316, 0 },
                    { 78, 2, "The Links at Boyton Beach", 9, 14, 4, "Front", 329, 309, 226, 216, 0 },
                    { 79, 2, "The Links at Boyton Beach", 15, 15, 3, "Front", 179, 159, 102, 96, 0 },
                    { 80, 2, "The Links at Boyton Beach", 5, 16, 4, "Front", 386, 369, 300, 292, 0 },
                    { 81, 2, "The Links at Boyton Beach", 17, 17, 3, "Front", 178, 158, 127, 80, 0 },
                    { 82, 2, "The Links at Boyton Beach", 7, 18, 5, "Front", 515, 493, 475, 398, 0 }
                });

            migrationBuilder.InsertData(
                table: "GolfRounds",
                columns: new[] { "GRId", "GCId", "GRDate", "GRType", "LId" },
                values: new object[] { 1, 1, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GLW Club, Stroke Play", 1 });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "MemberId", "Email", "EmailConfirmed", "FirstName", "FullName", "Handicap", "LId", "LastName", "MemberStatus", "MemberTee", "MemberType", "PhoneNumber", "PreferredNotification", "Registered" },
                values: new object[,]
                {
                    { 1, "willffdunn@gmail.com", false, "William", "William Dunn", 9, 1, "Dunn", "Active", "White", "TBRes", "484-885-7000", "Both", false },
                    { 2, "karenfdunn@gmail.com", false, "Karen", "Karen Dunn", 19, 1, "Dunn", "Active", "Red", "TBRes", "610-733-38380", "Both", false }
                });

            migrationBuilder.InsertData(
                table: "EvParticipants",
                columns: new[] { "EPId", "EPEmail", "EPFName", "EPLName", "EPPhoneNumber", "EPType", "EvId", "LId", "MemberId" },
                values: new object[] { 1, "willffdunn@tropicbaygolf.com", "Bill", "Dunn", "(999)999-9999", "Sponsor", 1, 0, 1 });

            migrationBuilder.InsertData(
                table: "GolfGroups",
                columns: new[] { "GGId", "GGName", "GGTtime", "GRId" },
                values: new object[] { 1, "Group 1", new TimeSpan(0, 6, 54, 0, 0), 1 });

            migrationBuilder.InsertData(
                table: "GolfPlayerScores",
                columns: new[] { "GPSId", "GHId", "GPId", "GPSFW", "GPSGIY", "GPSPutts", "GPSStrokes" },
                values: new object[,]
                {
                    { 1, 1, 1, "Yes", "Yes", 2, 4 },
                    { 2, 2, 1, "Yes", "Yes", 2, 4 },
                    { 3, 3, 1, "Yes", "Yes", 2, 4 },
                    { 4, 4, 1, "Yes", "Yes", 2, 4 },
                    { 5, 5, 1, "Yes", "Yes", 2, 4 },
                    { 6, 6, 1, "Yes", "Yes", 2, 4 },
                    { 7, 7, 1, "Yes", "Yes", 2, 4 },
                    { 8, 8, 1, "Yes", "Yes", 2, 4 },
                    { 9, 9, 1, "Yes", "Yes", 2, 4 },
                    { 10, 10, 1, "Yes", "Yes", 2, 4 },
                    { 11, 11, 1, "Yes", "Yes", 2, 4 },
                    { 12, 12, 1, "Yes", "Yes", 2, 4 },
                    { 13, 13, 1, "Yes", "Yes", 2, 4 },
                    { 14, 14, 1, "Yes", "Yes", 2, 4 },
                    { 15, 15, 1, "Yes", "Yes", 2, 4 },
                    { 16, 16, 1, "Yes", "Yes", 2, 4 },
                    { 17, 17, 1, "Yes", "Yes", 2, 4 },
                    { 18, 18, 1, "Yes", "Yes", 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "GolfPlayers",
                columns: new[] { "GPId", "FTot1", "FTot2", "FTot3", "GGId", "GPFName", "GPLName", "GPType", "GTot1", "GTot2", "GTot3", "MemberId", "PTot1", "PTot2", "PTot3", "Tot1", "Tot2", "Tot3" },
                values: new object[] { 1, 0, 0, 0, 1, "William", "Dunn", "Member", 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Events_LId",
                table: "Events",
                column: "LId");

            migrationBuilder.CreateIndex(
                name: "IX_EvParticipants_EvId",
                table: "EvParticipants",
                column: "EvId");

            migrationBuilder.CreateIndex(
                name: "IX_GolfCourseHoles_GCId",
                table: "GolfCourseHoles",
                column: "GCId");

            migrationBuilder.CreateIndex(
                name: "IX_GolfGroups_GRId",
                table: "GolfGroups",
                column: "GRId");

            migrationBuilder.CreateIndex(
                name: "IX_GolfPlayers_GGId",
                table: "GolfPlayers",
                column: "GGId");

            migrationBuilder.CreateIndex(
                name: "IX_GolfPlayers_MemberId",
                table: "GolfPlayers",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_GolfPlayerScores_GHId",
                table: "GolfPlayerScores",
                column: "GHId");

            migrationBuilder.CreateIndex(
                name: "IX_GolfRounds_GCId",
                table: "GolfRounds",
                column: "GCId");

            migrationBuilder.CreateIndex(
                name: "IX_GolfRounds_LId",
                table: "GolfRounds",
                column: "LId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_LId",
                table: "Members",
                column: "LId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EvParticipants");

            migrationBuilder.DropTable(
                name: "GolfPlayers");

            migrationBuilder.DropTable(
                name: "GolfPlayerScores");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "GolfGroups");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "GolfCourseHoles");

            migrationBuilder.DropTable(
                name: "GolfRounds");

            migrationBuilder.DropTable(
                name: "GolfCourses");

            migrationBuilder.DropTable(
                name: "Leagues");
        }
    }
}
