using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationService.BE;
using AuthenticationService.Dto;
using AuthenticationService.helpers;
using AuthenticationService.Interfaces;
using AuthenticationService.Models;
using AutoMapper;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace UnitTests.AuthTests
{
    public class AuthTest
    {
        [Fact]
        public void LoginUser_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var appSettings = Options.Create(new AppSettings { Secret = "sE8zUVq2r6PasjDI2hfi3jsafHDOuf93i4gWJZgGdI7w==" });
            var authRepo = Substitute.For<IAuthRepo>();
            var mapper = Substitute.For<IMapper>();

            var authValidationService = new AuthValidationService(appSettings, authRepo, mapper);

            var loginDto = new LoginDto { Username = "valid_username", Password = "valid_password" };
            var login = new Login { Id = 1, Username="valid_username", Password = "valid_password" };

            authRepo.GetUsersByUsername(loginDto.Username).Returns(login);
            authRepo.AddTokenToLogin(Arg.Any<Token>());

            // Act
            var result = authValidationService.LoginUser(loginDto);

            // Assert
            authRepo.Received(1).GetUsersByUsername(Arg.Any<string>());
            authRepo.Received(1).AddTokenToLogin(Arg.Any<Token>());
        }

        [Fact]
        public void LoginUser_InvalidCredentials_ThrowsException()
        {
            // Arrange
            var appSettings = Options.Create(new AppSettings { Secret = "your_secret_key" });
            var authRepo = Substitute.For<IAuthRepo>();
            var mapper = Substitute.For<IMapper>();

            var authValidationService = new AuthValidationService(appSettings, authRepo, mapper);

            var loginDto = new LoginDto { Username = "invalid_username", Password = "invalid_password" };
            var login = new Login { Id = 1, Username = "invalid_username", Password = "valid_password" };

            authRepo.GetUsersByUsername(loginDto.Username).Returns(login);

            // Act & Assert
            Assert.Throws<Exception>(() => authValidationService.LoginUser(loginDto));
        }
    }
}
