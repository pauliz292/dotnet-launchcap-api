using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Property
{

    public class List
    {
        public class Query : IRequest<List<PropertyDto>>
        {
        }

        public class Handler : IRequestHandler<Query, List<PropertyDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                this._userAccessor = userAccessor;
                this._mapper = mapper;
                this._context = context;
            }

            public async Task<List<PropertyDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var ctx_properties = await _context.Properties.ToListAsync();

                var properties = _mapper.Map<List<PropertyDto>>(ctx_properties);
                return properties;
            }
        }
    }
}
