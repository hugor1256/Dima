using Dima.Api.Common.Endpoints;
using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var cnnStr = builder
    .Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x => { x.UseSqlServer(cnnStr); });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddSwaggerGen(s =>
    s.CustomSchemaIds(t => t.FullName));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => new { message = "Ok" });
app.MapEndpoints();

app.Run();