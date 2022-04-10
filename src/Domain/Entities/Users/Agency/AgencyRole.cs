using Yoli.Core.Domain.Entities;

namespace Yoli.Core.Domain.ValueObjects
{
    public class AgencyRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int AgencyId { get; set; }
        public virtual Agency Agency { get; set; }
    }
}