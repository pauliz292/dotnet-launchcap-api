using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Validators;
using Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User
{
    public class ChangePassword
    {
        public class Command : IRequest
        {
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.OldPassword).Password();
                RuleFor(x => x.NewPassword).Password();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IUserAccessor _userAccessor;
            public Handler(UserManager<AppUser> userManager, IUserAccessor userAccessor)
            {
                this._userAccessor = userAccessor;
                this._userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());

           
                var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

                if (result.Succeeded)
                {
                    return Unit.Value;
                }

                return Unit.Value;
            }
        }
    }
}