using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class Issue
    {
        public Issue()
        {

        }

        public Issue(int issueID, int customerID, string issueCategory, string issueDescription, string issueStatus, DateTime issueDate)
        {
            IssueID = issueID;
            CustomerID = customerID;
            IssueCategory = issueCategory;
            IssueDescription = issueDescription;
            IssueStatus = issueStatus;
            IssueDate = issueDate;
        }

        public int IssueID { get; set; }
        public int CustomerID { get; set; }
        public string IssueCategory { get; set; }
        public string IssueDescription { get; set; }
        public string IssueStatus { get; set; }
        public DateTime IssueDate { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
