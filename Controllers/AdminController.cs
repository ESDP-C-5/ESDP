using System.Linq;
using System.Threading.Tasks;
using CRM.Models;
using CRM.Services;
using CRM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly BranchService _branchService;
        private readonly LevelService _levelService;

        public AdminController(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            BranchService branchService, 
            LevelService levelService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _branchService = branchService;
            _levelService = levelService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllBranches()
        {
            var branches = await _branchService.GetAllBranch();
            return PartialView("_ViewsListBranches", branches);
        }

        public JsonResult UpdateBranch(string nameBranch, string addressBranch,int idBranch)
        {
            _branchService.UpdateBranch(nameBranch, addressBranch, idBranch);
            return new JsonResult(StatusCode(200));
        }
        public JsonResult AddBranch(string nameBranch, string addressBranch)
        {
            _branchService.AddBranch(nameBranch, addressBranch);
            return new JsonResult(StatusCode(200));
        }
        public async Task<IActionResult> GetAllLevels()
        {
            var levels = await _levelService.GetAllLevel();
            return PartialView("_ViewsListLevels", levels);
        }
        public JsonResult UpdateLevel(string nameLevel,int idLevel)
        {
            if (!string.IsNullOrWhiteSpace(nameLevel))
            {
                _levelService.UpdateLevel(nameLevel, idLevel);
                return new JsonResult(StatusCode(200));  
            }
            Response.StatusCode = 500;
            return new JsonResult("Название не может быть пустым");
        }
        public JsonResult AddLevel(string addNameLevel)
        {
            _levelService.AddLevel(addNameLevel);
            return new JsonResult(StatusCode(200));
        }
    }
}