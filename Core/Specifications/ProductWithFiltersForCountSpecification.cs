using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productParams)
            : base(x =>
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
                (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
            )
        {
        }
    }
}