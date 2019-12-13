﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRM.Models;
using CRM.Services;
using CRM.UoW;
using CRM.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: Leve
        public async Task<ActionResult> Index()
        {
            var student = await _studentService.GetAllStudents();

            return View(student);
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
            var createStudentViewModel = new CreateStudentViewModel
            {
                Levels = new SelectList(_studentService.GetAllLevel(), "Id", "Name")
            };
            return View(createStudentViewModel);
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

            return RedirectToAction(nameof(Index));
        }

        // GET: Student/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            var model = Mapper.Map<EditStudentViewModel>(student);

            model.Levels = new SelectList(_studentService.GetAllLevel(), "Id", "Name");
            model.Groups = new SelectList(_studentService.GetAllGroup().Select(x => new
            {
                Id = x.Id,
                Group = $"{x.Branch.Name}-{x.TimeTable.Day1}-{x.TimeTable.Day2}-{x.TimeTable.Time}"
            }), "Id", "Group", null);
                
                    
            
            return View(model);
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
    }
}