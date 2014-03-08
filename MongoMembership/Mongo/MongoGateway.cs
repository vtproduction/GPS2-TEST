using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoMembership.Utils;
using MongoMembership.Entities;
using MongoMembership.Providers;

namespace MongoMembership.Mongo
{
    class MongoGateway : IMongoGateway
    {
        private readonly MongoDatabase dataBase;
        private MongoCollection<User> UsersCollection
        {
            get { return dataBase.GetCollection<User>("TBL_USERS"); }
        }

        static MongoGateway()
        {
            RegisterClassMapping();
        }
        public MongoGateway(string mongoConnectionString)
        {
            var mongoUrl = new MongoUrl(mongoConnectionString);
            var server = new MongoClient(mongoUrl).GetServer();
            dataBase = server.GetDatabase(mongoUrl.DatabaseName);
            CreateIndex();
        }

        public void DropUsers()
        {
            UsersCollection.Drop();
        }

        public void DropRoles()
        {
            //RolesCollection.Drop();
        }

        #region User
        public void CreateUser(User user)
        {
            

            UsersCollection.Insert(user);
        }

        public void UpdateUser(User user)
        {
            UsersCollection.Save(user);
        }

        public void RemoveUser(User user)
        {
            user.IsDeleted = true;
            UpdateUser(user);
        }

        public User GetById(string id)
        {
            if (id.IsNullOrWhiteSpace() || UsersCollection.Count() == 0)
                return null;

            return UsersCollection.FindOneById(id);
        }

        public User GetByUserName(string username)
        {
            if (username.IsNullOrWhiteSpace() || UsersCollection.Count() == 0)
                return null;

            return UsersCollection
                    .AsQueryable()
                    .SingleOrDefault(user => user.Username == username
                            && user.IsDeleted == false);
        }

        public User GetByEmail(string email)
        {
            if (email.IsNullOrWhiteSpace() || UsersCollection.Count() == 0)
                return null;

            return UsersCollection
                    .AsQueryable()
                    .SingleOrDefault(user
                        => user.IsDeleted == false);
        }

        public IEnumerable<User> GetAllByEmail(string email, int pageIndex, int pageSize, out int totalRecords)
        {
            if (email.IsNullOrWhiteSpace() || UsersCollection.Count() == 0)
            {
                totalRecords = 0;
                return Enumerable.Empty<User>();
            }

            var users = UsersCollection
                        .AsQueryable()
                        .Where(user
                            => user.IsDeleted == false);

            totalRecords = users.Count();
            return users.Skip(pageIndex * pageSize).Take(pageSize);
        }

        public IEnumerable<User> GetAllByUserName(string username, int pageIndex, int pageSize, out int totalRecords)
        {
            if (username.IsNullOrWhiteSpace() || UsersCollection.Count() == 0)
            {
                totalRecords = 0;
                return Enumerable.Empty<User>();
            }

            var users = UsersCollection
                        .AsQueryable()
                        .Where(user => user.Username == username 
                            && user.IsDeleted == false);

            totalRecords = users.Count();
            return users.Skip(pageIndex * pageSize).Take(pageSize);
        }

        public IEnumerable<User> GetAllAnonymByUserName(string username, int pageIndex, int pageSize, out int totalRecords)
        {
            if (username.IsNullOrWhiteSpace() || UsersCollection.Count() == 0)
            {
                totalRecords = 0;
                return Enumerable.Empty<User>();
            }

            var users = UsersCollection
                .AsQueryable()
                .Where(user => user.Username == username
                    && user.IsAnonymous == true
                    && user.IsDeleted == false);

            totalRecords = users.Count();
            return users.Skip(pageIndex * pageSize).Take(pageSize);
        }

        public IEnumerable<User> GetAll(int pageIndex, int pageSize, out int totalRecords)
        {
            totalRecords = (int)UsersCollection.Count();

            if (totalRecords == 0)
                return Enumerable.Empty<User>();

            return UsersCollection
                    .AsQueryable()
                    .Where(user
                        => user.IsDeleted == false)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize);
        }

