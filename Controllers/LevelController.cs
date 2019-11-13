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
    public class LevelController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public LevelController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Leve
        public async Task<ActionResult> Index()
        {
            var levelUoF = _unitOfWork.Levels;
            var levels =await levelUoF.GetAllAsync();

            return View(levels);
        }

        // GET: Leve/Details/5
        public async Task<ActionResult>  Details(int id)
        {
            var levelUoF = _unitOfWork.Levels;
            var level = await levelUoF.GetByIdAsync(id);
            return View(level);
        }

        // GET: Leve/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Leve/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>  Create(Level level)
        {
            if (ModelState.IsValid)
            {
                var levelUoF = _unitOfWork.Levels;
                await levelUoF.CreateAsync(level);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(level);
        }

        // GET: Leve/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var levelUoF = _unitOfWork.Levels;
            var level = await levelUoF.GetByIdAsync(id);
            return View(level);
        }

        // POST: Leve/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Level level)
        {
            try
            {
                var levelUoF = _unitOfWork.Levels;
                levelUoF.UpdateAsync(level);
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
            var levelUoF = _unitOfWork.Levels;
            var level = await levelUoF.GetByIdAsync(id);
            return View(level);
        }

        // POST: Leve/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Level level)
        {
            try
            {
                var levelUoF = _unitOfWork.Levels;
                levelUoF.RemoveAsync(level);
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