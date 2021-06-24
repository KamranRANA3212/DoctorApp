using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DoctorApp.CustomFilters;
using DoctorApp.Errors;
using DoctorApp.Interfaces;
using DoctorApp.Middlewares;
using DoctorApp.Modals;
using DoctorApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;

namespace DoctorApp
{
    public class Startup
    {
        public UserManager<ApplicationUser> _userManager;

        public RoleManager<IdentityRole> _roleManager;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        //public ILogger logger1 { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                  .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 0;

                //// Lockout settings.
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //options.Lockout.MaxFailedAccessAttempts = 5;
                //options.Lockout.AllowedForNewUsers = true;

                //// User settings.
                //options.User.AllowedUserNameCharacters =
                //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                //options.User.RequireUniqueEmail = false;
            });

            //services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    /* ClockSkew = TimeSpan.Zero,*/// default 5 min
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (true)
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    }
                };
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.WriteIndented = true;
            })
                .ConfigureApiBehaviorOptions(option => option.InvalidModelStateResponseFactory = actionContext =>
                 {
                     var errors = actionContext.ModelState.Where(z => z.Value.Errors.Count > 0).SelectMany(z => z.Value.Errors).Select(z => z.ErrorMessage);

                     var errorResponse = new APIValidationErrorsResponse()
                     {
                         Errors = errors
                     };

                     return new BadRequestObjectResult(errorResponse);
                 });

            services.AddSwaggerGen(c => c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "Api For Doctor/Patient App", Version = "v1" }));

            services.AddScoped<IUnitOfWork, UnitOfWorkService>();
            services.AddTransient<UserService, UserService>();
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimPrincipleFactory>();

            services.AddCors();

            StripeConfiguration.ApiKey = Configuration["StripeConfig:ApiKey"];
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "v1"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHsts();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            DefaultUsersAndRoles.SeedData(_userManager, _roleManager);

            app.UseMiddleware<ExceptionMiddleWare>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();//Redirect http to https
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors(option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}