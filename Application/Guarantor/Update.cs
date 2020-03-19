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
using System.Threading;
using System.Threading.Tasks;

namespace Application.Guarantor
{

    public class Update
    {
        public class Command : IRequest
        {
            public int Id { get; set; }

            public string FirstName { get; set; }

            public string MiddleName { get; set; }

            public string LastName { get; set; }

            public string ContactNumber { get; set; }

            public string Email { get; set; }

            public string Company { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
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
                var ctx_guarantor = await _context.Guarantors.FindAsync(request.Id);

                if (ctx_guarantor == null)
                    throw new RestException(HttpStatusCode.BadRequest, "Property not found.");

                var guarantor = _mapper.Map<Domain.Models.Guarantor>(request);
                _context.Entry(ctx_guarantor).CurrentValues.SetValues(guarantor);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
