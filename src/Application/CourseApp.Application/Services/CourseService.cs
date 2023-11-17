using AutoMapper;
using CourseApp.Application.Communication;
using CourseApp.Application.Dtos.Course;
using CourseApp.Application.Interfaces;
using CourseApp.Domain.Entities;
using CourseApp.Domain.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CourseApp.Application.Services {
    public class CourseService : ICourseService {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CourseAddDto> _validator;
        private readonly ILogger<CourseService> _logger;
        public CourseService(ICourseRepository courseRepository, IMapper mapper, IValidator<CourseAddDto> validator, ILogger<CourseService> logger) {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Response> AddAsync(CourseAddDto courseAddDto) {

            var validationResult = await _validator.ValidateAsync(courseAddDto);
            if (!validationResult.IsValid) {
                var message = string.Empty;
                foreach (var item in validationResult.Errors) {
                    message += item.ErrorMessage;
                }
                return new Response() { Message = message, Success = false };
            }
            try {
                var addedCourse = await _courseRepository.CreateAsync(_mapper.Map<Course>(courseAddDto));
                await _courseRepository.SaveAsync();
                return new Response<CourseDto>(_mapper.Map<CourseDto>(addedCourse));
            } catch (Exception ex) {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Response> GetCoursesByUserId(int userId) {
            var courses = await _courseRepository.GetAllAsync(c => c.UserId == userId).ToListAsync();
            return new Response<List<CourseDto>>(_mapper.Map<List<CourseDto>>(courses));
        }
    }
}
