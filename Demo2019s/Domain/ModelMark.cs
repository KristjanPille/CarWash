using DAL.Base;

namespace Domain
{
    public class ModelMark : DomainEntity
    {
        public int ModelMarkId { get; set; }

        public string Mark { get; set; } = default!;
        public string Model { get; set; } = default!;
    }
}