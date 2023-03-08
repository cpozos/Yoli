using Yoli.App.Extensions;

namespace Yoli.App;

public record Result<T>
{
    private bool? _success;
    public bool Succeeded => _success ??= !Errors.Any() && !Data.IsNullOrDefault();
    public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();
    public T Data { get; set; } = default!;

    public Result(T data)
    {
        Data = data;
    }
}