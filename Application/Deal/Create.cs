using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using Application.Borrower;
using Application.Guarantor;

namespace Application.Deal
{
    public class Create
    {
        public class Command : IRequest
        {
            public string Name { get; set; }

            public string Pipeline { get; set; }

            public string DealStage { get; set; }

            public decimal Amount { get; set; }

            public DateTime ClosedDate { get; set; }

            public string DealOwner { get; set; }

            public string DealType { get; set; }

            public string Purpose { get; set; }

            public string LoanTerm { get; set; }

            public int InterestRate { get; set; }

            public decimal CommitmentFee { get; set; }

            public decimal EstablishmentFee { get; set; }

            public decimal ManagementFee { get; set; }

            public decimal BrokerageFee { get; set; }

            public string GoverningLaw { get; set; }

            public int BorrowerId { get; set; }

            public BorrowerDto Borrower { get; set; }

            public List<Domain.Models.Guarantor> Guarantors { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Amount).NotEmpty();
                RuleFor(x => x.Purpose).NotEmpty();
                RuleFor(x => x.LoanTerm).NotEmpty();
                RuleFor(x => x.InterestRate).NotEmpty();
                RuleFor(x => x.CommitmentFee).NotEmpty();
                RuleFor(x => x.EstablishmentFee).NotEmpty();
                RuleFor(x => x.ManagementFee).NotEmpty();
                RuleFor(x => x.BrokerageFee).NotEmpty();
                RuleFor(x => x.BorrowerId).NotEmpty();
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

                var deal = _mapper.Map<Domain.Models.Deal>(request);

                _context.Deals.Add(deal);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
