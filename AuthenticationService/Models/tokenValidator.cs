using AuthenticationService.Interfaces;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace AuthenticationService.Models
{
    public class tokenValidator : IResourceOwnerPasswordValidator
    {
        private readonly IAuthRepo _AuthRepo;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="authRepo"></param>
        public tokenValidator(IAuthRepo authRepo)
        {
            _AuthRepo = authRepo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var token = context.Password;

            var user = _AuthRepo.GetUserByToken(token);
            if (user != null)
            {
                context.Result = new GrantValidationResult(user.Id.ToString(), "password");
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid token");
            }
        }
    }
}
