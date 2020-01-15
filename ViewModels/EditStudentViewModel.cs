using CRM.Helpers;
using CRM.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.ViewModels
{
    public class EditStudentViewModel : Student
    {
        public SelectList Levels { get; set; }
        public SelectList Groups { get; set; }
        public StudentStatusEnum StudentStatusEnum { get; set; }
        
    }
}