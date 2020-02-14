using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.User
{
    public class List
    {
        public class Query : IRequest<IEnumerable<UserDetailDto>>
        {
            public string Role { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<UserDetailDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            private readonly UserManager<AppUser> _userManager;

            public Handler(UserManager<AppUser> userManager, DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                this._userAccessor = userAccessor;
                this._mapper = mapper;
                this._context = context;
                this._userManager = userManager;
            }

            public async Task<IEnumerable<UserDetailDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var users = await _userManager.GetUsersInRoleAsync(request.Role);

                return _mapper.Map<IEnumerable<UserDetailDto>>(users);
            }
        }
    }
}