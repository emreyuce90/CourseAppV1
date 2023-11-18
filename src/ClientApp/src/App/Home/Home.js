import axios from "axios";
import { jwtDecode } from "jwt-decode";
import { useEffect } from "react";
import { getToken } from "../../jwt/TokenManager";

const Home = () => {
  const token = getToken();
  useEffect(() => {
    async function fetchData() {
      const response = await axios.get(
        "https://localhost:44390/api/Course/GetAllCoursesByUserId",
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );
    }
    fetchData();
  }, []);

  return <>Home Component</>;
};

export default Home;
