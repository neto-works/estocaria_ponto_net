import { ReactNode } from "react";

export interface LinkNext {
  linkNext: string;
}
export interface IpropsLink {
  children: ReactNode;
}
export interface IcontextProvider {
  linkNext: string;
  setlinkNext: (linkNext: string) => void;
}
