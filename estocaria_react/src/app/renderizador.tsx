"use client";
import { LayoutAdmin, LayoutBase } from "@/components/Layouts";
import { Footer, SideBar, SideBarRight } from "@/components/Screen";
import Link from "next/link";
import { usePathname } from "next/navigation";
import { ReactNode } from "react";

interface IRenderizador { children: ReactNode }
const Renderizador: React.FC<IRenderizador> = ({ children }) => {
    const path = usePathname();

    const handleRenderLayout = () => {
        console.log(path);
        if (path.includes("/admin")) {
            return (
                <LayoutAdmin>
                    {children}
                    <SideBar />
                    <Footer tituloPrimary="Parcerias" tituloSecondary="Contatos" tituloTertiary="Outros">
                        <Link className="link" href="">alguma coisa</Link>
                        <Link className="link" href="">alguma coisa</Link>
                    </Footer>
                </LayoutAdmin>
            );
        } else {
            return (
                <LayoutBase>
                    {children}
                    <SideBarRight />
                    <Footer tituloPrimary="Parcerias" tituloSecondary="Contatos" tituloTertiary="Outros">
                        <Link className="link" href="">alguma coisa</Link>
                        <Link className="link" href="">alguma coisa</Link>
                    </Footer>
                </LayoutBase>
            );
        }
    }
    return (<>
        {handleRenderLayout()}
    </>);
}
export default Renderizador;