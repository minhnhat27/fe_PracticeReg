using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DangKyPhongThucHanhCNTT.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditHours = table.Column<int>(type: "int", nullable: false),
                    NumberOfPractice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CPUs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HardDrives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardDrives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lectures",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NumberSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RAMs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SemesterStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentSemster = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Softwares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Softwares", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Times", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weeks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseGroups",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumberOfStudents = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseGroups_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hardwares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPUId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RAMId = table.Column<int>(type: "int", nullable: true),
                    HardDriveId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hardwares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hardwares_CPUs_CPUId",
                        column: x => x.CPUId,
                        principalTable: "CPUs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hardwares_HardDrives_HardDriveId",
                        column: x => x.HardDriveId,
                        principalTable: "HardDrives",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hardwares_RAMs_RAMId",
                        column: x => x.RAMId,
                        principalTable: "RAMs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Teachings",
                columns: table => new
                {
                    CourseGroupId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumberOfSessionId = table.Column<int>(type: "int", nullable: false),
                    LecturerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SemesterId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachings", x => new { x.CourseGroupId, x.NumberOfSessionId, x.LecturerId, x.SemesterId });
                    table.ForeignKey(
                        name: "FK_Teachings_CourseGroups_CourseGroupId",
                        column: x => x.CourseGroupId,
                        principalTable: "CourseGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teachings_Lectures_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teachings_NumberSessions_NumberOfSessionId",
                        column: x => x.NumberOfSessionId,
                        principalTable: "NumberSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teachings_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstallSoftwares",
                columns: table => new
                {
                    HardwareId = table.Column<int>(type: "int", nullable: false),
                    SoftwareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallSoftwares", x => new { x.HardwareId, x.SoftwareId });
                    table.ForeignKey(
                        name: "FK_InstallSoftwares_Hardwares_HardwareId",
                        column: x => x.HardwareId,
                        principalTable: "Hardwares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstallSoftwares_Softwares_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Softwares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NumberOfComputers = table.Column<int>(type: "int", nullable: false),
                    HardwareId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Hardwares_HardwareId",
                        column: x => x.HardwareId,
                        principalTable: "Hardwares",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Day = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    WeekId = table.Column<int>(type: "int", nullable: true),
                    TeachingCourseGroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TeachingNumberOfSessionId = table.Column<int>(type: "int", nullable: true),
                    TeachingLecturerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TeachingSemesterId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => new { x.Day, x.TimeId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_Schedules_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Teachings_TeachingCourseGroupId_TeachingNumberOfSessionId_TeachingLecturerId_TeachingSemesterId",
                        columns: x => new { x.TeachingCourseGroupId, x.TeachingNumberOfSessionId, x.TeachingLecturerId, x.TeachingSemesterId },
                        principalTable: "Teachings",
                        principalColumns: new[] { "CourseGroupId", "NumberOfSessionId", "LecturerId", "SemesterId" });
                    table.ForeignKey(
                        name: "FK_Schedules_Times_TimeId",
                        column: x => x.TimeId,
                        principalTable: "Times",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Weeks_WeekId",
                        column: x => x.WeekId,
                        principalTable: "Weeks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SuitableCourses",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuitableCourses", x => new { x.RoomId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_SuitableCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuitableCourses_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseGroups_CourseId",
                table: "CourseGroups",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Hardwares_CPUId",
                table: "Hardwares",
                column: "CPUId");

            migrationBuilder.CreateIndex(
                name: "IX_Hardwares_HardDriveId",
                table: "Hardwares",
                column: "HardDriveId");

            migrationBuilder.CreateIndex(
                name: "IX_Hardwares_RAMId",
                table: "Hardwares",
                column: "RAMId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallSoftwares_SoftwareId",
                table: "InstallSoftwares",
                column: "SoftwareId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HardwareId",
                table: "Rooms",
                column: "HardwareId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_RoomId",
                table: "Schedules",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TeachingCourseGroupId_TeachingNumberOfSessionId_TeachingLecturerId_TeachingSemesterId",
                table: "Schedules",
                columns: new[] { "TeachingCourseGroupId", "TeachingNumberOfSessionId", "TeachingLecturerId", "TeachingSemesterId" });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TimeId",
                table: "Schedules",
                column: "TimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_WeekId",
                table: "Schedules",
                column: "WeekId");

            migrationBuilder.CreateIndex(
                name: "IX_SuitableCourses_CourseId",
                table: "SuitableCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachings_LecturerId",
                table: "Teachings",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachings_NumberOfSessionId",
                table: "Teachings",
                column: "NumberOfSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachings_SemesterId",
                table: "Teachings",
                column: "SemesterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstallSoftwares");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "SuitableCourses");

            migrationBuilder.DropTable(
                name: "Softwares");

            migrationBuilder.DropTable(
                name: "Teachings");

            migrationBuilder.DropTable(
                name: "Times");

            migrationBuilder.DropTable(
                name: "Weeks");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "CourseGroups");

            migrationBuilder.DropTable(
                name: "Lectures");

            migrationBuilder.DropTable(
                name: "NumberSessions");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "Hardwares");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "CPUs");

            migrationBuilder.DropTable(
                name: "HardDrives");

            migrationBuilder.DropTable(
                name: "RAMs");
        }
    }
}
