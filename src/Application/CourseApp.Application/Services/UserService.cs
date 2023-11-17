using AutoMapper;
using CourseApp.Application.Communication;
using CourseApp.Application.Dtos.User;
using CourseApp.Application.Interfaces;
using CourseApp.Domain.Entities;
using CourseApp.Domain.Interfaces;
using CourseApp.Domain.Utilities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CourseApp.Application.Services {
    public class UserService : IUserService {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UserAddDto> _validator;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository, IMapper mapper, IValidator<UserAddDto> validator, ILogger<UserService> logger) {
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Response> AddAsync(UserAddDto userAddDto) {
            var validationResult = await _validator.ValidateAsync(userAddDto);
            if (!validationResult.IsValid) {
                var message = string.Empty;
                foreach (var item in validationResult.Errors) {
                    message += item.ErrorMessage;
                }
                return new Response() { Message = message, Success = false };
            }
            try {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(userAddDto.Password, out passwordHash, out passwordSalt);
                User user = new() {
                    CreatedDate = DateTime.UtcNow,
                    Email = userAddDto.Email,
                    IsActive = true,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Name = userAddDto.Name,
                    Surname = userAddDto.Surname
                };
                var addedUser = await _userRepository.CreateAsync(user);
                await _userRepository.SaveAsync();
                return new Response<UserDto>(_mapper.Map<UserDto>(addedUser));
            } catch (Exception ex) {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        public async Task<Response> GetUserByEmailAsync(string email) {
            var user = await _userRepository.GetSingle(u => u.Email == email);
            if (user == null) { return new Response() { Message = "Veritabanımıza kayıtlı böyle bir kullanıcı bulunmamaktadır", Success = false }; }
            return new Response<UserDto>(_mapper.Map<UserDto>(user));
        }
    }
}
