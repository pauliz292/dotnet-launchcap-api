using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Borrower
{

    public class List
    {
        public class Query : IRequest<List<BorrowerDto>>
        {
        }

        public class Handler : IRequestHandler<Query, List<BorrowerDto>>
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

            public async Task<List<BorrowerDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var ctx_borrowers = await _context.Borrowers.ToListAsync();

                var borrowers = _mapper.Map<List<BorrowerDto>>(ctx_borrowers);
                return borrowers;
            }
        }
    }
}
