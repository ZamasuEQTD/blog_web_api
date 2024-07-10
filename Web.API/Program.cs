using Application.Configuration;
using Persistence.Configuration;
using Infraestructure.Configuration;
using WebApi.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json.Serialization;
using WebApi.Configuration;
using WebApi.Middlewares;
using SharedKernel;
using Hellang.Middleware.ProblemDetails;
using WebApi.Configuration.Validation;
using MediatR;
using Application.Behaviors;
using FluentValidation;
using Domain;
var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddSignalR();

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeLineBehavior<,>));

builder.Services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly, includeInternalTypes:true);

builder.Services
.AddDomain()
.AddApplication()
.AddInfraestructure()
.AddPersistence(builder.Configuration)
.AddSwaggerBearerTokenSupport();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();

}

app.UseCors(policy => policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();