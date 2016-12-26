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
        public List<NOSQL_Interface>  Tasks = null;
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
        public JsonResult ListTasks(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            string filter;
            string SortFieldName = "";
            string SortOrder = "";

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
                    jsonWriter.WritePropertyName("UserId");
                    jsonWriter.WriteValue("hgulseven");
                    jsonWriter.WritePropertyName("ProcessStep");
                    jsonWriter.WriteValue("Teklif");

                    jsonWriter.WriteEndObject();

                    filter = sw.ToString();
                    Tasks = myInterface.noSQL_getListOfDocuments(filter);
/*
                    //Get data from database
                    Tasks = new List<Models.ListTasks>()
                {
                    new Models.ListTasks() {ProcessStep="Teklif",AgencyId="1000",UserId="hgulseven", ProposalNo="2016000001001",CustomerName="Hakan",CustomerSurname="Gülseven",ProductName="Kasko" },
                    new Models.ListTasks() { ProcessStep="Tahsilat",AgencyId="1000",UserId="hgulseven", ProposalNo="2016000001002",CustomerName="Ege",CustomerSurname="Gülseven",ProductName="Trafik" },
                    new Models.ListTasks() { ProcessStep="Poliçeleştirme",AgencyId="1000",UserId="hgulseven", ProposalNo="2016000001003",CustomerName="Merih",CustomerSurname="Gülseven",ProductName="Yangın" }
                };
*/
                }
                catch (Exception ex)
                {
                    return Json(new { Result = "ERROR", Message = ex.Message });
                }
            }
            if (jtSorting != null)
            {
                SortFieldName = jtSorting.Substring(0, jtSorting.LastIndexOf(' '));
                SortOrder = jtSorting.Substring(jtSorting.LastIndexOf(' ') + 1, 3);
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
            }
            if (SortOrder.CompareTo("DES") == 0)
            {
                Tasks.Reverse();
            }
            //Return result to jTable
            return Json(new { Result = "OK", Records = Tasks, TotalRecordCount = Tasks.Count });

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
