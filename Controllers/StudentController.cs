﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models;
using CRM.Services;
using CRM.UoW;
using CRM.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;
        private readonly BranchService _branchService;


        public StudentController(StudentService studentService, BranchService branchService)
        {
            _studentService = studentService;
            _branchService = branchService;

        }

        public async Task<ActionResult> SelectArchiveStudentsByBranchId(int id, Helpers.StudentStatusEnum? status)
        {
            var students = await _studentService.GetArchiveStudentsByBranchIdAsync(id, status);
            return PartialView("_ArchiveStudents", students);
        }

        public async Task<ActionResult> SelectLeadStudents()
        {
            var students = await _studentService.SelectLeadStudentsAsync();
            return PartialView("_ArchiveLeadsStudents", students);
        }
        
        public async Task<ActionResult> Index()
        {
            BranchesWithStudentsViewModel branchesWithStudents = new BranchesWithStudentsViewModel()
            {
                students = await _studentService.SelectLeadStudentsAsync(),

                branches = await _branchService.GetAllBranch()
            };
            return View(branchesWithStudents);
        }

        // GET: Student/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var students = await _studentService.GetByIdAsync(id);
            return View(students);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                await _studentService.CreateAsync(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Student student)
        {
            try
            {
                await _studentService.EditAsync(student);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Student/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Student student)
        {
            try
            {
                await _studentService.DeleteAsync(student);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> List(string column, string value)
        {
            var students = await _studentService.SearchAsync(column, value);
            return View(students);
        }

        public IActionResult SearchAjax()
        {
            return View();
        }

        public async Task<IActionResult> SearchAjaxResult(string column, string keyWord)
        {
            var students = await _studentService.SearchAsync(column, keyWord);

            return PartialView("UserSearchResult", students);
        }
    }
}