using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Models;
using CRM.Services;
using CRM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AccountService _accountService;
        public UserManagementController(
            UserManager<ApplicationUser> userManager, 
            AccountService accountService)
        {
            _userManager = userManager;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = _accountService.GetAllUsers();

            return View(users);
        }

        //Create new user => add passwordGenerator
        [HttpGet]
        public IActionResult Register()
        {
            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.CreateUserAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                _accountService.ErrorWriter(result.Errors, ModelState);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.CreateRoleAsync(model); 

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "UserManagement");
                }

                _accountService.ErrorWriter(result.Errors, ModelState);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _accountService.GetAllRoles();

            return View(roles);
        }
        
        [HttpGet]
        public async Task<IActionResult> EditRole(string Id)
        {
            var model = await _accountService.GetRoleForEdit(Id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.UpdateRoleAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                _accountService.ErrorWriter(result.Errors, ModelState);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string Id)
        {
            ViewBag.roleId = Id;

            var model = await _accountService.GetUsersInRoleAsync(Id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            await _accountService.UpdateUsersInRoleAsync(model, roleId);

            return RedirectToAction("EditRole", new {Id = roleId});
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string Id)
        {
            var model = await _accountService.GetUserForEditAsync(Id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.UpdateUserAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                _accountService.ErrorWriter(result.Errors, ModelState);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string Id)
        {
            ViewBag.userId = Id;

            var model = await _accountService.GetRolesInUserAsync(Id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesViewModel> model, string userId)
        {
            await _accountService.UpdateRolesInUserAsync(model, userId);

            return RedirectToAction("EditUser", new {Id = userId});
        }

        public async Task<IActionResult> ChangePassword(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            var model = new ChangePasswordViewModel{UserId = user.Id, Email = user.Email};

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                
                if (!string.IsNullOrEmpty(model.Password))
                {
                    var result = await _accountService.ChangePassword(user, model.Password);
                    if (result.Succeeded)
                        return RedirectToAction("EditUser", new { Id = user.Id });
                }
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                
            }

            return View(model);
        }

        [HttpGet]
        public string GeneratePassword()
        {
            var password = _accountService.PasswordGenerator();

            return password;
        }
    }
}