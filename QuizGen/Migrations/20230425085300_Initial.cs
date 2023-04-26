using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizGen.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OptionA = table.Column<string>(nullable: false),
                    OptionB = table.Column<string>(nullable: false),
                    OptionC = table.Column<string>(nullable: false),
                    OptionD = table.Column<string>(nullable: false),
                    CorrectOption = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}
