using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Validators;
using Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace Application.User
{
    public class ResetPassword
    {
        public class Command : IRequest
        {
            public string Code { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Code).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).Password();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly UserManager<AppUser> _userManager;
            public Handler(UserManager<AppUser> userManager)
            {
                this._userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    return Unit.Value;
                }

                var passwordResetCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
                var result = await _userManager.ResetPasswordAsync(user, passwordResetCode, request.Password);

                if (result.Succeeded)
                {
                    return Unit.Value;
                }

                return Unit.Value;
            }
        }
    }
}