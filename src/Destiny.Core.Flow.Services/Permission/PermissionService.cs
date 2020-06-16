﻿using Destiny.Core.Flow.Dependency;
using Destiny.Core.Flow.Dtos.Permissions;
using Destiny.Core.Flow.IServices.Permission;
using Destiny.Core.Flow.Model.Entities.Identity;
using Destiny.Core.Flow.Ui;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny.Core.Flow.Services.Permission
{
    [Dependency(ServiceLifetime.Scoped)]
    public class PermissionService : IPermissionService
    {
        private readonly RoleManager<Role> _roleManager = null;

        public PermissionService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        /// <summary>
        /// 得到角色集合
        /// </summary>
        /// <returns></returns>
        public  async Task<OperationResponse<RolePermissionOutputDto[]>> GetRolePermissionListAsync()
        {
            var roles=  await _roleManager.Roles.OrderByDescending(o => o.CreatedTime).Select(o => new RolePermissionOutputDto { Id = o.Id, Description = o.Description, Name = o.Name }).ToArrayAsync();
            return new OperationResponse<RolePermissionOutputDto[]>("查询成功", roles, Enums.OperationResponseType.Success);
        }
    }
}
