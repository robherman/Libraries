using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SouthApps.STS.SDK.DAL.Model;
using System.Data.Objects;

namespace Southapps.Framework.Backoffice.SDK.DataAccess
{
    public class UserDao : DataAccessObject
    {
        #region Get

        public List<User> GetUsers(int? roleId, string name, int? entityId, bool active)
        {
            try
            {
                using (var entities = this.EntityContext)
                {
                    var query = from e in entities.Users
                                where (entityId == null || e.entityId == entityId)
                                && (roleId == null || e.roleId == roleId)
                                && (e.active == active)
                                && (string.IsNullOrEmpty(name) || e.name.ToLower().Contains(name.ToLower()))
                                select e;

                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                throw this.HandleException(ex);
            }
        }

        public User GetUserById(int userId)
        {
            using (var entities = this.EntityContext)
            {
                var query = from e in entities.Users
                            where e.userId == userId
                            select e;

                return query.FirstOrDefault();
            }
        }

        public User ValidateUserPassword(string username, string password, int? entityId)
        {
            using (var entities = this.EntityContext)
            {
                var user = (from e in entities.Users.Include("bo_role")
                            where e.username.Equals(username)
                            && e.password.Equals(password)
                            && (entityId == null || e.entityId == entityId)
                            && e.active
                            select e).FirstOrDefault();

                return user;
            }
        }

        #endregion

        #region Add

        public int AddUser(User user)
        {
            try
            {
                using (var entities = this.EntityContext)
                {
                    user.active = true;
                    entities.AddToUsers(user);

                    entities.SaveChanges();

                    return user.userId;
                }
            }
            catch (Exception ex)
            {
                throw this.HandleException(ex);
            }
        }

        #endregion

        #region Modify

        public void ModifyUser(User user)
        {
            using (var entities = this.EntityContext)
            {
                var oldUser = (from e in entities.Users
                               where e.userId == user.userId
                               select e).FirstOrDefault();

                oldUser.entityId = user.entityId;
                oldUser.password = user.password;
                oldUser.name = user.name;
                oldUser.roleId = user.roleId;
                oldUser.admin = user.admin;
                oldUser.comment = user.comment;
                oldUser.email = user.email;

                entities.SaveChanges();
            }
        }

        #endregion

        #region Remove

        public void RemoveUser(int userId)
        {
            try
            {
                using (var entities = this.EntityContext)
                {
                    User oldUser = entities.Users.FirstOrDefault(e =>
                        e.userId == userId);

                    if (oldUser.active)
                    {
                        oldUser.active = false;
                    }
                    else
                    {
                        oldUser.active = true;
                    }

                    entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw this.HandleException(ex);
            }
        }

        #endregion
    }
}