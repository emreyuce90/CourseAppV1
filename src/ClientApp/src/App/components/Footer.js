import FacebookIcon from '@mui/icons-material/Facebook';
import InstagramIcon from '@mui/icons-material/Instagram';
import TwitterIcon from '@mui/icons-material/Twitter';
const Footer = () => {
  return (
    <footer className="bg-gray-800 text-white p-8">
      <div className="container mx-auto flex justify-between items-center">
        <div>
          <h1 className="text-2xl font-bold">CourseApp</h1>
        </div>

        <div className="flex items-center">
          <div className="mr-8">
            <p className="mb-2">İletişim Bilgileri:</p>
            <p>Email: info@courseapp.com</p>
            <p>Telefon: +90 123 456 7890</p>
          </div>

          <div className="flex space-x-4">
            <a href="https://twitter.com" target="_blank" rel="noopener noreferrer">
              <FacebookIcon className='w-6 h-6'/>
            </a>
            <a href="https://facebook.com" target="_blank" rel="noopener noreferrer">
                <InstagramIcon className='w-6 h-6'/>
            </a>
            <a href="https://youtube.com" target="_blank" rel="noopener noreferrer">
             <TwitterIcon className='w-6 h-6'/>
            </a>
          </div>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
