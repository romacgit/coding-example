using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StringValidation.Library.Models;
using StringValidation.Library.Repository;
using System.Diagnostics.CodeAnalysis;

namespace StringValidation.Library;

/// <summary>
/// Startup class for API
/// </summary>
public class Startup
{
    private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<Startup>();
    private readonly IConfiguration _configuration;

    public Startup([NotNull] IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularApp",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200") // Adjust to your Angular app URL
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Version = "v1",
                    Title = "StringValidator",
                    Description = string.Empty
                });

            var basePath = AppDomain.CurrentDomain.BaseDirectory ?? string.Empty;
            var xmlFileName = "StringValidator.xml";
            var xmlPath = Path.Combine(basePath, xmlFileName);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }
            else
            {
                Log.Error($"ConfigureServices - Could not find xml documentation on path {xmlPath} for swagger");
            }
        });

        //services.AddDbContextPool<DatabaseContext>(opt =>
        //{
        //    var connectionString = _configuration.GetConnectionString("DefaultConnection");
        //    if (string.IsNullOrEmpty(connectionString))
        //    {
        //        throw new Exception("Database not configured");
        //    }

        //    opt.UseSqlite(connectionString);
        //});

        // adding controllers
        services.AddControllers();
        //services.AddScoped<IUserInputRepository, UserInputRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure([NotNull] IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCors("AllowAngularApp");

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "StringValidator - API");
            c.RoutePrefix = string.Empty;
        });
        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        //UpdateDatabase(app);
    }

    //private static void UpdateDatabase([NotNull] IApplicationBuilder app)
    //{
    //    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
    //    {
    //        var context = serviceScope.ServiceProvider.GetService<DatabaseContext>();
    //        context.Database.Migrate();
    //    }
    //}
}

