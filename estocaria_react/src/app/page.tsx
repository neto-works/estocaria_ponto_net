import { NavLink } from "@/components/Screen";
import Image from 'next/image';
import Link from "next/link";

export default function Home() {
  return (
    <div className={`container-fluid m-0 bg-white w-100 h-100 p-3 d-flex flex-column align-items-center round-side`}>
      <NavLink>
        <Link href={"/"} className="link"> Home</Link>
        <Link href={"/admin"} className="link"> Admin</Link>
        <Link href={"/signin"} className="link"> Login</Link>
      </NavLink>

      <h6 className={`py-5 lightText`}>Ja pensou em estocar seus produtos de forma inteligente?</h6>

      <h1 className="py-4">Assunto principal </h1>

      <div className="p-5">
        <p className="py-2">Lorem ipsum dolor sit amet, consectetur adipisicing elit. Doloremque neque aut cumque eveniet iure error repellat consectetur.
          Dolorem dicta commodi autem repellat quisquam necessitatibus, nam fuga impedit debitis aut aperiam?
        </p>

        <p className="py-2">Lorem ipsum dolor sit amet, consectetur adipisicing elit. Doloremque neque aut cumque eveniet iure error repellat consectetur.
          Dolorem dicta commodi autem repellat quisquam necessitatibus, nam fuga impedit debitis aut aperiam?
        </p>

      </div>

      <div className={`imagem`}>
        <Image src="/assets/imgs/imagem.png" alt="Descrição da imagem" width={500} height={300} />
      </div>
    </div>
  );
}
