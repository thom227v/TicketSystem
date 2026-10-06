import {
  Routes,
  Route
} from "react-router";
import Home from './pages/Home';
import Ticket from './pages/Ticket';
import Layout from './components/Layout';
import ServiceAgreement from "./pages/ServiceAgreement";
import Department from "./pages/Department";
import { Toaster } from "sonner";

function App() {
    return(
    <>
    <Toaster/>
      <Routes>
        <Route element={<Layout />}>
          <Route path="/" element={<Home />} />
          <Route path="/Ticket" element={<Ticket />} />
          <Route path="/ServiceAgreement" element={<ServiceAgreement />} />
          <Route path="/Department" element={<Department />} />
        </Route>
      </Routes>
    </>
    );

}
export default App;
