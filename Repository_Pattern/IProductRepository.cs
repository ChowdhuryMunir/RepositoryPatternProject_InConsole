using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository_Domain;

namespace Repository_Pattern
{
    // IProductRepository interface specific to the Product entity.
    public interface IProductRepository : IRepository<Product, int>
    {
    }
}
