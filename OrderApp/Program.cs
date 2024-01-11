using DotNetEnv;
using Microsoft.AspNetCore.Identity;
using OrderApp.Endpoints.Categories;
using OrderApp.Endpoints.Employees;
using OrderApp.Infra.Data;

var builder = WebApplication.CreateBuilder(args);

// Load the environment variables from the .env file
Env.Load();
Env.TraversePath().Load();

// Get the database connection string from the .env file
string connectionString = Env.GetString("DB_CONNECTION_STRING");

// Add services to the container.

builder.Services.AddSqlServer<ApplicationDbContext>(connectionString);

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapMethods(CategoryPost.Template, CategoryPost.Methods, CategoryPost.Handle);
app.MapMethods(CategoryGet.Template, CategoryGet.Methods, CategoryGet.Handle);
app.MapMethods(CategoryPut.Template, CategoryPut.Methods, CategoryPut.Handle);
app.MapMethods(EmployeePost.Template, EmployeePost.Methods, EmployeePost.Handle);
app.MapMethods(EmployeeGet.Template, EmployeeGet.Methods, EmployeeGet.Handle);

app.Run();