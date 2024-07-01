import { ReactNode } from "react";
import Styles from "./layout.module.css";

interface LayoutBaseProps {
    children: Array<React.ReactNode>;
}

const LayoutBase: React.FC<LayoutBaseProps> = ({ children }): ReactNode => {
    const [contentChildren, sidebarChildren, footerChildren] = children;
    return (
        <div className={`container-fluid m-0 p-0 h-auto ${Styles.layotBase}`}>
            <main>{contentChildren}</main><aside>{sidebarChildren}</aside>
            <footer>{footerChildren}</footer>
        </div>
    );
};

export default LayoutBase;