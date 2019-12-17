using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models;
using CRM.Services;
using CRM.UoW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Controllers
{
    public class BranchController : Controller
    {
        private readonly BranchService _branchService;
        private readonly StudentService _studentService;
        private readonly GroupService _groupService;
        private readonly UserManager<ApplicationUser> _userManager;

        public BranchController(BranchService branchService, GroupService groupService, StudentService studentService, UserManager<ApplicationUser> userManager)
        {
            _branchService = branchService;
            _groupService = groupService;
            _studentService = studentService;
            _userManager = userManager;
        }

        // GET: Leve
        public async Task<ActionResult> Index()
        {
            var branchs = await _branchService.GetAllBranch();

            return View(branchs);
        }

        // GET: Leve/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var branch = await _branchService.GetById(id);
            return View(branch);
        }

        // GET: Leve/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Leve/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Branch branch)
        {
            if (ModelState.IsValid)
            {
                await _branchService.CreateBranch(branch);
                return RedirectToAction(nameof(Index));
            }
            return View(branch);
        }

        // GET: Leve/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var branch = await _branchService.GetById(id);
            return View(branch);
        }

        // POST: Leve/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Branch branch)
        {
            try
            {
                await _branchService.EditBranch(branch);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Leve/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
           var branch = await _branchService.GetById(id);
            return View(branch);
        }

        // POST: Leve/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Branch branch)
        {
            try
            {
                await _branchService.DeleteBranch(branch);
                return RedirectToAction(nameof(Index));
            } 
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
        public async Task<IActionResult> SelectGroupesByBranchId(int Id)
        {
            var groupes = await _groupService.GetGroupesByBranchIdAsync(Id);
            ViewData["BranchId"] = Id;
            return PartialView("_ViewsListGroupes", groupes);
        }
        public async Task<IActionResult> SelectStudentsBuGroupId(int Id)
        {
            var students = await _studentService.GetAllStudentsByGroupIdAsync(Id);
            return PartialView("_ViewsListStunets", students);
        }

        public async Task<ActionResult> ShowFormForAddGroup(int id)
        {
            CreateGroupViewModel createGroupViewModel = new CreateGroupViewModel();
            createGroupViewModel.BranchId = id;
            createGroupViewModel.Users = new SelectList(_userManager.Users.ToList(), "Id", "Email");
            return PartialView("_AddGroupForm", createGroupViewModel);
        }

        public async Task<ActionResult> AddGroup(CreateGroupViewModel groupViewModel)
        {
            await _branchService.CreateGroupAndTimeTable(groupViewModel);
            return RedirectToAction("Index");
        }
    }
}
