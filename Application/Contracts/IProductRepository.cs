using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts;
using Domain;

namespace Application
{
    public interface IProductRepository : IGenericRepository<Products>
    {
        
    }
}
