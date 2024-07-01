import { ReactNode } from "react";

// tipar as props do contexto
export interface IpropsAuthContext {
  children: ReactNode;
}

export interface UsuarioAuthenticado {
  id: number;
  nome: string;
  email: string;
  status: string;
  password: string;
  dataNascimento: Date;
  tipoUsuario: string;
}

export type ContextProvider = {
  usuarioAuthenticado: UsuarioAuthenticado;
  setUsuarioAutenticado: (usuarioAuthenticado: UsuarioAuthenticado) => void;
  singIn: (email: string, password: string) => Promise<boolean>;
};

export interface IUserComplete {
  id: number;
}
