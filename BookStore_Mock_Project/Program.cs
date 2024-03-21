using BookStore_Mock_Project.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore_Mock_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Thêm dịch vụ session vào ứng dụng
            builder.Services.AddSession(options =>
            {
                // Thiết lập thời gian sống mặc định cho session (ở đây là 20 phút)
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });

            // Add services to the container.
            builder.Services.AddRazorPages();
            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            var app = builder.Build();

            DbInitializer.Initialize(new ApplicationDbContext());



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();


            app.Run();
        }
    }
}
