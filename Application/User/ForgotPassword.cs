using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;

namespace Application.User
{
    public class ForgotPassword
    {
        public class Command : IRequest
        {
            public string Email { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Email).EmailAddress().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IEmailSender _emailSender;
            private readonly string _clientUrl;
            public Handler(UserManager<AppUser> userManager, IEmailSender emailSender, IConfiguration config)
            {
                this._clientUrl = config["ClientUrl"];
                this._emailSender = emailSender;
                this._userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return Unit.Value;
                }

                var passwordResetCode = await _userManager.GeneratePasswordResetTokenAsync(user);
                passwordResetCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(passwordResetCode));
                var callbackUrl = $"{_clientUrl}/reset-password/{passwordResetCode}";

                _emailSender.SendEmail(
                    request.Email,
                    "Reset Password",
                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return Unit.Value;
            }
        }
    }
}