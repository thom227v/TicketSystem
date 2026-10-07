import { NavLink } from "react-router";
import 'bootstrap/dist/css/bootstrap.css';
import UserInfoForm from "../components/Auth/UserInfoForm"
export default function Navbar() {
  return (
    <nav className="nav nav-tabs sticky-top navbar-expand-lg gap-4 p-3 d-flex justify-content-between">
        <div className="container">
            <div className="row">
                <div className="col"></div>  
                <div className="col">
                <NavLink className="nav-item btn " to="/">Home</NavLink>
                <NavLink className="nav-item btn" to="/Ticket">Ticket</NavLink>
                <NavLink className="nav-item btn" to="/ServiceAgreement">Service Agreement</NavLink>
                <NavLink className="nav-item btn" to="/Department">Department</NavLink>
                </div>
                <div className="col d-flex justify-content-end">
                 <UserInfoForm/> 
                </div>
            </div>
      </div>
    </nav>
  );
}   