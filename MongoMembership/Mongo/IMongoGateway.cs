using System;
using System.Collections.Generic;
using MongoMembership.Entities;

namespace MongoMembership.Mongo
{
    internal interface IMongoGateway
    {
        void DropUsers();
        void DropRoles();
        void CreateUser(User user);
        void UpdateUser(User user);
        void RemoveUser(User user);
        User GetById(string id);
        User GetByUserName(string username);
        User GetByEmail(string email);
        IEnumerable<User> GetAllByEmail(string email, int pageIndex, int pageSize, out int totalRecords);
        IEnumerable<User> GetAllByUserName(string username, int pageIndex, int pageSize, out int totalRecords);
        IEnumerable<User> GetAllAnonymByUserName(string username, int pageIndex, int pageSize, out int totalRecords);
        IEnumerable<User> GetAll(int pageIndex, int pageSize, out int totalRecords);
        IEnumerable<User> GetAllAnonym(int pageIndex, int pageSize, out int totalRecords);
        IEnumerable<User> GetAllInactiveSince(DateTime inactiveDate, int pageIndex, int pageSize, out int totalRecords);
        IEnumerable<User> GetAllInactiveAnonymSince(DateTime inactiveDate, int pageIndex, int pageSize, out int totalRecords);
        IEnumerable<User> GetInactiveSinceByUserName(string username, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords);
        IEnumerable<User> GetInactiveAnonymSinceByUserName(string username, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords);
        int GetUserForPeriodOfTime(TimeSpan timeSpan);
        void CreateRole(Role role);
        void RemoveRole(string roleName);
        string[] GetAllRoles(string applicationName);
        string[] GetRolesForUser(string username);
        string[] GetUsersInRole(string roleName);
        bool IsUserInRole(string username, string roleName);
        bool IsRoleExists(string roleName);
    }
}