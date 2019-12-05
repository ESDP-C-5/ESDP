﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models;
using CRM.UoW;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services
{
    public class GroupService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public GroupService(UnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Group> CreateAsync(Group group)
        {
            var groups = _unitOfWork.Groups;
            await groups.CreateAsync(group);
            await _unitOfWork.CompleteAsync();
            await _userManager.Users.ToListAsync();
            return group;
        }
        public async Task<Group> GetByIdAsync(int id)
        {
            var groups = _unitOfWork.Groups;
            var group = await groups.GetByIdAsync(id);
            group.User = User(group.UserId).Result;
            return group;
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            var groups = _unitOfWork.Groups;
            var allgroups = await groups.GetAllAsync();
            foreach (var group in allgroups)
            {
                group.User = User(group.UserId).Result;
            }

            return allgroups;
        }

        public async Task<ApplicationUser> User(string Id)
        {
            var user = _userManager.FindByIdAsync(Id);
            return await user;
        }

        public IEnumerable<Branch> GetAllBranches()
        {
            var branchs = _unitOfWork.Branchs;
            return branchs.GetAll();
        }
        public IEnumerable<TimeTable> GetAllTimeTables()
        {
            var ttables = _unitOfWork.TimeTables;
            return ttables.GetAll();
        }

        public async Task EditAsync(Group group)
        {
            var groups = _unitOfWork.Groups;
            groups.UpdateAsync(group);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(Group group)
        {
            var groups = _unitOfWork.Groups;
            groups.RemoveAsync(group);
            await _unitOfWork.CompleteAsync();
        }
    }
}
