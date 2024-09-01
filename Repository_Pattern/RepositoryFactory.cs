using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Pattern
{
    // RepositoryFactory for creating repositories based on context types.
    public static class RepositoryFactory
    {
        public static TRepository Create<TRepository>(ContextTypes ctype) where TRepository : class
        {
            if (typeof(TRepository) == typeof(IProductRepository))
            {
                return new ProductXMLRepository() as TRepository;
            }
            return null;
        }
    }
}