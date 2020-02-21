using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Service
    {
        public int serviceID { get; set; }
        [MaxLength(64)]
        public string nameOfService { get; set; }
        
        public int campaignID { get; set; }
        public ICollection<Campaign>? Campaign { get; set; }
    }
}