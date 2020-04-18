using System;
using Domain;

namespace PublicApi.DTO.v1.Mappers
{
    public class DTOMapper
    {
        /*
        public OwnerCreate MapOwnerCreate(BLL.App.DTO.Owner BLLOwner)
        {
            
        }
        
        public OwnerEdit MapOwnerEdit(BLL.App.DTO.Owner BLLOwner)
        {
            
        }
        */
        
        public Person MapOwner(BLL.App.DTO.Person BLLPerson)
        {
            return new Person()
            {
                Id = BLLPerson.Id,
                FirstName = BLLPerson.FirstName,
                LastName = BLLPerson.LastName,
            }; 
        }
        
        
    }
}