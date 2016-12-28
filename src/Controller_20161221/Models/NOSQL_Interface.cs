using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.IO;

namespace Controller_20161221.Models
{
    public class NOSQL_Interface
    {
        const int _JSON_CREATION_ERROR = 1;

        protected static IMongoClient _client;
        protected static IMongoDatabase _database;
        protected static IMongoCollection<BsonDocument> _collection;

        public ObjectId TrnId { get; set; }
        public string ProcessStep { get; set; }
        public string AgencyCode { get; set; }
        public string UserId { get; set; }
        public string ProposalNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string ProductName { get; set; }
        public DateTime ProposalDate { get; set; }
        public int ValidDays { get; set; }

        public void NOSQL_connect()
        {
            /* fro Default connection --- we can use MongoClient(connectionString) */
            _client = new MongoClient();
            _database = _client.GetDatabase("transaction_db");
            _collection = _database.GetCollection<BsonDocument>("trncoll");

        }

        public void NOSQL_Insert(string varJsonStr)
        {
            int i;

            var varBsonDoc = varJsonStr.ToBsonDocument();
            _collection.InsertOne(varBsonDoc);
        }
/* Returns first occurence of filtered records    */
          public string NOSQL_getDocument(string strFilter)
        {
            BsonDocument filter;
            List<BsonDocument> resultList;
           

            filter = BsonDocument.Parse(strFilter);
            resultList = _collection.Find(filter).ToList();
            return(resultList[0].ToJson());
        }

        public string noSQL_getListOfDocuments(string strFilter,ref int error,ref int noOfRecords,int startIndex,int pageSize)
        {
            BsonDocument filter;
            List<BsonDocument> resultList;
            string retJsonString;

            error = 0;
            try
            {
                filter = BsonDocument.Parse(strFilter);
                resultList= _collection.Find(filter).Limit(pageSize).Skip(startIndex).ToList();
                noOfRecords=(int)_collection.Count(filter);
                JsonWriterSettings writerSettings = new JsonWriterSettings();
                writerSettings.OutputMode = (JsonOutputMode)0;

                MongoDB.Bson.Serialization.BsonSerializationArgs arg = default(MongoDB.Bson.Serialization.BsonSerializationArgs);
                Type nominalType=resultList.GetType();
                retJsonString=resultList.ToJson(nominalType,writerSettings,null,null, arg);
            }
            catch (Exception ex)
            {
                error = _JSON_CREATION_ERROR;
                return (ex.Message);
            }
           return (retJsonString);
        }
        public void NOSQL_Close()
        {
        }

        public static int compareByProcessStep(NOSQL_Interface item1, NOSQL_Interface item2)
        {
            return item1.ProcessStep.CompareTo(item2.ProcessStep);
        }

        public static int compareByProposalNo(NOSQL_Interface item1, NOSQL_Interface item2)
        {
            return item1.ProposalNo.CompareTo(item2.ProposalNo);
        }
        public static int compareByCustomerName(NOSQL_Interface item1, NOSQL_Interface item2)
        {
            return item1.CustomerName.CompareTo(item2.CustomerName);
        }
        public static int compareByCustomerSurName(NOSQL_Interface item1, NOSQL_Interface item2)
        {
            return item1.CustomerSurname.CompareTo(item2.CustomerSurname);
        }
        public static int compareByProductName(NOSQL_Interface item1, NOSQL_Interface item2)
        {
            return item1.ProductName.CompareTo(item2.ProductName);
        }

    }

}
