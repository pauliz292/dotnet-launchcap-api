using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Product
{

    public class Update
    {
        public class Command : IRequest
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public string ImagePath { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()

            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command>
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

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var ctx_product = await _context.Products.FindAsync(request.Id);

                if (ctx_product == null)
                    throw new RestException(HttpStatusCode.BadRequest, "Product not found.");

                //_mapper.Map(request, ctx_product);
                var product = _mapper.Map<Domain.Models.Product>(request);
                _context.Entry(ctx_product).CurrentValues.SetValues(product);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
