using AutoMapper;
using CourseApp.Application.Communication;
using CourseApp.Application.Dtos.Course;
using CourseApp.Application.Interfaces;
using CourseApp.Cache;
using CourseApp.Domain.Entities;
using CourseApp.Domain.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;

namespace CourseApp.Application.Services {
    public class CourseService : ICourseService {
        private readonly IDatabase _redisCache;
        private const string cacheKey = "course-cache";
        private readonly RedisService _redisService;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CourseAddDto> _validator;
        private readonly ILogger<CourseService> _logger;
        public CourseService(ICourseRepository courseRepository, IMapper mapper, IValidator<CourseAddDto> validator, ILogger<CourseService> logger, RedisService redisService) {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
            _redisService = redisService;
            _redisCache = _redisService.GetDatabase(0);
        }

        public async Task<Response> AddAsync(CourseAddDto courseAddDto, int userId) {

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
                addedCourse.CreatedDate = DateTime.UtcNow;
                addedCourse.UserId = userId;
                addedCourse.PictureUrl = "sampleUrl";
                await _courseRepository.SaveAsync();
                //redis kaydı
                if(await _redisCache.KeyExistsAsync(cacheKey)) {
                    await _redisCache.HashSetAsync(cacheKey,addedCourse.Id,JsonSerializer.Serialize(addedCourse));
                }
                return new Response<CourseDto>(_mapper.Map<CourseDto>(addedCourse));
            } catch (Exception ex) {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Response> GetCoursesByUserId(int userId) {
            //check cache 
            if (!await _redisCache.KeyExistsAsync(cacheKey)) {
                //run load cache
                var cachedData = await LoadToCache();
                return new Response() { Resource = cachedData.FirstOrDefault(d => d.UserId == userId) };
            }

            var courses = new List<Course>();
            //read data from cache
            var dataFromCache =await _redisCache.HashGetAllAsync(cacheKey);

            foreach (var data in dataFromCache.ToList())
            {
                var c = JsonSerializer.Deserialize<Course>(data.Value);
                courses.Add(c);
            }

            var res = new Response() { Resource = courses };
            return res;
        }

        private async Task<List<Course>> LoadToCache() {
            var courses = await _courseRepository.GetAllAsync().ToListAsync();
            courses.ForEach(async course => {
                await _redisCache.HashSetAsync(cacheKey, course.Id, JsonSerializer.Serialize(course));
            });
            return courses;
        }
    }
}
