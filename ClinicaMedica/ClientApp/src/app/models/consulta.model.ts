import { PacienteModel } from "./paciente.model";
import { ProfissionalModel } from "./profissional.module";
import { RemedioModel } from "./remedio.model";

export interface ConsultaModel {
    id: number,
    paciente: PacienteModel,
    medico: ProfissionalModel,
    data: Date,
    horario: HorarioModel,
    nomemedicolist: string,
    nomepacientelist: string,
    horariolist: string,
    historicoClinico: any
}

export interface HorarioModel {
    value: number,
    label: string
}

export interface HistoricoClinicoModel {
    id: number,
    consulta: ConsultaModel,
    exames: Array<PedidoExameModel>,
    receita: ReceitaModel,
    obseracao: string
}

export interface ExameModel {
    id: number,
    nome: string
}

export interface PedidoExameModel {
    id: number,
    resultado: string,
    entreguePaciente: boolean,
    exame: ExameModel,
    nomepacientelist: string,
    nomeexamelist: string,
    historicoClinico: any
}

export interface RemedioReceitaModel {
    id: number,
    receita: ReceitaModel,
    remedio: RemedioModel,
    observacoes: string
}

export interface ReceitaModel {
    id: number,
    remedios: Array<RemedioReceitaModel>,
    observacoes: string
}