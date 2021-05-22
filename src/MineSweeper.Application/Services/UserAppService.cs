using Microsoft.AspNetCore.Identity;
using MineSweeper.Application.Interfaces;
using MineSweeper.Application.ViewModels;
using MineSweeper.Domain.Entities;
using MineSweeper.Domain.Interfaces.Context;
using MineSweeper.Domain.Interfaces.Facades;
using MineSweeper.Domain.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace MineSweeper.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _repository;
        private readonly IUnitOfWork _uow;
        private readonly IAuthFacade _facadeAuth;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserAppService(IUserRepository repository,
                              IAuthFacade facadeAuth,
                              UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IUnitOfWork uow)
        {
            _uow = uow;
            _facadeAuth = facadeAuth;
            _userManager = userManager;
            _signInManager = signInManager;
            _repository = repository;
        }

        public async Task<bool> CreateUser(UserViewModel user)
        {
            User newUser = new User(user.Email);

            IdentityResult createResult = await _userManager.CreateAsync(newUser, user.Password);

            if (!createResult.Succeeded)
                throw new ArgumentException("Occurred a problem to create user. Check email and password! " +
                    "The password must be at least 8 chars length, have a special character and a number!");

            return true;
        }

        public async Task<string> Login(UserViewModel userLogin)
        {
            var loginResult = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, false);

            if (!loginResult.Succeeded)
                throw new ArgumentException("Invalid data. Check email and password!");

            User user = await _repository.GetByEmail(userLogin.Email);

            return _facadeAuth.GenerateToken(user.Id, user.Email);
        }
    }
}
