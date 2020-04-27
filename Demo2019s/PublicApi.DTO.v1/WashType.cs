using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class WashType : WashTypeEdit
    {
    }
    
    public class WashTypeCreate
    {
        public string NameOfWash { get; set; } = default!;
    }
    
    public class WashTypeEdit : WashTypeCreate
    {
        public Guid Id { get; set; }
    }
}