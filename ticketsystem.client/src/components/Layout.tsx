import { Outlet } from "react-router";
import Navbar from "./Navbar";

export default function Layout() {
  return (
    <>
      <Navbar />

      <main className="d-flex justify-content-center">
        <Outlet />
      </main>
    </>
  );
}