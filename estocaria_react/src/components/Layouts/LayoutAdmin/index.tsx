import { ReactNode } from "react";
import Styles from "./layoutadnin.module.css";

interface LayoutAdminProps {
    children: Array<React.ReactNode>;
}

const LayoutAdmin: React.FC<LayoutAdminProps> = ({ children }): ReactNode => {
    const [ contentChildren, sidebarChildren, footerChildren] = children;
    return (
        <div className="container-fluid m-0 p-0 h-100">

            <div className="row m-0 p-0 h-auto">
                <div className="col-md-4 col-lg-1 p-0 d-flex flex-row-reverse">{sidebarChildren}</div>
                <div className="col-md-6 col-lg-11 p-5">{contentChildren}</div>
            </div>

            <div className="row m-0 p-0 h-50">
                <div className="col-md-4 col-lg-1 p-0 d-flex flex-row-reverse">{sidebarChildren}</div>
                <div className="col-md-6 col-lg-11">{footerChildren}</div>
            </div>

        </div>
    );
};

export default LayoutAdmin;