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
    public class LevelController : Controller
    {
        private readonly LevelService _levelService;

        

        // GET: Leve
        public LevelController(LevelService levelService)
        {
            _levelService = levelService;
        }

        public async Task<ActionResult> Index()
        {

            var levels = await _levelService.GetAllLevel();

            return View(levels);
        }

        // GET: Leve/Details/5
        public async Task<ActionResult>  Details(int id)
        {
            var level = await _levelService.GetLevelById(id);
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
               await _levelService.CreateLevel(level);
               return RedirectToAction(nameof(Index));
            }
            return View(level);
        }

        // GET: Leve/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var level = await _levelService.GetLevelById(id);
            return View(level);
        }

        // POST: Leve/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Level level)
        {
            try
            {
                await _levelService.EditLevel(level);
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
            var level = await _levelService.GetLevelById(id);
            return View(level);
        }

        // POST: Leve/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Level level)
        {
            try
            {
                await _levelService.DeleteLevel(level);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}