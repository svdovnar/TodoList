using Microsoft.EntityFrameworkCore;
using TodoList.DAL;
using TodoList.DAL.Interfaces;
using TodoList.DAL.Repositories;
using TodoList.Domain.Entity;
using TodoList.Service.Implementations;
using TodoList.Service.Interfaces;


namespace TodoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            builder.Services.AddScoped<IBaseRepository<TaskEntity>, TaskRepository>();
            builder.Services.AddScoped<ITaskService, TaskService>();


            var connectionString = builder.Configuration.GetConnectionString("MSSQL");
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            var app = builder.Build();

            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Task}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
