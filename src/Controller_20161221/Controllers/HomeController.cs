using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Controller_20161221.Models;
using Controller_20161221.Models.ManageViewModels;
using System.Text;
using System.IO;

namespace Controller_20161221.Controllers
{
    public class HomeController : Controller
    {
        screenParts menuAndFunctions;
        public List<ListTasks>  Tasks = null;
        NOSQL_Interface myInterface;



        public HomeController(IOptions<MyOptions> o)
        {
            menuAndFunctions = new screenParts();
            menuAndFunctions.Filters = o.Value.filters;
            menuAndFunctions.Functions = o.Value.functions;
        }

        public IActionResult Index()
        {
            return View(menuAndFunctions);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpPost]
        [HttpGet]
        public string ListTasks(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            string filter=null;
            string SortFieldName = "";
            string SortOrder = "";
            int error=0;
            string records=null;
            string retval=null;
            int noOfRecords = 0;

            if (Tasks == null)
            {
                try
                {
                    
                    myInterface = new NOSQL_Interface();
                    myInterface.NOSQL_connect();
                    StringBuilder sb = new StringBuilder();
                    StringWriter sw = new StringWriter(sb);
                    JsonWriter jsonWriter = new JsonTextWriter(sw);
                    jsonWriter.WriteStartObject();
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.WriteValue("hgulseven");
                    jsonWriter.WritePropertyName("processStep");
                    jsonWriter.WriteValue("Teklif");
                    jsonWriter.WriteEndObject();
                    filter = sw.ToString();

                    records= myInterface.noSQL_getListOfDocuments(filter, ref error,ref noOfRecords,jtStartIndex,jtPageSize);

                    sb = new StringBuilder();
                    sw = new StringWriter(sb);
                    jsonWriter = new JsonTextWriter(sw);
                    jsonWriter.WriteStartObject();
                    jsonWriter.WritePropertyName("result");
                    jsonWriter.WriteValue("OK");
                    jsonWriter.WritePropertyName("records");
                    jsonWriter.WriteRawValue(records);
                    jsonWriter.WritePropertyName("TotalRecordCount");
                    jsonWriter.WriteValue(noOfRecords);
                    jsonWriter.WriteEndObject();
                    retval= sw.ToString();
                }
                catch (Exception ex)
                {
                    return ("");/* Json(new { Result = "ERROR", Message = ex.Message });*/
                }
            }
            if (jtSorting != null)
            {
                SortFieldName = jtSorting.Substring(0, jtSorting.LastIndexOf(' '));
                SortOrder = jtSorting.Substring(jtSorting.LastIndexOf(' ') + 1, 3);
/*
                switch (SortFieldName)
                {
                    case "processStep":
                        Tasks.Sort(Models.NOSQL_Interface.compareByProcessStep);
                        break;
                    case "proposalNo":
                        Tasks.Sort(Models.NOSQL_Interface.compareByProposalNo);
                        break;
                    case "customerName":
                        Tasks.Sort(Models.NOSQL_Interface.compareByCustomerName);
                        break;
                    case "customerSurName":
                        Tasks.Sort(Models.NOSQL_Interface.compareByCustomerSurName);
                        break;
                    case "productName":
                        Tasks.Sort(Models.NOSQL_Interface.compareByProductName);
                        break;
                }
*/
            }
            if (SortOrder.CompareTo("DES") == 0)
            {
                Tasks.Reverse();
            }
            //Return result to jTable
            return (retval);
/*            return Json(new { Result = "OK", Records = Tasks, TotalRecordCount = Tasks.Count });*/

        }

        public IActionResult Contact()
        {
            return View(menuAndFunctions);
        }

        public IActionResult Error()
        {
            return View();
        }
    }



}
