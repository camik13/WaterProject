using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterProject.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Projects",
            //    columns: table => new
            //    {
            //        ProjectId = table.Column<int>(nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        ProjectName = table.Column<string>(nullable: true),
            //        ProjectType = table.Column<string>(nullable: true),
            //        ProjectRegionalProgram = table.Column<string>(nullable: true),
            //        ProjectImpact = table.Column<int>(nullable: false),
            //        ProjectPhase = table.Column<string>(nullable: true),
            //        ProjectFunctionalityStatus = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Projects", x => x.ProjectId);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
