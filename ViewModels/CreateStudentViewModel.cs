using CRM.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.ViewModels
{
    public class CreateStudentViewModel : Student
    {
        public SelectList Levels { get; set; }
        public string Comment { get; set; }
    }
}