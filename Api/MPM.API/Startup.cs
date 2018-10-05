using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MPM.Databases.Models;
using MPM.Model;
using MPM.Repository;
using MPM.Repository.Interfaces;
using MPM.Services;
using MPM.Services.Interfaces;
using NJsonSchema;
using NSwag.AspNetCore;
using System.Reflection;
using System.Text;

namespace MPM.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<IEmailService, EmailService>();
            services.AddCors();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("TokenModel");
            services.Configure<TokenModel>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<TokenModel>();
            var key = Encoding.ASCII.GetBytes(appSettings.Key);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            var connection = Configuration.GetConnectionString("ConnectionString");
            services.AddDbContext<mpmContext>(options => options.UseSqlServer(connection));

            services.AddScoped<IPriorityRepository, PriorityRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUserProjectRepository, UserProjectRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITypeRepository, TypeRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder => builder.WithOrigins("*")
                                    .AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowCredentials());

            app.UseAuthentication();
            app.UseHttpsRedirection();

            // Enable the Swagger UI middleware and the Swagger generator
            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;
            });

            app.UseMvc();
        }
    }
}
