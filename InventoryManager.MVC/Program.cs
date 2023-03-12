using InventoryManager.BLL.Implementation;
using InventoryManager.BLL.Interfaces;
using InventoryManager.DAL.Entities;
using InventoryManager.DAL.Seeds;
using Microsoft.EntityFrameworkCore;
using TodoList.DAL.Repository;

namespace InventoryManager.MVC
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<InventoryManagerDbContext>(options => {
                var defualtConnectionString = builder.Configuration.GetConnectionString("DefaultConn");
                options.UseSqlServer(defualtConnectionString);
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork<InventoryManagerDbContext>>();
            builder.Services.AddScoped<IUserService, UserService>();//todo: show other life-cycles
            builder.Services.AddScoped<IProductService, ProductService>();//todo: show other life-cycles

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Start}/{action=Index}/{id?}");

            await SeedData.EnsurePopulatedAsync(app);
            app.Run();
        }
    }
}