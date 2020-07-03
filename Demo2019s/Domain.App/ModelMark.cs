using Domain.Base;

namespace Domain.App
{
    public class ModelMark : DomainEntityIdMetadata
    {
        public string Mark { get; set; } = default!;
        public string Model { get; set; } = default!;
        
        public int ModelMarkSize{ get; set; } = default!;
    }
}

