namespace Andreys.App
{
    using System.Collections.Generic;

    using Data;

    using SIS.MvcFramework;
    using SIS.HTTP;
    using Andreys.Services;
    using Microsoft.EntityFrameworkCore;

    public class Startup : IMvcApplication
    {
        public void Configure(IList<Route> serverRoutingTable)
        {
            new AndreysDbContext().Database.Migrate();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IProductsService, ProductsService>();
        }
    }
}
