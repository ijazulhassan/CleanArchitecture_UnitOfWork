using Application;
using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Persistance
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {

        public ProductRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public int Add(Products entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            entity.AddedOn = DateTime.Now;
            object Param = new
            {
                PName = entity.Name,
                pDescription = entity.Description,
                pPrice = entity.Price,
                pAddedOn = entity.AddedOn
            };
            var sql = "UspInsertProduct";
            return Connection.Execute(sql, 
                param:Param,
                transaction:Transaction,
                commandType:CommandType.StoredProcedure);
        }

        public int Delete(int id)
        {
            var sql = "UspRemoveProduct";
            return Connection.Execute(sql,
                param: new { pID = id },
                transaction: Transaction,
                commandType: CommandType.StoredProcedure);

        }

        public Products Get(int id)
        {
            var sql = "UspGetProduct";
            return Connection.Query<Products>(sql, param: new { pId = id },
                transaction: Transaction,
                commandType: CommandType.StoredProcedure).
                FirstOrDefault();
        }

        public IEnumerable<Products> GetAll()
        {
            var sql = "UspGetProducts";
            return Connection.Query<Products>(sql,
                transaction: Transaction,
                commandType: CommandType.StoredProcedure 
                );
        }



        public int Update(Products entity, int id)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            entity.ModifiedOn = DateTime.Now;
            object Param = new
            {
                pID = id,
                PName = entity.Name,
                pDescription = entity.Description,
                pPrice = entity.Price,
                pModifiedOn = entity.ModifiedOn
            };
            var sql = "UspUpdateProduct";
            return Connection.Execute(sql,
                param: Param,
                transaction: Transaction,
                commandType: CommandType.StoredProcedure);
        }
    }
}
