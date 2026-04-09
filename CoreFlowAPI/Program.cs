using AutoMapper;
using CoreFlowAPI.Business.Interface;
using CoreFlowAPI.Business.Middleware;
using CoreFlowAPI.Business.Services;
using CoreFlowAPI.Data.Infrastructure;
using CoreFlowSharedLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

namespace CoreFlowAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Autentisering
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

            // 2. Controllers & JSON-inställningar (Flyttat från första programmet)
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                // Gör att Enums visas som strängar istället för siffror
                options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
            });

            // 3. Swagger & Schema
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SchemaFilter<EnumSchemaFilter>();
            });

            // 4. Core Services
            builder.Services.AddDataAccess(builder.Configuration);
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddValidators(builder.Configuration);

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
            });

            // 5. CORS-Policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // 6. HttpClient & Email Integration
            builder.Services.AddHttpClient("EmailApi", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["EmailApi:BaseUrl"] ?? "https://localhost:7012");
                client.Timeout = TimeSpan.FromSeconds(30);
            });
 

            var app = builder.Build();

            // --- Middleware Pipeline ---

            if (app.Environment.IsDevelopment())
            {
                await DatabaseInitializer.InitAsync(app.Configuration);
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseCors("AllowReactApp");

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}