using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using Application.DTOs.Account;
using Domain.Entities;
using Domain.Settings;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.Persistence;

namespace Application
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepositoryAsync _userRepo;
        private readonly JWTSettings _jwtSettings;
        private readonly IMapper _mapper;
        private IMediator _mediator;

        public AccountService(
            IOptions<JWTSettings> jwtSettings,
            IUserRepositoryAsync userRepositoryAsync,
            IMapper mapper,
            IMediator mediator)
        {
            _jwtSettings = jwtSettings.Value;
            _userRepo = userRepositoryAsync;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            var user = await _userRepo.FindByNameAsync(request.UserName);
            if (user == null)
            {
                throw new ApiException($"No ApplicationUsers Registered with {request.UserName}.");
            }
            var password = StringCipher.Decrypt(user.PasswordHash, _jwtSettings.Key);

            if (password != request.Password)
            {
                throw new ApiException($"Invalid Credentials for '{request.UserName}'.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
            AuthenticationResponse response = new AuthenticationResponse();
            response.Id = user.Id;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return new Response<AuthenticationResponse>(response, $"Authenticated {user.UserName}");
        }

        public async Task<Response<int>> RegisterAsync(RegisterRequest request)
        {
            if (request.Password == null ||
             request.ConfirmPassword == null)
            {
                throw new ApiException($"One of this fields missing: Password, ConfirmPassword, Email, LastName, FirstName.");
            }
            if (request.Password.Length < 6)
            {
                throw new ApiException($"password Minimum length 6.");
            }
            if (request.ConfirmPassword != request.Password)
            {
                throw new ApiException($"Confirm Password wrong.");
            }

            var userWithSameUserName = await _userRepo.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                throw new ApiException($"Email '{request.UserName}' is already taken.");
            }

            var user = new ApplicationUser
            {
                UserName = request.UserName,
                PasswordHash = StringCipher.Encrypt(request.Password, _jwtSettings.Key)
            };
            await _userRepo.AddAsync(user); 
            return new Response<int>(user.Id, message: $"User Registered."); 
        }

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString())
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }

}
