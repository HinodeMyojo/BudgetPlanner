using BudgetPlanner.Application;
using BudgetPlanner.Core;
using BudgetPlanner.DataBase;
using BudgetPlanner.DataBase.Repositories;
using BudgetPlanner.Logic;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// Add services to the container.
//��������� DbContext � ��������� DI
builder.Services.AddDbContext<BudgetPlannerDbContext>(
    options =>
    {
        options.UseNpgsql("Host=localhost;Username=postgres;Password=SkyCote36;Database=bpdbtest546456456");
    });

//���������� ��������
builder.Services.AddScoped<OperationsService>();
builder.Services.AddScoped<UsersService>();

//���������� ������������
builder.Services.AddScoped<OperationsRepository>();
builder.Services.AddScoped<UsersRepository>();

builder.Services.AddScoped<PasswordHasher>();
builder.Services.AddScoped<JWTProvider>();

//����������� ����������� �� ������ JWT
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            //���������, ����� �� �������������� �������� ��� ��������� ������
//            ValidateIssuer = true,
//            //������, �������������� ��������
//            ValidIssuer = AuthOptions.ISSUER,
//            //����� �� �������������� ����������� ������
//            ValidateAudience = true,
//            //��������� ����������� ������
//            ValidAudience = AuthOptions.AUDIENCE,
//            //����� �� �������������� ����� �������������
//            ValidateLifetime = true,
//            //��������� ����� ������������
//            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
//            //��������� ����� ������������
//            ValidateIssuerSigningKey = true,

//        };
//    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//User settngs
app.UseCors(
    builder => builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
    );

//app.UseHttpsRedirection();
//app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

//app.Run("http://*:8888");
app.Run();
