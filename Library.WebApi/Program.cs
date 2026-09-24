using LibraryApi.Business.Interfaces;
using LibraryApi.Business.Interfaces.Repositories;
using LibraryApi.Business.Interfaces.Services;
using LibraryApi.Business.Services;
using LibraryApi.DataAccess;
using LibraryApi.DataAccess.Data;
using LibraryApi.DataAccess.Repositories;
using LibraryApi.Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using LibraryApi.Business.DTOs.Books;

var builder = WebApplication.CreateBuilder(args);

var renderPort = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrWhiteSpace(renderPort))
{
    builder.WebHost.UseUrls($"http://0.0.0.0:{renderPort}");
}

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",
            "https://library-api-pink.vercel.app"    
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServer => sqlServer.EnableRetryOnFailure(
            maxRetryCount: 2,
            maxRetryDelay: TimeSpan.FromSeconds(2),
            errorNumbersToAdd: null)));

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];
if (string.IsNullOrWhiteSpace(secretKey) || Encoding.UTF8.GetByteCount(secretKey) < 32)
{
    throw new InvalidOperationException("JwtSettings:SecretKey ortam değişkeni en az 32 bayt olmalıdır.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],

        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),

        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateBookDtoValidator>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<ILoanRepository,LoanRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IMemberService, MemberService>();


builder.Services.AddScoped<IGenericRepository<Member>, MemberRepository>();
builder.Services.AddScoped<IGenericService<Member>, GenericService<Member>>();

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ILoanService, LoanService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT token girin. Örnek: Bearer {token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();

app.UseMiddleware<LibraryApi.WebApi.Middleware.ExceptionHandlingMiddleware>();

app.UseCors("AllowReactApp");

app.UseSwagger();
app.UseSwaggerUI();



app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.MapGet("/live", () => Results.Ok(new { status = "healthy" }));
app.MapGet("/health", async (LibraryDbContext db, CancellationToken cancellationToken) =>
    await db.Database.CanConnectAsync(cancellationToken)
        ? Results.Ok(new { status = "healthy" })
        : Results.StatusCode(StatusCodes.Status503ServiceUnavailable));

app.Run();
