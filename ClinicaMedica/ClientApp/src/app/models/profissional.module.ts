import { TipoProfissional } from "./tipo.profissional.enum";
import { UsuarioModel } from "./usuario.model";

export interface ProfissionalModel {
    id: number;
    nome: string;
    cpf: string;
    numeroCarteiraTrabalho: string;
    crm: string;
    tipoString: string;
    tipo: TipoProfissional
    dataNascimento: Date;
    email: string;
    telefone: string;
    usuario: UsuarioModel;
}