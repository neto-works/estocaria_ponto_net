import React, { ReactNode } from 'react';
import Styles from "./navlink.module.css";

interface INavLinkProps {
    children: ReactNode[];
}

const NavLink: React.FC<INavLinkProps> = ({ children }) => {
    return (
        <div className={`${Styles.bodyContainer}`}>
            <ul className={`${Styles.ul}`}>{children.map((childElement, index) => (<li className={`${Styles.li}`} key={index}>{childElement}</li>))}</ul>
        </div>
    )
}

export default NavLink;