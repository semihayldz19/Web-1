using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.InAndOutModels
{
   public class MethodResponse<T>
    {
        public int Status { get; set; } = 200;
        public List<String> StatusTexts { get; set; } = new List<string>();
        public T Item { get; set; } // by ıtembir veri eklediğimizde veriyi görmemiizi sağlar
        public long count { get; set; } = 0;
        public TimeSpan WorkingTime { get; set; }
        public object StatusText { get; set; }
    }
}
