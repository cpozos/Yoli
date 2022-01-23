using Yoli.Core.Domain.Entities;

namespace Yoli.Core.App.Entities
{
    public class YoliIdentityResult
    { 
        public bool Succeeded { get; set; }
        public IUser User { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public YoliIdentityResult(bool succeeded, IEnumerable<string> errors = null)
        {
            Succeeded = succeeded;
            Errors = errors ?? Enumerable.Empty<string>();
        }
    }
}