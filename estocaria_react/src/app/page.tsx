import { NavLink } from "@/components/Screen";
import Link from "next/link";

export default function Home() {
  return (
    <div className={`container-fluid m-0 bg-white w-100 h-100 p-3 d-flex flex-column align-items-center round-side`}>
      <NavLink>
        <Link href={"/"}>Home</Link>
        <Link href={"/admin"}>Admin</Link>
        <Link href={"/signin"}>Login</Link>
      </NavLink>

      <h3 className="py-3">Assunto secundario </h3>

      <h1 className="py-4">Assunto principal </h1>
      <p className="py-2">Lorem ipsum dolor sit amet, consectetur adipisicing elit. Doloremque neque aut cumque eveniet iure error repellat consectetur.
        Dolorem dicta commodi autem repellat quisquam necessitatibus, nam fuga impedit debitis aut aperiam?
      </p>

      <p className="py-2">Lorem ipsum dolor sit amet, consectetur adipisicing elit. Doloremque neque aut cumque eveniet iure error repellat consectetur.
        Dolorem dicta commodi autem repellat quisquam necessitatibus, nam fuga impedit debitis aut aperiam?
      </p>
      <p>imagem</p>
    </div>
  );
}
