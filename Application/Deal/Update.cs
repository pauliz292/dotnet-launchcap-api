using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Deal
{

    public class Update
    {
        public class Command : IRequest
        {
            public int Id { get; set; }

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

            public List<Domain.Models.Guarantor> Guarantors { get; set; }
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
                var ctx_deal = await _context.Deals.FindAsync(request.Id);

                if (ctx_deal == null)
                    throw new RestException(HttpStatusCode.BadRequest, "Property not found.");

                if (ctx_deal.Guarantors.Any())
                {
                    _context.Guarantors.RemoveRange(ctx_deal.Guarantors);
                    await _context.SaveChangesAsync();
                }

                var deal = _mapper.Map<Domain.Models.Deal>(request);
                _context.Entry(ctx_deal).CurrentValues.SetValues(deal);

                request.Guarantors.ToList().ForEach(x => 
                {
                    x.Id = 0;
                    ctx_deal.Guarantors.Add(x);
                });

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
