using AutoMapper;
using CourseApp.Application.Communication;
using CourseApp.Application.Dtos.User;
using CourseApp.Application.Interfaces;
using CourseApp.Infrastructure.Context.Ef_Core.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CourseApp.Application.Services {
    public class AuthService : IAuthService {
        private readonly EfUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UserLoginDto> _validator;
        private readonly ILogger<AuthService> _logger;
        public AuthService(EfUserRepository userRepository, IMapper mapper, IValidator<UserLoginDto> validator, ILogger<AuthService> logger) {
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public Task<Response> CreateToken(UserDto userDto) {
            throw new NotImplementedException();
        }

        public Task<Response> VerifyUser(UserLoginDto userLoginDto) {
            //veriler girilmiş mi
            //email sistemde kayıtlı mı
            //şifre doğru mu

        }
    }
}
