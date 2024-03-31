using System.Data;
using System.IO;
using EmployeeService.Data;
using EmployeeService.Data.Repos.CompanyRepo;
using EmployeeService.Data.Repos.DepartmentRepo;
using EmployeeService.Data.Repos.EmployeeRepo;
using EmployeeService.Data.Repos.PassportRepo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace EmployeeService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var defaultConnectionString = Configuration.GetConnectionString("DefaultConnection");
            var initialConnectionString = Configuration.GetConnectionString("InitialConnection");
            const string scriptTableCreateFileName = "CreateTables.sql";
            const string scriptTablesMockDataFillFileName = "TablesMockData.sql";
            var scriptFilePath = Path.Combine("..", "EmployeeService.Data", "Scripts", scriptTableCreateFileName);
            var scriptTablesMockDataFillPath = Path.Combine("..", "EmployeeService.Data", "Scripts", scriptTablesMockDataFillFileName);

            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IPassportRepository, PassportRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeeService", Version = "v1" });
            });

            using (var initialConnection = new SqlConnection(initialConnectionString))
            {
                var initializer = new DatabaseInitializer(initialConnection, scriptFilePath, scriptTablesMockDataFillPath);
                initializer.InitializeDatabase();
            }

            services.AddTransient<IDbConnection>(sp =>
                new SqlConnection(defaultConnectionString));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeeService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}