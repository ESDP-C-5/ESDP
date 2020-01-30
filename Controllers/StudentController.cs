using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRM.Helpers;
using CRM.Helpers.SortHelper;
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
        private readonly BranchService _branchService;
        private readonly CommentService _commentService;


        public StudentController(StudentService studentService, BranchService branchService, CommentService commentService)
        {
            _studentService = studentService;
            _branchService = branchService;
            _commentService = commentService;

        }

        public async Task<ActionResult> SelectArchiveStudentsByBranchId(SortingEnum sort, int id)
        {
            var students = await _studentService.GetArchiveStudentsByBranchIdAsync(id, sort);
            ViewBag.BranchId = id;
            return PartialView("_ArchiveStudents", students);
        }
        public async Task<ActionResult> SelectStudingStudentsByBranchId(SortingEnum sort, int id)
        {
            var students = await _studentService.GetStudingStudentsByBranchIdAsync(id,sort);
            ViewBag.BranchId = id;
            return PartialView("_StudingStudents", students);
        }
        public async Task<ActionResult> SelectTrialStudentsByBranchId(SortingEnum sort, int id)
        {
            var students = await _studentService.GetTrialStudentsByBranchIdAsync(id, sort);
            ViewBag.BranchId = id;
            return PartialView("_TrialStudents", students);
        }

        public async Task<ActionResult> SelectLeadStudents(SortingEnum sortState = SortingEnum.LastNameAsc)
        {
            var students = await _studentService.SelectLeadStudentsAsync(sortState);
            return View(students);
        }
        public async Task<ActionResult> SelectTrialStudents(SortingEnum sortState = SortingEnum.LastNameAsc)
        {
            var students = await _studentService.SelectTrialStudentsAsync(sortState);
            ViewData["Branches"] = await _branchService.GetAllBranch();
            return View(students);
        }
        public async Task<ActionResult> SelectStudyingStudents(SortingEnum sortState = SortingEnum.LastNameAsc)
        {
            var students = await _studentService.SelectStudyingStudentsAsync(sortState);
            ViewData["Branches"] = await _branchService.GetAllBranch();
            return View(students);
        }
        
        public async Task<ActionResult> Index(SortingEnum sortState = SortingEnum.LastNameAsc)
        {
            StudentViewModel st = SortStudents.Sort(await _studentService.GetAllStudentsByArchive(), sortState);
            st.branches = await _branchService.GetAllBranch();
            return View(st);
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
        public async Task<ActionResult> Create(CreateStudentViewModel student)
        {
            if (ModelState.IsValid)
            {
                Student newStudent = new Student()
                {
                    Name = student.Name,
                    LastName = student.LastName,
                    FatherName = student.FatherName,
                    PhoneNumber = student.PhoneNumber,
                    DateOfBirthday = student.DateOfBirthday,
                    TrialDate = student.TrialDate,
                    ParentName = student.ParentName,
                    ParentLastName = student.ParentLastName,
                    ParentFatherName = student.ParentFatherName
                };
                var studentId = await _studentService.CreateAsyncReturnId(student);
                if(student.Comment != null)
                {
                    Comment comment = new Comment
                    {
                        StudentId = studentId,
                        Text = student.Comment,
                        Create = DateTime.Now
                    };
                    await _commentService.CreateAsync(comment);
                }
                
                return RedirectToAction(nameof(SelectLeadStudents));
            }

            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            var model = Mapper.Map<EditStudentViewModel>(student);

            model.Levels = new SelectList(_studentService.GetAllLevel(), "Id", "Name");
            model.StudentStatusEnum = student.Status;
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
        public async Task<ActionResult> Edit(EditStudentViewModel student)
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

        public async Task<IActionResult> List(string value,
            SortingEnum sortState = SortingEnum.LastNameAsc)
        {
            ViewBag.value = value;
            var students = await _studentService.SearchAsync(value, sortState);

            return View(students);
        }

        [Produces("application/json")]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var students = await _studentService.SearchAsync(term);
                var names = students.Student.Select(p => p.LastName ?? p.Name ?? p.PhoneNumber ?? p.ParentLastName).ToList();
                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }

        public async Task<JsonResult> AddStudent(string name, string lastName, string fatherName, DateTime dateOfBirth, DateTime trialDate,
            DateTime startDate, string parentName, string parentLastName, string parentFatherName, string phoneNumber, int status,int levelId, string text, int groupId)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                await _studentService.AddStudent(name, lastName, fatherName, dateOfBirth, trialDate,
                    startDate,parentName, parentLastName, parentFatherName, phoneNumber, status,levelId, text, groupId);

                return new JsonResult(StatusCode(200));
            }

            Response.StatusCode = 500;
            return new JsonResult("Укажите номер телефона");
        }
    }
}