        public IEnumerable<User> GetAllAnonym(int pageIndex, int pageSize, out int totalRecords)
        {
            if (UsersCollection.Count() == 0)
            {
                totalRecords = 0;
                return Enumerable.Empty<User>();
            }

            var users = UsersCollection
                .AsQueryable()
                .Where(user
                    => user.IsAnonymous
                    && user.IsDeleted == false);

            totalRecords = users.Count();
            return users.Skip(pageIndex * pageSize).Take(pageSize);
        }

        public IEnumerable<User> GetAllInactiveSince(DateTime inactiveDate, int pageIndex, int pageSize, out int totalRecords)
        {
            if (UsersCollection.Count() == 0)
            {
                totalRecords = 0;
                return Enumerable.Empty<User>();
            }

            var users = UsersCollection
                        .AsQueryable()
                        .Where(user
                            => user.LastActivityDate <= inactiveDate
                            && user.IsDeleted == false);
            totalRecords = users.Count();
            return users
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize);
        }

        public IEnumerable<User> GetAllInactiveAnonymSince(DateTime inactiveDate, int pageIndex, int pageSize, out int totalRecords)
        {
            if (UsersCollection.Count() == 0)
            {
                totalRecords = 0;
                return Enumerable.Empty<User>();
            }

            var users = UsersCollection
                        .AsQueryable()
                        .Where(user
                            => user.LastActivityDate <= inactiveDate
                            && user.IsAnonymous
                            && user.IsDeleted == false);
            totalRecords = users.Count();
            return users
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize);
        }

        public IEnumerable<User> GetInactiveSinceByUserName(string username, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            if (UsersCollection.Count() == 0)
            {
                totalRecords = 0;
                return Enumerable.Empty<User>();
            }

            var users = UsersCollection
                        .AsQueryable()
                        .Where(user => user.Username == username
                            && user.LastActivityDate <= userInactiveSinceDate
                            && user.IsDeleted == false);
            totalRecords = users.Count();
            return users
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize);
        }

        public IEnumerable<User> GetInactiveAnonymSinceByUserName(string username, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            if (UsersCollection.Count() == 0)
            {
                totalRecords = 0;
                return Enumerable.Empty<User>();
            }

            var users = UsersCollection
                        .AsQueryable()
                        .Where(user => user.Username == username
                            && user.LastActivityDate <= userInactiveSinceDate
                            && user.IsAnonymous
                            && user.IsDeleted == false);
            totalRecords = users.Count();
            return users
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize);
        }

        public int GetUserForPeriodOfTime(TimeSpan timeSpan)
        {
            return UsersCollection
                    .AsQueryable()
                    .Count(user
                        => user.LastActivityDate > DateTime.UtcNow.Subtract(timeSpan));
        }
        #endregion

        #region Role
        public void CreateRole(Role role)
        {
            //if (role.RoleName != null) role.RoleNameLowercased = role.RoleName.ToLowerInvariant();

            //RolesCollection.Insert(role);
        }

        public void RemoveRole(string roleName)
        {
            //var query = new QueryDocument
            //{
            //    new Dictionary<string, object>
            //    {
            //        {Util.GetElementNameFor<Role>(_ => _.), },
            //        {Util.GetElementNameFor<Role>(_ => _.RoleNameLowercased), roleName.ToLowerInvariant()}
            //    }
            //};

            //RolesCollection.Remove(query);
        }

        public string[] GetAllRoles(string role)
        {
            //return RolesCollection
            //        .AsQueryable()
            //        .Where(role => role. == )
            //        .Select(role => role.RoleName)
            //        .ToArray();
            return null;
        }

        public string[] GetRolesForUser(string username)
        {
            //if (username.IsNullOrWhiteSpace())
            //    return null;

            //User user = GetByUserName(, username);

            //if (user == null || user.Roles == null)
            //    return null;

            //return user.Roles.ToArray();
            return null;
        }

        public string[] GetUsersInRole(string roleName)
        {
            //if (roleName.IsNullOrWhiteSpace())
            //    return null;

            //return UsersCollection
            //        .AsQueryable()
            //        .Where(user
            //            => user. == 
            //            && (user.Roles.Contains(roleName.ToLowerInvariant()) || user.Roles.Contains(roleName)))
            //        .Select(user => user.Username)
            //        .ToArray();
            return null;
        }

        public bool IsUserInRole(string username, string roleName)
        {
            //if (username.IsNullOrWhiteSpace() || roleName.IsNullOrWhiteSpace())
            //    return false;

            //return UsersCollection
            //        .AsQueryable()
            //        .Any(user
            //            => user. == 
            //            && user.UsernameLowercase == username.ToLowerInvariant()
            //            && (user.Roles.Contains(roleName.ToLowerInvariant()) || user.Roles.Contains(roleName)));
            return true;
        }

        public bool IsRoleExists(string roleName)
        {
            //if (roleName.IsNullOrWhiteSpace())
            //    return false;

            //return RolesCollection
            //        .AsQueryable()
            //        .Any(role
            //            => role. == 
            //            && role.RoleNameLowercased == roleName.ToLowerInvariant());
            return true;
        }
        #endregion

        #region Private Methods
        private static void RegisterClassMapping()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(User)))
            {
                // Initialize Mongo Mappings
                BsonClassMap.RegisterClassMap<User>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                    cm.SetIsRootClass(true);
                    cm.MapIdField(c => c.Id);
                    cm.MapProperty( c => c.CreateDate).SetElementName("register_datetime");
                    cm.MapProperty( c => c.CreatePerson).SetElementName("register_person");
                    cm.MapProperty( c => c.LastPasswordChangedDate).SetElementName("update_datetime");                     
                    cm.MapProperty( c => c.LastPasswordChangePerson).SetElementName("update_person");
                    cm.MapProperty( c => c.Status).SetElementName("user_status");                     
                    cm.MapProperty( c => c.Username).SetElementName("user_name");
                    cm.MapProperty( c => c.Password).SetElementName("user_password");                     
                    cm.MapProperty( c => c.UserType).SetElementName("user_type");
                    cm.MapProperty( c => c.UserFullname).SetElementName("user_fullname"); 
                    cm.MapProperty( c => c.UserPhone).SetElementName("user_phone");
                    cm.MapProperty( c => c.Email).SetElementName("user_email");
                    cm.MapProperty( c => c.UserAddress).SetElementName("user_address");                     
                    cm.MapProperty( c => c.Comment).SetElementName("user_extra_information");
                    cm.MapProperty( c => c.UserContactPerson).SetElementName("user_contact_person");                     
                    cm.MapProperty( c => c.UserContactPhone).SetElementName("user_contact_phone");
                    cm.MapProperty( c => c.UserTimeZone).SetElementName("user_timezone");                     
                    cm.MapProperty( c => c.LastActivityDate).SetElementName("membership_lastactivitydate");
                    cm.MapProperty( c => c.LastLoginDate).SetElementName("membership_lastlogindate");                     
                    cm.MapProperty( c => c.LastUpdatedDate).SetElementName("membership_lastupdatedate");
                    cm.MapProperty( c => c.PasswordSalt).SetElementName("membership_passwordsalt");                     
                    cm.MapProperty( c => c.PasswordQuestion).SetElementName("membership_passwordquestion");
                    cm.MapProperty( c => c.PasswordAnswer).SetElementName("membership_passwordanswer");                     
                    cm.MapProperty( c => c.IsApproved).SetElementName("membership_isapproved");
                    cm.MapProperty( c => c.IsDeleted).SetElementName("membership_isdeleted");                     
                    cm.MapProperty( c => c.IsLockedOut).SetElementName("membership_islockedout");
                    cm.MapProperty( c => c.IsAnonymous).SetElementName("membership_isanonymous");
                    cm.MapProperty( c => c.FailedPasswordAttemptCount).SetElementName("membership_failedpassattcount");
                    cm.MapProperty( c => c.FailedPasswordAttemptWindowStart).SetElementName("membership_failedpassattwindst");
                    cm.MapProperty( c => c.FailedPasswordAnswerAttemptCount).SetElementName("membership_failedpassansattcount");                     
                    cm.MapProperty( c => c.FailedPasswordAnswerAttemptWindowStart).SetElementName("membership_failedpassansattwindst");
                });
            }

        }

        private void CreateIndex()
        {
            UsersCollection.EnsureIndex("_id");
        }
        #endregion
    }
}