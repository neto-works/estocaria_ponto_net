"use client";
import { LayoutAdmin, LayoutBase } from "@/components/Layouts";
import { Footer, SideBar, SideBarRight } from "@/components/Screen";
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
                    <Footer />
                </LayoutAdmin>
            );
        } else {
            return (
                <LayoutBase>
                    {children}
                    <SideBarRight />
                    <Footer />
                </LayoutBase>
            );
        }
    }
    return (<>
        {handleRenderLayout()}
    </>);
}
export default Renderizador;