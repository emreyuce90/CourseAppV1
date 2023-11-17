using CourseApp.Application.Communication;
using CourseApp.Application.Dtos.Course;
using CourseApp.Application.Interfaces;

namespace CourseApp.Application.Services {
    public class CourseService : ICourseService {
        public Task<Response> AddAsync(CourseAddDto courseAddDto) {
            throw new NotImplementedException();
        }

        public Task<Response> GetCoursesByUserId(int userId) {
            throw new NotImplementedException();
        }
    }
}
