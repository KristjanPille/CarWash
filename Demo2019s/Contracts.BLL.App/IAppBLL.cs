using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        public IPersonService Persons { get; }
        public ICarService Cars { get; }
        public IPersonCarService PersonCars { get; }
    }
}