using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Mental_web.Data;

namespace Mental_web
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    var connectionString = context.Configuration.GetConnectionString("DefaultConnection") 
                        ?? "Server=localhost;Database=mental_health;User=root;Password=;";
                    services.AddDbContext<MentalHealthContext>(options =>
                        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

                    services.AddScoped<AuthService>();
                    services.AddTransient<MainForm>();
                })
                .Build();

            ServiceProvider = host.Services;

            using var loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK && loginForm.AuthenticatedUser != null)
            {
                var mainForm = ServiceProvider.GetRequiredService<MainForm>();
                mainForm.SetUserSession(loginForm.AuthenticatedUser);
                Application.Run(mainForm);
            }
        }
    }
}