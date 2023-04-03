using Api.src.Authorization;
using Api.src.Database;
using Api.src.Middlewares;
using Api.src.Repositories.AuthRepo;
using Api.src.Repositories.ProductRepo;
using Api.src.Repositories.UserRepo;
using Api.src.Services.AuthService;
using Api.src.Services.ProductService;
using Api.src.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

      builder.WebHost.UseKestrel(options =>
        {
            options.ListenLocalhost(5000); //http: no data encription
            options.ListenLocalhost(5001, options => options.UseHttps()); // https: with encription
        }); 
               builder.Services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(
                "oauth2",
                new OpenApiSecurityScheme
                {
                    Description = "Bearer token authentication",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                }
            );
            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });
        builder.Services.AddDbContext<DatabaseContext>();
        builder.Services.AddAutoMapper(typeof(Program).Assembly);

        builder.Services
            .AddScoped<IUserRepo, UserRepo>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IProductRepo, ProductRepo>()
            .AddScoped<IProductService, ProductService>()
            .AddScoped<IAuthRepo, AuthRepo>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<ICategoryRepo, CategoryRepo>()
            .AddScoped<ICategoryService, CategoryService>();

        builder.Services.AddTransient<ErrorHandlerMiddleware>().AddTransient<LoggerMiddleware>();

        builder.Services.AddTransient<IAuthorizationHandler, UpdateUserPermission>();

        /* add configuration for authentication middleware */
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(
                            builder.Configuration.GetSection("AppSettings:Token").Value!
                        )
                    )
                };
            });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            options.AddPolicy("AdminOrOwner", policy => policy.AddRequirements(new UpdateUserRequirement()));
        });

        var app = builder.Build();

        app.UseHttpsRedirection();
        // Configure the HTTP request pipeline.S
        /*         if (app.Environment.IsDevelopment())
                { */
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo");
            options.RoutePrefix = string.Empty;
        });
        /*   } */

        app.UseMiddleware<ErrorHandlerMiddleware>();

        app.UseMiddleware<LoggerMiddleware>();

        app.UseAuthentication(); /* check if user exists in database ? */

        app.UseAuthorization(); /*  personal information, validation */

        app.MapControllers();

        app.Run();
    }
}
