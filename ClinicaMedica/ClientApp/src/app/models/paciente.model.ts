export interface PacienteModel {
    id: number;
    nome: string;
    cpf: string;
    dataNascimento: Date;
    email: string;
    telefone: string;
    possuiConvenio: boolean;
    numeroCarteirinha: string;
    nomeConvenio: string;
}