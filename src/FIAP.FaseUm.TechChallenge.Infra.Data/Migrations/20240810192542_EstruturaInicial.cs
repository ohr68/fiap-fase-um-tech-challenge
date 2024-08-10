using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FIAP.FaseUm.TechChallenge.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class EstruturaInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contatos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    NumeroTelefone = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    DddTelefone = table.Column<string>(type: "VARCHAR(5)", nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contatos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contatos");
        }
    }
}
