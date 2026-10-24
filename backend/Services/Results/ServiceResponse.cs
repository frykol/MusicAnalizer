namespace backend.Services.Results;

public record ServiceResponse<TStatus, TValue> where TStatus : struct, Enum
{
    public required TStatus Status {get; init;}
    public TValue? Value {get; init;}
}
