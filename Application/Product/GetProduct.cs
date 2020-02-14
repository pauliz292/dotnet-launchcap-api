using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Product
{

    public class GetProduct
    {
        public class Query : IRequest<ProductDto>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ProductDto>
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

            public async Task<ProductDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var ctx_product = await _context.Products.FindAsync(request.Id);

                if (ctx_product == null)
                    throw new RestException(HttpStatusCode.BadRequest, "Product not found.");

                var product = _mapper.Map<ProductDto>(ctx_product);

                return product;
            }
        }
    }
}
