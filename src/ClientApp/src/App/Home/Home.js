import Courselist from '../course/CourseList';
import Topbar from '../components/Topbar';
import Footer from '../components/Footer';

const Home = () => {


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
