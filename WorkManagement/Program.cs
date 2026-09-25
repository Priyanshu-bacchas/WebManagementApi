using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using WorkManagement.Models;

using WorkManagement.Repositories.Implementations;
using WorkManagement.Repositories.Interfaces;

using WorkManagement.Services.Implementations;
using WorkManagement.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


// ========================================
// DATABASE
// ========================================

builder.Services.AddDbContext<WorkManagementDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


// ========================================
// JWT AUTHENTICATION
// ========================================

var jwtSection = builder.Configuration.GetSection("Jwt");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    jwtSection["Key"]!))
        };
    });


// ========================================
// AUTHORIZATION
// ========================================

builder.Services.AddAuthorization();


// ========================================
// CONTROLLERS
// ========================================

builder.Services.AddControllers();


// ========================================
// SWAGGER
// ========================================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",

        Type = SecuritySchemeType.Http,

        Scheme = "Bearer",

        BearerFormat = "JWT",

        In = ParameterLocation.Header,

        Description = "Enter: Bearer {your JWT token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },

            Array.Empty<string>()
        }
    });
});


// ========================================
// REPOSITORIES
// ========================================

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

builder.Services.AddScoped<IProjectMemberRepository, ProjectMemberRepository>();

builder.Services.AddScoped<IWorkItemRepository, WorkItemRepository>();

builder.Services.AddScoped<ICommentRepository, CommentRepository>();


// ========================================
// SERVICES
// ========================================

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddScoped<IProjectMemberService, ProjectMemberService>();

builder.Services.AddScoped<IWorkItemService, WorkItemService>();

builder.Services.AddScoped<ICommentService, CommentService>();


// ========================================
// CORS
// ========================================

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// ========================================
// BUILD APP
// ========================================

var app = builder.Build();


// ========================================
// SWAGGER
// ========================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}


// ========================================
// MIDDLEWARE
// ========================================

app.UseHttpsRedirection();

// IMPORTANT:
// React UI = http://localhost:5173
// Backend = https://localhost:7081

app.UseCors("ReactPolicy");

app.UseAuthentication();

app.UseAuthorization();


// ========================================
// MAP CONTROLLERS
// ========================================

app.MapControllers();


// ========================================
// RUN
// ========================================

app.Run();