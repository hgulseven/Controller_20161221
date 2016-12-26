using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Controller_20161221.Models
{
    public class NOSQL_Interface
    {
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

            var document = new BsonDocument
            {
                {"trnID", TrnId},
                {"processStep", ProcessStep },
                {"agencyCode", AgencyCode },
                {"UserId", UserId },
                {"ProposalNo",ProposalNo },
                {"CustomerName",CustomerName },
                {"CustomerSurname", CustomerSurname},
                {"ProductName",ProductName},
                {"ProposalDate",ProposalDate },
                {"ValidDays",ValidDays }
            };
            var varBsonDoc = varJsonStr.ToBsonDocument();
            int elementCount=varBsonDoc.Count();
            for (i = 0; i < elementCount; i++)
            {
                document.Append(varBsonDoc.GetElement(i));
            }
            _collection.InsertOne(document);
        }

        public void NOSQL_Insert(BsonDocument doc)
        {
            _collection.InsertOne(doc);

        }

          public string NOSQL_getDocument(string strFilter)
        {
            BsonDocument filter;
            List<BsonDocument> resultList;
            string kk;

            filter = BsonDocument.Parse(strFilter);
            resultList = _collection.Find(filter).ToList();
            return(resultList[0].ToJson());
        }

        public List<NOSQL_Interface> noSQL_getListOfDocuments(string strFilter)
        {
            BsonDocument filter;
            List<BsonDocument> resultList;
            List<NOSQL_Interface> myList = new List<NOSQL_Interface>();
            int i;

            filter = BsonDocument.Parse(strFilter);
            resultList = _collection.Find(filter).ToList();

            for (i = 0; i < resultList.Count; i++)
            {

            }
            return (myList);
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
