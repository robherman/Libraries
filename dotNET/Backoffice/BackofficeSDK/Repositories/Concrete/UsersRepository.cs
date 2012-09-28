using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SouthApps.Framework.LoggerService;
using SouthApps.Framework.DataAccess.Exceptions;
using Southapps.Framework.Backoffice.SDK.DataAccess;
using Southapps.Framework.Backoffice.SDK.Repositories.Abstract;

namespace Southapps.Framework.Backoffice.SDK.Repositories.Concrete
{
    class UsersRepository : IUsersRepository
    {
        UserDao userDao;
        RoleDao roleDao;
        UserPermissionDao userPermissionDao;
        RolePermissionDao rolePermissionDao;

        public UsersRepository()
        {
            this.userDao = new UserDao();
            this.roleDao = new RoleDao();
            this.userPermissionDao = new UserPermissionDao();
            this.rolePermissionDao = new RolePermissionDao();
        }

        public List<Role> GetRoles()
        {
            try
            {
                return this.roleDao.GetRoles();
            }
            catch (Exception ex)
            {
                LoggerService.Log(LogLevel.Error, "An error ocurred while getting roles.", ex);
                throw;
            }
        }

        public User GetUserById(int userId)
        {
            try
            {
                return this.userDao.GetUserById(userId);
            }
            catch (Exception ex)
            {
                LoggerService.Log(LogLevel.Error, "An error ocurred while getting user by id.", ex);
                throw;
            }
        }

        public void ModifyUser(User user)
        {
            try
            {
                this.userDao.ModifyUser(user);
            }
            catch (Exception ex)
            {
                LoggerService.Log(LogLevel.Error, "An error ocurred while modifying a user.", ex);
                throw;
            }
        }

        public int AddUser(User user)
        {
            try
            {
                return this.userDao.AddUser(user);
            }
            catch (Exception ex)
            {
                LoggerService.Log(LogLevel.Error, "An error ocurred while adding a user.", ex);
                throw;
            }
        }

        public User ValidateUserPassword(string username, string password, int? entityId)
        {
            try
            {
                return this.userDao.ValidateUserPassword(username, password, entityId);
            }
            catch (Exception ex)
            {
                LoggerService.Log(LogLevel.Error, "An error ocurred while validating user/password.", ex);
                throw;
            }
        }

        public List<UserPermission> GetUserPermissions(int userId)
        {
            try
            {
                return this.userPermissionDao.GetUserPermissions(userId);
            }
            catch (Exception ex)
            {
                LoggerService.Log(LogLevel.Error, "An error ocurred while getting user permissions.", ex);
                throw;
            }
        }

        public List<RolePermission> GetRolePermissions(int roleId)
        {
            try
            {
                return this.rolePermissionDao.GetRolePermissions(roleId);
            }
            catch (Exception ex)
            {
                LoggerService.Log(LogLevel.Error, "An error ocurred while getting role permissions.", ex);
                throw;
            }
        }

        public void RemoveUser(int userId)
        {
            try
            {
                this.userDao.RemoveUser(userId);
            }
            catch (Exception ex)
            {
                LoggerService.Log(LogLevel.Error, "An error ocurred while removing a user.", ex);
                throw;
            }
        }

        public List<User> GetUsers(int? roleId, string name, int? entityId, bool active)
        {
            try
            {
                return this.userDao.GetUsers(roleId, name, entityId, active);
            }
            catch (Exception ex)
            {
                LoggerService.Log(LogLevel.Error, "An error ocurred while getting users.", ex);
                throw;
            }
        }
    }
}
