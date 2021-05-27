using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RunLocalPowershell.Models
{
    public class User
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LogonName { get; set; }

        public string DisplayName { get; set; }

        public string EMail { get; set; }
        public string MobileNR { get; set; }
        public string TelephoneNR { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string Kostenstelle { get; set; }
        public string Department { get; set; }
        public string Office { get; set; }

        public string Manager { get; set; }
        public int Accounttype { get set; }
        
        public string Street { get; set; }
        public string City { get; set; }
        public string ZIP { get; set; }
        public string WebPage { get; set; }
        public string Gruppenmitgliedschaft { get; set; }
        [DefaultValue(null)]
        public DateTime? ExpirationDate { get; set; }
        public string License { get; set; }

        public string Password { get; set; }

    }
}
