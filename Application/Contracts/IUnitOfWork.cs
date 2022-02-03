using System;

namespace Application
{
    public interface IUnitOfWork: IDisposable
    {
        IProductRepository ProductRepository { get; }
        void Commit();



    }
}