using Microsoft.AspNetCore.Identity;
using MineSweeper.Application.Interfaces;
using MineSweeper.Application.ViewModels;
using MineSweeper.Domain.Entities;
using MineSweeper.Domain.Interfaces.Context;
using MineSweeper.Domain.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace MineSweeper.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _repository;
        private readonly IUnitOfWork _uow;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public UserAppService(IUserRepository repository,
                              UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IUnitOfWork uow)
        {
            _uow = uow;
            _userManager = userManager;
            _signInManager = signInManager;
            _repository = repository;
        }

        public async Task<bool> CreateUser(UserViewModel user)
        {
            User newUser = new User(user.Email);

            IdentityResult createResult = await _userManager.CreateAsync(newUser, user.Password);

            if (!createResult.Succeeded)
                throw new ArgumentException("Occurred a problem to create user. Check email and password!");

            return true;
        }
    }
}
