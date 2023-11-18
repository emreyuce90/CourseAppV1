import { useSelector } from "react-redux";
import CourseItem from "./CourseItem";
import { selectCourses } from "./courseSlice";

const Courselist = () => {
const courses = useSelector(selectCourses);
console.log(courses)
  return (
    <div className="container mx-auto my-8">
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-8">
        {courses?.map((course, index) => (
          <CourseItem
            key={index}
            title={course.title}
            id={course.id}
            description={course.description}
            image={"https://placehold.co/600x400"}
          />
        ))}
      </div>
    </div>
  );
};

export default Courselist;
