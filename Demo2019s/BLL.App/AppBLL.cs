using System;
using System.Threading.Tasks;
using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using DAL.App.EF;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IPersonService Persons =>
            GetService<IPersonService>(() => new PersonService(UnitOfWork));

        public ICarService Cars =>
            GetService<ICarService>(() => new CarService(UnitOfWork));

        public IPersonCarService PersonCars =>
            GetService<IPersonCarService>(() => new PersonCarService(UnitOfWork));
    }
}