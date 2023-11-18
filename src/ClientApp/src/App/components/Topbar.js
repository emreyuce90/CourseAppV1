import { useNavigate } from "react-router-dom";

const Topbar = () => {
  const navigate = useNavigate();
  const handleClick = () => {
    localStorage.removeItem("jwt");
    navigate("/login");
  };
  return (
    <div className="bg-blue-500 text-white p-4 flex justify-between items-center">
      <div className="flex items-center">
        <span className="font-bold text-xl">CourseApp</span>
      </div>

      <div className="relative">
        <input
          type="text"
          placeholder="Ara..."
          className="py-2 px-8 rounded-full bg-white text-gray-800 focus:outline-none focus:ring focus:border-blue-300"
        />
      </div>

      <div className="flex items-center">
        <span className="mr-4">
          Hoşgeldiniz, Emre Yüce
        </span>
        <button
          onClick={handleClick}
          className="bg-white text-blue-500 px-4 py-2 rounded-full"
        >
          Çıkış Yap
        </button>
      </div>
    </div>
  );
};

export default Topbar;
