using System.Data;

namespace Persistance
{
    public abstract class RepositoryBase
    {
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { get; private set; }


        public RepositoryBase(IDbTransaction transaction)
        {
            Transaction = transaction;
            Connection = transaction.Connection;
        }



    }
}
