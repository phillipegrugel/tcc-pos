using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicaMedica.Migrations
{
    public partial class Historico_Clinico3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoricosClinicos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsultaId = table.Column<int>(nullable: false),
                    Observacoes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosClinicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricosClinicos_Consultas_ConsultaId",
                        column: x => x.ConsultaId,
                        principalTable: "Consultas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidosExames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoricoClinicoId = table.Column<int>(nullable: false),
                    ExameId = table.Column<int>(nullable: false),
                    PacienteId = table.Column<int>(nullable: false),
                    Resultado = table.Column<string>(nullable: true),
                    EntreguePaciente = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosExames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidosExames_Exames_ExameId",
                        column: x => x.ExameId,
                        principalTable: "Exames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidosExames_HistoricosClinicos_HistoricoClinicoId",
                        column: x => x.HistoricoClinicoId,
                        principalTable: "HistoricosClinicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidosExames_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Receitas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoricoClinicoId = table.Column<int>(nullable: false),
                    Observacoes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receitas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receitas_HistoricosClinicos_HistoricoClinicoId",
                        column: x => x.HistoricoClinicoId,
                        principalTable: "HistoricosClinicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RemedioReceitas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceitaId = table.Column<int>(nullable: false),
                    RemedioId = table.Column<int>(nullable: false),
                    Observacoes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemedioReceitas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemedioReceitas_Receitas_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receitas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RemedioReceitas_Remedios_RemedioId",
                        column: x => x.RemedioId,
                        principalTable: "Remedios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosClinicos_ConsultaId",
                table: "HistoricosClinicos",
                column: "ConsultaId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosExames_ExameId",
                table: "PedidosExames",
                column: "ExameId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosExames_HistoricoClinicoId",
                table: "PedidosExames",
                column: "HistoricoClinicoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosExames_PacienteId",
                table: "PedidosExames",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_HistoricoClinicoId",
                table: "Receitas",
                column: "HistoricoClinicoId");

            migrationBuilder.CreateIndex(
                name: "IX_RemedioReceitas_ReceitaId",
                table: "RemedioReceitas",
                column: "ReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_RemedioReceitas_RemedioId",
                table: "RemedioReceitas",
                column: "RemedioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidosExames");

            migrationBuilder.DropTable(
                name: "RemedioReceitas");

            migrationBuilder.DropTable(
                name: "Exames");

            migrationBuilder.DropTable(
                name: "Receitas");

            migrationBuilder.DropTable(
                name: "HistoricosClinicos");
        }
    }
}
