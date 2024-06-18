using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitectureCosmosDb.Application.DTOs;
using CleanArchitectureCosmosDb.Domain.Entity; // Assuming User class is in Domain.Entity
using CleanArchitectureCosmosDb.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitectureCosmosDb.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<AuthResponseDto> Authenticate(UserDto userDto)
        {
            // Assuming UserDto maps to User entity; adjust as per your application structure
            var user = await _userRepository.GetUserByUsernameAndPasswordAsync(userDto.Username, userDto.Password);
            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JwtSettings:ExpiryMinutes")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResponseDto
            {
                Token = tokenHandler.WriteToken(token),
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}
