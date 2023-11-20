import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { searchCourse } from "../course/courseSlice";
import { useEffect, useState } from "react";

const Topbar = () => {
  const [nameSurname, setNameSurname] = useState("");
  const userInfosString = localStorage.getItem("userInfos");
  const userInfos = JSON.parse(userInfosString);
  //const {nameSurname} = useSelector(state=>state.user);
  const [search, setSearch] = useState("");
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const handleClick = () => {
    localStorage.removeItem("jwt");
    localStorage.removeItem("userInfos");
    navigate("/login");
  };
  const handleKeyup = () => {
    dispatch(searchCourse(search));
  };
  useEffect(() => {
    setNameSurname(userInfos);
  }, [userInfos]);
  return (
    <div className="bg-blue-500 text-white p-4 flex justify-between items-center">
      <div className="flex items-center">
        <span className="font-bold text-xl">CourseApp</span>
      </div>

      <div className="relative">
        <input
          value={search}
          onChange={(e) => setSearch(e.target.value)}
          type="text"
          placeholder="Ara..."
          className="py-2 px-8 rounded-full bg-white text-gray-800 focus:outline-none focus:ring focus:border-blue-300"
          onKeyUp={handleKeyup}
        />
      </div>

      <div className="flex items-center">
        <span className="mr-4">Hoşgeldiniz, {nameSurname}</span>
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
