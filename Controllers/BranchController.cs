using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models;
using CRM.Services;
using CRM.UoW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    public class BranchController : Controller
    {
        private readonly BranchService _branchService;
        private readonly GroupService _groupService;

        public BranchController(BranchService branchService, GroupService groupService)
        {
            _branchService = branchService;
            _groupService = groupService;
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
            return PartialView("_ViewsListGroupes", groupes);
        }
        
    }
}
