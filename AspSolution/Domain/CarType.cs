using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class CarType
    {
        public int carTypeID { get; set; }
        [MaxLength(64)]
        public string name { get; set; }
    }
}