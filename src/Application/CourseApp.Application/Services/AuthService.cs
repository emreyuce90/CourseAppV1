﻿using AutoMapper;
using CourseApp.Application.Communication;
using CourseApp.Application.Dtos.User;
using CourseApp.Application.Interfaces;
using CourseApp.Application.Utilities.JWT;
using CourseApp.Domain.Interfaces;
using CourseApp.Domain.Utilities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CourseApp.Application.Services {
    public class AuthService : IAuthService {
        private readonly IUserRefreshToken _userRefreshToken;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UserLoginDto> _validator;
        private readonly ILogger<AuthService> _logger;
        private readonly ITokenCreate _createToken;
        public AuthService(IUserRepository userRepository, IMapper mapper, IValidator<UserLoginDto> validator, ILogger<AuthService> logger, ITokenCreate createToken, IUserRefreshToken userRefreshToken) {
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
            _createToken = createToken;
            _userRefreshToken = userRefreshToken;
        }

        public async Task<Response> CreateToken(UserLoginDto userLoginDto) {
            var user = await _userRepository.GetSingle(u => u.Email == userLoginDto.Email);
            if (!user.IsDeleted && user.IsActive) {
                var accessToken = _createToken.CreateToken(user);
                //check users refresh token already exist
                var refreshToken = await _userRefreshToken.GetSingle(rt => rt.UserId == user.Id);

                //eğer refresh token yok ise token create üzerinden gelen verilerle yeni bir refresh token kaydı yap
                if (refreshToken == null) {
                    await _userRefreshToken.CreateAsync(new Domain.Entities.UserRefreshToken { UserId = user.Id, Code = accessToken.RefreshToken, Expiration = accessToken.RefreshTokenExpiration });
                }
                //Eğer kullanıcının zaten bir refresh tokenı var ise bu refresh tokenı güncelle     
                else {
                    refreshToken.Code = accessToken.RefreshToken;
                    refreshToken.Expiration = accessToken.RefreshTokenExpiration;
                    _userRefreshToken.UpdateAsync(refreshToken);
                }
                await _userRefreshToken.SaveAsync();
                var userResource = new UserResource {
                    Email = user.Email,
                    Name = user.Name,
                    Surname = user.Surname,
                    Token = accessToken
                };
                return new Response<UserResource>(userResource);
            }
            return new Response { Success = false, Message = "Giriş yapmaya çalıştığınız kullanıcı pasif veya silinmiştir" };
        }

        public async Task<Response> CreateTokenByRefreshToken(string refreshToken) {
            var existRefreshToken = await _userRefreshToken.GetSingle(rt => rt.Code == refreshToken);

            if (existRefreshToken == null)  return new Response { Success = false, Message = "Refresh token bulunamadı" };
               
            var user = await _userRepository.GetById(existRefreshToken.UserId);
            if(user == null) return new Response { Success = false, Message = "Kullanıcı  bulunamadı" };

            var accessToken = _createToken.CreateToken(user);
            existRefreshToken.Code = accessToken.RefreshToken;
            existRefreshToken.Expiration = accessToken.RefreshTokenExpiration;
            await _userRefreshToken.SaveAsync();

            var userResource = new UserResource {
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Token = accessToken
            };
            return new Response<UserResource>(userResource);
        }

        public async Task<Response> VerifyUser(UserLoginDto userLoginDto) {
            var validationResult = await _validator.ValidateAsync(userLoginDto);
            if (!validationResult.IsValid) {
                var message = string.Empty;
                foreach (var item in validationResult.Errors) {
                    message += item.ErrorMessage;
                }
                return new Response() { Message = message, Success = false };
            }
            var user = await _userRepository.GetSingle(u => u.Email == userLoginDto.Email);
            if (user == null) return new Response { Message = "Kullanıcı adı veya şifreniz hatalıdır", Success = false };
            bool isTrue = HashingHelper.VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt);
            if (!isTrue) return new Response { Success = false, Message = "Kullanıcı adı veya şifreniz hatalıdır" };
            return new Response { Success = true, Message = "Kullanıcı doğrulama başarılı" };
        }
    }
}
