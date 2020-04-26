namespace Andreys.Services
{
    using Andreys.Data;
    using Andreys.Models;
    using Andreys.ViewModels.Products;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductsService : IProductsService
    {
        private readonly AndreysDbContext db;

        public ProductsService(AndreysDbContext db)
        {
            this.db = db;
        }

        public void Add(ProductInputModel inputModel)
        {
            var product = new Product()
            {
                Name = inputModel.Name,
                Price = inputModel.Price,
                Description = inputModel.Description,
                Category = Enum.Parse<Category>(inputModel.Category),
                Gender = Enum.Parse<Gender>(inputModel.Gender),
                ImageUrl = inputModel.ImageUrl,
            };

            this.db.Products.Add(product);
            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = this.db.Products.Where(x => x.Id == id).FirstOrDefault();
            this.db.Products.Remove(product);
            this.db.SaveChanges();
        }

        public IEnumerable<ProductsListingViewModel> GetAll()
        => this.db.Products.Select(x => new ProductsListingViewModel()
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price,
            ImageUrl = x.ImageUrl,
        }).ToArray();

        public ProductDetailsViewModel GetById(int id)
        => this.db.Products.Where(x => x.Id == id).Select(x => new ProductDetailsViewModel()
        {
            Name = x.Name,
            Id = x.Id,
            Price = x.Price,
            Description = x.Description,
            ImageUrl = x.ImageUrl,
            Category = x.Category.ToString(),
            Gender = x.Gender.ToString(),
        })
        .FirstOrDefault();
    }
}
