using Application.Borrower;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Guarantor
{

    public class List
    {
        public class Query : IRequest<List<GuarantorDto>>
        {
        }

        public class Handler : IRequestHandler<Query, List<GuarantorDto>>
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

            public async Task<List<GuarantorDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var ctx_guarantors = await _context.Guarantors.ToListAsync();

                var guarantors = _mapper.Map<List<GuarantorDto>>(ctx_guarantors);
                return guarantors;
            }
        }
    }
}
