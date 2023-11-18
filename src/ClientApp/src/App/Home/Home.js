import Courselist from '../course/CourseList';
import Topbar from '../components/Topbar';
import Footer from '../components/Footer';
import { useDispatch } from 'react-redux';
import { useEffect } from 'react';
import { getCourses } from '../course/courseSlice';

const Home = () => {
const dispatch = useDispatch();
useEffect(()=>{
  dispatch(getCourses());
},[dispatch])
  return(
      <div className="min-h-screen flex flex-col">
      <Topbar />
      <div className="flex-grow">
        <Courselist/>
      </div>
      <Footer />
    </div>) ;
};

export default Home;
