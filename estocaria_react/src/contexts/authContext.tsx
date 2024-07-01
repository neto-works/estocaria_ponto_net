"use client";
import { createContext, useState } from "react";
import axios from "axios";
import { setCookie } from "nookies";
import {
  ContextProvider,
  IpropsAuthContext,
  UsuarioAuthenticado,
} from "./interfaces/utils";

export const ContextAuth = createContext({} as ContextProvider);

export default function AuthContext({ children }: IpropsAuthContext) {
  const [usuarioAuthenticado, setUsuarioAutenticado] =
    useState<UsuarioAuthenticado>();

  /**
   * @description Essa função para logar o usuario,
   * > * ela faz a chamada ao backend via axios pelo email e senha
   *
   * > * se o usuario existir, é recebido um objeto json com 1 token de acesso e permisao e 1 objeto de usuario que logou
   *
   * > * é colocado  o token  nos cookies então
   *
   * > * é colocado o usuario logado no estado do contexto
   * @returns Uma promessa contendo true se usuario logado ou false não conseguiu logar
   */
  async function singIn(emailLogin: string, passwordLogin: string): Promise<boolean> {
    const options = {
      method: "POST",
      url: "http://localhost:3000/auth/login",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      data: {
        email: emailLogin,
        password: passwordLogin,
      },
    };

    const response = await axios(options)
      .then((response) => response.data)
      .catch(function (error) {
        console.error(error);
      });

    setCookie(undefined, "app_ginasio_token", response.token, {
      maxAge: 60 * 60 * 2, // 2h token expire
    });
    await setUsuarioAutenticado({ ...response.userAutenticado } as UsuarioAuthenticado);
    if (!response) {
      return false;
    }
    return true;
  }

  return (
    <>
      <ContextAuth.Provider
        value={
          {
            usuarioAuthenticado,
            setUsuarioAutenticado,
            singIn,
          } as unknown as ContextProvider
        }
      >
        {children}
      </ContextAuth.Provider>
    </>
  );
}
