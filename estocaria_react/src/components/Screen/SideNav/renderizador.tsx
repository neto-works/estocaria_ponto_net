"use client";
import { usePathname } from "next/navigation";
import SideNav from ".";

const Renderizador: React.FC = () => {
  const rota = usePathname();
  return <>{rota === "/manager" ? null : <SideNav />}</>;
};
export default Renderizador;
