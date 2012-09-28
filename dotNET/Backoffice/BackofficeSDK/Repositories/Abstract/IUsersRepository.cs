using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SouthApps.STS.SDK.DAL.Model;
using Southapps.Framework.Backoffice.SDK.DataAccess;

namespace Southapps.Framework.Backoffice.SDK.Repositories.Abstract
{
    public interface IUsersRepository
    {
        User GetUserById(int userId);
        void ModifyUser(User user);
        void RemoveUser(int userId);
        int AddUser(User user);

        List<User> GetUsers(int? roleId, string name, int? entityId, bool active);

        User ValidateUserPassword(string username, string password, int? entityId);

        List<Role> GetRoles();
        List<UserPermission> GetUserPermissions(int userId);
        List<RolePermission> GetRolePermissions(int roleId);
    }
}
