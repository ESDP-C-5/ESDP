using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;
using CRM.Models;

namespace CRM.ViewModels
{
    public class StudentViewModel : Student
    {
        public string Comment { get; set; }

        public List<Student> Student { get; set; }
        public List<Branch> branches { get; set; }
        public SortingEnum LastNameSortState { get; set; }
        public SortingEnum DateOfBirthdaySortState { get; set; }
        public SortingEnum CreatedDateSortState { get; set; }
        public SortingEnum ParentLastNameSortState { get; set; }
        public SortingEnum PhoneNumberSortState { get; set; }
        public SortingEnum StatusSortState { get; set; }
        public SortingEnum GroupSortState { get; set; }
        public SortingEnum TrialDateSortState { get; set; }
        public SortingEnum LevelSortState { get; set; }
        public SortingEnum CommentSortState { get; set; }
        public SortingEnum ChangeStatusSortState { get; set; }
    }
}
