using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace WebApplication1.Models.DAL
{
    public class AppDbContext: IDesignTimeDbContextFactory<AppDB>
    {
        public AppDB CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDB>();
            optionsBuilder.UseSqlServer("Server=tcp:test-server-fivd.database.windows.net,1433;Initial Catalog=ProductManagementDb;Persist Security Info=False;User ID=v.k.anand;Password=Vk@12340;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            return new AppDB(optionsBuilder.Options);
        }
    }
}
