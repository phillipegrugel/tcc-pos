import { PacienteModel } from "./paciente.model";
import { ProfissionalModel } from "./profissional.module";

export interface ConsultaModel {
    id: number,
    paciente: PacienteModel,
    medico: ProfissionalModel,
    data: Date,
    horario: HorarioModel,
    nomemedicolist: string,
    nomepacientelist: string,
    horariolist: string
}

export interface HorarioModel {
    value: number,
    label: string
}