using CRM.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.ViewModels
{
    public class EditStudentViewModel : StudentViewModel
    {
        public SelectList Levels { get; set; }
        public SelectList Groups { get; set; }
        
    }
}