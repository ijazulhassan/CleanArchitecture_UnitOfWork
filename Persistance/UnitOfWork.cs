using Application;
using Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Persistance
{
    internal class UnitOfWork : IUnitOfWork
    {

        private  IDbConnection _connection;
        private  IDbTransaction _transaction;
        private  IProductRepository _productRepository;
        private  IConfiguration _configuration;
        private  bool _disposed;

        public UnitOfWork( IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DB"));
            _connection.Open();
            _transaction = _connection.BeginTransaction();

        }

        public IProductRepository ProductRepository
        {
            get { return _productRepository ??= new ProductRepository(_transaction); }   
        }


        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
            }
            finally
            {
                _transaction.Dispose();
                _transaction=_connection.BeginTransaction();
                resetRepositories();
            }
        }
        private void resetRepositories()
        {
            _productRepository = null;
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if(_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
               
                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposed = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~UnitOfWork()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

       
    }
}
