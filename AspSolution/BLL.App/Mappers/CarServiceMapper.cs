﻿
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;
namespace BLL.App.Mappers
{
    public class CarServiceMapper : BLLMapper<DALAppDTO.Car, BLLAppDTO.Car>, ICarServiceMapper
    {
        
    }
}