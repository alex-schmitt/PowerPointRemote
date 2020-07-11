using System;
using System.Collections.Generic;
using PowerPointRemote.WebApi.Models;

namespace PowerPointRemote.WebAPI.Data.Repositories
{
    public class UserPermissionRepository : IUserPermissionRepository
    {
        public UserPermissionRepository()
        {
            Data = new Dictionary<Guid, UserPermission>();
        }

        private Dictionary<Guid, UserPermission> Data { get; }

        public void SetPermission(Guid userId, UserPermission userPermission)
        {
            Data[userId] = userPermission;
        }

        public UserPermission GetPermission(Guid userId)
        {
            Data.TryGetValue(userId, out var permission);
            return permission;
        }

        public void RemoveUser(Guid userId)
        {
            Data.Remove(userId);
        }
    }
}