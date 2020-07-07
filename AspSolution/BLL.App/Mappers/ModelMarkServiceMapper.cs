using System.Threading.Tasks;

using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;
namespace BLL.App.Mappers
{
    public class ModelMarkServiceMapper : BLLMapper<DALAppDTO.ModelMark, BLLAppDTO.ModelMark>, IModelMarkServiceMapper
    {

    }
}