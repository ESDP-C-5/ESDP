using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models;
using CRM.ViewModels;

namespace CRM.Helpers.SortHelper
{
    public static class SortStudents
    {
        public static StudentViewModel Sort(List<Student> students, SortingEnum sortState)
        {
            var model = new StudentViewModel
            {
                LastNameSortState = sortState == SortingEnum.LastNameAsc
                    ? SortingEnum.LastNameDesc
                    : SortingEnum.LastNameAsc,
                CreatedDateSortState = sortState == SortingEnum.CreatedDateAsc
                    ? SortingEnum.CreatedDateDesc
                    : SortingEnum.CreatedDateAsc,
                DateOfBirthdaySortState = sortState == SortingEnum.DateOfBirthdayAsc
                    ? SortingEnum.DateOfBirthdayDesc
                    : SortingEnum.DateOfBirthdayAsc,
                ParentLastNameSortState = sortState == SortingEnum.ParentLastNameAsc
                    ? SortingEnum.ParentLastNameDesc
                    : SortingEnum.ParentLastNameAsc,
                PhoneNumberSortState = sortState == SortingEnum.PhoneNumberAsc
                    ? SortingEnum.PhoneNumberDesc
                    : SortingEnum.PhoneNumberAsc,
                StatusSortState = sortState == SortingEnum.StatusAsc
                    ? SortingEnum.StatusDesc
                    : SortingEnum.StatusAsc
            };



            switch (sortState)
            {
                case SortingEnum.LastNameDesc:
                    students = students.OrderByDescending(s => s.LastName ?? s.Name ?? s.FatherName).ToList();
                    break;
                case SortingEnum.CreatedDateAsc:
                    students = students.OrderBy(s => s.CreatedDate).ToList();
                    break;
                case SortingEnum.CreatedDateDesc:
                    students = students.OrderByDescending(s => s.CreatedDate).ToList();
                    break;
                case SortingEnum.DateOfBirthdayAsc:
                    students = students.OrderBy(s => s.DateOfBirthday).ToList();
                    break;
                case SortingEnum.DateOfBirthdayDesc:
                    students = students.OrderByDescending(s => s.DateOfBirthday).ToList();
                    break;
                case SortingEnum.ParentLastNameAsc:
                    students = students.OrderBy(s => s.ParentLastName).ToList();
                    break;
                case SortingEnum.ParentLastNameDesc:
                    students = students.OrderByDescending(s => s.ParentLastName).ToList();
                    break;
                case SortingEnum.PhoneNumberAsc:
                    students = students.OrderBy(s => s.PhoneNumber).ToList();
                    break;
                case SortingEnum.PhoneNumberDesc:
                    students = students.OrderByDescending(s => s.PhoneNumber).ToList();
                    break;
                case SortingEnum.StatusAsc:
                    students = students.OrderBy(s => s.Status).ToList();
                    break;
                case SortingEnum.StatusDesc:
                    students = students.OrderByDescending(s => s.Status).ToList();
                    break;
            }


            model.Student = students;


            return model;
        }
    }
}
