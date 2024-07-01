export interface IUsuario {
    id: number;
    nome: string;
    email: string;
    password: string;
    status: string;
    dataNascimento: Date;
    nNoticiasCadastradas: number;
    nHorariosAceitos: number;
    nHorariosRegeitados: number;
    tipoUsuario: string;
    noticias: Array<INoticia>;
    horarios: IHorario[];
  }
  
  export interface INoticia {
    id: number;
    titulo: string;
    descricao: string;
    nomeAutor: string;
    admin: IUsuario;
  }
  
  export interface IHorario {
    id: number;
    dia: string;
    horario: string;
    turno: string;
    nomeEquipe: string;
    responsavelEquipe: string;
    usuario: IUsuario;
    mural: IMural;
    createdAt:Date;
    updatedAt:Date;
  }
  
  export interface IMural {
    id: number;
    nome: string;
    status: string;
    horarios: IHorario[];
  }
  