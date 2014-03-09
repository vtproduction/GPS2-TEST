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

        public void Create(TBL_USER_MANAGEMENT userManagement)
        {
            
            _userMangement.Collection.Save(userManagement);
        }


        public void Delete(ObjectId id)
        {
            _userMangement.Collection.Remove(Query.EQ("_id", id));
        }

        public List<ObjectId> getUsersListUnderManagement(ObjectId ownerId)
        {
            TBL_USER_MANAGEMENT list = _userMangement.Collection.FindOne(Query.EQ("manager_row_id", ownerId));
            return list.user_row_id;
        }
        public IList<TBL_USER_MANAGEMENT> GetUserManagements()
        {
            return _userMangement.Collection.FindAll().SetSortOrder(SortBy.Descending("manage_row_id")).ToList();
        }

        public List<TBL_USER_MANAGEMENT> GetUsersUnderManagement(ObjectId ownerId)
        {
            return _userMangement.Collection.Find(Query.EQ("manager_row_id", ownerId)).ToList();
        }
        public List<string> GetUserManagements(string ownerName)
        {
            TBL_USERSservices userServices = new TBL_USERSservices();
            TBL_USERS owner = userServices.GetUserByUsername(ownerName);
            //var res =  _userMangement.Collection.AsQueryable<TBL_USER_MANAGEMENT>()
            //           .Where (c => c.manager_row_id == owner._id) 
            //           .Select(c => c);
            List<TBL_USER_MANAGEMENT> res = GetUsersUnderManagement(owner._id);
            TBL_USER_MANAGEMENT item = res[0];
            List<string> list = new List<string>();
            
            list.Add("owner: " + owner.user_name + " count: " + res.Count);
            List<ObjectId> llist = getUsersListUnderManagement(owner._id);

            if (list.Count > 0)
            {
                foreach (var llistItem in llist)
                {
                    list.Add(llistItem.ToString());
                    if (userServices.GetUserByObjectIdAsString(llistItem.ToString()) != null)
                    {
                        list.Add(userServices.GetUserByObjectIdAsString(llistItem.ToString()).user_name);
                    }
                    else
                    {
                        list.Add("=====");
                    }
                    //TBL_USERS user = userServices.GetUser(owner._id);
                    //list.Add(user.user_name);
                }
            }

            //list.Add(userServices.GetUser(item.user_row_id).user_name);
            //foreach (var item in res)
            //{
            //    //ObjectId userId = item.user_row_id;
            //    //TBL_USERS user = userServices.GetUser(userId);
            //    //string name = user.user_name;
            //    list.Add(userServices.GetUser(item.user_row_id).user_name);
            //    //list.Add(item._id + " \n " + item.manager_row_id + "\n " + item.user_row_id);
            //    //TBL_USERS user = userServices.GetUser(item.user_row_id);
            //    //System.Diagnostics.Debug.Write("-------------username: " + user.user_name + "----" + item.user_row_id + "-------------------\n");
            //}

            //TBL_USERS user = userServices.GetUser(owner._id);
            //System.Diagnostics.Debug.Write("-------------username: " + user.user_name + "-----------------------\n");


            return list;
        }

    }
}
