using CourseApp.Application.Communication;
using CourseApp.Application.Dtos.Course;

namespace CourseApp.Application.Interfaces {
    public interface ICourseService {
        Task<Response> GetCoursesByUserId(int userId);
        Task<Response> AddAsync(CourseAddDto courseAddDto);
    }
}
