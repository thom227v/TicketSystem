import { NavLink } from "react-router";
import 'bootstrap/dist/css/bootstrap.css';
export default function Navbar() {
  return (
    <nav className="nav nav-tabs sticky-top navbar-expand-lg gap-4 d-flex justify-content-center">
      <NavLink className={({ isActive, isPending }) =>isPending ? "nav-item" : isActive ? "nav-item active" : ""} to="/">Home</NavLink>
      <NavLink className={({ isActive, isPending }) =>isPending ? "nav-item" : isActive ? "nav-item active" : ""} to="/Ticket">Ticket</NavLink>
      <NavLink className={({ isActive, isPending }) =>isPending ? "nav-item" : isActive ? "nav-item active" : ""} to="/ServiceAgreement">Service Agreement</NavLink>
      <NavLink className={({ isActive, isPending }) =>isPending ? "nav-item" : isActive ? "nav-item active" : ""} to="/Department">Department</NavLink>
    </nav>
  );
}