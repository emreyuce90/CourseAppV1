using AutoMapper;
using CourseApp.Application.Communication;
using CourseApp.Application.Dtos.Course;
using CourseApp.Application.Dtos.User;
using CourseApp.Domain.Entities;

namespace CourseApp.Application.Mappings {
    public class AutoMapperConfiguration : Profile {
        public AutoMapperConfiguration() {
            CreateMap<User, UserAddDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Course, CourseAddDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap(typeof(Response<>), typeof(Response<>)).ConvertUsing(typeof(ResponseConverter<,>));
        }
        private class ResponseConverter<T, K> : ITypeConverter<Response<T>, Response<K>> {
            public Response<K> Convert(Response<T> sourceMember, Response<K> destination, ResolutionContext context) =>
                sourceMember.Success
                    ? new Response<K>(context.Mapper.Map<K>(sourceMember.Resource)) {
                        Message = sourceMember.Message,
                        Comment = sourceMember.Comment
                    }
                    : new Response<K>() {
                        Code = sourceMember.Code,
                        Comment = sourceMember.Comment,
                        Details = sourceMember.Details,
                        Message = sourceMember.Message,
                        Success = false,
                    };
        }
    }
}
