import Link from "next/link";
import Styles from "./sidebarright.module.css";

const SideNav: React.FC = () => {
  //     var isMouseDown, initX, initY, height = draggable.offsetHeight, width = draggable.offsetWidth;

  //     draggable.addEventListener('mousedown', function (e) {
  //         isMouseDown = true;
  //         document.body.classList.add('no-select');
  //         initX = e.offsetX + 10;
  //         initY = e.offsetY + 10;
  //     })

  //     document.addEventListener('mousemove', function (e) {
  //         if (isMouseDown) {
  //             var cx = e.clientX - initX,
  //                 cy = e.clientY - initY;
  //             if (cx < 0) {
  //                 cx = 0;
  //             }
  //             if (cy < 0) {
  //                 cy = 0;
  //             }
  //             if (window.innerWidth - e.clientX + initX < width) {
  //                 cx = window.innerWidth - width;
  //             }
  //             if (e.clientY > window.innerHeight - height + initY) {
  //                 cy = window.innerHeight - height;
  //             }
  //             draggable.style.left = cx + 'px';
  //             draggable.style.top = cy + 'px';
  //         }
  //     })

  //     draggable.addEventListener('mouseup', function () {
  //         isMouseDown = false;
  //         document.body.classList.remove('no-select');
  //     })

  return (
    <div id={Styles.draggable} className="no-select p-3 shadow my-4">
      <div id={Styles.mydraggable}>
        <svg
          xmlns="http://www.w3.org/2000/svg"
          width={16}
          fill="currentColor"
          viewBox="0 0 16 16"
        >
          <path
            fillRule="evenodd"
            d="M7.646.146a.5.5 0 0 1 .708 0l2 2a.5.5 0 0 1-.708.708L8.5 1.707V5.5a.5.5 0 0 1-1 0V1.707L6.354 2.854a.5.5 0 1 1-.708-.708l2-2zM8 10a.5.5 0 0 1 .5.5v3.793l1.146-1.147a.5.5 0 0 1 .708.708l-2 2a.5.5 0 0 1-.708 0l-2-2a.5.5 0 0 1 .708-.708L7.5 14.293V10.5A.5.5 0 0 1 8 10zM.146 8.354a.5.5 0 0 1 0-.708l2-2a.5.5 0 1 1 .708.708L1.707 7.5H5.5a.5.5 0 0 1 0 1H1.707l1.147 1.146a.5.5 0 0 1-.708.708l-2-2zM10 8a.5.5 0 0 1 .5-.5h3.793l-1.147-1.146a.5.5 0 0 1 .708-.708l2 2a.5.5 0 0 1 0 .708l-2 2a.5.5 0 0 1-.708-.708L14.293 8.5H10.5A.5.5 0 0 1 10 8z"
          />
        </svg>
      </div>

      {/* start: menu link */}
      <ul className="list-unstyled mb-0 d-flex flex-column align-items-center pt-3 mt-3 border-top">

        <li className="m-2">
          <Link className="p-2" href="/noticias">
            <i className={`bi bi-newspaper ${Styles.wh20}`} />
          </Link>
        </li>

        <li className="m-2">
          <Link className="p-2" href="/mural">
            <i className={`bi bi-calendar-week ${Styles.wh20}`} />
          </Link>
        </li>

        <li className="m-2">
          <a className="p-2" href="#">
            <i className={`bi bi-app-indicator ${Styles.wh20}`} />
          </a>
        </li>

        <li className="m-2">
          <a className="p-2" href="https://github.com/oficina-do-brito/ginasio" target="_blank">
            <i className={`bi bi-shield ${Styles.wh20}`} />
          </a>
        </li>

        <li className="m-2">
          <a className="p-2" href="https://github.com/oficina-do-brito/ginasio" target="_blank">
            <i className={`bi bi-xbox ${Styles.wh20}`} />
          </a>
        </li>

        <li className="mb-auto">
          <div className="dropdown">
            <a className="p-2 dropdown-toggle"  href="#" type="button"  data-bs-toggle="dropdown" aria-expanded="false" >
             <i className={`bi  bi-list-ul ${Styles.wh20}`}/>
            </a>
            <ul className="dropdown-menu p-2 shadow border-0">
              <li>
                <a className="dropdown-item" href="https://github.com/oficina-do-brito/ginasio"  target="_blank" >
                  UI UX Design
                </a>
              </li>
              <li>
                <a className="dropdown-item"  href="https://github.com/oficina-do-brito/ginasio" target="_blank" >
                  Web Development
                </a>
              </li>
              <li>
                <a  className="dropdown-item" href="https://github.com/oficina-do-brito/ginasio"  target="_blank">
                  Mobile Development
                </a>
              </li>
            </ul>
          </div>
        </li>
        <li>
          <a
            className="p-2"
            href="https://github.com/oficina-do-brito/ginasio"
            target="_blank"
          >
           <i className={`bi bi-whatsapp ${Styles.wh20}`} />
          </a>
        </li>
      </ul>
    </div>
  );
};
export default SideNav;
