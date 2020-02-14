using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User
{
    public class CurrentUser
    {
        public class Query : IRequest<UserDto> { }

        public class Handler : IRequestHandler<Query, UserDto>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IUserAccessor _userAccessor;
            public Handler(UserManager<AppUser> userManager, IJwtGenerator jwtGenerator, IUserAccessor userAccessor)
            {
                this._userAccessor = userAccessor;
                this._jwtGenerator = jwtGenerator;
                this._userManager = userManager;
            }

            public async Task<UserDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());

                return new UserDto
                {
                    Username = user.UserName,
                    Token = _jwtGenerator.CreateToken(user),
                };
            }
        }
    }
}