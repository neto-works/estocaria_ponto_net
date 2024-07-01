'use client';
import { ChangeEvent, SyntheticEvent, useContext, useState } from "react";
import Styles from "./login.module.css";
import Image from "next/image";
import { useRouter } from "next/navigation";
import Link from "next/link";
import { ContextAuth } from "@/contexts/authContext";
import { NavLink } from "@/components/Screen";

export default function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const auth = useContext(ContextAuth);
  const route = useRouter();

  const handleInputPasswordChange = (event: ChangeEvent<HTMLInputElement>) => {
    setPassword(event.target.value);
  };
  const handleInputEmailChange = (event: ChangeEvent<HTMLInputElement>) => {
    setEmail(event.target.value);
  };
  const handleSumitAuth = async (event: SyntheticEvent) => {
    event.preventDefault();
    const redirecionah = await auth.singIn(email, password);
    redirecionah ? route.push('/manager') : null;
  };

  return (
    <>
      <div className="container-fluid bg-white px-1 px-md-5 px-lg-1 px-xl-5 py-5 mx-auto">
        <div className=" d-flex flex-column align-items-center p-5">
          <NavLink>
            <Link className="link" href={"/"}>Home</Link>
            <Link className="link" href={"/admin"}>Admin</Link>
          </NavLink>
        </div>
        <div className={`${Styles.card} ${Styles.card0} b-0`}>
          <div className="row d-flex">
            <div className="col-lg-6">
              <div className={`pb-5`}>
                <div className="row px-3 justify-content-center mt-4 mb-5 borderLine">
                  <Image
                    src="/assets/imgs/original.png"
                    width={560}
                    height={368}
                    alt=""
                  />
                </div>
              </div>
            </div>
            <div className="col-lg-6">
              <div className={`${Styles.card2} ${Styles.card} b-0 px-4 py-5`}>
                <div className="row px-3">
                  <label className="mb-1">
                    <h6 className="mb-0 text-sm">Email Address</h6>
                  </label>
                  <input
                    className="mb-4"
                    type="text"
                    name="email"
                    placeholder="Enter a valid email address"
                    onChange={handleInputEmailChange}
                  />
                </div>
                <div className="row px-3">
                  <label className="mb-1">
                    <h6 className={`mb-0 ${Styles.textSm}`}>Password</h6>
                  </label>
                  <input
                    type="password"
                    name="password"
                    placeholder="Enter password"
                    onChange={handleInputPasswordChange}
                  />
                </div>
                <div className="row px-3 mb-4">
                  <div className="custom-control custom-checkbox custom-control-inline">
                  </div>
                  <a href="#" className="ml-auto mb-0 text-sm">
                    Forgot Password?
                  </a>
                </div>
                <div className="row mb-3 px-3">
                  <button
                    type="submit"
                    className={`btn ${Styles.btnBlue} text-center`}
                    onClick={handleSumitAuth}
                  >
                    Login
                  </button>
                </div>
                <div className="row mb-4 px-3">
                  <small className="font-weight-bold text-primary">
                    Dont have an account?{" "}
                    <Link href={"/register"} className="text-success">Register</Link>
                  </small>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
