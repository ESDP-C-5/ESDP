using CRM.Models;
using CRM.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.ViewModels;

namespace CRM.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly AttendanceService _attendanceService;
        private readonly GroupService _groupService;
        private readonly StudentService _studentService;

        public AttendanceController(AttendanceService attendanceService, GroupService groupService, StudentService studentService)
        {
            _attendanceService = attendanceService;
            _groupService = groupService;
            _studentService = studentService;
        }

        public async Task<ActionResult> Index()
        {
            var attendaces = await _attendanceService.GetAllAttendance();

            return View(attendaces);
        }

        // GET: Leve/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var attendace = await _attendanceService.GetById(id);
            return View(attendace);
        }

        // GET: Leve/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Leve/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Attendance attendace)
        {
            if (ModelState.IsValid)
            {
                await _attendanceService.CreateAttendance(attendace);
                return RedirectToAction(nameof(Index));
            }
            return View(attendace);
        }

        // GET: Leve/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var attendace = await _attendanceService.GetById(id);
            return View(attendace);
        }

        // POST: Leve/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Attendance attendance)
        {
            try
            {
                await _attendanceService.EditAttendance(attendance);
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
            var attendace = await _attendanceService.GetById(id);
            return View(attendace);
        }

        // POST: Leve/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Attendance attendance)
        {
            try
            {
                await _attendanceService.DeleteAttendance(attendance);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetGroupesByBranchId(int Id)
        {
            var groupes = await _groupService.GetGroupesByBranchIdAsync(Id);
            return PartialView("_ViewGroupsByBranchId", groupes);
        }


        public async Task<IActionResult> ViewStudentsAttendanceByGroupId(int Id)
        {
            var group = await _groupService.GetGroupByIdAsync(Id);

            var students = await _studentService.GetStudyingAndTrialStudentsWithoutAttendanceByGroupId(Id);

            await _attendanceService.SetAttendances(DateTime.Now, group, students);

            var model = await _studentService.GetStudyingAndTrialStudentsAttendanceByGroupId(Id);

            return PartialView("_ViewsListStudents", model);
        }

        public async Task<JsonResult> UpdateAttendance(int studentId, int attendanceId, int attendanceDay, string attendanceMonth, int isAttend, string comment)
        {
            await _attendanceService.UpdateAttendance(studentId, attendanceId, attendanceDay, attendanceMonth, isAttend,
                comment);
            var student = _studentService.GetByIdAsync(studentId);
            return new JsonResult(StatusCode(200));
        }
    }
}
