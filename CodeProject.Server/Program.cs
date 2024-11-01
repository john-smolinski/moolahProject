using CodeProject.Server.Context;
using CodeProject.Server.Models.Mappings;
using CodeProject.Server.Providers;
using Microsoft.EntityFrameworkCore;

namespace CodeProject.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("CodeProject");
            builder.Services.AddDbContext<MoolahContext>(options =>  
            { 
                options.UseSqlServer(connectionString); 
            });
        
            // add provider factory and scoped services
            builder.Services.AddScoped<ProviderFactory>();
            builder.Services.AddScoped<HomeService>()
                .AddScoped<IProviderService, HomeService>(x => x.GetService<HomeService>()!);

            builder.Services.AddScoped<OfficeService>()
                .AddScoped<IProviderService, OfficeService>(x => x.GetService<OfficeService>()!);

            builder.Services.AddScoped<IProviderMapService, ProviderMapService>();

            builder.Services.AddSingleton<MappingProfile>(serviceProvider => new MappingProfile(serviceProvider));

            builder.Services.AddAutoMapper(cfg =>
            {
                // Add MappingProfile directly to AutoMapper configuration
                var serviceProvider = builder.Services.BuildServiceProvider();
                var mappingProfile = serviceProvider.GetRequiredService<MappingProfile>();
                cfg.AddProfile(mappingProfile);
            });

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("AllowVueApp",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:5173")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });


            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors("AllowVueApp");

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
