using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicaMedica.Migrations
{
    public partial class TipoProfissional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Profissionais",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Profissionais");
        }
    }
}
