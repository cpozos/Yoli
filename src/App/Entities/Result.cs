namespace App.Entities
{
    public record Result<T>
    {
        private bool? _success;
        public bool Succeeded => _success ??= Errors.Any();
        public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();
        public T Data { get; set; } = default(T)!;
    }
}