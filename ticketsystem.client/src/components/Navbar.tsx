import { NavLink } from "react-router";

export default function Navbar() {
  return (
    <nav>
      <NavLink to="/">Home</NavLink>
      <NavLink to="/Ticket">Ticket</NavLink>
      <NavLink to="/ServiceAgreement">Service Agreement</NavLink>
      <NavLink to="/Department">Department</NavLink>
    </nav>
  );
}