using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoMembership.Entities
{
    [BsonIgnoreExtraElements]
    public class TBL_USERS
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string _t { get; set; }
        public DateTime register_datetime { get; set; }
        public ObjectId register_person { get; set; }
        public DateTime update_datetime { get; set; }
        public ObjectId update_person { get; set; }
        public int status { get; set; }
        public string user_name { get; set; }
        public string user_password { get; set; }
        public int user_type { get; set; }
        public string user_fullname { get; set; }
        public string user_phone { get; set; }
        public string user_email { get; set; }
        public string user_address { get; set; }
        public string user_extra_information { get; set; }
        public string user_contact_person { get; set; }
        public string user_contact_phone { get; set; }
        public int user_timezone { get; set; }

        public DateTime membership_lastactivitydate { get; set; }
        public DateTime membership_lastlogindate { get; set; }
        public DateTime LastLockedOutDate { get; set; }
        public DateTime membership_lastupdatedate { get; set; }
        public string membership_passwordsalt { get; set; }
        public string membership_passwordquestion { get; set; }
        public string membership_passwordanswer { get; set; }
        public bool membership_isapproved { get; set; }
        public bool membership_isdeleted { get; set; }
        public bool membership_islockedout { get; set; }
        public bool membership_isanonymous { get; set; }
        public int membership_failedpassattcount { get; set; }
        public DateTime membership_failedpassattwindst { get; set; }
        public int membership_failedpassansattcount { get; set; }
        public DateTime membership_failedpassansattwindst { get; set; }
        
    }
}
