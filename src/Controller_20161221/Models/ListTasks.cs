using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controller_20161221.Models
{
    public class ListTasks
    {
        public string processStep { get; set; }
        public string agencyCode { get; set; }
        public string userId { get; set; }
        public string proposalNo { get; set; }
        public string customerName { get; set; }
        public string customerSurname { get; set; }
        public string productName { get; set; }
        public DateTime proposalDate { get; set; }
        public int validDays { get; set; }

        public static int compareByProcessStep(ListTasks item1, ListTasks item2)
        {
            return item1.processStep.CompareTo(item2.processStep);
        }

        public static int compareByProposalNo(ListTasks item1, ListTasks item2)
        {
            return item1.proposalNo.CompareTo(item2.proposalNo);
        }
        public static int compareByCustomerName(ListTasks item1, ListTasks item2)
        {
            return item1.customerName.CompareTo(item2.customerName);
        }
        public static int compareByCustomerSurName(ListTasks item1, ListTasks item2)
        {
            return item1.customerSurname.CompareTo(item2.customerSurname);
        }
        public static int compareByProductName(ListTasks item1, ListTasks item2)
        {
            return item1.productName.CompareTo(item2.productName);
        }
    }
}
