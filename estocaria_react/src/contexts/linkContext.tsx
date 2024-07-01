"use client";
import { createContext, useState } from "react";
import { IcontextProvider, IpropsLink, LinkNext } from "./interfaces/link";

export const ContextLinkNext = createContext({} as IcontextProvider);

export default function LinkContext({ children }: IpropsLink) {
  const [linkNext, setlinkNext] = useState<LinkNext>();

  return (
    <>
      <ContextLinkNext.Provider
        value={{ linkNext, setlinkNext } as unknown as IcontextProvider}
      >
        {children}
      </ContextLinkNext.Provider>
    </>
  );
}