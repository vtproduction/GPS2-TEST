using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoMembership.Entities
{
    public class TBL_USER_MANAGEMENT
    {
        public ObjectId _id { get; set; }
        public ObjectId manager_row_id { get; set; }
        public ObjectId user_row_id { get; set; }
    }
}
