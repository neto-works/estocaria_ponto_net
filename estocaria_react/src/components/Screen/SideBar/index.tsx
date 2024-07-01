import { MouseEventHandler, useContext } from "react";
import styles from "./styles.module.css";
import { ContextLinkNext } from "@/contexts/linkContext";
import Image from "next/image";

interface IsideBarProps {
  nomeUsuario?: string;
}

export default function SideBar({ nomeUsuario }: IsideBarProps) {
  const contextLink = useContext(ContextLinkNext);

  return (
    <>
      <div
        className={`col-sm-12 col-md-3 col-2 p-0 h-auto my-5 mx-0  ${styles.sidebarNav}`}
      >
        <div className={`${styles.sidebar}`}>
          <ul>
            <li className={`${styles.logo}`}>
              <a onClick={()=>{contextLink.setlinkNext("/home");}}>
                <span className={`${styles.icon}`}>
                  <i className={"bi bi-clipboard-data"} />
                </span>
                <span className={`${styles.text}`}>Manage</span>
              </a>
            </li>
          </ul>
          <ul>
            <li>
              <a onClick={()=>{contextLink.setlinkNext("/home");}}>
                <span className={`${styles.icon}`}>
                  <i className={"bi bi-house"} />
                </span>
                <span className={`${styles.text}`}>Home</span>
              </a>
            </li>
            <li>
              <a onClick={()=>{contextLink.setlinkNext("/home");}}>
                <span className={`${styles.icon}`}>
                  <i className={"bi bi-person"} />
                </span>
                <span className={`${styles.text}`}>Profile</span>
              </a>
            </li>
            <li>
              <a  onClick={()=>{contextLink.setlinkNext("/novohorario");}}>
                <span className={`${styles.icon}`}>
                  <i className={"bi bi-calendar-plus"} />
                </span>
                <span className={`${styles.text}`}>Novo Horario</span>
              </a>
            </li>
            <li>
              <a onClick={()=>{contextLink.setlinkNext("/home");}}>
                <span className={`${styles.icon}`}>
                  <i className={`bi bi-chat-dots`} />
                </span>
                <span className={`${styles.text}`}>Inbox</span>
              </a>
            </li>
            <li>
              <a onClick={()=>{contextLink.setlinkNext("/home");}}>
                <span className={`${styles.icon}`}>
                  <i className={`bi bi-bar-chart-fill`} />
                </span>
                <span className={`${styles.text}`}>Analise</span>
              </a>
            </li>
            <li>
              <a onClick={()=>{contextLink.setlinkNext("/noticias");}}>
                <span className={`${styles.icon}`}>
                  <i className={`bi bi-newspaper`} />
                </span>
                <span className={`${styles.text}`}>Noticias</span>
              </a>
            </li>
            <li>
              <a onClick={()=>{contextLink.setlinkNext("/home");}}>
                <span className={`${styles.icon}`}>
                  <i className={`bi bi-gear`} />
                </span>
                <span className={`${styles.text}`}>Settings</span>
              </a>
            </li>
          </ul>
          <ul>
            <div className={`${styles.bottom}`}>
              <li id={`${styles.nohover}`}>
                <a onClick={()=>{contextLink.setlinkNext("/home");}}>
                  <span className={`${styles.icon}`}>
                    <div className={`${styles.avatar}`}>
                      <Image src="/assets/images/logo.png" width={70} height={70} alt="" />
                    </div>
                  </span>
                  <span className={`${styles.text}`}>
                    {nomeUsuario ? nomeUsuario : "Usuario X"} : ON
                  </span>
                </a>
              </li>
            </div>
          </ul>
        </div>
      </div>
    </>
  );
}
