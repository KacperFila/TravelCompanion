﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TravelCompanion.Modules.Users.Core.DTO;
using TravelCompanion.Modules.Users.Core.Entities;
using TravelCompanion.Modules.Users.Core.Exceptions;
using TravelCompanion.Modules.Users.Core.Repositories;
using TravelCompanion.Modules.Users.Shared.DTO;
using TravelCompanion.Shared.Abstractions.Auth;
using TravelCompanion.Shared.Abstractions.Emails;
using TravelCompanion.Shared.Abstractions.Time;

namespace TravelCompanion.Modules.Users.Core.Services
{
    internal class IdentityService : IIdentityService
    {
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly Random Random = new();
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAuthManager _authManager;
        private readonly IClock _clock;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<IdentityService> _logger;

        public IdentityService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher,
            IAuthManager authManager, IClock clock, IEmailSender emailSender, IHttpContextAccessor contextAccessor, ILogger<IdentityService> logger)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _authManager = authManager;
            _clock = clock;
            _emailSender = emailSender;
            _contextAccessor = contextAccessor;
            _logger = logger;
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
                    ActivePlanId = user.ActivePlanId,
                    ActiveTravelId = user.ActiveTravelId,
                    CreatedOnUtc = user.CreatedOnUtc,
                    ModifiedOnUtc = user.ModifiedOnUtc
                };
        }

        public async Task<JsonWebToken> SignInAsync(SignInDto dto)
        {
            _logger.LogInformation("Signing in...");
            
            if (dto.Email is null || dto.Password is null)
            {
                throw new InvalidCredentialsException();
            }

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
                throw new UserNotActiveException();
            }

            var jwt = _authManager.CreateToken(user.Id.ToString(), user.Role, user.Email, claims: user.Claims);
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

        public async Task<List<UserInfoDto>> BrowseActiveUsersAsync()
        {
            var users = await _userRepository.BrowseActiveAsync();
            var userInfoDto = users.Select(x => new UserInfoDto()
            {
                UserId = x.Id,
                Email = x.Email,
                UserName = x.Email.Split("@")[0],
                ActivePlanId = x.ActivePlanId,
                ActiveTravelId = x.ActiveTravelId
            });
            return userInfoDto.ToList();
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
