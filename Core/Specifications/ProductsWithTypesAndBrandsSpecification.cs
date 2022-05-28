using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
#pragma warning disable CS8603 // Possible null reference return.
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public ProductsWithTypesAndBrandsSpecification(int id)
            : base(x => x.Id == id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}