import Link from "next/link";

export default function Home() {
  return (
    <div className={`container-fluid m-0 bg-white w-100 h-100 p-3 round-side`}>
      <h2>Nav Link </h2>

      <h3>Assunto secundario </h3>

      <h1>Assunto principal </h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Doloremque neque aut cumque eveniet iure error repellat consectetur.
        Dolorem dicta commodi autem repellat quisquam necessitatibus, nam fuga impedit debitis aut aperiam?
      </p>

      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Doloremque neque aut cumque eveniet iure error repellat consectetur.
        Dolorem dicta commodi autem repellat quisquam necessitatibus, nam fuga impedit debitis aut aperiam?
      </p>
      <p>imagem</p>
      

      <br />
      <Link href="/admin">Admin</Link>
    </div>
  );
}
