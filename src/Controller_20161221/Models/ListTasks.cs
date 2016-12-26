using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controller_20161221.Models
{
    public class ListTasks
    {
        public string ProcessStep { get; set; }
        public string AgencyId { get; set; }
        public string UserId { get; set; }
        public string ProposalNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string ProductName { get; set; }

        public static int compareByProcessStep(ListTasks item1, ListTasks item2)
        {
            return item1.ProcessStep.CompareTo(item2.ProcessStep);
        }

        public static int compareByProposalNo(ListTasks item1, ListTasks item2)
        {
            return item1.ProposalNo.CompareTo(item2.ProposalNo);
        }
        public static int compareByCustomerName(ListTasks item1, ListTasks item2)
        {
            return item1.CustomerName.CompareTo(item2.CustomerName);
        }
        public static int compareByCustomerSurName(ListTasks item1, ListTasks item2)
        {
            return item1.CustomerSurname.CompareTo(item2.CustomerSurname);
        }
        public static int compareByProductName(ListTasks item1, ListTasks item2)
        {
            return item1.ProductName.CompareTo(item2.ProductName);
        }
    }
}
