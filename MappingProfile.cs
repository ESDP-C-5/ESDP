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
        }
        private void CreateGroupsMapping()
        {
            CreateMap<CreateGroupViewModel, Group>();
            CreateMap<Group, CreateGroupViewModel>();
            CreateMap<Group, EditGroupViewModel>();
            CreateMap<EditGroupViewModel, Group>();
        }
    }
}
