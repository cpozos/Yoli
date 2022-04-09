using Yoli.Core.App.Extensions;

namespace Yoli.Core.App.Entities
{
    public record Result<T>
    {
        private bool? _success;
        public bool Succeeded => _success ??= !Errors.Any() && !Data.IsNullOrDefault();
        public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();
        public T Data { get; set; } = default!;
    }
}