using System.Threading.Tasks;
using Contracts.BLL.Base;
using Contracts.DAL.Base;

namespace BLL.Base
{
    public class BaseBLL<TUnitOfWork> : IBaseBLL
        where TUnitOfWork: IBaseUnitOfWork
    {
        public Task<int> SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new System.NotImplementedException();
        }
    }
}