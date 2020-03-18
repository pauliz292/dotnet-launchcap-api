using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Deal
{

    public class List
    {
        public class Query : IRequest<List<DealDto>>
        {
        }

        public class Handler : IRequestHandler<Query, List<DealDto>>
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

            public async Task<List<DealDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var ctx_deals = await _context.Deals.ToListAsync();

                var deals = _mapper.Map<List<DealDto>>(ctx_deals);
                return deals;
            }
        }
    }
}
