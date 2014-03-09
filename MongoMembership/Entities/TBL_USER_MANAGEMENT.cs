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
        public List<ObjectId> user_row_id { get; set; }

        public TBL_USER_MANAGEMENT(ObjectId manager_row_id, List<ObjectId> user_row_id)
        {
            this.manager_row_id = manager_row_id;
            this.user_row_id = user_row_id;
        }
    }
}
