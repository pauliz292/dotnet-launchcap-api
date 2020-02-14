using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Category
{

    public class Update
    {
        public class Command : IRequest
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()

            {
                RuleFor(x => x.Name).NotEmpty();
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
                var ctx_category = await _context.Categories.FindAsync(request.Id);

                if (ctx_category == null)
                    throw new RestException(HttpStatusCode.BadRequest, "Category not found.");

                //_mapper.Map(request, ctx_category);
                var category = _mapper.Map<Domain.Models.Category>(request);
                _context.Entry(ctx_category).CurrentValues.SetValues(category);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
