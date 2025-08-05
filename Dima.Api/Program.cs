using Dima.Core.Enums;
using Dima.Core.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<Handler>();
builder.Services.AddSwaggerGen(s => 
    s.CustomSchemaIds(t => t.FullName));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/", (Request request, Handler handler) => handler.Handle(request))
    .WithName("Transaction: Create")
    .WithSummary("Cria nova transação")
    .Produces<Response>();

app.Run();

public class Request
{
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ETransactionType Type { get; set; } = ETransactionType.Withdraw;
    public decimal Amount { get; set; }
    public Category Category { get; set; } = null!;
    public string UserId { get; set; } = string.Empty;
}

public class Response
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
}


public class Handler
{
    public Response Handle(Request request)
    {
        return new Response
        {
            Id = 4,
            Title = request.Title
        };
    }
}

