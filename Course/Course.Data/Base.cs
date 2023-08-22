using Course.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Data
{
    public  class Base
    {
    
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public EuIsDeleted IsDeleted { get; set; } = EuIsDeleted.No; //burda isdeleted true silinmiş kayıtlar false silinmemiş kayıtlar
        


    }
}
