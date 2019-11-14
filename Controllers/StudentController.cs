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
    public class StudentController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public StudentController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Leve
        public async Task<ActionResult> Index()
        {
            var studentUow = _unitOfWork.Student; 
            var student = await studentUow.GetAllAsync();

            return View(student);
        }

        // GET: Student/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var studentUow = _unitOfWork.Student;
            var students = await studentUow.GetByIdAsync(id);
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
                var studentUow = _unitOfWork.Student;
                await studentUow.CreateAsync(student);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Edit/5
        public async Task< ActionResult> Edit(int id)
        {
            var studentUow = _unitOfWork.Student;
            var student = await studentUow.GetByIdAsync(id);
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Student student)
        {
            try
            {
                var studentUow = _unitOfWork.Student;
                studentUow.UpdateAsync(student);
                await _unitOfWork.CompleteAsync();
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
            var studentUow = _unitOfWork.Student;
            var student = await studentUow.GetByIdAsync(id);
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Student student)
        {
            try
            {
                var studentUow = _unitOfWork.Student;
                studentUow.RemoveAsync(student);
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