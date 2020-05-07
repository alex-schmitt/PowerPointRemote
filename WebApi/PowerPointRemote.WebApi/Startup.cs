using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PowerPointRemote.WebApi.ApplicationSettings;
using PowerPointRemote.WebAPI.Data;
using PowerPointRemote.WebApi.Hubs;

namespace PowerPointRemote.WebApi
{
    public class Startup
    {
        private const string DevelopmentCorsPolicy = "_developmentCorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
                options.AddPolicy(DevelopmentCorsPolicy,
                    builder => builder
                        .WithOrigins("http://127.0.0.1:3000", "http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials())
            );

            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                var mySqlSettings = Configuration.GetSection("MySql").Get<MySqlSettings>();
                options.UseMySql(
                    $"Server={mySqlSettings.Server};Database={mySqlSettings.Database};User={mySqlSettings.User};Password={mySqlSettings.Password};");
            });

            services.AddControllers();
            services.AddSignalR();
            services.AddMemoryCache();

            var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();
            services.AddSingleton(s => jwtSettings);

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    options.Events = new JwtBearerEvents
                    {
                        // SignalR includes the access token in the query string due to limitations in Browser APIs.
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            if (!string.IsNullOrEmpty(accessToken)) context.Token = accessToken;

                            return Task.CompletedTask;
                        }
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();

            if (env.IsDevelopment())
                app.UseCors(DevelopmentCorsPolicy);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<UserHub>("hub/user");
                endpoints.MapHub<HostHub>("hub/host");
                endpoints.MapControllers();
            });
        }
    }
}