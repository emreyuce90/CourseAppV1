
import 'devextreme/dist/css/dx.light.css';
import './App.css';
import { Button } from 'devextreme-react';
import { Route, Routes, useNavigate } from 'react-router-dom';
import { useEffect } from 'react';
import Home from './App/Home/Home';
import Login from './App/auth/Login/Login';
import SignUp from './App/auth/Signup/Signup';

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
