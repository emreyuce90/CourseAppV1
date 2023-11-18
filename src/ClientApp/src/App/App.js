import { useEffect } from "react";
import "./App.css";
import { useNavigate,Routes,Route } from "react-router-dom";
import Home from "./Home/Home";
import Login from "./auth/Login/Login";
import 'devextreme/dist/css/dx.light.css';
import axios from "axios";
import SignUp from "./auth/Signup/Signup";


function App() {
  const navigate = useNavigate();
  useEffect(() => {
    var item = localStorage.getItem("jwt");
    if (!item) {
      navigate('/login')
    }
  }, []);
  return (
    <Routes>
      <Route path="/" element={<Home/>}></Route>
      <Route path="/login" element={<Login/>}></Route>
      <Route path="/signup" element={<SignUp/>}></Route>
    </Routes>
  );
}

export default App;
