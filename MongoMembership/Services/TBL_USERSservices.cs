using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoMembership.Entities;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using System.Text.RegularExpressions;
using System.Web;
using MongoMembership.Helpers;
using System.Diagnostics;

namespace MongoMembership.Services
{
    public class TBL_USERSservices
    {
        private readonly MongoHelper<TBL_USERS> _users;

        public TBL_USERSservices()
        {
            _users = new MongoHelper<TBL_USERS>();
        }

        public void Create(TBL_USERS TBL_USERS)
        {
            _users.Collection.Save(TBL_USERS);
        }

        //public void Edit(TBL_USERS TBL_USERS)
        //{
        //    _users.Collection.Update(
        //        Query.EQ("_id", TBL_USERS._id), 
        //        Update.Set("Title", TBL_USERS.Title)
        //            .Set("Url", TBL_USERS.Url)
        //            .Set("Summary", TBL_USERS.Summary)
        //            .Set("Details", TBL_USERS.Details));
        //}

        public void Delete(ObjectId id)
        {
            _users.Collection.Remove(Query.EQ("_id", id));
        }

        public IList<TBL_USERS> GetUsers()
        {
            return _users.Collection.FindAll().SetSortOrder(SortBy.Descending("user_name")).ToList();
        }

        public TBL_USERS GetUser(ObjectId id)
        {
            var user = _users.Collection.Find(Query.EQ("_id", id)).SingleOrDefault();
            return user;
        }

        public TBL_USERS GetUserByObjectIdAsString(string id)
        {
            TBL_USERS user = _users.Collection.FindOne(Query.EQ("_id", ObjectId.Parse(id)));
            return user;
        }

        public TBL_USERS GetUserByUsername(string username)
        {
            System.Diagnostics.Debug.Write("---->" + username + "\n");
            var user = _users.Collection.Find(Query.EQ("user_name", username)).SingleOrDefault();
            //System.Diagnostics.Debug.Write("query return " + user.user_email + "\n");
            return user;
        }

    }
}
