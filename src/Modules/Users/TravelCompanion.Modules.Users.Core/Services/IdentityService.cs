using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TravelCompanion.Modules.Users.Core.DTO;
using TravelCompanion.Modules.Users.Core.Entities;
using TravelCompanion.Modules.Users.Core.Exceptions;
using TravelCompanion.Modules.Users.Core.Repositories;
using TravelCompanion.Shared.Abstractions.Auth;
using TravelCompanion.Shared.Abstractions.Emails;
using TravelCompanion.Shared.Abstractions.Time;

namespace TravelCompanion.Modules.Users.Core.Services
{
    internal class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAuthManager _authManager;
        private readonly IClock _clock;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _contextAccessor;
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly Random Random = new Random();

        public IdentityService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher,
            IAuthManager authManager, IClock clock, IEmailSender emailSender, IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _authManager = authManager;
            _clock = clock;
            _emailSender = emailSender;
            _contextAccessor = contextAccessor;
        }

        public async Task<AccountDto> GetAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);

            return user is null
                ? null
                : new AccountDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = user.Role,
                    Claims = user.Claims,
                    CreatedAt = user.CreatedAt
                };
        }

        public async Task<JsonWebToken> SignInAsync(SignInDto dto)
        {
            var user = await _userRepository.GetAsync(dto.Email.ToLowerInvariant());
            if (user is null)
            {
                throw new InvalidCredentialsException();
            }

            if (_passwordHasher.VerifyHashedPassword(default, user.Password, dto.Password) ==
                PasswordVerificationResult.Failed)
            {
                throw new InvalidCredentialsException();
            }

            if (!user.IsActive)
            {
                throw new UserNotActiveException(user.Id);
            }

            var jwt = _authManager.CreateToken(user.Id.ToString(), user.Role, claims: user.Claims);
            jwt.Email = user.Email;

            return jwt;
        }

        public async Task SignUpAsync(SignUpDto dto)
        {
            dto.Id = Guid.NewGuid();
            var email = dto.Email.ToLowerInvariant();
            var user = await _userRepository.GetAsync(email);
            if (user is not null)
            {
                throw new EmailInUseException();
            }

            var password = _passwordHasher.HashPassword(default, dto.Password);
            user = new User
            {
                Id = dto.Id,
                Email = email,
                Password = password,
                Role = dto.Role?.ToLowerInvariant() ?? "user",
                CreatedAt = _clock.CurrentDate(),
                IsActive = false,
                Claims = dto.Claims ?? new Dictionary<string, IEnumerable<string>>(),
                VerificationToken = CreateRandomToken(64)
            };
            await _userRepository.AddAsync(user);

            var activationLink = CreateActivationLink(user.VerificationToken);
            await _emailSender.SendEmailAsync(new AccountVerificationEmailDto(activationLink), user.Email);
        }

        public async Task ActivateAccountAsync(string token)
        {
            var user = await _userRepository.GetByTokenAsync(token);

            if (user is null)
            {
                throw new InvalidVerificationTokenException();
            }

            if (user.IsActive)
            {
                throw new AccountAlreadyActivatedException();
            }

            user.IsActive = true;
            user.VerifiedAt = _clock.CurrentDate();

            await _userRepository.UpdateAsync(user);
        }

        public static string CreateRandomToken(int length)
        {
            return CreateRandomToken(Alphabet, length);
        }

        public static string CreateRandomToken(string characters, int length)
        {
            return new string(Enumerable
                .Range(0, length)
                .Select(num => characters[Random.Next() % characters.Length])
                .ToArray());
        }

        private string CreateActivationLink(string verificationToken)
        {
            var request = _contextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";

            return $"{baseUrl}/users-module/Account/activate/{verificationToken}";
        }
    }
}
