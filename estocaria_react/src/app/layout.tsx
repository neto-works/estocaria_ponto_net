import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "../styles/main.css";
import "bootstrap-icons/font/bootstrap-icons.css";
import Renderizador from "./renderizador";

const inter = Inter({ subsets: ["latin"] });
export const metadata: Metadata = { title: "Estocaria.net", description: "app de estoque" };

export default function RootLayout({ children }: Readonly<{ children: React.ReactNode; }>) {

  return (
    <html lang="pt-Br">
      <head>
        <script
          src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"
          defer
        />
      </head>
      <body className={`container-fluid g-0 ${inter.className} bg-padrao`}>
        <Renderizador>{children}</Renderizador>
      </body>
    </html>
  );
}