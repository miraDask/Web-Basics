namespace Andreys.Controllers
{
    using Andreys.Services;
    using Andreys.ViewModels.Products;
    using SIS.HTTP;
    using SIS.MvcFramework;

    class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(ProductInputModel inputModel)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(inputModel.Name) || inputModel.Name.Length < 4 || inputModel.Name.Length > 20)
            {
                return this.View();
            }

            if (inputModel.Description.Length > 10)
            {
                return this.View();
            }

            this.productsService.Add(inputModel);
            return this.Redirect("/");
        }

        public HttpResponse Details(int id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var product = this.productsService.GetById(id);
            return this.View(product);
        }

        public HttpResponse Delete(int id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.productsService.Delete(id);
            return this.Redirect("/");
        }

    }
}
