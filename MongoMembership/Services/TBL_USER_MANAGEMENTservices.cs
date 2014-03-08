using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoMembership.Entities;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using System.Text.RegularExpressions;
using System.Web;
using MongoMembership.Helpers;
using System.Diagnostics;

namespace MongoMembership.Services
{
    public class TBL_USER_MANAGEMENTservices
    {
        private readonly MongoHelper<TBL_USER_MANAGEMENT> _userMangement;

        public TBL_USER_MANAGEMENTservices()
        {
            _userMangement = new MongoHelper<TBL_USER_MANAGEMENT>();
        }

        public void Create(TBL_USER_MANAGEMENT TBL_USER_MANAGEMENT)
        {
            _userMangement.Collection.Save(TBL_USER_MANAGEMENT);
        }


        public void Delete(ObjectId id)
        {
            _userMangement.Collection.Remove(Query.EQ("_id", id));
        }

        public IList<TBL_USER_MANAGEMENT> GetUserManagements()
        {
            return _userMangement.Collection.FindAll().SetSortOrder(SortBy.Descending("manage_row_id")).ToList();
        }

        public List<string> GetUserManagements(string ownerName)
        {
            TBL_USERSservices userServices = new TBL_USERSservices();
            TBL_USERS owner = userServices.GetUserByUsername(ownerName);
            var res = _userMangement.Collection.AsQueryable<TBL_USER_MANAGEMENT>()
                       .Where (c => c.manager_row_id == owner._id) 
                       .Select(c => c);
            List<string> list = new List<string>();
            foreach(var item in res){
                list.Add(userServices.GetUser(item.user_row_id).user_name);
            }
            return list;
        }

    }
}
