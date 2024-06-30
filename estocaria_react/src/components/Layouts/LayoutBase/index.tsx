import { ReactNode } from "react";
// import Styles from "./layout.module.css";

interface LayoutBaseProps {
    children: Array<React.ReactNode>;
}

const LayoutBase: React.FC<LayoutBaseProps> = ({ children }): ReactNode => {
    const [contentChildren, sidebarChildren, footerChildren] = children;
    return (
        <div className="container-fluid m-0 p-0 h-100">

            <div className="row m-0 p-0 h-auto">
                <div className={`col-md-11 col-lg-11 p-0`}>{contentChildren}</div>
                <div className="col-md-1 col-lg-1 p-0 d-flex flex-row-reverse">{sidebarChildren}</div>
            </div>

            <div className="row m-0 p-0 h-25">
                <div className="col-md-11 col-lg-11 p-0">{footerChildren}</div>
                <div className="col-md-1 col-lg-1 p-0 d-flex flex-row-reverse">{sidebarChildren}</div>
            </div>

        </div>
    );
};

export default LayoutBase;