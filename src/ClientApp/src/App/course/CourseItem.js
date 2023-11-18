const CourseItem = ({ id, title, description,image }) => {
  return (
    <div key={id} className="bg-white p-6 rounded-md shadow-md transition duration-300 ease-in-out transform hover:scale-105">
    <img src={image} alt={title} className="mb-4 rounded-md w-full h-40 object-cover" />
    <h3 className="text-lg font-semibold mb-2">{title}</h3>
    <p className="text-gray-600">{`Description: ${description}`}</p>
    <button className="mt-4 bg-blue-500 text-white py-2 px-4 rounded-full">
      Enroll Now
    </button>
  </div>
  );
};

export default CourseItem;
