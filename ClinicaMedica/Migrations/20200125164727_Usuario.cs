using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicaMedica.Migrations
{
    public partial class Usuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Profissionais",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Pessoas",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Excluido = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profissionais_UsuarioId",
                table: "Profissionais",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profissionais_Usuarios_UsuarioId",
                table: "Profissionais",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profissionais_Usuarios_UsuarioId",
                table: "Profissionais");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Profissionais_UsuarioId",
                table: "Profissionais");

            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Profissionais");

            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Pessoas");
        }
    }
}
