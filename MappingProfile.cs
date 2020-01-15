using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRM.Models;
using CRM.UoW;
using CRM.ViewModels;

namespace CRM
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateGroupsMapping();
            EditGroupsMapping();
            EditStudentMapping();
            StudentPaymentViewModel();
            StudentViewModelMapping();
        }

        private void StudentViewModelMapping()
        {
            CreateMap<StudentViewModel, Comment>();
            CreateMap<Comment, StudentViewModel>();
        }
        private void CreateGroupsMapping()
        {
            CreateMap<CreateGroupViewModel, Group>();
            CreateMap<Group, CreateGroupViewModel>();
            CreateMap<Group, EditGroupViewModel>();
            CreateMap<EditGroupViewModel, Group>();
        }

        private void EditGroupsMapping()
        {
            CreateMap<Group, EditGroupViewModel>();
            CreateMap<EditGroupViewModel, Group>();
        }

        private void EditStudentMapping()
        {
            CreateMap<Student, EditStudentViewModel>();
            CreateMap<EditStudentViewModel, Student>();
        }
        private void StudentPaymentViewModel()
        {
            CreateMap<Student, StudentPaymentViewModel>();
            CreateMap<StudentPaymentViewModel, Student>();
        }
    }
}
