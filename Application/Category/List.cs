using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Category
{

    public class List
    {
        public class Query : IRequest<List<CategoryDto>>
        {
        }

        public class Handler : IRequestHandler<Query, List<CategoryDto>>
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

            public async Task<List<CategoryDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var ctx_categories = await _context.Categories.ToListAsync();

                var categories = _mapper.Map<List<CategoryDto>>(ctx_categories);
                return categories;
            }
        }
    }
}
