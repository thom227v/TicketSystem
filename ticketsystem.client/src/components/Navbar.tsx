import { NavLink } from "react-router";
import 'bootstrap/dist/css/bootstrap.css';
export default function Navbar() {
  return (
    <nav className="nav nav-tabs gap-4">
      <NavLink className={({ isActive, isPending }) =>isPending ? "nav-link" : isActive ? "nav-link active" : ""} to="/">Home</NavLink>
      <NavLink className={({ isActive, isPending }) =>isPending ? "nav-link" : isActive ? "nav-link active" : ""} to="/Ticket">Ticket</NavLink>
      <NavLink className={({ isActive, isPending }) =>isPending ? "nav-link" : isActive ? "nav-link active" : ""} to="/ServiceAgreement">Service Agreement</NavLink>
      <NavLink className={({ isActive, isPending }) =>isPending ? "nav-link" : isActive ? "nav-link active" : ""} to="/Department">Department</NavLink>
    </nav>
  );
}