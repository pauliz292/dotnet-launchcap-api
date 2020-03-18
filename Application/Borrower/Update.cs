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

namespace Application.Borrower
{

    public class Update
    {
        public class Command : IRequest
        {
            public int Id { get; set; }

            public string Name { get; set; }
            
            public string Address { get; set; }

            public string Email { get; set; }

            public string ContactNumber { get; set; }

            public string ACN { get; set; }

            public List<AppUser> Users { get; set; }

            public List<Domain.Models.Property> Properties { get; set; }
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
                var ctx_borrower = await _context.Borrowers.FindAsync(request.Id);

                if (ctx_borrower == null)
                    throw new RestException(HttpStatusCode.BadRequest, "Property not found.");

                if (ctx_borrower.Users.Any())
                {
                    _context.Users.RemoveRange(ctx_borrower.Users);
                    await _context.SaveChangesAsync();
                }

                if (ctx_borrower.Properties.Any())
                {
                    _context.Properties.RemoveRange(ctx_borrower.Properties);
                    await _context.SaveChangesAsync();
                }

                var borrower = _mapper.Map<Domain.Models.Borrower>(request);
                _context.Entry(ctx_borrower).CurrentValues.SetValues(borrower);

                request.Users.ToList().ForEach(x => 
                {
                    x.Id = 0;
                    ctx_borrower.Users.Add(x);
                });

                request.Properties.ToList().ForEach(x => 
                {
                    x.Id = 0;
                    ctx_borrower.Properties.Add(x);
                });

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
