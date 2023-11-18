import axios from "axios";
import { LoadPanel, TextBox, Toast } from "devextreme-react";
import Form, { ButtonItem, SimpleItem } from "devextreme-react/form";
import { useState } from "react";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { addUserInfos } from "../store";
import { jwtDecode } from "jwt-decode";

const Login = () => {
  const dispatch = useDispatch();
    const [loadPanel,setLoadPanel] = useState({
        loadPanelVisible:false,
        showIndicator:true,
        shading:true,
        showPane:true,
        hideOnOutsideClick:false
    });
  const navigate = useNavigate();
  const [toastConfig, setToastConfig] = useState({
    isVisible: false,
    type: "info",
    message: "",
  });
  const [form, setForm] = useState({
    email: "",
    password: "",
  });

  const handleFormSubmit = async (e) => {
    setLoadPanel({
        ...loadPanel,
        loadPanelVisible:true
    });
    e.preventDefault();
    try {
      const response = await axios.post(
        "https://localhost:44390/api/auth/Login",
        form
      );
      if (response.data.success) {
        //jwt yi localStorage a kaydet
        localStorage.setItem("jwt", JSON.stringify(response.data.resource));
        //decode jwt
        const decoded=jwtDecode(localStorage.getItem("jwt"));
        const nameSurname ="http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
        dispatch(addUserInfos(decoded[nameSurname]))
        
        setToastConfig({
          ...toastConfig,
          isVisible: true,
          message:
            "Giriş başarılı,ana sayfaya yönlendiriliyorsunuz,lütfen bekleyiniz",
          type: "success",
        });
        setLoadPanel({
            ...loadPanel,
            loadPanelVisible:false
        });
        navigate("/");
      }else{
        setLoadPanel({
          ...loadPanel,
          loadPanelVisible:false
      });
      setToastConfig({
        ...toastConfig,
        isVisible: true,
        message: `${response.data.message}`,
        type: "error",
      });
      }
    } catch (error) {
 
      setToastConfig({
        ...toastConfig,
        isVisible: true,
        message: `${error.response.data.message}`,
        type: "error",
      });
      setLoadPanel({
        ...loadPanel,
        loadPanelVisible:false
    });
    }
  };
  const onHiding = () => {
    setToastConfig({
      ...toastConfig,
      isVisible: false,
    });
  };
  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };
  const handleNavigation=() =>{
    navigate('/signup')
  }
  return (
    <>
      <div className="flex items-center justify-center min-h-screen bg-rose-50">
        <div className="relative flex flex-col m-6 space-y-10 bg-white shadow-2xl rounded-2xl md:flex-row md:space-y-0 md:m-0">
          <form onSubmit={handleFormSubmit}>
            <div className="p-6 md:p-20">
              <h2 className="font-mono mb-5 text-4xl font-bold">Log In</h2>
              <p className="max-w-sm mb-12 font-sans font-light text-gray-600">
                Log in to your account to upload or download pictures, videos or
                music.
              </p>
              <input
                type="text"
                name="email"
                className="w-full p-6 border border-gray-300 rounded-md placeholder:font-sans placeholder:font-light my-2"
                placeholder="Enter your email address"
                onChange={handleChange}
              />
              <input
                type="password"
                name="password"
                className="w-full p-6 border border-gray-300 rounded-md placeholder:font-sans placeholder:font-light"
                placeholder="Enter your password"
                onChange={handleChange}
              />

              <div className="flex flex-col items-center justify-between mt-6 space-y-6 md:flex-row md:space-y-0">
                <div className="font-thin text-cyan-700">Forgot password</div>

                <button className="w-full md:w-auto flex justify-center items-center p-6 space-x-4 font-sans font-bold text-white rounded-md shadow-lg px-9 bg-cyan-700 shadow-cyan-100 hover:bg-opacity-90 shadow-sm hover:shadow-lg border transition hover:-translate-y-0.5 duration-150">
                  <span>Next</span>
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    className="w-7"
                    viewBox="0 0 24 24"
                    stroke-width="1.5"
                    stroke="#ffffff"
                    fill="none"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                  >
                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                    <line x1="5" y1="12" x2="19" y2="12" />
                    <line x1="13" y1="18" x2="19" y2="12" />
                    <line x1="13" y1="6" x2="19" y2="12" />
                  </svg>
                </button>
              </div>

              <div className="mt-12 border-b border-b-gray-300"></div>

              <p className="py-6 text-sm font-thin text-center text-gray-400 hover:cursor-pointer" onClick={handleNavigation}>
                if you are not our member,please register first
              </p>

              <div className="flex flex-col space-x-0 space-y-6 md:flex-row md:space-x-4 md:space-y-0">
                <button className="flex items-center justify-center py-2 space-x-3 border border-gray-300 rounded shadow-sm hover:bg-opacity-30 hover:shadow-lg hover:-translate-y-0.5 transition duration-150 md:w-1/2">
                  <img
                    src="../assets/images/facebook.png"
                    alt=""
                    className="w-9"
                  />
                  <span className="font-thin">Facebook</span>
                </button>
                <button className="flex items-center justify-center py-2 space-x-3 border border-gray-300 rounded shadow-sm hover:bg-opacity-30 hover:shadow-lg hover:-translate-y-0.5 transition duration-150 md:w-1/2">
                  <img
                    src="../assets/images/google.png"
                    alt=""
                    className="w-9"
                  />
                  <span className="font-thin">Google</span>
                </button>
              </div>
            </div>
          </form>

          <img
            src="../assets/images/image.jpg"
            alt=""
            className="w-[430px] hidden md:block"
          />

          <div className="group absolute -top-5 right-4 flex items-center justify-center w-10 h-10 bg-gray-200 rounded-full md:bg-white md:top-4 hover:cursor-pointer hover:-translate-y-0.5 transition duration-150">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className="w-6 h-6 text-black group-hover:text-gray-600"
              viewBox="0 0 24 24"
              stroke-width="1.5"
              stroke="currentColor"
              fill="none"
              stroke-linecap="round"
              stroke-linejoin="round"
            >
              <path stroke="none" d="M0 0h24v24H0z" fill="none" />
              <line x1="18" y1="6" x2="6" y2="18" />
              <line x1="6" y1="6" x2="18" y2="18" />
            </svg>
          </div>
        </div>
      </div>
      <Toast
        visible={toastConfig.isVisible}
        message={toastConfig.message}
        type={toastConfig.type}
        onHiding={onHiding}
        position={"top center"}
        displayTime={1000}
      />
      <LoadPanel
        shadingColor="rgba(0,0,0,0.4)"
        //position={position}
        onHiding={loadPanel.hideLoadPanel}
        visible={loadPanel.loadPanelVisible}
        showIndicator={loadPanel.showIndicator}
        shading={loadPanel.shading}
        showPane={loadPanel.showPane}
        hideOnOutsideClick={loadPanel.hideOnOutsideClick}
      />
    </>
  );
};

export default Login;
