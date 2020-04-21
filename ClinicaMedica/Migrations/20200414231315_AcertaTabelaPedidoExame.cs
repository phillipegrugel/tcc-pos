using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicaMedica.Migrations
{
    public partial class AcertaTabelaPedidoExame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidosExames_Pacientes_PacienteId",
                table: "PedidosExames");

            migrationBuilder.DropIndex(
                name: "IX_Receitas_HistoricoClinicoId",
                table: "Receitas");

            migrationBuilder.DropIndex(
                name: "IX_PedidosExames_PacienteId",
                table: "PedidosExames");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "PedidosExames");

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_HistoricoClinicoId",
                table: "Receitas",
                column: "HistoricoClinicoId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Receitas_HistoricoClinicoId",
                table: "Receitas");

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "PedidosExames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_HistoricoClinicoId",
                table: "Receitas",
                column: "HistoricoClinicoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosExames_PacienteId",
                table: "PedidosExames",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosExames_Pacientes_PacienteId",
                table: "PedidosExames",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
