using Newtonsoft.Json.Serialization;
using BuisnessLayer.Configuration;
using SharedLayer.Core.ExceptionManagement;
using AutoMapper;
using BuisnessLayer.Mapper;
using Book_Borrowing_System.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Book_Borrowing_System.DTO;
using Book_Borrowing_System.Service;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

namespace Book_Borrowing_System
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        private MapperConfiguration MapperConfiguration { get; set; }

        private IExceptionManager exceptionManager;


        public Startup(IConfigurationRoot configuration)
        {
            Configuration = configuration;
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
                cfg.AddProfile(new ApiMappingProfile());
            });
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(sp => MapperConfiguration.CreateMapper());

            services.RegisterServices(Configuration.GetConnectionString("DefaultConnection"));

            services.AddScoped<IFileService, FileService>();

            var _jwtsetting = Configuration.GetSection("JWTSetting");
            services.Configure<JWTSetting>(_jwtsetting);

            var authkey = Configuration.GetValue<string>("JWTSetting:securitykey");

            services.AddAuthentication(item =>
            {
                item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(item =>
            {

                item.RequireHttpsMetadata = true;
                item.SaveToken = true;
                item.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authkey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddMvc().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                s.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            SharedLayer.Core.Logging.ILogger logger = new SharedLayer.Core.Logging.Logger();
            exceptionManager = new ExceptionManager(logger);
            services.AddScoped<SharedLayer.Core.Logging.ILogger, SharedLayer.Core.Logging.Logger>();
            services.AddScoped<IExceptionManager, ExceptionManager>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MyAPI",
                    Description = "ASP.NET Core API"
                });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Uploads")),
                RequestPath = "/Resources"
            });
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI");
                c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
