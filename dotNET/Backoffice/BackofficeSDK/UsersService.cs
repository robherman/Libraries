using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Southapps.Framework.Backoffice.SDK.DataAccess;

using Southapps.Framework.Backoffice.SDK.Repositories.Abstract;
using Southapps.Framework.Backoffice.SDK.Repositories.Concrete;
using SouthApps.STS.SDK.DAL.Model;
using Southapps.Framework.Backoffice.SDK.Model;

namespace Southapps.Framework.Backoffice.SDK
{
    public class UsersService
    {
        private IUsersRepository usersRepository;
        
        public UsersService()
        {
            this.usersRepository = new UsersRepository();
        }

        public List<Role> GetRoles()
        {
            return this.usersRepository.GetRoles();
        }

        public List<User> GetUsers(int? roleId)
        {
            return this.usersRepository.GetUsers(roleId, null, null, true);
        }

        public List<User> GetUsers(int? roleId, bool active)
        {
            return this.usersRepository.GetUsers(roleId, null, null, active);
        }

        public List<User> GetUsers(int? roleId, string name, int? entityId, bool active)
        {
            return this.usersRepository.GetUsers(roleId, name, entityId, active);
        }

        public User GetUser(int userId)
        {
            return this.usersRepository.GetUserById(userId);
        }

        public void ModifyUser(User user)
        {
            this.usersRepository.ModifyUser(user);
        }

        public void RemoveUser(int userId)
        {
            this.usersRepository.RemoveUser(userId);
        }

        public void AddUser(User user)
        {
            user.creationDate = DateTime.Now;
            this.usersRepository.AddUser(user);
        }

        public BackendLoginResult DoLogin(string username, string password)
        {
            return DoLogin(username, password, null);
        }

        public BackendLoginResult DoLogin(string username, string password, int? entityId)
        {
            BackendLoginResult result = new BackendLoginResult();

            User user = this.usersRepository.ValidateUserPassword(username, password, entityId);

            if (user == null)
            {
                return result;
            }

            result.User = user;
            result.Authenticated = true;

            List<UserPermission> userPermissions = this.usersRepository.GetUserPermissions(user.userId);
            List<RolePermission> rolePermissions = this.usersRepository.GetRolePermissions(user.roleId);

            List<ModulePermission> permissions = new List<ModulePermission>();

            foreach (UserPermission userPermission in userPermissions)
            {
                ModulePermission modulePermission = new ModulePermission();
                modulePermission.Module = userPermission.bo_module_permission.bo_module.name;
                modulePermission.Permission = (Permissions)Enum.Parse(typeof(Permissions), userPermission.bo_module_permission.bo_permission.name);
                modulePermission.Url = userPermission.bo_module_permission.url;

                permissions.Add(modulePermission);
            }

            foreach (RolePermission rolePermission in rolePermissions)
            {
                ModulePermission modulePermission = new ModulePermission();

                if (userPermissions.Exists(e => e.bo_module_permission.bo_module.name.Equals(rolePermission.bo_module_permission.bo_module.name)))
                {
                    continue;
                }

                modulePermission.Module = rolePermission.bo_module_permission.bo_module.name;
                modulePermission.Permission = (Permissions)Enum.Parse(typeof(Permissions), rolePermission.bo_module_permission.bo_permission.name);
                modulePermission.Url = rolePermission.bo_module_permission.url;

                permissions.Add(modulePermission);
            }

            result.Permissions = permissions;

            return result;
        }
    }
}
