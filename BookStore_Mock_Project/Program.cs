using BookStore_Mock_Project.Data;
using BookStore_Mock_Project.Service;
using Microsoft.EntityFrameworkCore;

namespace BookStore_Mock_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddSignalR();
            // Thêm HttpContextAccessor
            builder.Services.AddHttpContextAccessor();
            // Đăng ký SessionService
            builder.Services.AddScoped<SessionService>();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });

            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            var app = builder.Build();

            DbInitializer.Initialize(new ApplicationDbContext());



            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            // Sử dụng Session Middleware
            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id}");
                endpoints.MapHub<SignalServer>("/signalrServer");
            });
            app.MapRazorPages();


            app.Run();
        }
    }
}
