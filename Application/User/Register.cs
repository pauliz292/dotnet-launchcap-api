using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Application.Validators;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;

namespace Application.User
{
    public class Register
    {
        public class Command : IRequest<UserDto>
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public Gender Gender { get; set; }
            public DateTime Birthday { get; set; }
            public string Occupation { get; set; }
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
            public string Role { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Username).Username();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).Password();
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.Gender).NotEmpty();
                RuleFor(x => x.Address).NotEmpty();
                RuleFor(x => x.PhoneNumber).NotEmpty();
                RuleFor(x => x.Role).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, UserDto>
        {
            private readonly DataContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IEmailSender _emailSender;
            private readonly string _clientUrl;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, UserManager<AppUser> userManager, IJwtGenerator jwtGenerator, IEmailSender emailSender, IConfiguration config, IMapper mapper, IUserAccessor userAccessor)
            {
                this._userAccessor = userAccessor;
                this._mapper = mapper;
                this._clientUrl = config["ClientUrl"];
                this._jwtGenerator = jwtGenerator;
                this._emailSender = emailSender;
                this._userManager = userManager;
                _context = context;
            }

            public async Task<UserDto> Handle(Command request, CancellationToken cancellationToken)
            {

                if (await _context.Users.Where(x => x.Email == request.Email).AnyAsync())
                    throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exists" });

                if (await _context.Users.Where(x => x.UserName == request.Username).AnyAsync())
                    throw new RestException(HttpStatusCode.BadRequest, new { Username = "Username already exists" });

                var user = _mapper.Map<AppUser>(request);
                user.IsActive = true;

                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, request.Role);

                    var emailConfirmCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    emailConfirmCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(emailConfirmCode));

                    var callbackUrl = $"{_clientUrl}/confirm-email/{user.Id}/{emailConfirmCode}";

                    _emailSender.SendEmail(request.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    return new UserDto
                    {
                        Username = user.UserName,
                        Role = request.Role
                    };
                }

                throw new Exception("Problem saving changes");
            }
        }
    }
}