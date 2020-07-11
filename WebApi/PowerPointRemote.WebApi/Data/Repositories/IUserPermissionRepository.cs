using System;
using PowerPointRemote.WebApi.Models;

namespace PowerPointRemote.WebAPI.Data.Repositories
{
    public interface IUserPermissionRepository
    {
        void SetPermission(Guid userId, UserPermission userPermission);
        UserPermission GetPermission(Guid userId);
        void RemoveUser(Guid userId);
    }
}