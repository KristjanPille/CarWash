using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class PersonType
    {
        public int personTypeID { get; set; }
        [MaxLength(64)]
        public string name { get; set; }
    }
}