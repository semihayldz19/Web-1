using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.InAndOutModels
{
    public class MFilterFormBase
    {
        public string? Search { get; set; }
        public int? Offset { get; set; } = 0;
        public int? Take { get; set; } = 10;
        public FSort? Sort { get; set; } = new FSort();



        public class FSort
        {
            public string? Column { get; set; } = "";
            public string? Type { get; set; } = "DESC";
        }


    }
}


