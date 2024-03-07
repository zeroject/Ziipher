using AuthenticationService.Interfaces;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace AuthenticationService.Models
{
    public class tokenValidator : IResourceOwnerPasswordValidator
    {
        private readonly IAuthRepo _AuthRepo;
        private readonly ILogger<tokenValidator> _logger;
        public tokenValidator(IAuthRepo authRepo, ILogger<tokenValidator> logger)
        {
            _AuthRepo = authRepo;
            _logger = logger;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var token = context.Password;
            _logger.LogInformation("Validating username: ," + context.UserName + " password: "+ context.Password);

            var user = _AuthRepo.GetUserByToken(token);
            if (user != null)
            {
                context.Result = new GrantValidationResult(user.Id.ToString(), "password");
                _logger.LogInformation("User validated");
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid token");
            }
        }
    }
}
