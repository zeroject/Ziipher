using AuthenticationService.BE;
using AuthenticationService.Dto;
using AuthenticationService.helpers;
using AuthenticationService.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthValidationService : IAuthValidationService
    {
        private double expiriationTime;
        
        private readonly AppSettings _appSettings;
        private IAuthRepo _authRepo;
        private IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appSettings"></param>
        /// <param name="authRepo"></param>
        public AuthValidationService(IOptions<AppSettings> appSettings, IAuthRepo authRepo, IMapper mapper)
        {
            _appSettings = appSettings.Value;
            _authRepo = authRepo;
            _mapper = mapper;
            expiriationTime = 6;
        }
        
        //TODO refactor for cleaner code
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string LoginUser(LoginDto loginDto)
        {
            bool isValid = Validate(loginDto.Username, loginDto.Password);
            if (isValid)
            {
                var token = GenerateJSONWebToken(loginDto);
                Login login = _authRepo.GetUsersByUsername(loginDto.Username);
                _authRepo.AddTokenToLogin(new Token 
                { 
                    JwtToken = token,
                    UserId = login.Id
                });
                return token;
            }
            throw new Exception("Invalid credentials");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidateUserByCredentials(string username, string password)
        {
            return Validate(username, password);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool ValidateUserByToken(string token)
        {
            return _authRepo.GetUserByToken(token) != null;
        }

        private string GenerateJSONWebToken(LoginDto userInfo)
        {
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("username", userInfo.Username) }),
                Expires = DateTime.UtcNow.AddHours(expiriationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        private bool Validate(string username, string password)
        {
            Login login = _authRepo.GetUsersByUsername(username);
            return login.Password == password; 
        }

        /// <summary>
        /// registers a new user in the database
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        public LoginDto RegisterNewLogin(LoginDto loginDto)
        {
            //map dto to Be though automapper
            Login login = _mapper.Map<Login>(loginDto);
            //save to db
            return _mapper.Map<LoginDto>(_authRepo.AddLogin(login));
        }


        /// <summary>
        /// 
        /// </summary>
        public void Rebuild()
        {
            _authRepo.Rebuild();
        }
    }
}
