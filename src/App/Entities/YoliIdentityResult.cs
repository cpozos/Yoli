using Yoli.Domain.Entities;

namespace Yoli.App.Entities
{
    public class YoliIdentityResult
    { 
        public bool Succeeded => Errors?.Count() > 0 && User != null;
        public IUser? User { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public YoliIdentityResult(IEnumerable<string>? errors = null)
        {
            Errors = errors ?? Enumerable.Empty<string>();
        }
    }
}