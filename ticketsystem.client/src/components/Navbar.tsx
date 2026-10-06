import { NavLink } from "react-router";
import 'bootstrap/dist/css/bootstrap.css';
export default function Navbar() {
  return (
    <nav className="nav nav-tabs sticky-top navbar-expand-lg gap-4 p-3 d-flex justify-content-center">
      <NavLink className="nav-item btn" to="/">Home</NavLink>
      <NavLink className="nav-item btn" to="/Ticket">Ticket</NavLink>
      <NavLink className="nav-item btn" to="/ServiceAgreement">Service Agreement</NavLink>
      <NavLink className="nav-item btn" to="/Department">Department</NavLink>
      <NavLink className="nav-item btn" to="/Login">Login</NavLink>
    </nav>
  );
}   