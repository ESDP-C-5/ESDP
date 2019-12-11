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