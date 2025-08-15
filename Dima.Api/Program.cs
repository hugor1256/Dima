using Dima.Api.Data;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var cnnStr = builder
    .Configuration.
    GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(cnnStr);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<Handler>();
builder.Services.AddSwaggerGen(s => 
    s.CustomSchemaIds(t => t.FullName));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/v1/categories", 
        (CreateCategoriesRequest request, 
            Handler handler) 
            => handler.Handle(request))
    .WithName("Categories: Create")
    .WithSummary("Cria umna nova categoria")
    .Produces<Response<Category>>();

app.Run();

