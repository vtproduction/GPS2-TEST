using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;

namespace MongoMembership.Entities
{
    internal class User
    {
        //***Required fields***
        public ObjectId Id { get; set; }
        public DateTime CreateDate { get; set; }
        public ObjectId CreatePerson { get; set; }
        public DateTime LastPasswordChangedDate { get; set; }
        public ObjectId LastPasswordChangePerson { get; set; }
        public int Status { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
        public string UserFullname { get; set; }
        public string UserPhone { get; set; }
        public string Email { get; set; }
        public string UserAddress { get; set; }
        public string Comment { get; set; }
        public string UserContactPerson { get; set; }
        public string UserContactPhone { get; set; }
        public int UserTimeZone { get; set; }


        //***Membership required fields***
        public DateTime LastActivityDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastLockedOutDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsAnonymous { get; set; }
        public int FailedPasswordAttemptCount { get; set; }
        public DateTime FailedPasswordAttemptWindowStart { get; set; }
        public int FailedPasswordAnswerAttemptCount { get; set; }
        public DateTime FailedPasswordAnswerAttemptWindowStart { get; set; }

        //***User roles and value, currently disabled***
        //public List<string> Roles { get; set; }
        //public Dictionary<string, object> Values { get; set; }

        public User()
        {
            //this.Roles = new List<string>();
            //this.Values = new Dictionary<string, object>();
        }

        //public void AddValues(Dictionary<string, object> values)
        //{
        //    //foreach (var value in values)
        //    //{
        //    //    if (this.Values.ContainsKey(value.Key))
        //    //    {
        //    //        this.Values[value.Key] = value.Value;
        //    //    }
        //    //    else
        //    //    {
        //    //        this.Values.Add(value.Key, value.Value);
        //    //    }
        //    //}
        //}

        public override string ToString()
        {
            return this.Username + " <" + this.Email + ">";
        }
    }
}
