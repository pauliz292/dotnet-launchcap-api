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

namespace Application.Category
{

    public class GetCategory
    {
        public class Query : IRequest<CategoryDto>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, CategoryDto>
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

            public async Task<CategoryDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var ctx_category = await _context.Categories.FindAsync(request.Id);

                if (ctx_category == null)
                    throw new RestException(HttpStatusCode.BadRequest, "Category not found.");

                var category = _mapper.Map<CategoryDto>(ctx_category);

                return category;
            }
        }
    }
}
