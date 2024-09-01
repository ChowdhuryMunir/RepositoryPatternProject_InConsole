using Repository_Domain;
using Repository_Source;

namespace Repository_Pattern
{
    // ProductXMLRepository for specific product repository operations.
    public class ProductXMLRepository : XMLRepositoryBase<XMLSet<Product>, Product, int>, IProductRepository
    {
        public ProductXMLRepository() : base("ProductInformation.xml")
        {
        }
    }

}
