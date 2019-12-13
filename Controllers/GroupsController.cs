﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRM.Data;
using CRM.Models;
using CRM.Services;
using CRM.UoW;
using CRM.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CRM.Controllers
{
    public class GroupsController : Controller
    {
        private readonly GroupService _groupService;
        private readonly UserManager<ApplicationUser> _userManager;

        public GroupsController(GroupService groupService, UserManager<ApplicationUser> userManager)
        {
            _groupService = groupService;
            _userManager = userManager;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            var groupsList = await _groupService.GetAllAsync();
            return View(groupsList);
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var group = await _groupService.GetByIdAsync(id.Value);

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
                TimeTables = new SelectList(_groupService.GetAllTimeTables().
                    Select(x=> new
                    {
                        Id = x.Id,
                        Day = $"{x.Day1}-{x.Day2}"
                    }), "Id", "Day", null),
                Branches = new SelectList(_groupService.GetAllBranches(), "Id", "Name"),
                Users = new SelectList(_userManager.Users.ToList(), "Id", "Email")
            };
            return View(createGroupViewModel);
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Group group)
        {
            if (ModelState.IsValid)
            {
                
                var createdGroup = await _groupService.CreateAsync(group);
                return RedirectToAction(nameof(Index), createdGroup);
            }
            
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var group = await _groupService.GetByIdAsync(id.Value);

            if (group == null)
            {
                return NotFound();
            }

            var model = Mapper.Map<EditGroupViewModel>(group);
            model.Branches = new SelectList(_groupService.GetAllBranches(), "Id", "Name", model.BranchId);
            model.TimeTables = new SelectList(_groupService.GetAllTimeTables(), "Id", "Day1", model.TimeTableId);
            model.Users = new SelectList(_userManager.Users.ToList(), "Id", "Email", model.UserId);
            
            return View(model);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Group group)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _groupService.EditAsync(group);
                    
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return BadRequest(ex.Message);
                   
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var group = await _groupService.GetByIdAsync(id.Value);

            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Group group)
        {
            await _groupService.DeleteAsync(group);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
