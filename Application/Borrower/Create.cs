using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using Application.User;
using Application.Property;

namespace Application.Borrower
{
    public class Create
    {
        public class Command : IRequest
        {
            public string Name { get; set; }

            public string Address { get; set; }

            public string Email { get; set; }

            public string ContactNumber { get; set; }

            public string ACN { get; set; }

            public virtual List<UserDetailDto> Users { get; set; }

            public virtual List<PropertyDto> Properties { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Address).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.ContactNumber).NotEmpty();
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

                var borrower = _mapper.Map<Domain.Models.Borrower>(request);

                _context.Borrowers.Add(borrower);

                var success = await _context.SaveChangesAsync()> 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
