using System.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using WebApi.Factories;
using WebApi.Middlewares;
using WebApi.Models.Context;
using WebApi.Repositories;

namespace WebApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("Postgre");

            services.AddControllers();
            services.AddSwaggerGen();
            services.AddScoped(typeof(ICustomerRepository<>), typeof(CustomerRepository<>));
            services.AddScoped<IConnectionFactory, ConnectionFactory>();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseNpgsql(connection);
                options.UseSnakeCaseNamingConvention();
            });

            services.AddScoped(typeof(DbContext), typeof(DataContext));

            services.AddScoped<IDbConnection>(e => new NpgsqlConnection(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<Interceptor>();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}