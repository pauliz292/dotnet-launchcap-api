using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Product
{

    public class List
    {
        public class Query : IRequest<List<ProductDto>>
        {
        }

        public class Handler : IRequestHandler<Query, List<ProductDto>>
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

            public async Task<List<ProductDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var ctx_products = await _context.Products.ToListAsync();

                var products = _mapper.Map<List<ProductDto>>(ctx_products);
                return products;
            }
        }
    }
}
