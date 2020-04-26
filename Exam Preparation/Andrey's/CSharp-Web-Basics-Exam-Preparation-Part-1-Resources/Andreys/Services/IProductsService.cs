namespace Andreys.Services
{
    using Andreys.ViewModels.Products;
    using System.Collections.Generic;

    public interface IProductsService
    {
        IEnumerable<ProductsListingViewModel> GetAll();

        void Add(ProductInputModel inputModel);

        ProductDetailsViewModel GetById(int id);

        void Delete(int id);
    }
}
