import CourseItem from "./CourseItem";

const Courselist = () => {
  const courses = [
    { id: 1, title: "Web Development with React", instructor: "John Doe" },
    { id: 2, title: "UI/UX Design Fundamentals", instructor: "Jane Smith" },
    {
      id: 3,
      title: "Mobile App Development with Flutter",
      instructor: "Alice Johnson",
    },
    { id: 4, title: "Data Science Masterclass", instructor: "Bob Williams" },
    { id: 5, title: "Digital Marketing Strategies", instructor: "Emily Davis" },
  ];
  return (
    <div className="container mx-auto my-8">
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-8">
        {courses.map((course, index) => (
          <CourseItem
            key={index}
            title={course.title}
            id={course.id}
            instructor={course.instructor}
            image={"https://placehold.co/600x400"}
          />
        ))}
      </div>
    </div>
  );
};

export default Courselist;
