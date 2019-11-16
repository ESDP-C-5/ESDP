using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRM.Data;
using CRM.Models;
using CRM.UoW;
using CRM.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CRM.Controllers
{
    public class GroupsController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public GroupsController(UnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            var groupsList = await _unitOfWork.Groups.GetAllAsync();
            return View(groupsList);
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var group = await _unitOfWork.Groups.GetByIdAsync(id.Value);

            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            var createGroupViewModel = new CreateGroupViewModel
            {
                Levels = new SelectList(_unitOfWork.Levels.GetAll(), "Id", "Name"),
                Branches = new SelectList(_unitOfWork.Branchs.GetAll(), "Id", "Name"),
                Users = new SelectList(_userManager.Users.ToList(), "Id", "Email")
            };
            return View(createGroupViewModel);

            //ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Id");
            //ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "Id");
            //ViewData["TimeTableId"] = new SelectList(_context.Set<TimeTable>(), "Id", "Id");
            //return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                var group =  Mapper.Map<Group>(model);
                await _unitOfWork.Groups.CreateAsync(group);
                await  _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Id", @group.BranchId);
            //ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "Id", @group.LevelId);
            //ViewData["TimeTableId"] = new SelectList(_context.Set<TimeTable>(), "Id", "Id", @group.TimeTableId);
            return View(model);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var group = await _unitOfWork.Groups.GetByIdAsync(id.Value);

            if (group == null)
            {
                return NotFound();
            }

            var model = Mapper.Map<EditGroupViewModel>(group);
            model.Branches = new SelectList(_unitOfWork.Branchs.GetAll(), "Id", "Name", model.BranchId);
            model.Levels = new SelectList(_unitOfWork.Levels.GetAll(), "Id", "Name", model.LevelId);
            model.Users = new SelectList(_userManager.Users.ToList(), "Id", "Email", model.UserId);


            //ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Id", @group.BranchId);
            //ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "Id", @group.LevelId);
            //ViewData["TimeTableId"] = new SelectList(_context.Set<TimeTable>(), "Id", "Id", @group.TimeTableId);
            return View(model);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var group = Mapper.Map<Group>(model);
                    _unitOfWork.Groups.UpdateAsync(group);
                    await _unitOfWork.CompleteAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return BadRequest(ex.Message);

                    //if (!GroupExists(@group.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            model.Branches = new SelectList(_unitOfWork.Branchs.GetAll(), "Id", "Name", model.BranchId);
            model.Levels = new SelectList(_unitOfWork.Levels.GetAll(), "Id", "Name", model.LevelId);
            model.Users = new SelectList(_userManager.Users.ToList(), "Id", "Email", model.UserId);
            return View(model);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var group = await _unitOfWork.Groups.GetByIdAsync(id.Value);

            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var group = await _unitOfWork.Groups.GetByIdAsync(id);
            _unitOfWork.Groups.RemoveAsync(group);
            await _unitOfWork.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return _unitOfWork.Groups.GetByIdAsync(id) != null;
        }
    }
}
