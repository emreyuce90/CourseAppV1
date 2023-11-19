import Courselist from '../course/CourseList';
import Topbar from '../components/Topbar';
import Footer from '../components/Footer';
import { useDispatch } from 'react-redux';
import { useEffect } from 'react';
import { getCourses } from '../course/courseSlice';
import AddCourseComponent from '../components/AddCourseComponent';
import { jwtDecode } from 'jwt-decode';
import { addUserInfos } from '../auth/store';

const Home = () => {

const dispatch = useDispatch();
const decoded=jwtDecode(localStorage.getItem("jwt"));
const nameSurname ="http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
useEffect(()=>{

  dispatch(addUserInfos(decoded[nameSurname]))
  dispatch(getCourses());

},[dispatch,decoded])
  return(
      <div className="min-h-screen flex flex-col">
      <Topbar />
      <AddCourseComponent/>
      <div className="flex-grow">
        <Courselist/>
      </div>
      <Footer />
    </div>) ;
};

export default Home;
