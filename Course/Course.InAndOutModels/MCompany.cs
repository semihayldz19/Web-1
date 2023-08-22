using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.InAndOutModels
{
    public class MCompany
    {
        public object Response()
        {
            throw new NotImplementedException();
        }

        public class Form
        {
            public string? Id { get; set; }
            public string? CompanyName { get; set; }
            public string? Address { get; set; }
            public string? CompanyAbout { get; set; }
            public string? Phone { get; set; }
            public string? Mail { get; set; }
            public string? Website { get; set; }

        }
        public class FilterForm:MFilterFormBase 
        { 
        }

        public class Response
        {
            public string? Id { get; set; }
            public string? CompanyName { get; set; }
            public string? Address { get; set; }
            public string? CompanyAbout { get; set; }
            public string? Phone { get; set; }
            public string? Mail { get; set; }
            public string? Website { get; set; }

        }

        public class Repsonse
        {
        }
    }
}
