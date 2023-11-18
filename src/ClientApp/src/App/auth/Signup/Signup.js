import axios from "axios";
import { LoadPanel, Toast } from "devextreme-react";
import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";

const SignUp = () => {
  const navigate = useNavigate();
  const [form, setForm] = useState({
    name:"",
    surname:"",
    email: "",
    password: "",
    passwordConfirm: "",
  });
  const [loadPanel, setLoadPanel] = useState({
    loadPanelVisible: false,
    showIndicator: true,
    shading: true,
    showPane: true,
    hideOnOutsideClick: false,
  });
  const [toastConfig, setToastConfig] = useState({
    type: "",
    message: "",
    isVisible: false,
  });

  const handleFormSubmit = async (e) => {
    e.preventDefault();
    setLoadPanel({
      ...loadPanel,
      loadPanelVisible: true,
    });
    //loading true ya çek
    try {
      const response = await axios.post(
        "https://localhost:44390/api/auth/Register",
        form
      );
      if (response.data.success) {
        //loading i false a çekme
        setLoadPanel({
          ...loadPanel,
          loadPanelVisible: false,
        });
        //başarılı mesajı
        
        setToastConfig({
          ...toastConfig,
          type: "success",
          message:
            "Kayıt olma işleminiz başarıyla tamamlanmıştır,login ekranına yönlendirilirken lütfen bekleyiniz",
          isVisible: true,
        });
        setTimeout(() => {
          navigate("/login");
        }, 1000);
      }
      setToastConfig({
        ...toastConfig,
        type: "error",
        message:
          `${response.data.message}`,
        isVisible: true,
      });
    } catch (error) {
      console.log(error);
      //başarısız mesaj


      setToastConfig({
        ...toastConfig,
        type: "error",
        message: `Error Message :${error.response.data.message}`,
        isVisible: true,
      });
      setLoadPanel({
        ...loadPanel,
        loadPanelVisible: false,
      });
    }
  };
  const handleChange = (e) => {
    setForm({
      ...form,
      [e.target.name]: e.target.value,
    });
  };

  const onHiding = () => {
    setToastConfig({
      ...toastConfig,
      isVisible: false,
    });
  };
  return (
    <>
      <div className="flex items-center justify-center min-h-screen min-w-screen bg-rose-50">
        <div className="relative flex flex-col m-6 space-y-10 bg-white shadow-2xl rounded-2xl md:flex-row md:space-y-0 md:m-0">
          <form onSubmit={handleFormSubmit}>
            <div className="p-6 md:p-3">
              <h2 className="font-mono mb-5 text-4xl font-bold">Sign Up</h2>
              <p className="max-w-sm mb-12 font-sans font-light text-gray-600">
                Sign up to your account
              </p>
              <div className="">
              <input
                type="text"
                name="name"
                className="w-1/2 p-3 border border-gray-300 rounded-md placeholder:font-sans placeholder:font-light my-2"
                placeholder="Enter your name"
                onChange={handleChange}
              />
                <input
                type="text"
                name="surname"
                className="w-1/2 p-3 border border-gray-300 rounded-md placeholder:font-sans placeholder:font-light my-2"
                placeholder="Enter your surname"
                onChange={handleChange}
              />
              </div>
              <input
                type="text"
                name="email"
                className="w-full p-3 border border-gray-300 rounded-md placeholder:font-sans placeholder:font-light my-2"
                placeholder="Enter your email address"
                onChange={handleChange}
              />
              <input
                type="password"
                name="password"
                className="w-full p-3 border border-gray-300 rounded-md placeholder:font-sans placeholder:font-light my-2"
                placeholder="Enter your password"
                onChange={handleChange}
              />
              <input
                type="password"
                name="passwordConfirm"
                className="w-full p-3 border border-gray-300 rounded-md placeholder:font-sans placeholder:font-light my-2"
                placeholder="Password confirm"
                onChange={handleChange}
              />
              <div className="flex flex-col items-center justify-between mt-6 space-y-6 md:flex-row md:space-y-0">
                <Link to={"/login"} className="font-thin text-cyan-700">
                  Already member? Login
                </Link>

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

              <p className="py-6 text-sm font-thin text-center text-gray-400">
                or log in with
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

export default SignUp;
