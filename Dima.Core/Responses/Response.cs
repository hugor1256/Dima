using System.Text.Json.Serialization;

namespace Dima.Core.Responses;

public class Response<TData>
{
    private readonly int _code;

    [JsonConstructor]
    public Response() => _code = Configurations.DefaultStatusCode;
    
    public Response(TData? data, int statusCode = Configurations.DefaultStatusCode, string? message = null)
    {
        Data = data;
        Message = message;
        _code = statusCode;
    }

    public TData? Data { get; set; }
    public string? Message { get; set; }

    [JsonIgnore]
    public bool IsSucess => _code is >= 200 and <= 299;
}