using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models;
using CRM.UoW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    public class BranchController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public BranchController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Leve
        public async Task<ActionResult> Index()
        {
            var branchUow = _unitOfWork.Branchs;
            var branchs = await branchUow.GetAllAsync();

            return View(branchs);
        }

        // GET: Leve/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var branchUow = _unitOfWork.Branchs;
            var branch = await branchUow.GetByIdAsync(id);
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
                var branchUow = _unitOfWork.Branchs;
                await branchUow.CreateAsync(branch);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(branch);
        }

        // GET: Leve/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var branchUow = _unitOfWork.Branchs;
            var branch = await branchUow.GetByIdAsync(id);
            return View(branch);
        }

        // POST: Leve/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Branch branch)
        {
            try
            {
                var branchUow = _unitOfWork.Branchs;
                branchUow.UpdateAsync(branch);
                await _unitOfWork.CompleteAsync();
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
            var branchUow = _unitOfWork.Branchs;
            var branch = await branchUow.GetByIdAsync(id);
            return View(branch);
        }

        // POST: Leve/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Branch branch)
        {
            try
            {
                var branchUow = _unitOfWork.Branchs;
                branchUow.RemoveAsync(branch);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            } 
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
