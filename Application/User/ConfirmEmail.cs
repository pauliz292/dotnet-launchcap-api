using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace Application.User
{
    public class ConfirmEmail
    {
        public class Command : IRequest<UserDto>
        {
            public int Id { get; set; }
            public string Code { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.Code).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, UserDto>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(UserManager<AppUser> userManager, IJwtGenerator jwtGenerator)
            {
                this._jwtGenerator = jwtGenerator;
                this._userManager = userManager;
            }

            public async Task<UserDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.Id.ToString());

                if (user == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { User = $"Unable to find user with ID '{request.Id}'." });
                }

                var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
                var result = await _userManager.ConfirmEmailAsync(user, code);

                if (result.Succeeded)
                {
                    return new UserDto
                    {
                        Username = user.UserName,
                        Token = _jwtGenerator.CreateToken(user)
                    };
                }

                throw new RestException(HttpStatusCode.BadRequest, new { User = "Error confirming your email." });
            }
        }
    }
}