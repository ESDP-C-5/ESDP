using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Models;
using CRM.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CRM.Services
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public AccountService(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
        }

        public IQueryable<ApplicationUser> GetAllUsers()
        {
            return _userManager.Users;
        }


        public async Task<IdentityResult> CreateUserAsync(CreateUserViewModel model)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname,
                PhoneNumber = model.PhoneNumber
            };
            return await _userManager.CreateAsync(user, model.Password);
        }

        public async Task<IdentityResult> CreateRoleAsync(CreateRoleViewModel model)
        {
            var role = new IdentityRole { Name = model.RoleName };
            return await _roleManager.CreateAsync(role);
        }

        public IQueryable<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles;
        }

        public async Task<EditRoleViewModel> GetRoleForEdit(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);

            var model = new EditRoleViewModel { Id = role.Id, RoleName = role.Name };

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return model;
        }

        public async Task<IdentityResult> UpdateRoleAsync(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            role.Name = model.RoleName;
            return await _roleManager.UpdateAsync(role);
        }

        public async Task<List<UserRoleViewModel>> GetUsersInRoleAsync(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);

            var model = new List<UserRoleViewModel>();

            foreach (var user in _userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                userRoleViewModel.IsSelected = await _userManager.IsInRoleAsync(user, role.Name);

                model.Add(userRoleViewModel);
            }

            return model;
        }

        public async Task UpdateUsersInRoleAsync(List<UserRoleViewModel> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);

                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && (await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }
        }

        public async Task<EditUserViewModel> GetUserForEditAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            var userRoles = await _userManager.GetRolesAsync(user);

            return new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                PhoneNumber = user.PhoneNumber,
                Roles = userRoles
            };
        }

        public async Task<IdentityResult> UpdateUserAsync(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            user.Email = model.Email;
            user.UserName = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Name = model.Name;
            user.Surname = model.Surname;

            return await _userManager.UpdateAsync(user);
        }

        public async Task<List<UserRolesViewModel>> GetRolesInUserAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            var model = new List<UserRolesViewModel>();

            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                userRolesViewModel.IsSelected = await _userManager.IsInRoleAsync(user, role.Name);

                model.Add(userRolesViewModel);
            }

            return model;
        }

        public async Task UpdateRolesInUserAsync(List<UserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);

            await _userManager
                .AddToRolesAsync(user, model.Where(x => x.IsSelected)
                    .Select(y => y.RoleName));
        }

        public async Task<IdentityResult> ChangePassword(ApplicationUser user, string password)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, password);
            return await _userManager.UpdateAsync(user);
        }

        public void ErrorWriter(IEnumerable<IdentityError> resultErrors, ModelStateDictionary modelState)
        {
            foreach (var error in resultErrors)
            {
                modelState.AddModelError("", error.Description);
            }
        }

        public string PasswordGenerator()
        {
            var options = _userManager.Options.Password;

            int length = options.RequiredLength;

            bool nonAlphanumeric = options.RequireNonAlphanumeric;
            bool digit = options.RequireDigit;
            bool lowercase = options.RequireLowercase;
            bool uppercase = options.RequireUppercase;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);

                password.Append(c);

                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));

            return password.ToString();
        }
    }
}
