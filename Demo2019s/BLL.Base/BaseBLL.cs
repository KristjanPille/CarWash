using System.Threading.Tasks;
using Contracts.BLL.Base;
using Contracts.DAL.Base;

namespace BLL.Base
{
    public class BaseBLL<TUnitOfWork> : IBaseBLL
        where TUnitOfWork: IBaseUnitOfWork
    {
        protected readonly TUnitOfWork unitOfWork;
        public BaseBLL(TUnitOfWork unitOfWork)
        {
            unitOfWork = this.unitOfWork;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await unitOfWork.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return unitOfWork.SaveChanges();
        }
    }
}